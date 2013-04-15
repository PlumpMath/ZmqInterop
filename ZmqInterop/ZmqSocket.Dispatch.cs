namespace ZeroMQ
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Threading.Tasks;

	public abstract partial class ZmqSocket
	{
		private static Object MessageSyncRoot = new Object();

		private static IntPtr Message = Marshal.AllocHGlobal( IntPtr.Size + 32 ); // yeah, it leaks ~64-96 bytes per AppDomain - wanna fight about it?

		private static Thread MessagePollThread = new Thread( ZmqSocket.PollSockets );

		protected static void PollSockets()
		{
			if( ZmqSocket.MessagePollThread.IsAlive == false )
			{
				ZmqSocket.MessagePollThread.IsBackground = true;
				ZmqSocket.MessagePollThread.Start();
				return;
			}

			while( ZmqSocket.Context != IntPtr.Zero )
			{
				lock( ZmqSocket.Sockets )
				{
					var items = new Interop.zmq_pollitem_t[ZmqSocket.Sockets.Count];

					for( var i = 0; i < items.Length; i++ )
					{
						items[i] = new Interop.zmq_pollitem_t
						{
							socket = ZmqSocket.Sockets[i].Item2,
							fd = IntPtr.Zero,
							events = (Int16)( Interop.ZMQ_POLLIN ),
							revents = 0,
						};
					}

					var pollResult = Interop.zmq_poll( items, items.Length, 0 );

					if( pollResult == -1 ) throw new ZmqException( Interop.zmq_errno() );

					if( pollResult == 0 )
					{
						if( Thread.Yield() == false ) Thread.Sleep( 0 );
					}

					if( pollResult > 0 )
					{
						for( var i = 0; i < items.Length; i++ )
						{
							if( items[i].revents == 0 ) continue;

							var zmqSocket = ZmqSocket.Sockets.First( tup => tup.Item2 == items[i].socket ).Item1;

							var parts = new List<Byte[]>();

							lock( ZmqSocket.MessageSyncRoot )
							{
								do
								{
									if( Interop.zmq_msg_init( ZmqSocket.Message ) != 0 )
									{
										throw new ZmqException( Interop.zmq_errno() );
									}

									var len = Interop.zmq_msg_recv( zmqSocket.Socket, ZmqSocket.Message, 0 );

									var data = Interop.zmq_msg_data( ZmqSocket.Message );

									var part = new Byte[len];

									Marshal.Copy( data, part, 0, part.Length );

									parts.Add( part );

									if( Interop.zmq_msg_close( ZmqSocket.Message ) != 0 )
									{
										throw new ZmqException( Interop.zmq_errno() );
									}
								}
								while( zmqSocket.GetIntSocketOption( Interop.ZMQ_RCVMORE ) == 1 );
							}

							Parallel.ForEach( zmqSocket.Subscribers, subscriber => subscriber.Value.OnNext( parts ) );
						}
					}
				}
			}
		}
	}
}
