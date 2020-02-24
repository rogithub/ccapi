using System;

namespace Entities
{
    public class Cuenta : I2ids
    {
        public Int64 Id { get; set; }
        public Guid Guid { get; set; }
        
        public string Banco { get; set; }
        public string Clabe { get; set; }
        public string NoCuenta { get; set; }
        public string Beneficiario { get; set; }
        public string Email { get; set; }
        public string Nombre { get; set; }
        public bool   Efectivo { get; set; }
        public bool Activo { get; set; }
    }
}
