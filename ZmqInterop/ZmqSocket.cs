namespace ZeroMQ
{
	using System;
	using System.Runtime.InteropServices;

	public abstract partial class ZmqSocket: IDisposable
	{
		private ZmqContext Context;

		internal IntPtr Socket;

		internal IntPtr Message;

		public ZmqSocket( ZmqContext context, Int32 socketType )
		{
			this.Context = context;

			this.Socket = Interop.zmq_socket( this.Context.Pointer, socketType );

			if( this.Socket == IntPtr.Zero )
			{
				throw new ZmqException( Interop.zmq_errno() );
			}

			this.Context.RegisterSocket( this.Socket, this );

			this.Message = Marshal.AllocHGlobal( IntPtr.Size + 32 );
		}

		protected void Dispose( Boolean disposing )
		{
			if( this.Socket == IntPtr.Zero ) throw new ObjectDisposedException( "ZmqSocket" );

			if( disposing )
			{
			}

			Marshal.FreeHGlobal( this.Message );

			this.Message = IntPtr.Zero;

			this.Context.UnregisterSocket( this.Socket );

			this.Context = null;

			if( Interop.zmq_close( this.Socket ) != 0 )
			{
				throw new ZmqException( Interop.zmq_errno() );
			}

			this.Socket = IntPtr.Zero;
		}

		public void Dispose()
		{
			this.Dispose( disposing: true );
		}

		~ZmqSocket()
		{
			this.Dispose( disposing: false );
		}

		protected Int32 GetIntSocketOption( Int32 option )
		{
			if( this.Socket == IntPtr.Zero ) throw new ObjectDisposedException( "ZmqSocket" );

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
			if( this.Socket == IntPtr.Zero ) throw new ObjectDisposedException( "ZmqSocket" );

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

		// todo: socket options

		public Int32 Affinity
		{
			get;
			set;
		}

		public Byte[] Identity
		{
			get;
			set;
		}

		public void Subscribe( Byte[] topic )
		{
		}

		public void Unsubscribe( Byte[] topic )
		{
		}

		public Int32 MulticastRateKbits
		{
			get;
			set;
		}

		public Int32 RecoveryIntervalMsecs
		{
			get;
			set;
		}

		public Int32 SendBufferBytes
		{
			get;
			set;
		}

		public Int32 ReceiveBufferBytes
		{
			get;
			set;
		}

		public Boolean ReceiveMore
		{
			get;
		}

		public IntPtr FileDescriptor
		{
			get;
		}

		[Flags]
		public enum PendingEventFlags
		{
			None,
			PollIn = Interop.ZMQ_POLLIN,
			PollOut = Interop.ZMQ_POLLOUT,
		}

		public PendingEventFlags PendingEvents
		{
			get;
		}

		public enum SocketTypes
		{
			None,

		}

		public SocketTypes SocketType
		{
			get;
		}

		public Int32 LingerIntervalMsecs
		{
			get;
			set;
		}

		public Int32 ReconnectIntervalMsecs
		{
			get;
			set;
		}

		public Int32 ConnectionBacklog
		{
			get;
			set;
		}

		public Int32 MaxReconnectIntervalMsecs
		{
			get;
			set;
		}

		public Int32 MaxMessageBytes
		{
			get;
			set;
		}

		public Int32 OutboundHighWatermark
		{
			get;
			set;
		}

		public Int32 InboundHighWatermark
		{
			get;
			set;
		}

		public Int32 MaxMulticastHops
		{
			get;
			set;
		}

		public Int32 ReceiveTimeoutMsecs
		{
			get;
			set;
		}

		public Int32 SendTimeoutMsecs
		{
			get;
			set;
		}

		public Boolean Ip4Only
		{
			get;
			set;
		}

		public Byte[] LastBoundEndpoint
		{
			get;
		}

		public Boolean OnlyRoutableMessages
		{
			set;
		}

		public Boolean? UseTcpKeepalive
		{
			get;
			set;
		}

		public Int32? MaxTcpKeepaliveCount
		{
			get;
			set;
		}

		public Int32? MaxTcpKeepaliveIdleTime
		{
			get;
			set;
		}

		public Int32? TcpKeepaliveInterval
		{
			get;
			set;
		}

		public void FilterTcpAddress( Byte[] address )
		{
		}

		public void ClearTcpAddressFilters()
		{
		}

		public Boolean WaitForConnections
		{
			get;
			set;
		}

		public Boolean VerboseSubscriptions
		{
			set;
		}
	}
}
