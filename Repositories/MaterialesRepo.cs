using System;
using System.Collections.Generic;
using System.Data;
using Entities;
using Ro.Npgsql.Data;

namespace Repositories
{

    public class MaterialesRepo : BaseRepo<Material>, IBaseRepo<Material>
    {
        public MaterialesRepo(IDbAsync db) : base(db)
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
            {1}
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
                comentarios=@comentarios
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

        protected override Dictionary<string, IDbDataParameter> ToParams(Material model)
        {
            return new Dictionary<string, IDbDataParameter>() {
                { "@id", "@id".ToParam(DbType.Int64, model.Id) },
                { "@guid", "@guid".ToParam(DbType.Guid, model.Guid) },
                { "@nombre", "@nombre".ToParam(DbType.String, model.Nombre) },
                { "@color", "@color".ToParam(DbType.String, model.Color) },
                { "@unidad", "@unidad".ToParam(DbType.String, model.Unidad) },
                { "@marca", "@marca".ToParam(DbType.String, model.Marca ?? "") },
                { "@modelo", "@modelo".ToParam(DbType.String, model.Modelo ?? "") },
                { "@comentarios", "@comentarios".ToParam(DbType.String, model.Comentarios ?? "") },
                { "@activo", "@activo".ToParam(DbType.Boolean, model.Activo) }
            };
        }

        protected override IDbDataParameter[] ToSaveParams(Material model)
        {
            var d = ToParams(model);
            return new IDbDataParameter[] {
                d["@guid"],
                d["@nombre"],
                d["@color"],
                d["@unidad"],
                d["@marca"],
                d["@modelo"],
                d["@comentarios"],
                d["@activo"]
            };
        }

        protected override IDbDataParameter[] ToUpdateParams(Material model)
        {
            var d = ToParams(model);
            return new IDbDataParameter[] {
                d["@guid"],
                d["@nombre"],
                d["@color"],
                d["@unidad"],
                d["@marca"],
                d["@modelo"],
                d["@comentarios"]
            };
        }
    }
}
