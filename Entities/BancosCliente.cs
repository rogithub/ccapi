using System;

namespace Entities
{
    public class BancosCliente : I2ids
    {
        public Int64 Id { get; set; }
        public Guid Guid { get; set; }
        public string Beneficiario { get; set; }
        public string Banco { get; set; }
        public string Clabe { get; set; }
        public string NoCuenta { get; set; }
        public string Email { get; set; }
    }
}
