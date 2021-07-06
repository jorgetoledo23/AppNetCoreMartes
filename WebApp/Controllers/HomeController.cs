using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index(int Pagina)
        {
            CategoriaHomeViewModel categoriaHomeViewModel = new CategoriaHomeViewModel();
            if (Pagina == 0)
            {
                categoriaHomeViewModel.Pagina = 1;
            }
            else {
                categoriaHomeViewModel.Pagina = Pagina;
            }

            int muestra = 3;
            int cantidad = _context.tblCategorias.ToList().Count / 3; // 10 / 3
            if (cantidad % muestra == 0)
            {
                categoriaHomeViewModel.CantidadPaginas = cantidad;
            }
            else {
                categoriaHomeViewModel.CantidadPaginas = cantidad + 1;
            }

            categoriaHomeViewModel.listaCategorias = _context
                .tblCategorias.Skip((categoriaHomeViewModel.Pagina - 1) * muestra)
                .Take(muestra).ToList();

            return View(categoriaHomeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
