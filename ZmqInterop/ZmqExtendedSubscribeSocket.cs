namespace ZeroMQ
{
	/// <summary></summary>
	public sealed class ZmqExtendedSubscribeSocket: ZmqSocket
	{
		public ZmqExtendedSubscribeSocket( ZmqContext context )
			: base( context, Interop.ZMQ_XSUB )
		{
		}
	}
}
