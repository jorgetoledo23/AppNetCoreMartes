using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class CategoriaHomeViewModel
    {
        public List<Categoria> listaCategorias { get; set; }
        public int Pagina { get; set; }
        public int CantidadPaginas { get; set; }
    }
}
