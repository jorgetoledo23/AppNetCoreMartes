using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ItemsCarro
    {
        public int ItemsCarroId { get; set; }

        public int ProductoId { get; set; }

        public Producto Producto { get; set; }

        public string CarroCompraId { get; set; }

        public int Cantidad { get; set; }


    }
}
