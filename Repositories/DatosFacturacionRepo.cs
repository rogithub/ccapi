using System;
using System.Collections.Generic;
using System.Data;
using Entities;
using Ro.Npgsql.Data;

namespace Repositories
{

    public class DatosFacturacionRepo : BaseRepo<DatosFacturacion>, IBaseRepo<DatosFacturacion>
    {
        public DatosFacturacionRepo(IDbAsync db) : base(db)
        {

        }

        protected override string GetByIdSql =>
        @"SELECT id, guid, nombre, calle, noexterior, nointerior,
            colonia, ciudad, estado, cp, rfc, email 
        FROM 
            public.datosfacturacion WHERE id=@id;";
        protected override string GetByGuidSql =>
        @"SELECT id, guid, nombre, calle, noexterior, nointerior,  
            colonia, ciudad, estado, cp, rfc, email 
        FROM public.datosfacturacion WHERE guid=@guid;";

        protected override string SerchSql =>
        @"SELECT 
            id, guid, nombre, calle, noexterior, nointerior, colonia, ciudad, estado, cp, rfc, email
            COUNT(*) OVER() as total_rows 
        FROM 
            public.datosfacturacion WHERE 1=1 {0} 
        ORDER BY 
            {1}
        LIMIT @limit OFFSET @offset;";

        protected override string DeleteSql =>
        @"DELETE FROM public.datosfacturacion WHERE guid=@guid;";

        protected override string UpdateSql =>
        @"UPDATE public.datosfacturacion SET 
                nombre=@nombre,
                calle=@calle,
                noexterior=@noexterior,
                nointerior=@nointerior,
                colonia=@colonia,
                ciudad=@ciudad,
                estado=@estado,
                cp=@cp,
                rfc=@rfc,
                email=@email
             WHERE guid=@guid;";

        protected override string SaveSql =>
        @"INSERT INTO public.datosfacturacion
            (guid, nombre, calle, noexterior, nointerior, colonia, ciudad, estado, cp, rfc, email)
            VALUES 
            (@guid, @nombre, @calle, @noexterior, @nointerior, @colonia, @ciudad, @estado, @cp, @rfc, @email) ;";

        protected override DatosFacturacion GetData(IDataReader dr)
        {
            return new DatosFacturacion()
            {
                Id = dr.GetInt("id"),
                Guid = dr.GetGuid("guid"),
                Nombre = dr.GetString("nombre"),
                Calle = dr.GetString("calle"),
                NoExterior = dr.GetString("noexterior"),
                NoInterior = dr.GetString("nointerior"),
                Colonia = dr.GetString("colonia"),
                Ciudad = dr.GetString("ciudad"),
                Estado = dr.GetString("estado"),
                Cp = dr.GetString("cp"),
                Rfc = dr.GetString("rfc"),
                Email = dr.GetString("email"),
            };
        }

        protected override Dictionary<string, IDbDataParameter> ToParams(DatosFacturacion model)
        {
            return new Dictionary<string, IDbDataParameter>() {
                { "@id", "@id".ToParam(DbType.Int64, model.Id) },
                { "@guid", "@guid".ToParam(DbType.Guid, model.Guid) },
                { "@nombre", "@nombre".ToParam(DbType.String, model.Nombre) },
                { "@calle", "@calle".ToParam(DbType.String, model.Calle) },
                { "@noexterior", "@noexterior".ToParam(DbType.String, model.NoExterior) },
                { "@nointerior", "@nointerior".ToParam(DbType.String, model.NoInterior ?? "") },
                { "@colonia", "@colonia".ToParam(DbType.String, model.Colonia) },
                { "@ciudad", "@ciudad".ToParam(DbType.String, model.Ciudad) },
                { "@estado", "@estado".ToParam(DbType.String, model.Estado) },
                { "@cp", "@cp".ToParam(DbType.String, model.Cp) },
                { "@rfc", "@rfc".ToParam(DbType.String, model.Rfc) },
                { "@email", "@email".ToParam(DbType.String, model.Email) }
            };
        }

        protected override IDbDataParameter[] ToSaveParams(DatosFacturacion model)
        {
            var d = ToParams(model);
            return new IDbDataParameter[] {
                d["@guid"],
                d["@nombre"],
                d["@calle"],
                d["@noexterior"],
                d["@nointerior"],
                d["@colonia"],
                d["@ciudad"],
                d["@estado"],
                d["@cp"],
                d["@rfc"],
                d["@email"]
            };
        }

        protected override IDbDataParameter[] ToUpdateParams(DatosFacturacion model)
        {
            var d = ToParams(model);
            return new IDbDataParameter[] {
                d["@guid"],
                d["@nombre"],
                d["@calle"],
                d["@noexterior"],
                d["@nointerior"],
                d["@colonia"],
                d["@ciudad"],
                d["@estado"],
                d["@cp"],
                d["@rfc"],
                d["@email"]
            };
        }
    }
}
