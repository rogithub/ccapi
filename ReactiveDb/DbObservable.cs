using System;
using System.Data;

namespace ReactiveDb
{
    internal abstract class DbObservable<T> : IObservable<T>
    {
        protected IDbCommand _cmd;
        public DbObservable(IDbCommand cmd, IDbConnection conn)
        {
            this._cmd = cmd;
            this._cmd.Connection = conn;
        }

        protected IDbConnection OpenConnection(IDbConnection conn)
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }
            return conn;
        }

        protected IDbConnection CloseConnection(IDbConnection conn)
        {
            if (conn.State != System.Data.ConnectionState.Closed)
            {
                conn.Close();
            }
            return conn;
        }

        public abstract IDisposable Subscribe(IObserver<T> observer);
    }
}
