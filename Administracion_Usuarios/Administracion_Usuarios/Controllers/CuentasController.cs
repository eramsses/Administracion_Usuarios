using Microsoft.AspNetCore.Mvc;

namespace Administracion_Usuarios.Controllers
{
    public class CuentasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
