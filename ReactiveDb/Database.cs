using System;
using System.Data;
using Npgsql;

namespace ReactiveDb
{
    public class Database : IDatabase
    {
        private string ConnectionString { get; set; }
        public Database(string connString)
        {
            this.ConnectionString = connString;
        }

        private IDbConnection GetConnection()
        {
            return new NpgsqlConnection(this.ConnectionString);
        }

        public IObservable<int> ExecuteNonQuery(IDbCommand cmd)
        {
            return new ExecuteNonQuery(cmd, GetConnection());
        }

        public IObservable<T> ExecuteScalar<T>(IDbCommand cmd, Func<object, T> mapper)
        {
            return new ExecuteEscalar<T>(cmd, GetConnection(), mapper);
        }

        public IObservable<T> ExecuteDataReader<T>(IDbCommand cmd, Func<IDataReader, T> mapper)
        {
            return new ExecuteDataReader<T>(cmd, GetConnection(), mapper);
        }
    }
}
