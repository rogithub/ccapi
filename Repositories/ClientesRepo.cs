using System;
using System.Collections.Generic;
using System.Data;
using Entities;
using ReactiveDb;

namespace Repositories
{
    public class ClientesRepo : BaseRepo<Cliente>, IBaseRepo<Cliente>
    {
        public ClientesRepo(IDatabase db) : base(db)
        {

        }

        protected override string GetByGuidSql =>
        @"SELECT id, guid, facturacionid, contacto, empresa, 
                 telefono, email, domicilio, fechacreado, activo 
        FROM public.clientes WHERE guid=@guid AND activo=TRUE;";

        protected override string GetByIdSql =>
        @"SELECT id, guid, facturacionid, contacto, empresa, 
                 telefono, email, domicilio, fechacreado, activo 
        FROM public.clientes WHERE id=@id AND activo=TRUE;";
        protected override string SerchSql =>
        @"SELECT 
            id, guid, facturacionid, contacto, empresa, telefono, email, domicilio, fechacreado, activo, 
            COUNT(*) OVER() as total_rows 
        FROM 
            public.clientes WHERE activo=TRUE {0} 
        ORDER BY 
            {1}
        LIMIT @limit OFFSET @offset;";

        protected override string DeleteSql =>
        @"UPDATE public.clientes SET activo=FALSE WHERE guid=@guid;";

        protected override string UpdateSql =>
        @"UPDATE public.clientes SET
                contacto=@contacto,
                empresa=@empresa,
                telefono=@telefono,
                email=@email,
                domicilio=@domicilio,
             WHERE guid=@guid;";

        protected override string SaveSql =>
        @"INSERT INTO public.clientes 
            (guid, facturacionid, contacto, empresa, telefono, email, domicilio, fechacreado, activo) 
            VALUES 
            (@guid, @facturacionid, @contacto, @empresa, @telefono, @email, @domicilio, @fechacreado, @activo);";

        protected override Cliente GetData(IDataReader dr)
        {
            return new Cliente()
            {
                Id = dr.GetInt("id"),
                Guid = dr.GetGuid("guid"),
                FacturacionGuid = dr.GetGuid("facturacionid"),
                Contacto = dr.GetString("contacto"),
                Empresa = dr.GetString("empresa"),
                Telefono = dr.GetString("telefono"),
                Email = dr.GetString("email"),
                Domicilio = dr.GetString("domicilio"),
                FechaCreado = dr.GetDate("fechacreado"),
                Activo = dr.GetValue<bool>("activo"),

            };
        }

        protected override IDbDataParameter[] ToUpdateParams(Cliente model)
        {
            var d = ToParams(model);
            return new IDbDataParameter[] {
                d["@contacto"],
                d["@empresa"],
                d["@telefono"],
                d["@email"],
                d["@domicilio"]
            };
        }

        protected override IDbDataParameter[] ToSaveParams(Cliente model)
        {
            var d = ToParams(model);
            return new IDbDataParameter[] {
                d["@guid"],
                d["@facturacionid"],
                d["@contacto"],
                d["@empresa"],
                d["@telefono"],
                d["@email"],
                d["@domicilio"],
                d["@fechacreado"],
                d["@activo"]
            };
        }

        protected override Dictionary<string, IDbDataParameter> ToParams(Cliente model)
        {
            return new Dictionary<string, IDbDataParameter>() {
                { "@id", "@id".ToParam(DbType.Int64, model.Id) },
                { "@guid", "@guid".ToParam(DbType.Guid, model.Guid) },
                { "@facturacionid", "@facturacionid".ToParam(DbType.Guid, model.FacturacionGuid) },
                { "@contacto", "@contacto".ToParam(DbType.String, model.Contacto) },
                { "@empresa", "@empresa".ToParam(DbType.String, model.Empresa) },
                { "@telefono", "@telefono".ToParam(DbType.String, model.Telefono) },
                { "@email", "@email".ToParam(DbType.String, model.Email ?? "") },
                { "@domicilio", "@domicilio".ToParam(DbType.String, model.Domicilio ?? "") },
                { "@fechacreado", "@fechacreado".ToParam(DbType.DateTime, model.FechaCreado) },
                { "@activo", "@activo".ToParam(DbType.Boolean, model.Activo) }
            };
        }
    }
}
