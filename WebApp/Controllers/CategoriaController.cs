using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnviroment;
        public CategoriaController(AppDbContext context, IWebHostEnvironment hostEnviroment)
        {
            _context = context;
            _hostEnviroment = hostEnviroment;
        }

        [HttpGet]
        public IActionResult HomeCategorias()
        {
            //List<Categoria> listaCategorias = _context.tblCategorias.ToList();
            
            
            return View(_context.tblCategorias.ToList());
        }

        public IActionResult addCategorias() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> addCategorias(Categoria C) {
            //Guardar la Categoria

            if (ModelState.IsValid)
            {

                if (C.archivoImagen == null)
                {
                    //Desde el form NO se cargo ninguna imagen
                    C.Imagen = "no-disponible.png";
                }
                else {

                    string wwwRootPath = _hostEnviroment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(C.archivoImagen.FileName); // no-disponible
                    string extension = Path.GetExtension(C.archivoImagen.FileName); // .png
                    C.Imagen = fileName + DateTime.Now.ToString("ddMMyyyyHHmmss") + extension; // no-disponible29062021173630.png
                    string path = Path.Combine(wwwRootPath + "/images/" + C.Imagen);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await C.archivoImagen.CopyToAsync(fileStream);
                    }

                }
           
                _context.Add(C);
                _context.SaveChanges();
                //return RedirectToAction("HomeCategorias");
                return RedirectToAction(nameof(HomeCategorias));
            }
            else
            {
                return View(C);
            }
        }


        public IActionResult editCategoria(int CategoriaId) {
            var C = _context.tblCategorias.Where(c => c.CategoriaId.Equals(CategoriaId)).FirstOrDefault();
            if (C != null)
            {
                return View(C);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> editCategoria(Categoria C)
        {
            if (ModelState.IsValid)
            {
                var CategoriaEditada = _context.tblCategorias.Where(c => c.CategoriaId.Equals(C.CategoriaId)).FirstOrDefault();

                if (C.archivoImagen == null)
                {
                    //Desde el form NO se cargo ninguna imagen
                    //CategoriaEditada.Imagen = "no-disponible.png";
                }
                else
                {
                    string wwwRootPath = _hostEnviroment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(C.archivoImagen.FileName); // no-disponible
                    string extension = Path.GetExtension(C.archivoImagen.FileName); // .png
                    CategoriaEditada.Imagen = fileName + DateTime.Now.ToString("ddMMyyyyHHmmss") + extension; // no-disponible29062021173630.png
                    string path = Path.Combine(wwwRootPath + "/images/" + CategoriaEditada.Imagen);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await C.archivoImagen.CopyToAsync(fileStream);
                    }
                }
                CategoriaEditada.Nombre = C.Nombre;
                CategoriaEditada.Descripcion = C.Descripcion;
                _context.SaveChanges();
                return RedirectToAction(nameof(HomeCategorias));
            }
            else
            {
                return View(C);
            } 
        }

        public IActionResult deleteCategoria(int CategoriaId)
        {
            var C = _context.tblCategorias.Where(c => c.CategoriaId.Equals(CategoriaId)).FirstOrDefault();
            if (C != null)
            {
                return View(C);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult deleteCategoria(Categoria C)
        {
            if (ModelState.IsValid)
            {
                var CategoriaEliminada = _context.tblCategorias.Where(c => c.CategoriaId.Equals(C.CategoriaId)).FirstOrDefault();
                _context.Attach(CategoriaEliminada).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                _context.SaveChanges();
                return RedirectToAction(nameof(HomeCategorias));
            }
            else
            {
                return View(C);
            }
        }



        //TODO: Generar Delete (1 Punto para la 3ra Evaluacion!)

    }
}
