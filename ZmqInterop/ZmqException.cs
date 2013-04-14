namespace ZeroMQ
{
	using System;

	/// <summary>Exception class representing an error thrown by the ZeroMQ library</summary>
	public class ZmqException : Exception
	{
		/// <summary>The error code thrown by the ZeroMQ API</summary>
		public readonly Int32 ErrorCode;

		/// <summary>Create an exception representing a ZeroMQ error code</summary>
		public ZmqException( Int32 errorCode )
			: base( Interop.zmq_strerror( errorCode ) )
		{
			this.ErrorCode = errorCode;
		}
	}
}
