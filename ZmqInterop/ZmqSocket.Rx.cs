namespace ZeroMQ
{
	using System;
	using System.Collections.Generic;

	using SubscriberDictionary = System.Collections.Concurrent.ConcurrentDictionary<System.Byte[], System.IObserver<System.Collections.Generic.IEnumerable<System.Byte[]>>>;

	public abstract partial class ZmqSocket: IObservable<IEnumerable<Byte[]>>
	{
		internal readonly SubscriberDictionary Subscribers = new SubscriberDictionary();

		public IDisposable Subscribe( IObserver<IEnumerable<Byte[]>> observer )
		{
			Byte[] id;

			while( true )
			{
				id = Guid.NewGuid().ToByteArray();
				if( this.Subscribers.TryAdd( id, observer ) ) break;
			}

			return new Unsubscriber( id, this );
		}

		protected class Unsubscriber : IDisposable
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
				if( this.IsUnsubscribed ) return;
				IObserver<IEnumerable<Byte[]>> unused;
				this.Socket.Subscribers.TryRemove( this.ID, out unused );
				this.IsUnsubscribed = true;
			}
		}
	}
}
