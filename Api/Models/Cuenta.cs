using System;
using Entities;
using System.ComponentModel.DataAnnotations;
using Api.Validators;

namespace Api.Models
{
    public class Cuenta : I2ids
    {
        [Required]
        public Int64 Id { get; set; }
        [Required]
        [NonEmptyGuid]
        public Guid Guid { get; set; }
        
        [StringLength(300)]
        [Required]
        public string Banco { get; set; }
        [StringLength(18)]
        [Required]
        public string Clabe { get; set; }
        [StringLength(18)]
        [Required]
        public string NoCuenta { get; set; }
        [StringLength(300)]
        [Required]
        public string Email { get; set; }
        [StringLength(300)]
        [Required]
        public string Beneficiario { get; set; }
        [StringLength(300)]
        [Required]
        public string Nombre { get; set; }
        [Required]
        public bool Efectivo { get; set; }
        [Required]
        public bool Activo { get; set; }
    }
}
