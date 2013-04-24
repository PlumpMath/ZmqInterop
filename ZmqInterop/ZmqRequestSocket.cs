namespace ZeroMQ
{
	/// <summary></summary>
	public sealed class ZmqRequestSocket: ZmqSocket
	{
		public ZmqRequestSocket( ZmqContext context )
			: base( context, Interop.ZMQ_REQ )
		{
		}
	}
}
