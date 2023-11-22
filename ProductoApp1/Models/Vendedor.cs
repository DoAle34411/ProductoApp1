using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductoApp1.Models
{
    public class Vendedor
    {
        [Key]
        public int Cedula { get; set; }
        [Required]
        public string Nombres { get; set; }
        [Required]
        public string Apellidos { get; set; }
        [Required]
        public int CantidadVentas
        {
            get;
            set;
        }
        [Required]
        public string EsActivo { get; set; }
    }
}
