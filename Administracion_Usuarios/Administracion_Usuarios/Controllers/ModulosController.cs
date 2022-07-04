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
            return View(await _context.Modulos
                .Include(mc => mc.ModuloCategoria)
                .Include(op => op.Operaciones)
                .ToListAsync());
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
                        _notyf.Error($"Ya existe módulo con el nombre {nombre} ");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, dbUpdateException.InnerException.Message);
                        _notyf.Error(dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    _notyf?.Error(ex.InnerException.Message);
                }
            }

            model.ModuloCategorias = await _combosHelper.GetComboCategoriaModulosAsync();

            return View(model);

        }


        public async Task<IActionResult> Modificar(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Modulo modulo = await _context.Modulos
                .Include(mc => mc.ModuloCategoria)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (modulo == null)
            {
                return NotFound();
            }

            EditModuloViewModel model = new()
            {
                Id = modulo.Id,
                Nombre = modulo.Nombre,
                Descripcion = modulo.Descripcion,
                ModuloCategoriaId = modulo.ModuloCategoria.Id,
                ModuloCategorias = await _combosHelper.GetComboCategoriaModulosAsync(),
            };



            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modificar(int id, EditModuloViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string nombre = model.Nombre;
                try
                {
                    Modulo modulo = new()
                    {
                        Id = id,
                        ModuloCategoria = await _context.ModuloCategoria.FindAsync(model.ModuloCategoriaId),
                        Descripcion = model.Descripcion,
                        Nombre = model.Nombre
                    };

                    _context.Update(modulo);
                    await _context.SaveChangesAsync();

                    _notyf.Success($"El módulo <b>{nombre}</b> fue modificado exitosamente.", 3);
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
                        _notyf.Error(dbUpdateException.InnerException.Message, 4);
                    }
                }
                catch (Exception ex)
                {
                    _notyf.Error(ex.InnerException.Message, 4);
                }

            }

            //model.ModuloCategoriaId = model.ModuloCategoriaId;
            model.ModuloCategorias = await _combosHelper.GetComboCategoriaModulosAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            Modulo modulo = await _context.Modulos
                .Include(op => op.Operaciones)
                .FirstOrDefaultAsync(m => m.Id == id);

            try
            {
                if (modulo.CantidadOperaciones > 0)
                {
                    _notyf.Error($"El módulo <b>{modulo.Nombre}</b> no puede ser eliminado porque tiene operaciones asociadas.");
                    return RedirectToAction(nameof(Index));
                }

                if (modulo != null)
                {
                    _context.Modulos.Remove(modulo);
                }

                await _context.SaveChangesAsync();

                _notyf.Success($"El módulo <b>{modulo.Nombre}</b> fue eliminado exitosamente.", 3);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyf.Error($"Ocurrió un error al intentar eliminar el módulo <b>{modulo.Nombre}</b>: {ex.InnerException.Message}.", 3);
                return RedirectToAction(nameof(Index));
            }

        }


    }
}
