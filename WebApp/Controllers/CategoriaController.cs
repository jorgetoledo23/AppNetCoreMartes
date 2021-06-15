using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly AppDbContext _context;
        public CategoriaController(AppDbContext context)
        {
            _context = context;
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
        public IActionResult addCategorias(Categoria C) {
            //Guardar la Categoria

            if (ModelState.IsValid)
            {
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
        public IActionResult editCategoria(Categoria C)
        {
            if (ModelState.IsValid)
            {
                var CategoriaEditada = _context.tblCategorias.Where(c => c.CategoriaId.Equals(C.CategoriaId)).FirstOrDefault();
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

        //TODO: Generar Delete (1 Punto para la 3ra Evaluacion!)

    }
}
