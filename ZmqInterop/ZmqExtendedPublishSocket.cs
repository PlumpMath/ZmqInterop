namespace ZeroMQ
{
	/// <summary></summary>
	public sealed class ZmqExtendedPublishSocket: ZmqSocket
	{
		public ZmqExtendedPublishSocket( ZmqContext context )
			: base( context, Interop.ZMQ_XPUB )
		{
		}
	}
}
