namespace ZeroMQ
{
	/// <summary></summary>
	public sealed class ZmqPublishSocket: ZmqSocket
	{
		public ZmqPublishSocket( ZmqContext context )
			: base( context, Interop.ZMQ_PUB )
		{
		}
	}
}
