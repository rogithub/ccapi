using System;

namespace Entities
{
    public class Proveedor : I2ids
    {
        public Int64 Id { get; set; }
        public Guid Guid { get; set; }
        public string Contacto { get; set; }
        public string Empresa { get; set; }
        public string Telefono { get; set; }
        public string Email { get; set; }
        public string Domicilio { get; set; }
        public string Comentarios { get; set; }
        public bool Activo { get; set; }
    }
}
