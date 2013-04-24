namespace ZeroMQ
{
	/// <summary></summary>
	public sealed class ZmqPushSocket: ZmqSocket
	{
		public ZmqPushSocket( ZmqContext context )
			: base( context, Interop.ZMQ_PUSH )
		{
		}
	}
}
