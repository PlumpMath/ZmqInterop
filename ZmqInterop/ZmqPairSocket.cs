namespace ZeroMQ
{
	/// <summary></summary>
	public sealed class ZmqPairSocket: ZmqSocket
	{
		public ZmqPairSocket( ZmqContext context )
			: base( context, Interop.ZMQ_PAIR )
		{
		}
	}
}
