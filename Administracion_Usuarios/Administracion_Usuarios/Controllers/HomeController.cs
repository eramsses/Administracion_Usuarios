using Administracion_Usuarios.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Administracion_Usuarios.Controllers
{
    public class HomeController : Controller
    {
        private readonly INotyfService _notyf;

        public HomeController(INotyfService notyf)
        {
            _notyf = notyf;
        }

        public IActionResult Index()
        {
            _notyf.Error("Mensaje de error", 3);
            _notyf.Success("Notificación satisfactoria");
            return View();
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