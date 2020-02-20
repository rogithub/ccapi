using System;
using System.Data;
using System.Reactive.Disposables;

namespace ReactiveDb
{
    internal class ExecuteEscalar<T> : DbObservable<T>
    {
        private Func<object, T> _mapper;
        public ExecuteEscalar(IDbCommand cmd, IDbConnection conn, Func<object, T> mapper) : base(cmd, conn)
        {
            _mapper = mapper;
        }

        public override IDisposable Subscribe(IObserver<T> observer)
        {
            try
            {
                using (OpenConnection(_cmd.Connection))
                {
                    object o = null;
                    using (_cmd)
                    {
                        o = _cmd.ExecuteScalar();
                    }
                    if (o != null)
                    {
                        observer.OnNext(_mapper(o));
                    }
                }
            }
            catch (Exception ex)
            {
                observer.OnError(ex);
            }
            finally
            {
                observer.OnCompleted();
            }

            return Disposable.Empty;
        }
    }
}
