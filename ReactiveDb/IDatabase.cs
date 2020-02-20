using System;
using System.Data;

namespace ReactiveDb
{
    public interface IDatabase
    {
        IObservable<int> ExecuteNonQuery(IDbCommand cmd);
        IObservable<T> ExecuteScalar<T>(IDbCommand cmd, Func<object, T> mapper);
        IObservable<T> ExecuteDataReader<T>(IDbCommand cmd, Func<IDataReader, T> mapper);
    }
}
