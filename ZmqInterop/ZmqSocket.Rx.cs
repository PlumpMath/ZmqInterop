namespace ZeroMQ
{
	using System;
	using System.Collections.Concurrent;
	using System.Collections.Generic;

	public abstract partial class ZmqSocket: IObservable<Byte[]>
	{
		protected readonly ConcurrentDictionary<Byte[], IObserver<Byte[]>> Subscribers
			= new ConcurrentDictionary<Byte[], IObserver<Byte[]>>();

		/// <summary></summary>
		/// <returns></returns>
		public IDisposable Subscribe( IObserver<Byte[]> observer )
		{
			Byte[] id;

			while( true )
			{
				id = Guid.NewGuid().ToByteArray();
				if( this.Subscribers.TryAdd( id, observer ) ) break;
			}

			return new Unsubscriber( id, this );
		}

		private class Unsubscriber : IDisposable
		{
			private readonly Byte[] ID;

			private readonly ZmqSocket Socket;

			private Boolean IsUnsubscribed;

			public Unsubscriber( Byte[] id, ZmqSocket socket )
			{
				this.ID = id;
				this.Socket = socket;
			}

			public void Dispose()
			{
				IObserver<Byte[]> unused;
				this.Socket.Subscribers.TryRemove( this.ID, out unused );
			}
		}
	}
}
