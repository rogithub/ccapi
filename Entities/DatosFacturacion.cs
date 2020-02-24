using System;

namespace Entities
{
    public class DatosFacturacion : I2ids
    {
        public Int64 Id { get; set; }
        public Guid Guid { get; set; }
        public string Nombre { get; set; }
        public string Calle { get; set; }
        public string NoExterior { get; set; }
        public string NoInterior { get; set; }
        public string Colonia { get; set; }
        public string Ciudad { get; set; }
        public string Estado { get; set; }
        public string Cp { get; set; }
        public string Rfc { get; set; }
        public string Email { get; set; }
        
    }
}
