﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NuGet.DependencyResolver;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Subtitulo
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, ingresar la descripción")]
        [Display(Name = "Descripción")]
        [StringLength(50)]
        public string? Descripcion { get; set; }

        [Column(TypeName = "smalldatetime")]
        [DataType(DataType.Date)]
        public DateTime? FechaRegistro { get; set; }

    }
}