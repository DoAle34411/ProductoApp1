﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductoApp1.Models
{
    public class User
    {
        [Required]
        public int IdUsuario { get; set; }
        [Required]
        public string Cedula { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [Required]
        public string Clave { get; set; }
        [Required]
        [Range(1, 4)]
        public int CodigoAcceso { get; set; } // 1 para admin, 2 para usuario trabajador habilitado, 3 usuario trabajador no habilitado y 4 usuario externo
        [Required]
        public bool HaRetirado { get; set; }
        [ForeignKey("Producto")]
        public int IdLibroRetirado { get; set; }

        public int IdUsuarioActivo { get; set; }
    }
}
