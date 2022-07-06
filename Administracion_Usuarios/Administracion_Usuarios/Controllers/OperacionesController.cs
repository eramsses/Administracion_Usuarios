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
                .Include(m => m.RolesOperaciones)
                .ToListAsync();
            return View(operaciones);
        }

        public async Task<IActionResult> Agregar()
        {
            AgregarModificarOperacionViewModel model = new()
            {
                ModuloCategorias = await _combosHelper.GetComboCategoriaModulosAsync(),
                Modulos  = await _combosHelper.GetComboModulosAsync(0)
        };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(AgregarModificarOperacionViewModel model)
        {
            if (ModelState.IsValid)
            {
                Modulo modulo = await _context.Modulos.FindAsync(model.ModuloId);
                if (modulo == null)
                {
                    return NotFound();
                }

                string nombre = model.Nombre;
                string nombreClave = model.NombreClave;
                try
                {
                    Operacion operacion = new()
                    {
                        Modulo = await _context.Modulos.FindAsync(model.ModuloId),
                        Descripcion = model.Descripcion,
                        Nombre = model.Nombre,
                        NombreClave = model.NombreClave.Trim().Replace(' ', '_')
                    };

                    _context.Add(operacion);
                    await _context.SaveChangesAsync();

                    _notyf.Success($"La operación <b>{nombre}</b> fue creada exitosamente.", 6);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    string error = dbUpdateException.InnerException.Message;
                    if (error.Contains("duplicate") || error.Contains("clave duplicada"))
                    {
                        _notyf.Error($"Ya existe una operación con el nombre clave <b>{nombreClave}</b> ");
                    }
                    else
                    {
                        _notyf.Error(dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    _notyf?.Error(ex.InnerException.Message);
                }
            }

            model.ModuloCategorias = await _combosHelper.GetComboCategoriaModulosAsync();
            model.Modulos = await _combosHelper.GetComboModulosAsync(0);

            return View(model);

        }

        public async Task<IActionResult> Modificar(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Operacion operacion = await _context.Operaciones
                .Include(m => m.Modulo)
                .ThenInclude(c => c.ModuloCategoria)
                .FirstOrDefaultAsync(o => o.Id == id);
            if (operacion == null)
            {
                return NotFound();
            }

            AgregarModificarOperacionViewModel model = new()
            {
                Id = operacion.Id,
                Nombre = operacion.Nombre,
                NombreClave = operacion.NombreClave,
                Descripcion = operacion.Descripcion,
                ModuloId = operacion.Modulo.Id,
                Modulos = await _combosHelper.GetComboModulosAsync(operacion.Modulo.ModuloCategoria.Id),
                ModuloCategoriaId = operacion.Modulo.ModuloCategoria.Id,
                ModuloCategorias = await _combosHelper.GetComboCategoriaModulosAsync(),
            };



            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modificar(int id, AgregarModificarOperacionViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Modulo modulo = await _context.Modulos.FindAsync(model.ModuloId);
                string nombre = model.Nombre;
                string nombreClave = model.NombreClave;
                string nombreModulo = modulo.Nombre;
                try
                {
                    Operacion operacion = new()
                    {
                        Id = id,
                        Modulo = modulo,
                        Descripcion = model.Descripcion,
                        Nombre = model.Nombre,
                        NombreClave = model.NombreClave.Trim().Replace(' ', '_')
                    };

                    _context.Update(operacion);
                    await _context.SaveChangesAsync();

                    _notyf.Success($"La operación <b>{nombre}</b> en el módulo <b>{nombreModulo}</b> fue modificada exitosamente.", 3);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    string error = dbUpdateException.InnerException.Message;
                    if (error.Contains("duplicate") || error.Contains("clave duplicada"))
                    {
                        _notyf.Error($"Ya existe una operación con el nombre clave {nombreClave} ", 6);
                    }
                    else
                    {
                        _notyf.Error(dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    _notyf.Error(ex.InnerException.Message);
                }

            }

            model.Modulos = await _combosHelper.GetComboModulosAsync(model.ModuloCategoriaId);
            model.ModuloCategorias = await _combosHelper.GetComboCategoriaModulosAsync();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            Operacion operacion = await _context.Operaciones
                .FirstOrDefaultAsync(o => o.Id == id);

            try
            {
                //if (operacion.CantidadOperaciones > 0)
                //{
                //    _notyf.Error($"El módulo <b>{operacion.Nombre}</b> no puede ser eliminado porque tiene operaciones asociadas.");
                //    return RedirectToAction(nameof(Index));
                //}

                if (operacion != null)
                {
                    _context.Operaciones.Remove(operacion);
                }

                await _context.SaveChangesAsync();

                _notyf.Success($"La operación <b>{operacion.Nombre}</b> fue eliminada exitosamente.", 3);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyf.Error($"Ocurrió un error al intentar eliminar la operación <b>{operacion.Nombre}</b>: {ex.InnerException.Message}.");
                return RedirectToAction(nameof(Index));
            }

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
