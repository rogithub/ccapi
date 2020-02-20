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

        private Func<IDataReader, Cliente> _getData = (dr) =>
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
            };

        public IObservable<Cliente> Get(Guid id)
        {
            var param = "@guid".ToParam(DbType.Guid, id);

            var cmd = @"SELECT id, guid, facturacionid, contacto, empresa, 
            telefono, email, domicilio, fechacreado, activo 
            FROM public.clientes WHERE guid=@guid;".ToCmd(CommandType.Text, param);            

            return Db.ExecuteDataReader(cmd, _getData);
        }


        public IObservable<Cliente> GetAll()
        {
            var cmd = @"SELECT id, guid, facturacionid, contacto, empresa, 
            telefono, email, domicilio, fechacreado, activo 
            FROM public.clientes;".ToCmd();            

            return Db.ExecuteDataReader(cmd, _getData);
        }

        public IObservable<int> Delete(Guid id)
		{
            var param = "@guid".ToParam(DbType.Guid, id);

			var cmd = @"UPDATE public.clientes SET activo=@activo WHERE guid=@guid;".ToCmd(CommandType.Text, param); 

			return Db.ExecuteNonQuery(cmd);
		}

        public IObservable<int> Update(Cliente cliente)
		{
            var parameters = new IDbDataParameter[] {
                "@guid".ToParam(DbType.Guid, cliente.Guid),
                "@contacto".ToParam(DbType.String, cliente.Contacto),
                "@empresa".ToParam(DbType.String, cliente.Empresa),
                "@telefono".ToParam(DbType.String, cliente.Telefono),
                "@domicilio".ToParam(DbType.String, cliente.Domicilio),
            };
			var cmd = 
            @"UPDATE public.clientes SET 
                contacto=@contacto,
                empresa=@empresa,
                telefono=@telefono,
                domicilio=@domicilio
             WHERE guid=@guid;".ToCmd(CommandType.Text, parameters); 

			return Db.ExecuteNonQuery(cmd);
		}

        public IObservable<int> Save(Cliente cliente)
		{
			var parameters = new IDbDataParameter[] {
                "@guid".ToParam(DbType.Guid, cliente.Guid),
                "@facturacionid".ToParam(DbType.Guid, cliente.FacturacionGuid),
                "@contacto".ToParam(DbType.String, cliente.Contacto),
                "@empresa".ToParam(DbType.String, cliente.Empresa),
                "@telefono".ToParam(DbType.String, cliente.Telefono),
                "@domicilio".ToParam(DbType.String, cliente.Domicilio),
                "@fechacreado".ToParam(DbType.DateTime, cliente.FechaCreado),
                "@activo".ToParam(DbType.Boolean, cliente.Activo)
            };
			var cmd = 
            @"INSERT INTO public.clientes 
            (guid, facturacionid, contacto, empresa, telefono, email, domicilio, fechacreado, activo) 
            VALUES 
            (@guid, @facturacionid, @contacto, @empresa, @telefono, @email, @domicilio, @fechacreado, @activo);".ToCmd(CommandType.Text, parameters); 

			return Db.ExecuteNonQuery(cmd);
		}
    }
}
