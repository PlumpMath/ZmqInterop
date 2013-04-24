namespace ZeroMQ
{
	/// <summary></summary>
	public sealed class ZmqSubscribeSocket: ZmqSocket
	{
		public ZmqSubscribeSocket( ZmqContext context )
			: base( context, Interop.ZMQ_SUB )
		{
		}
	}
}
