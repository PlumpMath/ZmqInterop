namespace ZeroMQ
{
	/// <summary></summary>
	public sealed class ZmqRouterSocket: ZmqSocket
	{
		public ZmqRouterSocket( ZmqContext context )
			: base( context, Interop.ZMQ_ROUTER )
		{
		}
	}
}
