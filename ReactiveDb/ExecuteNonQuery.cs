using System;
using System.Data;
using System.Reactive.Disposables;

namespace ReactiveDb
{
    internal class ExecuteNonQuery : DbObservable<int>
    {
        public ExecuteNonQuery(IDbCommand cmd, IDbConnection conn) : base(cmd, conn)
        {

        }

        public override IDisposable Subscribe(IObserver<int> observer)
        {
            try
            {
                using (OpenConnection(_cmd.Connection))
                {
                    using (_cmd)
                    {
                        observer.OnNext(_cmd.ExecuteNonQuery());
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
