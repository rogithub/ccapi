using System;
using System.Collections.Generic;
using System.Data;
using Entities;
using ReactiveDb;

namespace Repositories
{

    public class MaterialesRepo : BaseRepo<Material>, IBaseRepo<Material>
    {
        public MaterialesRepo(IDatabase db) : base(db)
        {

        }

        protected override string GetByIdSql =>
        @"SELECT id, guid, nombre, color, unidad, 
            marca, modelo, comentarios, activo 
        FROM 
            public.materiales WHERE id=@id AND activo=TRUE;";
        protected override string GetByGuidSql =>
        @"SELECT id, guid, nombre, color, unidad, 
            marca, modelo, comentarios, activo 
        FROM public.materiales WHERE guid=@guid AND activo=TRUE;";

        protected override string SerchSql =>
        @"SELECT 
            id, guid, nombre, color, unidad, marca, modelo, comentarios, activo, 
            COUNT(*) OVER() as total_rows 
        FROM 
            public.materiales WHERE activo=TRUE {0} 
        ORDER BY 
            id
        LIMIT @limit OFFSET @offset;";

        protected override string DeleteSql =>
        @"UPDATE public.materiales SET activo=FALSE WHERE guid=@guid;";

        protected override string UpdateSql =>
        @"UPDATE public.materiales SET 
                nombre=@nombre,
                color=@color,
                unidad=@unidad,
                marca=@marca,
                modelo=@modelo,
                comentarios=@comentarios,
             WHERE guid=@guid;";

        protected override string SaveSql =>
        @"INSERT INTO public.materiales 
            (guid, nombre, color, unidad, marca, modelo, comentarios, activo) 
            VALUES 
            (@guid, @nombre, @color, @unidad, @marca, @modelo, @comentarios, @activo) ;";

        protected override Material GetData(IDataReader dr)
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
        }

        protected override IDbDataParameter[] ToSaveParams(Material model)
        {
            return new IDbDataParameter[] {
                "@guid".ToParam(DbType.Guid, model.Guid),
                "@nombre".ToParam(DbType.String, model.Nombre),
                "@color".ToParam(DbType.String, model.Color),
                "@unidad".ToParam(DbType.String, model.Unidad),
                "@marca".ToParam(DbType.String, model.Marca??""),
                "@modelo".ToParam(DbType.String, model.Modelo??""),
                "@comentarios".ToParam(DbType.String, model.Comentarios??""),
                "@activo".ToParam(DbType.Boolean, model.Activo)
            };
        }

        protected override IDbDataParameter[] ToUpdateParams(Material model)
        {
            return new IDbDataParameter[] {
                "@guid".ToParam(DbType.Guid, model.Guid),
                "@nombre".ToParam(DbType.String, model.Nombre),
                "@color".ToParam(DbType.String, model.Color),
                "@unidad".ToParam(DbType.String, model.Unidad),
                "@marca".ToParam(DbType.String, model.Marca),
                "@modelo".ToParam(DbType.String, model.Modelo),
                "@comentarios".ToParam(DbType.String, model.Comentarios),
            };
        }
    }
}
