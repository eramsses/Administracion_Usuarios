using Administracion_Usuarios.Data;
using Administracion_Usuarios.Data.Entities;
using Administracion_Usuarios.Models;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Administracion_Usuarios.Controllers
{
    public class RolesController : Controller
    {
        private readonly DataContext _context;
        private readonly INotyfService _notyf;

        public RolesController(DataContext context, INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Roles
                .Include(r => r.RolesOperaciones)
                .ToListAsync());
        }

        public async Task<IActionResult> Agregar()
        {
            AgregarModificarRolViewModel rol = new() { };

            IEnumerable<Operacion> operaciones = await _context.Operaciones
                .Include(m => m.Modulo)
                .ThenInclude(c => c.ModuloCategoria)
                .OrderBy(o => o.Modulo.Nombre)
                .ToListAsync();

            rol.Operaciones = operaciones;
            return View(rol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(Rol model)
        {
            if (ModelState.IsValid)
            {
                

                string nombre = model.Nombre;
                
                try
                {
                    Rol rol = new()
                    {
                        RolesOperaciones = new List<RolOperacion>(),
                        Descripcion = model.Descripcion,
                        Nombre = model.Nombre,
                        
                    };

                    _context.Add(rol);
                    await _context.SaveChangesAsync();

                    _notyf.Success($"El rol <b>{nombre}</b> fue creado exitosamente.", 6);
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    string error = dbUpdateException.InnerException.Message;
                    if (error.Contains("duplicate") || error.Contains("clave duplicada"))
                    {
                        _notyf.Error($"Ya existe un rol con el nombre <b>{nombre}</b> ");
                    }
                    else
                    {
                        _notyf.Error(dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception ex)
                {
                    _notyf?.Error(ex.InnerException.Message, 6);
                }
            }

            
            return View(model);

        }



    }
}
