using System;
using Entities;
using System.ComponentModel.DataAnnotations;
using Api.Validators;

namespace Api.Models
{
    public class Cliente : I2ids
    {
        [Required]
        public Int64 Id { get; set; }
        [Required]
        [NonEmptyGuid]
        public Guid Guid { get; set; }
        public Guid FacturacionGuid { get; set; }
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
        [Required]
        public string Email { get; set; }
        [StringLength(300)]
        [Required]
        public string Domicilio { get; set; }
        [StringLength(300)]
        [Required]
        public DateTime FechaCreado { get; set; }
        [Required]
        public bool Activo { get; set; }
    }
}
