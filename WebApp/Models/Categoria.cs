using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Categoria
    {
        public int CategoriaId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Nombre { get; set; }

        public string Descripcion { get; set; }
    }
}
