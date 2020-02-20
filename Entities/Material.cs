using System;

namespace Entities
{
    public class Material
    {
        public Int64 Id { get; set; }
        public Guid Guid { get; set; }
        public string Nombre { get; set; }
        public string Color { get; set; }
        public string Unidad { get; set; }
        public string Marca { get; set; }
        public string Model { get; set; }
        public string Comentarios { get; set; }
        public bool Activo { get; set; }
    }
}
