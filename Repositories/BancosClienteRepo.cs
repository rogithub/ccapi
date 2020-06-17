using System.Collections.Generic;
using System.Data;
using Entities;
using Ro.Npgsql.Data;

namespace Repositories
{
    public class BancosClienteRepo : SubtableBaseRepo<BancosCliente>, ISubtableBaseRepo<BancosCliente>
    {
        public BancosClienteRepo(IDbAsync db) : base(db)
        {
        }
        protected override string GetByGuidSql =>
        @"SELECT id, guid, banco, clabe, nocuenta, beneficiario, emailnotificacion,
                 nombre, efectivo, activo 
        FROM public.cuentas WHERE guid=@guid AND activo=TRUE;";

        protected override string GetByIdSql =>
        @"SELECT id, guid, banco, clabe, nocuenta, beneficiario, emailnotificacion,
                 nombre, efectivo, activo 
        FROM public.cuentas WHERE id=@id AND activo=TRUE;";
        protected override string SerchSql =>
        @"SELECT 
            id, guid, banco, clabe, nocuenta, beneficiario, emailnotificacion, nombre, efectivo, activo 
            COUNT(*) OVER() as total_rows 
        FROM 
            public.cuentas WHERE guid in 
            (SELECT cuentaid FROM public.clientescuentas WHERE clienteid = @parentId) 
            AND activo=TRUE {0} 
        ORDER BY 
            {1}
        LIMIT @limit OFFSET @offset;";

        protected override string DeleteSql =>
        @"UPDATE public.cuentas SET activo=FALSE WHERE guid=@guid;";

        protected override string UpdateSql =>
        @"UPDATE public.cuentas SET
                banco=@banco,
                clabe=@clabe,
                nocuenta=@nocuenta,
                beneficiario=@beneficiario,
                emailnotificacion=@emailnotificacion,
                nombre=@nombre,
                efectivo=@efectivo
             WHERE guid=@guid;";

        protected override string SaveSql =>
        @"INSERT INTO public.cuentas 
            (id, guid, banco, clabe, nocuenta, beneficiario, emailnotificacion, nombre, efectivo, activo) 
            VALUES 
            (@id, @guid, @banco, @clabe, @nocuenta, @beneficiario, @emailnotificacion, @nombre, @efectivo, @activo);";

        protected override BancosCliente GetData(IDataReader dr)
        {
            return new BancosCliente()
            {
                Id = dr.GetInt("id"),
                Guid = dr.GetGuid("guid"),
                Banco = dr.GetString("banco"),
                Clabe = dr.GetString("clabe"),
                NoCuenta = dr.GetString("nocuenta"),
                Beneficiario = dr.GetString("beneficiario"),
                Email = dr.GetString("emailnotificacion")
            };
        }

        protected override IDbDataParameter[] ToUpdateParams(BancosCliente model)
        {
            var d = ToParams(model);
            return new IDbDataParameter[] {
                d["@guid"],
                d["@banco"],
                d["@clabe"],
                d["@nocuenta"],
                d["@beneficiario"],
                d["@emailnotificacion"],
                d["@nombre"],
                d["@efectivo"]
            };
        }

        protected override IDbDataParameter[] ToSaveParams(BancosCliente model)
        {
            var d = ToParams(model);
            return new IDbDataParameter[] {
                d["@guid"],
                d["@banco"],
                d["@clabe"],
                d["@nocuenta"],
                d["@beneficiario"],
                d["@emailnotificacion"],
                d["@nombre"],
                d["@efectivo"],
                d["@activo"]
            };
        }

        protected override Dictionary<string, IDbDataParameter> ToParams(BancosCliente model)
        {
            return new Dictionary<string, IDbDataParameter>() {
                { "@id", "@id".ToParam(DbType.Int64, model.Id) },
                { "@guid", "@guid".ToParam(DbType.Guid, model.Guid) },
                { "@banco", "@banco".ToParam(DbType.String, model.Banco ?? "") },
                { "@clabe", "@clabe".ToParam(DbType.String, model.Clabe ?? "") },
                { "@nocuenta", "@nocuenta".ToParam(DbType.String, model.NoCuenta ?? "") },
                { "@beneficiario", "@beneficiario".ToParam(DbType.String, model.Beneficiario) },
                { "@emailnotificacion", "@emailnotificacion".ToParam(DbType.String, model.Email ?? "") },
                { "@nombre", "@nombre".ToParam(DbType.String, "") },
                { "@efectivo", "@efectivo".ToParam(DbType.Boolean, false) },
                { "@activo", "@activo".ToParam(DbType.Boolean, true) }
            };
        }
    }
}
