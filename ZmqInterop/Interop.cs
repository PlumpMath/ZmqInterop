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
// ØMQ, ZeroMQ, and 0MQ are trademarks of iMatix Corporation.
// 

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable ConvertToConstant.Global
// ReSharper disable ConvertToConstant.Local
// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable MemberCanBePrivate.Local
// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable FieldCanBeMadeReadOnly.Local

namespace ZeroMQ
{
	using System;
	using System.Runtime.InteropServices;

	/// <summary>Exposes the native ZeroMQ C API to the .NET Framework</summary>
	public static class Interop
	{
		/// <summary>Run-time API version detection</summary>
		[DllImport( "libzmq" )]
		public static extern void zmq_version( IntPtr major, IntPtr minor, IntPtr patch );

		/******************************************************************************/
		/*  0MQ errors.                                                               */
		/******************************************************************************/

		/// <summary>Avoid collision of errno on platforms that don't have certain values defined</summary>
		private static readonly Int32 ZMQ_HAUSNUMERO = 156384712;

		/// <summary>"Not supported"</summary>
		public static readonly Int32 ENOTSUP =
			Environment.OSVersion.Platform == PlatformID.Unix ? 95 :
			ZMQ_HAUSNUMERO + 1;

		/// <summary>"Protocol not supported"</summary>
		public static readonly Int32 EPROTONOSUPPORT =
			Environment.OSVersion.Platform == PlatformID.Unix ? 93 :
			ZMQ_HAUSNUMERO + 2;

		/// <summary>"No buffer space available"</summary>
		public static readonly Int32 ENOBUFS =
			Environment.OSVersion.Platform == PlatformID.Unix ? 105 :
			ZMQ_HAUSNUMERO + 3;

		/// <summary>"Network is down"</summary>
		public static readonly Int32 ENETDOWN =
			Environment.OSVersion.Platform == PlatformID.Unix ? 100 :
			ZMQ_HAUSNUMERO + 4;

		/// <summary>"Address in use"</summary>
		public static readonly Int32 EADDRINUSE =
			Environment.OSVersion.Platform == PlatformID.Unix ? 98 :
			ZMQ_HAUSNUMERO + 5;

		/// <summary>"Address not available"</summary>
		public static readonly Int32 EADDRNOTAVAIL =
			Environment.OSVersion.Platform == PlatformID.Unix ? 99 :
			ZMQ_HAUSNUMERO + 6;

		/// <summary>"Connection refused"</summary>
		public static readonly Int32 ECONNREFUSED =
			Environment.OSVersion.Platform == PlatformID.Unix ? 111 :
			ZMQ_HAUSNUMERO + 7;

		/// <summary>"Operation in progress"</summary>
		public static readonly Int32 EINPROGRESS =
			Environment.OSVersion.Platform == PlatformID.Unix ? 115 :
			ZMQ_HAUSNUMERO + 8;

		/// <summary>"Socket operation on non-socket"</summary>
		public static readonly Int32 ENOTSOCK =
			Environment.OSVersion.Platform == PlatformID.Unix ? 88 :
			Environment.OSVersion.Platform == PlatformID.Win32NT ? 108 :
			ZMQ_HAUSNUMERO + 9;

		/// <summary>"Message too long"</summary>
		public static readonly Int32 EMSGSIZE =
			Environment.OSVersion.Platform == PlatformID.Unix ? 90 :
			Environment.OSVersion.Platform == PlatformID.Win32NT ? 122 :
			ZMQ_HAUSNUMERO + 10;

		/// <summary>"Address family not supported by protocol"</summary>
		public static readonly Int32 EAFNOSUPPORT =
			Environment.OSVersion.Platform == PlatformID.Unix ? 97 :
			Environment.OSVersion.Platform == PlatformID.Win32NT ? 106 :
			ZMQ_HAUSNUMERO + 11;

		/// <summary>"Network is unreachable"</summary>
		public static readonly Int32 ENETUNREACH =
			Environment.OSVersion.Platform == PlatformID.Unix ? 101 :
			Environment.OSVersion.Platform == PlatformID.Win32NT ? 114 :
			ZMQ_HAUSNUMERO + 12;

		/// <summary>"Connection aborted"</summary>
		public static readonly Int32 ECONNABORTED =
			Environment.OSVersion.Platform == PlatformID.Unix ? 103 :
			Environment.OSVersion.Platform == PlatformID.Win32NT ? 113 :
			ZMQ_HAUSNUMERO + 13;

		/// <summary>"Connection reset by peer"</summary>
		public static readonly Int32 ECONNRESET =
			Environment.OSVersion.Platform == PlatformID.Unix ? 104 :
			Environment.OSVersion.Platform == PlatformID.Win32NT ? 104 :
			ZMQ_HAUSNUMERO + 14;

		/// <summary>"The socket is not connected"</summary>
		public static readonly Int32 ENOTCONN =
			Environment.OSVersion.Platform == PlatformID.Unix ? 107 :
			Environment.OSVersion.Platform == PlatformID.Win32NT ? 128 :
			ZMQ_HAUSNUMERO + 15;

		/// <summary>"Connection timed out"</summary>
		public static readonly Int32 ETIMEDOUT =
			Environment.OSVersion.Platform == PlatformID.Unix ? 110 :
			Environment.OSVersion.Platform == PlatformID.Win32NT ? 116 :
			ZMQ_HAUSNUMERO + 16;

		/// <summary>"No route to host"</summary>
		public static readonly Int32 EHOSTUNREACH =
			Environment.OSVersion.Platform == PlatformID.Unix ? 113 :
			Environment.OSVersion.Platform == PlatformID.Win32NT ? 118 :
			ZMQ_HAUSNUMERO + 17;

		/// <summary>"Network dropped connection because of reset"</summary>
		public static readonly Int32 ENETRESET =
			Environment.OSVersion.Platform == PlatformID.Unix ? 102 :
			Environment.OSVersion.Platform == PlatformID.Win32NT ? 126 :
			ZMQ_HAUSNUMERO + 18;

		/// <summary>"Operation cannot be accomplished in current state"</summary>
		public static readonly Int32 EFSM = ZMQ_HAUSNUMERO + 51;

		/// <summary>"The protocol is not compatible with the socket type"</summary>
		public static readonly Int32 ENOCOMPATPROTO = ZMQ_HAUSNUMERO + 52;

		/// <summary>"Context was terminated"</summary>
		public static readonly Int32 ETERM = ZMQ_HAUSNUMERO + 53;

		/// <summary>"No thread available"</summary>
		public static readonly Int32 EMTHREAD = ZMQ_HAUSNUMERO + 54;

		/// <summary>Retrieve the integer value of "errno" on the calling thread</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_errno();

		/// <summary>Retrieve the error message for a given "errno" integer</summary>
		[DllImport( "libzmq" )]
		[return: MarshalAs( UnmanagedType.LPStr )]
		public static extern String zmq_strerror( Int32 errnum );

		/******************************************************************************/
		/*  0MQ infrastructure (a.k.a. context) initialisation & termination.         */
		/******************************************************************************/

		/// <summary>Number of I/O threads</summary>
		public static readonly Int32 ZMQ_IO_THREADS = 1;

		/// <summary>Maximum number of sockets</summary>
		public static readonly Int32 ZMQ_MAX_SOCKETS = 2;

		/// <summary>Create a new ZeroMQ context</summary>
		[DllImport( "libzmq" )]
		public static extern IntPtr zmq_ctx_new();

		/// <summary>Destroy a ZeroMQ context</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_ctx_destroy( IntPtr context );

		/// <summary>Set a context's option</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_ctx_set( IntPtr context, Int32 option, Int32 optval );

		/// <summary>Get a context's option</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_ctx_get( IntPtr context, Int32 option );

		/******************************************************************************/
		/*  0MQ message definition.                                                   */
		/******************************************************************************/

		/////<summary></summary>
		//[StructLayout( LayoutKind.Sequential )]
		//public struct zmq_msg_t
		//{
		//	/// <summary></summary>
		//	[MarshalAs( UnmanagedType.ByValArray, SizeConst = 32 )]
		//	public readonly Byte[] _;
		//}

		/// <summary>A delegate that is capable of automatically freeing a message's data when it is no longer needed by ZeroMQ</summary>
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

		/// <summary>Send a message over a particular socket</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_msg_send( IntPtr msg, IntPtr socket, Int32 flags );

		/// <summary>Receive a message from a particular socket</summary>
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

		/// <summary>Determine whether this message is a multi-part message with more parts remaining</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_msg_more( IntPtr msg );

		/// <summary>Get the value of a message's option</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_msg_get( IntPtr msg, Int32 option );

		/// <summary>Set the value of a message's option</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_msg_set( IntPtr msg, Int32 option, Int32 optval );

		/******************************************************************************/
		/*  0MQ socket definition.                                                    */
		/******************************************************************************/

		/*  Socket types.                                                             */

		/// <summary>Exclusive Pair socket, used to connect a peer to precisely one other peer.</summary>
		/// <remarks>This pattern is meant to be used for inter-thread communication across the inproc
		/// transport and does not implement functionality such as auto-reconnection</remarks>
		public static readonly Int32 ZMQ_PAIR = 0;

		/// <summary>Publish socket, used for one-to-many distribution of data from a single
		/// publisher to multiple Subscribe or Extended Subscribe sockets in a fan out fashion to all
		/// connected peers</summary>
		/// <seealso cref="ZeroMQ.Interop.ZMQ_SUB" />
		/// <seealso cref="ZeroMQ.Interop.ZMQ_XSUB" />
		public static readonly Int32 ZMQ_PUB = 1;

		/// <summary>Subscribe socket, used to subscribe to data distributed by a publisher or
		/// an Extended Publish socket</summary>
		/// <seealso cref="ZeroMQ.Interop.ZMQ_PUB" />
		/// <seealso cref="ZeroMQ.Interop.ZMQ_XPUB" />
		public static readonly Int32 ZMQ_SUB = 2;

		/// <summary>Request socket, used for sending requests to one or more Reply or Router sockets,
		/// and for receiving subsequent replies the request that was sent</summary>
		/// <seealso cref="ZeroMQ.Interop.ZMQ_REP" />
		/// <seealso cref="ZeroMQ.Interop.ZMQ_ROUTER" />
		public static readonly Int32 ZMQ_REQ = 3;

		/// <summary>Reply socket, used to receive requests from and send replies to a Request or
		/// a Dealer socket</summary>
		/// <seealso cref="ZeroMQ.Interop.ZMQ_REQ" />
		/// <seealso cref="ZeroMQ.Interop.ZMQ_DEALER" />
		public static readonly Int32 ZMQ_REP = 4;

		/// <summary>Dealer socket, used to receive replies, consume their routing information,
		/// and them forward the replies along to the next hop in their return path</summary>
		/// <remarks>Previously known as an XREQ "Extended Request" socket</remarks>
		/// <seealso cref="ZeroMQ.Interop.ZMQ_REP" />
		/// <seealso cref="ZeroMQ.Interop.ZMQ_ROUTER" />
		/// <seealso cref="ZeroMQ.Interop.ZMQ_DEALER" />
		public static readonly Int32 ZMQ_DEALER = 5;

		/// <summary>Router socket, used to receive requests, prepend routing information to them,
		/// and then forward the requests along to Reply or Dealer sockets</summary>
		/// <remarks>Previously known as an XREP "Extended Reply" socket</remarks>
		/// <seealso cref="ZeroMQ.Interop.ZMQ_REQ" />
		/// <seealso cref="ZeroMQ.Interop.ZMQ_DEALER" />
		/// <seealso cref="ZeroMQ.Interop.ZMQ_ROUTER" />
		public static readonly Int32 ZMQ_ROUTER = 6;

		/// <summary>Pull socket, used to receive unidirectional messages from upstream Push sockets</summary>
		/// <seealso cref="ZeroMQ.Interop.ZMQ_PUSH" />
		public static readonly Int32 ZMQ_PULL = 7;

		/// <summary>Push socket, used to send unidirectional messages to downstream Pull sockets</summary>
		/// <seealso cref="ZeroMQ.Interop.ZMQ_PULL" />
		public static readonly Int32 ZMQ_PUSH = 8;

		/// <summary>Extended Publish socket, used like a regular Publish socket, except raw SUBSCRIBE and
		/// UNSUBSCRIBE messages can also be read from the socket, optionally allowing them to be forwarded
		/// along to other [Extended] Subscribe sockets</summary>
		/// <seealso cref="ZeroMQ.Interop.ZMQ_SUB" />
		/// <seealso cref="ZeroMQ.Interop.ZMQ_XSUB" />
		public static readonly Int32 ZMQ_XPUB = 9;

		/// <summary>Extended Subscribe socket, used like a regular Subscribe socket, except raw SUBSCRIBE and
		/// UNSUBSCRIBE messages can also be sent to the socket, optionally allowing them to be forwarded
		/// along to other [Extended] Publish sockets</summary>
		/// <seealso cref="ZeroMQ.Interop.ZMQ_PUB" />
		/// <seealso cref="ZeroMQ.Interop.ZMQ_XPUB" />
		public static readonly Int32 ZMQ_XSUB = 10;

		/*  Socket options.  */

		/// <summary>I/O thread affinity (get, set)</summary>
		public static readonly Int32 ZMQ_AFFINITY = 4;

		/// <summary>Socket "identity", the opaque binary string uniquely identifying a socket within a network (get, set)</summary>
		public static readonly Int32 ZMQ_IDENTITY = 5;

		/// <summary>Only receive messages with a certain "topic" (set)</summary>
		public static readonly Int32 ZMQ_SUBSCRIBE = 6;

		/// <summary>Stop receiving messages with a certain "topic" (set)</summary>
		public static readonly Int32 ZMQ_UNSUBSCRIBE = 7;

		/// <summary>Multicast data rate (get, set)</summary>
		public static readonly Int32 ZMQ_RATE = 8;

		/// <summary>Multicast recovery interval (get, set)</summary>
		public static readonly Int32 ZMQ_RECOVERY_IVL = 9;

		/// <summary>Kernel transmit buffer size (get, set)</summary>
		public static readonly Int32 ZMQ_SNDBUF = 11;

		/// <summary>Kernel receive buffer size (get, set)</summary>
		public static readonly Int32 ZMQ_RCVBUF = 12;

		/// <summary>More message data parts to follow (get)</summary>
		public static readonly Int32 ZMQ_RCVMORE = 13;

		/// <summary>File descriptor associated with the socket (get)</summary>
		public static readonly Int32 ZMQ_FD = 14;

		/// <summary>Socket event state (get)</summary>
		public static readonly Int32 ZMQ_EVENTS = 15;

		/// <summary>Socket type (get)</summary>
		public static readonly Int32 ZMQ_TYPE = 16;

		/// <summary>Linger period for socket shutdown (get, set)</summary>
		public static readonly Int32 ZMQ_LINGER = 17;

		/// <summary>Initial reconnection interval in milliseconds (get, set)</summary>
		public static readonly Int32 ZMQ_RECONNECT_IVL = 18;

		/// <summary>Maximum length of the queue of outstanding connections (get, set)</summary>
		public static readonly Int32 ZMQ_BACKLOG = 19;

		/// <summary>Maximum reconnection interval in milliseconds (get, set)</summary>
		public static readonly Int32 ZMQ_RECONNECT_IVL_MAX = 21;

		/// <summary>Maximum acceptable inbound message size (get, set)</summary>
		public static readonly Int32 ZMQ_MAXMSGSIZE = 22;

		/// <summary>High water mark for outgoing messages (get, set)</summary>
		public static readonly Int32 ZMQ_SNDHWM = 23;

		/// <summary>High water mark for incoming messages (get, set)</summary>
		public static readonly Int32 ZMQ_RCVHWM = 24;

		/// <summary>Maximum network hops for multicast packets (get, set)</summary>
		public static readonly Int32 ZMQ_MULTICAST_HOPS = 25;

		/// <summary>Maximum time before a recv operation returns with EAGAIN (get, set)</summary>
		public static readonly Int32 ZMQ_RCVTIMEO = 27;

		/// <summary>Maximum time before a send operation returns with EAGAIN (get, set)</summary>
		public static readonly Int32 ZMQ_SNDTIMEO = 28;

		/// <summary>Use IPv4-only sockets (get, set)</summary>
		public static readonly Int32 ZMQ_IPV4ONLY = 31;

		/// <summary>The last endpoint bound to the socket (get)</summary>
		public static readonly Int32 ZMQ_LAST_ENDPOINT = 32;

		/// <summary>Accept only routable messages on ROUTER sockets (set)</summary>
		public static readonly Int32 ZMQ_ROUTER_MANDATORY = 33;

		/// <summary>Override SO_KEEPALIVE socket option (get, set)</summary>
		public static readonly Int32 ZMQ_TCP_KEEPALIVE = 34;

		/// <summary>Override TCP_KEEPCNT socket option (get, set)</summary>
		public static readonly Int32 ZMQ_TCP_KEEPALIVE_CNT = 35;

		/// <summary>Override TCP_KEEPCNT or TCP_KEEPALIVE socket option (get, set)</summary>
		public static readonly Int32 ZMQ_TCP_KEEPALIVE_IDLE = 36;

		/// <summary>Override TCP_KEEPINTVL socket option (get, set)</summary>
		public static readonly Int32 ZMQ_TCP_KEEPALIVE_INTVL = 37;

		/// <summary>Filter a specific CIDR network from establishing new TCP connections (set)</summary>
		public static readonly Int32 ZMQ_TCP_ACCEPT_FILTER = 38;

		/// <summary>Accept messages only when connections are made, to prevent queues from filling while awaiting connection (get, set)</summary>
		public static readonly Int32 ZMQ_DELAY_ATTACH_ON_CONNECT = 39;

		/// <summary>Provide all subscription messages on XPUB sockets (set)</summary>
		public static readonly Int32 ZMQ_XPUB_VERBOSE = 40;

		/*  Message options  */

		/// <summary>Specifies that a message is a multi-part message</summary>
		public static readonly Int32 ZMQ_MORE = 1;

		/*  Send/recv options.  */

		/// <summary>Perform socket operations in non-blocking mode</summary>
		public static readonly Int32 ZMQ_DONTWAIT = 1;

		/// <summary>Indicates that the message is a multi-part message</summary>
		public static readonly Int32 ZMQ_SNDMORE = 2;

		/******************************************************************************/
		/*  0MQ socket events and monitoring                                          */
		/******************************************************************************/

		/*  Socket transport events (tcp and ipc only)  */

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

		///// <summary></summary>
		//[StructLayout( LayoutKind.Explicit )]
		//public struct zmq_event_t
		//{
		//	/// <summary></summary>
		//	[FieldOffset( 0 ), MarshalAs( UnmanagedType.SysInt )]
		//	public Int32 @event;
		//	/// <summary></summary>
		//	[FieldOffset( 1 ), MarshalAs( UnmanagedType.LPTStr, CharSet = CharSet.Ansi )]
		//	public String addr;
		//	/// <summary></summary>
		//	[FieldOffset( 2 )]
		//	public IntPtr fd;
		//	/// <summary></summary>
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

		/// <summary>Receive the contents of the next message on a ZeroMQ socket</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_recv( IntPtr socket, IntPtr buf, Int32 len, Int32 flags );

		/// <summary>Create a virtual Pair socket which sends events about the specified socket to a given "inproc" endpoint</summary>
		[DllImport( "libzmq", CharSet = CharSet.Ansi )]
		public static extern Int32 zmq_socket_monitor( IntPtr socket, String addr, Int32 events );

		/// <summary>Poll a socket for the ability to receive data</summary>
		public static readonly Int32 ZMQ_POLLIN = 1;

		/// <summary>Poll a socket for the ability to send data</summary>
		public static readonly Int32 ZMQ_POLLOUT = 2;

		/// <summary>Poll a socket for an error condition</summary>
		public static readonly Int32 ZMQ_POLLERR = 4;

		/// <summary>ZeroMQ poll items structure</summary>
		[StructLayout( LayoutKind.Sequential )]
		public struct zmq_pollitem_t
		{
			/// <summary>ZeroMQ socket</summary>
			public IntPtr socket;
			/// <summary>System socket</summary>
			public IntPtr fd;
			/// <summary>Interested events</summary>
			public Int16 events;
			/// <summary>Received events</summary>
			public Int16 revents;
		}

		/// <summary>Receive the next message on a ZeroMQ socket</summary>
		[DllImport( "libzmq" )]
		public static extern Int32 zmq_poll( [In] [Out] zmq_pollitem_t[] items, Int32 nitems, Int32 timeout );
	}
}
