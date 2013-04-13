// 
// Copyright © 2013 Alex Forster
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation the
// rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or substantial portions of
// the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT,
// TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// 
// ZeroMQ is a trademark of iMatix Corporation.
// 

namespace ZeroMQ
{
	using System;
	using System.Runtime.InteropServices;

	/// <summary></summary>
	public static class Interop
	{
		/// <summary></summary>
		[DllImport( "libzmq" )]
		public static extern void zmq_version( IntPtr major, IntPtr minor, IntPtr patch );

		/******************************************************************************/
		/*  0MQ errors.                                                               */
		/******************************************************************************/

		/// <summary></summary>
		public static readonly Int32 ZMQ_HAUSNUMERO = 156384712;

		// note- we're assuming the Posix error codes; these values aren't standardized

		/// <summary></summary>
		public static readonly Int32 ENOTSUP = Environment.OSVersion.Platform == PlatformID.Unix ? 134 : ZMQ_HAUSNUMERO + 1;

		/// <summary></summary>
		public static readonly Int32 EPROTONOSUPPORT = Environment.OSVersion.Platform == PlatformID.Unix ? 123 : ZMQ_HAUSNUMERO + 2;

		/// <summary></summary>
		public static readonly Int32 ENOBUFS = Environment.OSVersion.Platform == PlatformID.Unix ? 105 : ZMQ_HAUSNUMERO + 3;

		/// <summary></summary>
		public static readonly Int32 ENETDOWN = Environment.OSVersion.Platform == PlatformID.Unix ? 115 : ZMQ_HAUSNUMERO + 4;

		/// <summary></summary>
		public static readonly Int32 EADDRINUSE = Environment.OSVersion.Platform == PlatformID.Unix ? 112 : ZMQ_HAUSNUMERO + 5;

		/// <summary></summary>
		public static readonly Int32 EADDRNOTAVAIL = Environment.OSVersion.Platform == PlatformID.Unix ? 125 : ZMQ_HAUSNUMERO + 6;

		/// <summary></summary>
		public static readonly Int32 ECONNREFUSED = Environment.OSVersion.Platform == PlatformID.Unix ? 111 : ZMQ_HAUSNUMERO + 7;

		/// <summary></summary>
		public static readonly Int32 EINPROGRESS = Environment.OSVersion.Platform == PlatformID.Unix ? 119 : ZMQ_HAUSNUMERO + 8;

		/// <summary></summary>
		public static readonly Int32 ENOTSOCK = Environment.OSVersion.Platform == PlatformID.Unix ? 119 : ZMQ_HAUSNUMERO + 9;

		/// <summary></summary>
		public static readonly Int32 EMSGSIZE = ZMQ_HAUSNUMERO + 10;

		/// <summary></summary>
		public static readonly Int32 EAFNOSUPPORT = ZMQ_HAUSNUMERO + 11;

		/// <summary></summary>
		public static readonly Int32 ENETUNREACH = ZMQ_HAUSNUMERO + 12;

		/// <summary></summary>
		public static readonly Int32 ECONNABORTED = ZMQ_HAUSNUMERO + 13;

		/// <summary></summary>
		public static readonly Int32 ECONNRESET = ZMQ_HAUSNUMERO + 14;

		/// <summary></summary>
		public static readonly Int32 ENOTCONN = ZMQ_HAUSNUMERO + 15;

		/// <summary></summary>
		public static readonly Int32 ETIMEDOUT = ZMQ_HAUSNUMERO + 16;

		/// <summary></summary>
		public static readonly Int32 EHOSTUNREACH = ZMQ_HAUSNUMERO + 17;

		/// <summary></summary>
		public static readonly Int32 ENETRESET = ZMQ_HAUSNUMERO + 18;

		/// <summary></summary>
		public static readonly Int32 EFSM = ZMQ_HAUSNUMERO + 51;

		/// <summary></summary>
		public static readonly Int32 ENOCOMPATPROTO = ZMQ_HAUSNUMERO + 52;

		/// <summary></summary>
		public static readonly Int32 ETERM = ZMQ_HAUSNUMERO + 53;

		/// <summary></summary>
		public static readonly Int32 EMTHREAD = ZMQ_HAUSNUMERO + 54;

		/// <summary>Retrieve the value of "errno" for the calling thread</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_errno();

		/// <summary>Retrieve the error message string for a specific "errno" value</summary>
		[DllImport( "libzmq" )]
		public static extern IntPtr zmq_strerror( Int32 errnum );

		/******************************************************************************/
		/*  0MQ infrastructure (a.k.a. context) initialisation & termination.         */
		/******************************************************************************/

		/// <summary></summary>
		public static readonly Int32 ZMQ_IO_THREADS = 1;

		/// <summary></summary>
		public static readonly Int32 ZMQ_MAX_SOCKETS = 2;

		/// <summary></summary>
		public static readonly Int32 ZMQ_IO_THREADS_DFLT = 1;

		/// <summary></summary>
		public static readonly Int32 ZMQ_MAX_SOCKETS_DFLT = 1024;

		/// <summary></summary>
		[DllImport( "libzmq" )]
		public static extern IntPtr zmq_ctx_new();

		/// <summary></summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_ctx_destroy( IntPtr context );

		/// <summary></summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_ctx_set( IntPtr context, Int32 option, Int32 optval );

		/// <summary></summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_ctx_get( IntPtr context, Int32 option );

		/******************************************************************************/
		/*  0MQ message definition.                                                   */
		/******************************************************************************/

		//[StructLayout( LayoutKind.Sequential )]
		//public struct zmq_msg_t
		//{
		//	[MarshalAs( UnmanagedType.ByValArray, SizeConst = 32 )]
		//	public readonly Byte[] _;
		//}

		/// <summary>Allocate internal resources for a message</summary>
		[UnmanagedFunctionPointer( CallingConvention.Cdecl )]
		public delegate void zmq_free_fn( IntPtr data, IntPtr hint );

		/// <summary>Allocate internal resources for a message</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_msg_init( IntPtr msg );

		/// <summary>Allocate internal resources for a message of a specific size</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_msg_init_size( IntPtr msg, Int32 size );

		/// <summary>Allocate internal resources for a message of a specific size and with the specified content</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_msg_init_data( IntPtr msg, IntPtr data, Int32 size, zmq_free_fn ffn, IntPtr hint );

		/// <summary></summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_msg_send( IntPtr msg, IntPtr socket, Int32 flags );

		/// <summary></summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_msg_recv( IntPtr msg, IntPtr socket, Int32 flags );

		/// <summary>Deallocate internal resources for a message</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_msg_close( IntPtr msg );

		/// <summary>Move the contents of one message into another (pre-initialized) message, emptying the original</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_msg_move( IntPtr dest, IntPtr src );

		/// <summary>Copy the contents of one message into another (pre-initialized) message</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_msg_copy( IntPtr dest, IntPtr src );

		/// <summary>Retrieve a message's data pointer</summary>
		[DllImport( "libzmq" )]
		public static extern IntPtr zmq_msg_data( IntPtr msg );

		/// <summary>Retrieve a message's size</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_msg_size( IntPtr msg );

		/// <summary></summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_msg_more( IntPtr msg );

		/// <summary></summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_msg_get( IntPtr msg, Int32 option );

		/// <summary></summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_msg_set( IntPtr msg, Int32 option, Int32 optval );

		/******************************************************************************/
		/*  0MQ socket definition.                                                    */
		/******************************************************************************/

		/*  Socket types.                                                             */

		/// <summary></summary>
		public static readonly Int32 ZMQ_PAIR = 0;

		/// <summary></summary>
		public static readonly Int32 ZMQ_PUB = 1;

		/// <summary></summary>
		public static readonly Int32 ZMQ_SUB = 2;

		/// <summary></summary>
		public static readonly Int32 ZMQ_REQ = 3;

		/// <summary></summary>
		public static readonly Int32 ZMQ_REP = 4;

		/// <summary></summary>
		public static readonly Int32 ZMQ_DEALER = 5;

		/// <summary></summary>
		public static readonly Int32 ZMQ_ROUTER = 6;

		/// <summary></summary>
		public static readonly Int32 ZMQ_PULL = 7;

		/// <summary></summary>
		public static readonly Int32 ZMQ_PUSH = 8;

		/// <summary></summary>
		public static readonly Int32 ZMQ_XPUB = 9;

		/// <summary></summary>
		public static readonly Int32 ZMQ_XSUB = 10;

		/*  Socket options.                                                           */

		/// <summary></summary>
		public static readonly Int32 ZMQ_AFFINITY = 4;

		/// <summary></summary>
		public static readonly Int32 ZMQ_IDENTITY = 5;

		/// <summary></summary>
		public static readonly Int32 ZMQ_SUBSCRIBE = 6;

		/// <summary></summary>
		public static readonly Int32 ZMQ_UNSUBSCRIBE = 7;

		/// <summary></summary>
		public static readonly Int32 ZMQ_RATE = 8;

		/// <summary></summary>
		public static readonly Int32 ZMQ_RECOVERY_IVL = 9;

		/// <summary></summary>
		public static readonly Int32 ZMQ_SNDBUF = 11;

		/// <summary></summary>
		public static readonly Int32 ZMQ_RCVBUF = 12;

		/// <summary></summary>
		public static readonly Int32 ZMQ_RCVMORE = 13;

		/// <summary></summary>
		public static readonly Int32 ZMQ_FD = 14;

		/// <summary></summary>
		public static readonly Int32 ZMQ_EVENTS = 15;

		/// <summary></summary>
		public static readonly Int32 ZMQ_TYPE = 16;

		/// <summary></summary>
		public static readonly Int32 ZMQ_LINGER = 17;

		/// <summary></summary>
		public static readonly Int32 ZMQ_RECONNECT_IVL = 18;

		/// <summary></summary>
		public static readonly Int32 ZMQ_BACKLOG = 19;

		/// <summary></summary>
		public static readonly Int32 ZMQ_RECONNECT_IVL_MAX = 21;

		/// <summary></summary>
		public static readonly Int32 ZMQ_MAXMSGSIZE = 22;

		/// <summary></summary>
		public static readonly Int32 ZMQ_SNDHWM = 23;

		/// <summary></summary>
		public static readonly Int32 ZMQ_RCVHWM = 24;

		/// <summary></summary>
		public static readonly Int32 ZMQ_MULTICAST_HOPS = 25;

		/// <summary></summary>
		public static readonly Int32 ZMQ_RCVTIMEO = 27;

		/// <summary></summary>
		public static readonly Int32 ZMQ_SNDTIMEO = 28;

		/// <summary></summary>
		public static readonly Int32 ZMQ_IPV4ONLY = 31;

		/// <summary></summary>
		public static readonly Int32 ZMQ_LAST_ENDPOINT = 32;

		/// <summary></summary>
		public static readonly Int32 ZMQ_ROUTER_MANDATORY = 33;

		/// <summary></summary>
		public static readonly Int32 ZMQ_TCP_KEEPALIVE = 34;

		/// <summary></summary>
		public static readonly Int32 ZMQ_TCP_KEEPALIVE_CNT = 35;

		/// <summary></summary>
		public static readonly Int32 ZMQ_TCP_KEEPALIVE_IDLE = 36;

		/// <summary></summary>
		public static readonly Int32 ZMQ_TCP_KEEPALIVE_INTVL = 37;

		/// <summary></summary>
		public static readonly Int32 ZMQ_TCP_ACCEPT_FILTER = 38;

		/// <summary></summary>
		public static readonly Int32 ZMQ_DELAY_ATTACH_ON_CONNECT = 39;

		/// <summary></summary>
		public static readonly Int32 ZMQ_XPUB_VERBOSE = 40;
		
		/*  Message options                                                           */

		/// <summary></summary>
		public static readonly Int32 ZMQ_MORE = 1;

		/*  Send/recv options.                                                        */

		/// <summary></summary>
		public static readonly Int32 ZMQ_DONTWAIT = 1;

		/// <summary></summary>
		public static readonly Int32 ZMQ_SNDMORE = 2;

		/******************************************************************************/
		/*  0MQ socket events and monitoring                                          */
		/******************************************************************************/

		/*  Socket transport events (tcp and ipc only)                                */

		/// <summary></summary>
		public static readonly Int32 ZMQ_EVENT_CONNECTED = 1;

		/// <summary></summary>
		public static readonly Int32 ZMQ_EVENT_CONNECT_DELAYED = 2;

		/// <summary></summary>
		public static readonly Int32 ZMQ_EVENT_CONNECT_RETRIED = 4;

		/// <summary></summary>
		public static readonly Int32 ZMQ_EVENT_LISTENING = 8;

		/// <summary></summary>
		public static readonly Int32 ZMQ_EVENT_BIND_FAILED = 16;

		/// <summary></summary>
		public static readonly Int32 ZMQ_EVENT_ACCEPTED = 32;

		/// <summary></summary>
		public static readonly Int32 ZMQ_EVENT_ACCEPT_FAILED = 64;

		/// <summary></summary>
		public static readonly Int32 ZMQ_EVENT_CLOSED = 128;

		/// <summary></summary>
		public static readonly Int32 ZMQ_EVENT_CLOSE_FAILED = 256;

		/// <summary></summary>
		public static readonly Int32 ZMQ_EVENT_DISCONNECTED = 512;

		/// <summary></summary>
		public static readonly Int32 ZMQ_EVENT_ALL =
			( ZMQ_EVENT_CONNECTED | ZMQ_EVENT_CONNECT_DELAYED | ZMQ_EVENT_CONNECT_RETRIED | ZMQ_EVENT_LISTENING |
			  ZMQ_EVENT_BIND_FAILED | ZMQ_EVENT_ACCEPTED | ZMQ_EVENT_ACCEPT_FAILED | ZMQ_EVENT_CLOSED |
			  ZMQ_EVENT_CLOSE_FAILED | ZMQ_EVENT_DISCONNECTED );

		//[StructLayout( LayoutKind.Explicit )]
		//public struct zmq_event_t
		//{
		//	[FieldOffset( 0 ), MarshalAs( UnmanagedType.SysInt )]
		//	public Int32 @event;
		//
		//	[FieldOffset( 1 ), MarshalAs( UnmanagedType.LPTStr, CharSet = CharSet.Ansi )]
		//	public String addr;
		//
		//	[FieldOffset( 2 ), MarshalAs( UnmanagedType.SysInt )]
		//	public Int32 fd;
		//
		//	[FieldOffset( 2 ), MarshalAs( UnmanagedType.SysInt )]
		//	public Int32 err;
		//}

		/// <summary>Allocate internal resources for a ZeroMQ socket of a specific type</summary>
		[DllImport( "libzmq" )]
		public static extern IntPtr zmq_socket( IntPtr context, Int32 type );

		/// <summary>Deallocate internal resources for a ZeroMQ socket</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_close( IntPtr socket );

		/// <summary>Set an option for a ZeroMQ socket</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_setsockopt( IntPtr socket, Int32 option, IntPtr optval, Int32 optvallen );

		/// <summary>Get an option for a ZeroMQ socket</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_getsockopt( IntPtr socket, Int32 option, IntPtr optval, IntPtr optvallen );

		/// <summary>Bind a ZeroMQ socket to a specific address</summary>
		[DllImport( "libzmq", CharSet = CharSet.Ansi )]
		public static extern Int32 zmq_bind( IntPtr socket, String addr );

		/// <summary>Connect a ZeroMQ socket to a specific address</summary>
		[DllImport( "libzmq", CharSet = CharSet.Ansi )]
		public static extern Int32 zmq_connect( IntPtr socket, String addr );

		/// <summary>Bind a ZeroMQ socket to a specific address</summary>
		[DllImport( "libzmq", CharSet = CharSet.Ansi )]
		public static extern Int32 zmq_unbind( IntPtr socket, String addr );

		/// <summary>Connect a ZeroMQ socket to a specific address</summary>
		[DllImport( "libzmq", CharSet = CharSet.Ansi )]
		public static extern Int32 zmq_disconnect( IntPtr socket, String addr );

		/// <summary>Send a message on a ZeroMQ socket</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_send( IntPtr socket, IntPtr buf, Int32 len, Int32 flags );

		/// <summary>Receive the next message on a ZeroMQ socket</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_recv( IntPtr socket, IntPtr buf, Int32 len, Int32 flags );

		/// <summary>Receive the next message on a ZeroMQ socket</summary>
		[DllImport( "libzmq", CharSet = CharSet.Ansi )]
		public static extern Int32 zmq_socket_monitor( IntPtr socket, String addr, Int32 events );

		/// <summary>Send a message on a ZeroMQ socket</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_sendmsg( IntPtr socket, IntPtr msg, Int32 flags );

		/// <summary>Receive the next message on a ZeroMQ socket</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_recvmsg( IntPtr socket, IntPtr msg, Int32 flags );

		/// <summary></summary>
		public static readonly Int32 ZMQ_POLLIN = 1;

		/// <summary></summary>
		public static readonly Int32 ZMQ_POLLOUT = 2;

		/// <summary></summary>
		public static readonly Int32 ZMQ_POLLERR = 4;

		/// <summary></summary>
		[StructLayout( LayoutKind.Sequential )]
		public struct zmq_pollitem_t
		{
			public IntPtr socket;

			public IntPtr fd;

			public Int16 events;

			public Int16 revents;
		}

		/// <summary>Receive the next message on a ZeroMQ socket</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_poll( [In] [Out] zmq_pollitem_t[] items, Int32 nitems, Int32 timeout );
	}
}
