using System;
using System.ComponentModel.DataAnnotations;
using Api.Validators;
using Entities;

namespace Api.Models
{
    public class DatosFacturacion : I2ids
    {
        [Required]
        public Int64 Id { get; set; }
        [Required]
        [NonEmptyGuid]
        public Guid Guid { get; set; }
        [Required]
        [StringLength(300)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(300)]
        public string Calle { get; set; }
        [Required]
        [StringLength(100)]
        public string NoExterior { get; set; }

        [StringLength(100)]
        public string NoInterior { get; set; }
        [Required]
        [StringLength(300)]
        public string Colonia { get; set; }
        [Required]
        [StringLength(300)]
        public string Ciudad { get; set; }
        [Required]
        [StringLength(50)]
        public string Estado { get; set; }
        [Required]
        [StringLength(10)]
        public string Cp { get; set; }
        [Required]
        [StringLength(20)]
        public string Rfc { get; set; }
        [Required]
        [StringLength(300)]
        public string Email { get; set; }

    }
}
