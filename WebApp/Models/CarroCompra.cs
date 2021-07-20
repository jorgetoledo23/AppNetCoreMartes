using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class CarroCompra
    {

        private readonly AppDbContext _context;

        public List<ItemsCarro> ItemsCarro { get; set; }

        public string CarroCompraId { get; set; }

        public CarroCompra(AppDbContext context)
        {
            _context = context;
        }

        public static CarroCompra GetCarro(IServiceProvider services) {
            var context = services.GetRequiredService<AppDbContext>();
            ISession session = services.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
            //session.SetString("CarroId", "123");
            string CarroId = session.GetString("CarroId") ?? Guid.NewGuid().ToString();
            session.SetString("CarroId", CarroId);
            return new CarroCompra(context) { CarroCompraId = CarroId };
        }


        public void AddItem(Producto P) {
            var Item = _context.tblItemsCarro
                .Where(i => i.ProductoId == P.ProductoId && i.CarroCompraId == this.CarroCompraId)
                .FirstOrDefault();

            if (Item == null)
            {
                Item = new ItemsCarro
                {
                    CarroCompraId = this.CarroCompraId,
                    ProductoId = P.ProductoId,
                    Cantidad = 1
                };
                _context.Add(Item);
            }
            else {
                Item.Cantidad++;
            }

            _context.SaveChanges();
        }


        public void DelItem(Producto P)
        {
            var Item = _context.tblItemsCarro
                .Where(i => i.ProductoId == P.ProductoId && i.CarroCompraId == this.CarroCompraId)
                .FirstOrDefault();

            if (Item != null)
            {
                _context.Remove(Item);
            }
            _context.SaveChanges();
        }


        public List<ItemsCarro> GetItemsCarro() {
            return _context.tblItemsCarro.Where(i => i.CarroCompraId == this.CarroCompraId)
                .Include(i => i.Producto)
                .ThenInclude(p => p.Categoria)
                .ToList();
        }


        public void VaciarCarro() { 
            var Items = _context.tblItemsCarro.Where(i => i.CarroCompraId == this.CarroCompraId)
                .ToList();
            _context.RemoveRange(Items);
            _context.SaveChanges();
        }

        public int GetTotalCarro() {
            return _context.tblItemsCarro.Where(i => i.CarroCompraId == this.CarroCompraId)
                .Select(i => i.Producto.Precio * i.Cantidad).Sum();
        }

    }
}
