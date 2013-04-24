namespace ZeroMQ
{
	/// <summary></summary>
	public sealed class ZmqPullSocket: ZmqSocket
	{
		public ZmqPullSocket( ZmqContext context )
			: base( context, Interop.ZMQ_PULL )
		{
		}
	}
}
