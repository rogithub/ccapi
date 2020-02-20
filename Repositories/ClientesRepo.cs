using System;
using System.Data;
using Entities;
using ReactiveDb;

namespace Repositories
{
    public class ClientesRepo : IClientesRepo
    {
        private IDatabase Db { get; set; }
        public ClientesRepo(IDatabase db)
        {
            this.Db = db;
        }

        public IObservable<Cliente> Get(long id)
        {
            var param = "@id".ToParam(DbType.Int64, id);

            var cmd = @"SELECT id, guid, facturacionid, contacto, empresa, 
            telefono, email, domicilio, fechacreado, activo 
            FROM public.clientes WHERE id=@id;".ToCmd(CommandType.Text, param);

            Func<IDataReader, Cliente> getData = (dr) =>
            {
                return new Cliente()
                {
                    Id = id,
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
            };

            return Db.ExecuteDataReader(cmd, getData);
        }
    }
}
