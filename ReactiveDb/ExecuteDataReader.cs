using System;
using System.Data;
using System.Reactive.Disposables;


namespace ReactiveDb
{
    internal class ExecuteDataReader<T> : DbObservable<T>
    {
        private Func<IDataReader, T> _mapper;
        public ExecuteDataReader(IDbCommand cmd, IDbConnection conn, Func<IDataReader, T> mapper) : base(cmd, conn)
        {
            _mapper = mapper;
        }

        public override IDisposable Subscribe(IObserver<T> observer)
        {
            try
            {
                using (OpenConnection(_cmd.Connection))
                {
                    using (_cmd)
                    {
                        using (IDataReader dr = _cmd.ExecuteReader(CommandBehavior.CloseConnection))
                        {
                            while (dr.Read())
                            {
                                observer.OnNext(_mapper(dr));
                            }
                        }
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
