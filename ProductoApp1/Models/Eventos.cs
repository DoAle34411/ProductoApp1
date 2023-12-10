using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductoApp1.Models
{
    public class Eventos
    {
        [Required]
        public int idEvento { get; set; }
        [Required]
        public string NombreEvento { get; set; }
        [Required]
        public string DescripcionEvento { get; set; }
        [Required]
        public DateTime diaEvento { get; set; }
        public TimeSpan horaEvento { get; set; }
        public string Expositores { get; set; }
        public int IdUsuario { get; set; }
    }
}
