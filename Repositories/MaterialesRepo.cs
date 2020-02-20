using System;
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

        private Func<IDataReader, Material> _getData = (dr) =>
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

        public IObservable<Material> Get(Guid id)
        {
            var param = "@guid".ToParam(DbType.Guid, id);

            var cmd = @"SELECT id, guid, nombre, color, unidad, 
            marca, modelo, comentarios, activo 
            FROM public.materiales WHERE guid=@guid;".ToCmd(CommandType.Text, param);            

            return Db.ExecuteDataReader(cmd, _getData);
        }


        public IObservable<Material> GetAll()
        {
            var cmd = @"SELECT id, guid, nombre, color, unidad, 
            marca, modelo, comentarios, activo 
            FROM public.materiales;".ToCmd();            

            return Db.ExecuteDataReader(cmd, _getData);
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