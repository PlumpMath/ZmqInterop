namespace ZeroMQ
{
	using System;
	using System.Collections.Generic;
	using System.Runtime.InteropServices;

	/// <summary></summary>
	public abstract partial class ZmqSocket
	{
		protected static IntPtr Context;

		protected static void CreateContext()
		{
			if( ZmqSocket.Context != IntPtr.Zero )
			{
				throw new InvalidOperationException( "Context has already been created" );
			}

			ZmqSocket.Context = Interop.zmq_ctx_new();

			if( ZmqSocket.Context == IntPtr.Zero )
			{
				throw new ZmqException( Interop.zmq_errno() );
			}
		}

		protected static void DestroyContext()
		{
			if( ZmqSocket.Context == IntPtr.Zero )
			{
				throw new InvalidOperationException( "Context has already been destroyed" );
			}

			if( Interop.zmq_ctx_destroy( ZmqSocket.Context ) != 0 )
			{
				throw new ZmqException( Interop.zmq_errno() );
			}

			ZmqSocket.Context = IntPtr.Zero;
		}

		protected static Int32 GetContextOption( Int32 option )
		{
			if( ZmqSocket.Context == IntPtr.Zero ) throw new InvalidOperationException( "Context has not been created" );

			Int32 result;

			if( ( result = Interop.zmq_ctx_get( ZmqSocket.Context, option ) ) < 0 )
			{
				throw new ZmqException( Interop.zmq_errno() );
			}

			return result;
		}

		protected static void SetContextOption( Int32 option, Int32 value )
		{
			if( ZmqSocket.Context == IntPtr.Zero ) throw new InvalidOperationException( "Context has not been created" );

			if( Interop.zmq_ctx_set( ZmqSocket.Context, option, value ) < 0 )
			{
				throw new ZmqException( Interop.zmq_errno() );
			}
		}

		protected static readonly List<Tuple<ZmqSocket, IntPtr>> Sockets = new List<Tuple<ZmqSocket, IntPtr>>();

		protected IntPtr Socket;

		protected void CreateSocket( Int32 socketType )
		{
			if( ZmqSocket.Context == IntPtr.Zero ) throw new InvalidOperationException( "Context has not been created" );

			if( this.Socket != IntPtr.Zero ) throw new InvalidOperationException( "Socket has already been created" );

			this.Socket = Interop.zmq_socket( ZmqSocket.Context, socketType );

			if( this.Socket == IntPtr.Zero )
			{
				throw new ZmqException( Interop.zmq_errno() );
			}

			lock( ZmqSocket.Sockets ) ZmqSocket.Sockets.Add( new Tuple<ZmqSocket, IntPtr>( this, this.Socket ) );
		}

		protected void DestroySocket( IntPtr socket )
		{
			if( ZmqSocket.Context == IntPtr.Zero ) throw new InvalidOperationException( "Context has not been created" );

			if( this.Socket == IntPtr.Zero ) throw new InvalidOperationException( "Socket has already been destroyed" );

			lock( ZmqSocket.Sockets ) ZmqSocket.Sockets.Remove( new Tuple<ZmqSocket, IntPtr>( this, this.Socket ) );

			if( Interop.zmq_close( this.Socket ) != 0 )
			{
				throw new ZmqException( Interop.zmq_errno() );
			}

			this.Socket = IntPtr.Zero;
		}

		protected Int32 GetIntSocketOption( Int32 option )
		{
			if( ZmqSocket.Context == IntPtr.Zero ) throw new InvalidOperationException( "Context has not been created" );

			if( this.Socket == IntPtr.Zero ) throw new InvalidOperationException( "Socket has not been created" );

			var ptrValue = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Int32 ) ) );
			var ptrLength = Marshal.AllocHGlobal( IntPtr.Size );

			if( Interop.zmq_getsockopt( this.Socket, option, ptrValue, ptrLength ) != 0 )
			{
				Marshal.FreeHGlobal( ptrValue );
				throw new ZmqException( Interop.zmq_errno() );
			}

			var result = Marshal.ReadInt32( ptrValue );

			Marshal.FreeHGlobal( ptrValue );
			Marshal.FreeHGlobal( ptrLength );

			return result;
		}

		protected void SetIntSocketOption( Int32 option, Int32 value )
		{
			if( ZmqSocket.Context == IntPtr.Zero ) throw new InvalidOperationException( "Context has not been created" );

			if( this.Socket == IntPtr.Zero ) throw new InvalidOperationException( "Socket has not been created" );

			var ptrValue = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Int32 ) ) );

			Marshal.WriteInt32( ptrValue, value );

			if( Interop.zmq_setsockopt( this.Socket, option, ptrValue, Marshal.SizeOf( typeof( Int32 ) ) ) != 0 )
			{
				Marshal.FreeHGlobal( ptrValue );
				throw new ZmqException( Interop.zmq_errno() );
			}

			Marshal.FreeHGlobal( ptrValue );
		}

		protected Byte[] GetByteScoketOption( Int32 option )
		{
			if( ZmqSocket.Context == IntPtr.Zero ) throw new InvalidOperationException( "Context has not been created" );

			if( this.Socket == IntPtr.Zero ) throw new InvalidOperationException( "Socket has not been created" );

			var ptrValue = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Byte ) ) * 4000 );
			var ptrLength = Marshal.AllocHGlobal( IntPtr.Size );

			if( Interop.zmq_getsockopt( this.Socket, option, ptrValue, ptrLength ) != 0 )
			{
				Marshal.FreeHGlobal( ptrValue );
				throw new ZmqException( Interop.zmq_errno() );
			}

			var length = Marshal.ReadIntPtr( ptrLength ).ToInt32();

			Marshal.FreeHGlobal( ptrLength );

			if( length < 4000 )
			{
				Marshal.FreeHGlobal( ptrValue );
				ptrValue = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Byte ) ) * length );

				if( Interop.zmq_getsockopt( this.Socket, option, ptrValue, ptrLength ) != 0 )
				{
					Marshal.FreeHGlobal( ptrValue );
					throw new ZmqException( Interop.zmq_errno() );
				}

				length = Marshal.ReadInt32( ptrLength );
			}

			var result = new Byte[length];

			for( var i = 0; i < result.Length; i++ )
			{
				result[i] = Marshal.ReadByte( ptrValue, i );
			}

			Marshal.FreeHGlobal( ptrValue );

			return result;
		}

		protected void SetByteSocketOption( Int32 option, Byte[] value )
		{
			if( ZmqSocket.Context == IntPtr.Zero ) throw new InvalidOperationException( "Context has not been created" );

			if( this.Socket == IntPtr.Zero ) throw new InvalidOperationException( "Socket has not been created" );

			var ptrValue = Marshal.AllocHGlobal( Marshal.SizeOf( typeof( Byte ) ) * value.Length );

			for( var i = 0; i < value.Length; i++ )
			{
				Marshal.WriteByte( ptrValue, i, value[i] );
			}

			if( Interop.zmq_setsockopt( this.Socket, option, ptrValue, value.Length ) != 0 )
			{
				Marshal.FreeHGlobal( ptrValue );
				throw new ZmqException( Interop.zmq_errno() );
			}

			Marshal.FreeHGlobal( ptrValue );
		}
	}
}
