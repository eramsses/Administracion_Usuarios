using Administracion_Usuarios.Data;
using Administracion_Usuarios.Data.Entities;
using Administracion_Usuarios.Helpers;
using Administracion_Usuarios.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Administracion_Usuarios.Controllers
{
    public class OperacionesController : Controller
    {
        private readonly DataContext _context;
        private readonly INotyfService _notyf;
        private readonly ICombosHelper _combosHelper;

        public OperacionesController(DataContext context, INotyfService notyf, ICombosHelper combosHelper)
        {
            _context = context;
            _notyf = notyf;
            _combosHelper = combosHelper;
        }
        public async Task<IActionResult> Index()
        {
            List<Operacion> operaciones = await _context.Operaciones
                .Include(m => m.Modulo)
                .ToListAsync();
            return View(operaciones);
        }

        public async Task<IActionResult> Agregar()
        {
            AgregarOperacionViewModel model = new()
            {
                ModuloCategorias = await _combosHelper.GetComboCategoriaModulosAsync(),
                Modulos  = await _combosHelper.GetComboModulosAsync()
        };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(AgregarOperacionViewModel model)
        {
            if (ModelState.IsValid)
            {
                Modulo modulo = await _context.Modulos.FindAsync(model.ModuloId);
                if (modulo == null)
                {
                    return NotFound();
                }

                string nombre = model.Nombre;
                try
                {
                    Operacion operacion = new()
                    {
                        Modulo = await _context.Modulos.FindAsync(model.ModuloId),
                        Descripcion = model.Descripcion,
                        Nombre = model.Nombre
                    };

                    _context.Add(operacion);
                    await _context.SaveChangesAsync();

                    _notyf.Success($"La operación <b>{nombre}</b> fue creada exitosamente.", 3);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    string error = dbUpdateException.InnerException.Message;
                    if (error.Contains("duplicate") || error.Contains("clave duplicada"))
                    {
                        _notyf.Error($"Ya existe una operación con el nombre {nombre} ", 6);
                    }
                    else
                    {
                        _notyf.Error(dbUpdateException.InnerException.Message, 6);
                    }
                }
                catch (Exception ex)
                {
                    _notyf?.Error(ex.InnerException.Message, 6);
                }
            }

            model.ModuloCategorias = await _combosHelper.GetComboCategoriaModulosAsync();
            model.Modulos = await _combosHelper.GetComboModulosAsync();

            return View(model);

        }

        public JsonResult GetModulos(int ModuloCategoriaId)
        {
            ModuloCategoria categoria = _context.ModuloCategoria
                .Include(c => c.Modulos)
                .FirstOrDefault(c => c.Id == ModuloCategoriaId);
            if (categoria == null)
            {
                return null;
            }

            return Json(categoria.Modulos.OrderBy(d => d.Nombre));
        }
    }
}
