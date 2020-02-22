using System;
using System.ComponentModel.DataAnnotations;
using Api.Validators;
using Entities;

namespace Api.Models
{
    public class Material : I2ids
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
        public string Color { get; set; }
        [Required]
        [StringLength(300)]
        public string Unidad { get; set; }
        [StringLength(300)]
        public string Marca { get; set; }
        [StringLength(300)]
        public string Modelo { get; set; }
        [StringLength(500)]
        public string Comentarios { get; set; }
        [Required]
        public bool Activo { get; set; }
    }
}
