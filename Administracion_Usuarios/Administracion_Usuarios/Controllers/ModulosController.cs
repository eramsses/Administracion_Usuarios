using Administracion_Usuarios.Data;
using Administracion_Usuarios.Data.Entities;
using Administracion_Usuarios.Helpers;
using Administracion_Usuarios.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Administracion_Usuarios.Controllers
{
    public class ModulosController : Controller
    {
        private readonly DataContext _context;
        private readonly ICombosHelper _combosHelper;
        private readonly INotyfService _notyf;

        public ModulosController(DataContext context, ICombosHelper combosHelper, INotyfService notyf)
        {
            _context = context;
            _combosHelper = combosHelper;
            _notyf = notyf;
        }


        public async Task<IActionResult> Index()
        {
            return View(await _context.Modulos.ToListAsync());
        }

        public async Task<IActionResult> Agregar()
        {
            AgregarModuloViewModel model = new()
            {
                ModuloCategorias = await _combosHelper.GetComboCategoriaModulosAsync(),
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(AgregarModuloViewModel model)
        {
            if (ModelState.IsValid)
            {
                ModuloCategoria moduloCategoria = await _context.ModuloCategoria.FindAsync(model.ModuloCategoriaId);
                if (moduloCategoria == null)
                {
                    return NotFound();
                }

                string nombre = model.Nombre;
                try
                {
                    Modulo modulo = new()
                    {
                        Operaciones = new List<Operacion>(),
                        ModuloCategoria = await _context.ModuloCategoria.FindAsync(model.ModuloCategoriaId),
                        Descripcion = model.Descripcion,
                        Nombre = model.Nombre
                    };

                    _context.Add(modulo);
                    await _context.SaveChangesAsync();
                    
                    _notyf.Success($"El módulo <b>{nombre}</b> fue creado exitosamente.", 3);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    string error = dbUpdateException.InnerException.Message;
                    if (error.Contains("duplicate") || error.Contains("clave duplicada"))
                    {
                        _notyf.Error($"Ya existe módulo con el nombre {nombre} ", 6);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                        _notyf.Error(dbUpdateException.InnerException.Message, 6);
                    }
                }
                catch (Exception ex)
                {
                    _notyf?.Error(ex.Message, 6);
                }
            }

            model.ModuloCategorias = await _combosHelper.GetComboCategoriaModulosAsync();
            
            return View(model);





        }
    }
}
