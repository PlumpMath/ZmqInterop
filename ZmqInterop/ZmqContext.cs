namespace ZeroMQ
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;
	using System.Linq;
	using System.Runtime.InteropServices;
	using System.Threading;
	using System.Threading.Tasks;

	public class ZmqContext: IDisposable
	{
		private ConcurrentDictionary<IntPtr, ZmqSocket> Sockets;

		internal IntPtr Pointer;

		private Thread PollSocketsThread;

		public ZmqContext()
		{
			this.Sockets = new ConcurrentDictionary<IntPtr, ZmqSocket>();

			this.Pointer = Interop.zmq_ctx_new();

			if( this.Pointer == IntPtr.Zero )
			{
				throw new ZmqException( Interop.zmq_errno() );
			}

			this.PollSocketsThread = new Thread( this.PollSockets )
			{
				Name = "ZeroMQ Event Pump",
				IsBackground = true,
			};

			this.PollSocketsThread.Start();
		}

		public void Dispose()
		{
			this.PollSocketsThread.Abort();

			if( Interop.zmq_ctx_destroy( this.Pointer ) != 0 )
			{
				throw new ZmqException( Interop.zmq_errno() );
			}

			this.Pointer = IntPtr.Zero;

			this.Sockets = null;
		}

		internal void RegisterSocket( IntPtr pointer, ZmqSocket socket )
		{
			this.Sockets.TryAdd( pointer, socket );
		}

		internal void UnregisterSocket( IntPtr pointer )
		{
			ZmqSocket value;
			this.Sockets.TryRemove( pointer, out value );
		}

		private Int32 GetContextOption( Int32 option )
		{
			if( this.Pointer == IntPtr.Zero ) throw new ObjectDisposedException( "ZmqContext" );

			Int32 result;

			if( ( result = Interop.zmq_ctx_get( this.Pointer, option ) ) < 0 )
			{
				throw new ZmqException( Interop.zmq_errno() );
			}

			return result;
		}

		private void SetContextOption( Int32 option, Int32 value )
		{
			if( this.Pointer == IntPtr.Zero ) throw new ObjectDisposedException( "ZmqContext" );

			if( Interop.zmq_ctx_set( this.Pointer, option, value ) < 0 )
			{
				throw new ZmqException( Interop.zmq_errno() );
			}
		}

		// todo: context options

		public Int32 IOThreads
		{
			get;
			set;
		}

		public Int32 MaxSockets
		{
			get;
			set;
		}

		private void PollSockets()
		{
			try
			{
				while( true )
				{
					// poll all sockets in the context

					var items = this.Sockets
						.Select( socket =>
							new Interop.zmq_pollitem_t
							{
								socket = socket.Key,
								fd = IntPtr.Zero,
								events = (Int16)( Interop.ZMQ_POLLIN ),
								revents = 0,
							} )
						.ToArray();

					var pollResult = Interop.zmq_poll( items, items.Length, 0 );

					// result: poll failed

					if( pollResult < 0 )
					{
						throw new ZmqException( Interop.zmq_errno() );
					}

					// result: no polled sockets had messages available

					if( pollResult == 0 )
					{
						if( Thread.Yield() == false ) Thread.Sleep( 1 );
						continue;
					}

					// result: some polled sockets can be read from

					for( var i = 0; i < items.Length; i++ ) // for each socket...
					{
						if( items[i].revents == 0 ) continue; // skip this socket if no messages can be read

						var zmqSocket = this.Sockets.First( tup => tup.Key == items[i].socket ).Value;

						var messageParts = new List<Byte[]>();

						do
						{
							if( Interop.zmq_msg_init( zmqSocket.Message ) != 0 )
							{
								continue;
							}

							var len = Interop.zmq_msg_recv( zmqSocket.Socket, zmqSocket.Message, 0 );

							var data = Interop.zmq_msg_data( zmqSocket.Message );

							var part = new Byte[len];

							Marshal.Copy( data, part, 0, part.Length );

							messageParts.Add( part );

							if( Interop.zmq_msg_close( zmqSocket.Message ) != 0 )
							{
								continue;
							}
						}
						while( zmqSocket.ReceiveMore == true );

						Parallel.ForEach( zmqSocket.Subscribers, subscriber => subscriber.Value.OnNext( messageParts ) );
					}
				}
			}
			catch( ThreadAbortException )
			{
			}
		}
	}
}
