using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ProductoController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnviroment;
        public ProductoController(AppDbContext context, IWebHostEnvironment hostEnviroment)
        {
            _context = context;
            _hostEnviroment = hostEnviroment;
        }

        public IActionResult HomeProductos(int Pagina)
        {
            ProductoHomeViewModel Pvm = new ProductoHomeViewModel();

            if (Pagina == 0)
            {
                Pvm.Pagina = 1;
            }
            else
            {
                Pvm.Pagina = Pagina;
            }

            int muestra = 8;
            int cantidad = _context.tblProductos.ToList().Count / muestra; // 10 / 3
            if (_context.tblProductos.ToList().Count % muestra == 0)
            {
                Pvm.CantidadPaginas = cantidad;
            }
            else
            {
                Pvm.CantidadPaginas = cantidad + 1;
            }

            Pvm.listaProductos = _context
                .tblProductos.Skip((Pvm.Pagina - 1) * muestra)
                .Take(muestra).ToList();

            TempData["PaginaSiguiente"] = Pagina + 1;
            TempData["PaginaAnterior"] = Pagina - 1;
            return View(Pvm);
        }


        public IActionResult AddProducto() {
            ViewData["CategoriaId"] = new SelectList(_context.tblCategorias.ToList(), "CategoriaId", "Nombre");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProducto(Producto P) {

            if (ModelState.IsValid)
            {

                if (P.ImagenFile == null)
                {
                    //Desde el form NO se cargo ninguna imagen
                    P.Imagen = "no-disponible.png";
                }
                else
                {

                    string wwwRootPath = _hostEnviroment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(P.ImagenFile.FileName); // no-disponible
                    string extension = Path.GetExtension(P.ImagenFile.FileName); // .png
                    P.Imagen = fileName + DateTime.Now.ToString("ddMMyyyyHHmmss") + extension; // no-disponible29062021173630.png
                    string path = Path.Combine(wwwRootPath + "/images/" + P.Imagen);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await P.ImagenFile.CopyToAsync(fileStream);
                    }

                }

                _context.Add(P);
                _context.SaveChanges();
                //return RedirectToAction("HomeCategorias");
                return RedirectToAction(nameof(HomeProductos));
            }
            else
            {
                return View(P);
            }


        }
    }
}
