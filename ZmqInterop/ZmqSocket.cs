namespace ZeroMQ
{
	using System;
	using System.Collections.Generic;
	using System.Reactive;
	using System.Reactive.Subjects;
	using System.Reactive.Disposables;

	/// <summary></summary>
	public abstract class ZmqSocket: IConnectableObservable<Byte[]>
	{
		public IDisposable Subscribe( IObserver<Byte[]> observer )
		{
			throw new NotImplementedException();
		}

		protected readonly CancellationDisposable m_socketDisposer = new CancellationDisposable();

		/// <summary></summary>
		/// <returns></returns>
		public IDisposable Bind()
		{
			return this.Connect();
		}

		/// <summary></summary>
		/// <returns></returns>
		public IDisposable Connect()
		{
			this.BindOrConnect();

			return m_socketDisposer;
		}

		protected abstract void BindOrConnect();
	}
}
