using System;
using System.Collections.Generic;
using System.Data;
using Entities;
using Ro.Npgsql.Data;

namespace Repositories
{
    public class ProveedoresRepo : BaseRepo<Proveedor>, IBaseRepo<Proveedor>
    {
        public ProveedoresRepo(IDbAsync db) : base(db)
        {

        }

        protected override string GetByGuidSql =>
        @"SELECT id, guid, contacto, empresa, 
                 telefono, email, domicilio, comentarios, activo 
        FROM public.proveedores WHERE guid=@guid AND activo=TRUE;";

        protected override string GetByIdSql =>
        @"SELECT id, guid, contacto, empresa, 
                 telefono, email, domicilio, comentarios, activo 
        FROM public.proveedores WHERE id=@id AND activo=TRUE;";
        protected override string SerchSql =>
        @"SELECT 
            id, guid, contacto, empresa, telefono, email, domicilio, comentarios, activo, 
            COUNT(*) OVER() as total_rows 
        FROM 
            public.proveedores WHERE activo=TRUE {0} 
        ORDER BY 
            {1}
        LIMIT @limit OFFSET @offset;";

        protected override string DeleteSql =>
        @"UPDATE public.proveedores SET activo=FALSE WHERE guid=@guid;";

        protected override string UpdateSql =>
        @"UPDATE public.proveedores SET
                contacto=@contacto,
                empresa=@empresa,
                telefono=@telefono,
                email=@email,
                domicilio=@domicilio,
                comentarios=@comentarios
             WHERE guid=@guid;";

        protected override string SaveSql =>
        @"INSERT INTO public.proveedores 
            (guid, contacto, empresa, telefono, email, domicilio, comentarios, activo) 
            VALUES 
            (@guid, @contacto, @empresa, @telefono, @email, @domicilio, @comentarios, @activo);";

        protected override Proveedor GetData(IDataReader dr)
        {
            return new Proveedor()
            {
                Id = dr.GetInt("id"),
                Guid = dr.GetGuid("guid"),
                Contacto = dr.GetString("contacto"),
                Empresa = dr.GetString("empresa"),
                Telefono = dr.GetString("telefono"),
                Email = dr.GetString("email"),
                Domicilio = dr.GetString("domicilio"),
                Comentarios = dr.GetString("comentarios"),
                Activo = dr.GetValue<bool>("activo")
            };
        }

        protected override IDbDataParameter[] ToUpdateParams(Proveedor model)
        {
            var d = ToParams(model);
            return new IDbDataParameter[] {
                d["@guid"],
                d["@contacto"],
                d["@empresa"],
                d["@telefono"],
                d["@email"],
                d["@domicilio"],
                d["@comentarios"]
            };
        }

        protected override IDbDataParameter[] ToSaveParams(Proveedor model)
        {
            var d = ToParams(model);
            return new IDbDataParameter[] {
                d["@guid"],
                d["@contacto"],
                d["@empresa"],
                d["@telefono"],
                d["@email"],
                d["@domicilio"],
                d["@comentarios"],
                d["@activo"]
            };
        }

        protected override Dictionary<string, IDbDataParameter> ToParams(Proveedor model)
        {
            return new Dictionary<string, IDbDataParameter>() {
                { "@id", "@id".ToParam(DbType.Int64, model.Id) },
                { "@guid", "@guid".ToParam(DbType.Guid, model.Guid) },
                { "@contacto", "@contacto".ToParam(DbType.String, model.Contacto) },
                { "@empresa", "@empresa".ToParam(DbType.String, model.Empresa) },
                { "@telefono", "@telefono".ToParam(DbType.String, model.Telefono) },
                { "@email", "@email".ToParam(DbType.String, model.Email ?? "") },
                { "@domicilio", "@domicilio".ToParam(DbType.String, model.Domicilio ?? "") },
                { "@comentarios", "@comentarios".ToParam(DbType.String, model.Comentarios ?? "") },
                { "@activo", "@activo".ToParam(DbType.Boolean, model.Activo) }
            };
        }
    }
}
