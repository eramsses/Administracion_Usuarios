using Administracion_Usuarios.Data;
using Administracion_Usuarios.Filters;
using Administracion_Usuarios.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Administracion_Usuarios.Controllers
{
    public class HomeController : Controller
    {
        private readonly INotyfService _notyf;
        private readonly DataContext _context;

        public HomeController(INotyfService notyf, DataContext context)
        {
            _notyf = notyf;
            _context = context;
        }

        
        public IActionResult Index()
        {

            return View();
        }

        [Autorizacion("categoria_modulo_consultar, operaciones_eliminame")]
        public IActionResult Privacy()
        {
            Console.WriteLine("Ya Entró");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}