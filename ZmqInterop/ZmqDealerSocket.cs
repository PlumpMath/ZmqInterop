namespace ZeroMQ
{
	/// <summary></summary>
	public sealed class ZmqDealerSocket: ZmqSocket
	{
		public ZmqDealerSocket( ZmqContext context )
			: base( context, Interop.ZMQ_DEALER )
		{
		}
	}
}
