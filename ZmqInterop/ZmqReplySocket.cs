namespace ZeroMQ
{
	/// <summary></summary>
	public sealed class ZmqReplySocket: ZmqSocket
	{
		public ZmqReplySocket( ZmqContext context )
			: base( context, Interop.ZMQ_REP )
		{
		}
	}
}
