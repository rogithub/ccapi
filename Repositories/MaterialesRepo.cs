using System;
using System.Collections.Generic;
using System.Data;
using Entities;
using ReactiveDb;

namespace Repositories
{
    public class MaterialesRepo : IMaterialesRepo
    {
        private IDatabase Db { get; set; }
        public MaterialesRepo(IDatabase db)
        {
            this.Db = db;
        }

        private static Func<IDataReader, Material> _getData = (dr) =>
            {
                return new Material()
                {
                    Id = dr.GetInt("id"),
                    Guid = dr.GetGuid("guid"),
                    Nombre = dr.GetString("nombre"),
                    Color = dr.GetString("color"),
                    Unidad = dr.GetString("unidad"),
                    Marca = dr.GetString("marca"),
                    Modelo = dr.GetString("modelo"),
                    Comentarios = dr.GetString("comentarios"),
                    Activo = dr.GetValue<bool>("activo")
                };
            };

        private static Func<IDataReader, Resultset<Material>> _getResultSet = (dr) =>
            {
                return new Resultset<Material>(dr.GetLong("total_rows"), _getData(dr));
            };

        public IObservable<Material> Get(Guid id)
        {
            var param = "@guid".ToParam(DbType.Guid, id);

            var cmd = @"SELECT id, guid, nombre, color, unidad, 
            marca, modelo, comentarios, activo 
            FROM public.materiales WHERE guid=@guid;".ToCmd(CommandType.Text, param);

            return Db.ExecuteDataReader(cmd, _getData);
        }


        public IObservable<Resultset<Material>> GetAll(int limit, int offset, string search)
        {
            List<IDbDataParameter> parameters = new List<IDbDataParameter>();
            parameters.Add("@limit".ToParam(DbType.Int32, limit));
            parameters.Add("@offset".ToParam(DbType.Int32, offset));
            string whereClause = "";
            if (!string.IsNullOrWhiteSpace(search))
            {
                whereClause = "WHERE search_field @@ plainto_tsquery(@search)";
                parameters.Add("@search".ToParam(DbType.String, search));
            }
            var cmd = string.Format(@"            
                SELECT 
                    id, guid, nombre, color, unidad, marca, modelo, comentarios, activo, 
                    COUNT(*) OVER() as total_rows 
                FROM 
                    public.materiales {0} 
                ORDER BY 
                    id
                LIMIT @limit OFFSET @offset;
            ", whereClause).ToCmd(CommandType.Text, parameters.ToArray());

            return Db.ExecuteDataReader(cmd, _getResultSet);
        }

        public IObservable<int> Delete(Guid id)
        {
            var param = "@guid".ToParam(DbType.Guid, id);

            var cmd = @"UPDATE public.materiales SET activo=@activo WHERE guid=@guid;".ToCmd(CommandType.Text, param);

            return Db.ExecuteNonQuery(cmd);
        }

        public IObservable<int> Update(Material material)
        {
            var parameters = new IDbDataParameter[] {
                "@guid".ToParam(DbType.Guid, material.Guid),
                "@nombre".ToParam(DbType.String, material.Nombre),
                "@color".ToParam(DbType.String, material.Color),
                "@unidad".ToParam(DbType.String, material.Unidad),
                "@marca".ToParam(DbType.String, material.Marca),
                "@modelo".ToParam(DbType.String, material.Modelo),
                "@comentarios".ToParam(DbType.String, material.Comentarios),
            };
            var cmd =
            @"UPDATE public.materiales SET 
                nombre=@nombre,
                color=@color,
                unidad=@unidad,
                marca=@marca,
                modelo=@modelo,
                comentarios=@comentarios,
             WHERE guid=@guid;".ToCmd(CommandType.Text, parameters);

            return Db.ExecuteNonQuery(cmd);
        }

        public IObservable<int> Save(Material material)
        {
            var parameters = new IDbDataParameter[] {
                "@guid".ToParam(DbType.Guid, material.Guid),
                "@nombre".ToParam(DbType.String, material.Nombre),
                "@color".ToParam(DbType.String, material.Color),
                "@unidad".ToParam(DbType.String, material.Unidad),
                "@marca".ToParam(DbType.String, material.Marca),
                "@modelo".ToParam(DbType.String, material.Modelo),
                "@comentarios".ToParam(DbType.String, material.Comentarios),
                "@activo".ToParam(DbType.Boolean, material.Activo)
            };
            var cmd =
            @"INSERT INTO public.materiales 
            (guid, nombre, color, unidad, marca, modelo, comentarios, activo) 
            VALUES 
            (@guid, @nombre, @color, @unidad, @marca, @modelo, @comentarios, @activo) ;".ToCmd(CommandType.Text, parameters);

            return Db.ExecuteNonQuery(cmd);
        }
    }
}
