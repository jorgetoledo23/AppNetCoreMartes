using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class Producto
    {
        public int ProductoId { get; set; }

        public string Nombre { get; set; }

        public int Precio { get; set; }

        [Display(Name = "Precio Oferta")]
        public int PrecioOferta { get; set; }

        [Display(Name = "En Oferta")]
        public Boolean EnOferta { get; set; }

        public int Stock { get; set; }

        public string Imagen { get; set; }

        [NotMapped]
        [Display(Name = "Imagen")]
        public IFormFile ImagenFile { get; set; }

        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }

        public Categoria Categoria { get; set; }

    }
}
