using System;
using System.Collections.Generic;
using System.Data;
using Entities;
using ReactiveDb;

namespace Repositories
{
    public abstract class BaseRepo<T> : IBaseRepo<T>
    {
        protected IDatabase Db { get; set; }
        protected abstract string GetByIdSql { get; }
        protected abstract string GetByGuidSql { get; }
        protected abstract string SerchSql { get; }
        protected abstract string DeleteSql { get; }
        protected abstract string UpdateSql { get; }
        protected abstract string SaveSql { get; }
        public BaseRepo(IDatabase db)
        {
            this.Db = db;
        }
        protected abstract T GetData(IDataReader dr);
        protected abstract IDbDataParameter[] ToUpdateParams(T model);
        protected abstract IDbDataParameter[] ToSaveParams(T model);

        protected Resultset<T> GetResultSet(IDataReader dr)
        {
            return new Resultset<T>(dr.GetLong("total_rows"), GetData(dr));
        }

        public IObservable<T> Get(Guid id)
        {
            var param = "@guid".ToParam(DbType.Guid, id);

            var cmd = GetByGuidSql.ToCmd(CommandType.Text, param);

            return Db.ExecuteDataReader(cmd, GetData);
        }

        public IObservable<T> Get(Int64 id)
        {
            var param = "@id".ToParam(DbType.Int64, id);

            var cmd = GetByIdSql.ToCmd(CommandType.Text, param);

            return Db.ExecuteDataReader(cmd, GetData);
        }

        public IObservable<Resultset<T>> GetAll(int limit, int offset, string search)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add("@limit".ToParam(DbType.Int32, limit));
            parameters.Add("@offset".ToParam(DbType.Int32, offset));
            string whereClause = "";
            if (!string.IsNullOrWhiteSpace(search))
            {
                whereClause = "AND search_field @@ plainto_tsquery(@search)";
                parameters.Add("@search".ToParam(DbType.String, search));
            }
            var cmd = string.Format(SerchSql, whereClause).
            ToCmd(CommandType.Text, parameters.ToArray());

            return Db.ExecuteDataReader(cmd, GetResultSet);
        }

        public IObservable<int> Delete(Guid id)
        {
            var param = "@guid".ToParam(DbType.Guid, id);

            var cmd = DeleteSql.ToCmd(CommandType.Text, param);

            return Db.ExecuteNonQuery(cmd);
        }

        public IObservable<int> Update(T entity)
        {
            var parameters = ToUpdateParams(entity);
            var cmd = UpdateSql.ToCmd(CommandType.Text, parameters);
            return Db.ExecuteNonQuery(cmd);
        }

        public IObservable<int> Save(T entity)
        {
            var parameters = ToSaveParams(entity);
            var cmd = SaveSql.ToCmd(CommandType.Text, parameters);
            return Db.ExecuteNonQuery(cmd);
        }
    }
}
