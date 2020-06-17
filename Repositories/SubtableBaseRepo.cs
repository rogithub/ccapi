using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using Entities;
using Ro.Npgsql.Data;
using System.Threading.Tasks;

namespace Repositories
{
    public abstract class SubtableBaseRepo<T> : ISubtableBaseRepo<T>
    {
        protected IDbAsync Db { get; set; }
        protected abstract string GetByIdSql { get; }
        protected abstract string GetByGuidSql { get; }
        protected abstract string SerchSql { get; }
        protected abstract string DeleteSql { get; }
        protected abstract string UpdateSql { get; }
        protected abstract string SaveSql { get; }
        public SubtableBaseRepo(IDbAsync db)
        {
            this.Db = db;
        }
        protected abstract T GetData(IDataReader dr);
        protected abstract IDbDataParameter[] ToUpdateParams(T model);
        protected abstract IDbDataParameter[] ToSaveParams(T model);
        protected abstract Dictionary<string, IDbDataParameter> ToParams(T model);

        protected Resultset<T> GetResultSet(IDataReader dr)
        {
            return new Resultset<T>(dr.GetLong("total_rows"), GetData(dr));
        }

        public Task<T> Get(Guid id)
        {
            var param = "@guid".ToParam(DbType.Guid, id);

            var cmd = GetByGuidSql.ToCmd(CommandType.Text, param);

            return Db.GetOneRow(cmd, GetData);
        }

        public Task<T> Get(Int64 id)
        {
            var param = "@id".ToParam(DbType.Int64, id);

            var cmd = GetByIdSql.ToCmd(CommandType.Text, param);

            return Db.GetOneRow(cmd, GetData);
        }

        public Task<Resultset<T>> Search(SearchData entity, Guid parentId)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add("@limit".ToParam(DbType.Int32, entity.Limit));
            parameters.Add("@offset".ToParam(DbType.Int32, entity.Offset));
            parameters.Add("@parentId".ToParam(DbType.Guid, parentId));

            string whereClause = "";
            if (!string.IsNullOrWhiteSpace(entity.Pattern))
            {
                whereClause = "AND search_field @@ plainto_tsquery(@search)";
                parameters.Add("@search".ToParam(DbType.String, entity.Pattern));
            }
            if (entity.Columns == null || entity.Columns.Length == 0)
            {
                entity.Columns = new OrderCol[] { new OrderCol() { Col = "id", Order = Order.Asc } };
            }
            string orderby = string.Join(",",
            (from c in entity.Columns select c.ToString()).ToArray());

            var cmd = string.Format(SerchSql, whereClause, orderby).
            ToCmd(CommandType.Text, parameters.ToArray());

            return Db.GetOneRow(cmd, GetResultSet);
        }
        public Task<int> Delete(Guid id)
        {
            var param = "@guid".ToParam(DbType.Guid, id);

            var cmd = DeleteSql.ToCmd(CommandType.Text, param);

            return Db.ExecuteNonQuery(cmd);
        }

        public Task<int> Update(T entity)
        {
            var parameters = ToUpdateParams(entity);
            var cmd = UpdateSql.ToCmd(CommandType.Text, parameters);
            return Db.ExecuteNonQuery(cmd);
        }

        public Task<int> Save(T entity)
        {
            var parameters = ToSaveParams(entity);
            var cmd = SaveSql.ToCmd(CommandType.Text, parameters);
            return Db.ExecuteNonQuery(cmd);
        }
    }
}
