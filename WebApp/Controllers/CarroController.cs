using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CarroController : Controller
    {

        private readonly AppDbContext _context;
        private readonly CarroCompra _carro;

        public CarroController(AppDbContext context, CarroCompra carro)
        {
            _context = context;
            _carro = carro;
        }


        public IActionResult Index()
        {

            CarroCompraViewModel Cvm = new CarroCompraViewModel
            {
                ItemsCarro = _carro.GetItemsCarro(),
                Total = _carro.GetTotalCarro()
            };

            return View(Cvm);
        }

        public RedirectToActionResult Add(int ProductoId) {
            var P = _context.tblProductos.Where(p => p.ProductoId == ProductoId)
                .FirstOrDefault();
            _carro.AddItem(P);
            return RedirectToAction(nameof(Index)); 
        }

        public RedirectToActionResult Del(int ProductoId)
        {
            var P = _context.tblProductos.Where(p => p.ProductoId == ProductoId)
                .FirstOrDefault();
            _carro.DelItem(P);
            return RedirectToAction(nameof(Index));
        }
    }
}
