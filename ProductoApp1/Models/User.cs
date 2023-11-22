using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public string Clave { get; set; }
    }
}
