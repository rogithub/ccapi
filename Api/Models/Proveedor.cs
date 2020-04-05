using System;
using Entities;
using System.ComponentModel.DataAnnotations;
using Api.Validators;

namespace Api.Models
{
    public class Proveedor : I2ids
    {
        [Required]
        public Int64 Id { get; set; }
        [Required]
        [NonEmptyGuid]
        public Guid Guid { get; set; }


        [StringLength(300)]
        [Required]
        public string Contacto { get; set; }
        [StringLength(300)]
        [Required]
        public string Empresa { get; set; }
        [StringLength(300)]
        [Required]
        public string Telefono { get; set; }
        [StringLength(300)]
        public string Email { get; set; }
        [StringLength(500)]
        public string Domicilio { get; set; }
        [StringLength(500)]
        public string Comentarios { get; set; }
        [Required]
        public bool Activo { get; set; }
    }
}
