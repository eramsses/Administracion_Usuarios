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
            return View(await _context.CustomRoles
                .Include(r => r.RolesOperaciones)
                .Include(r => r.Usuarios)
                .ToListAsync());
        }

        public async Task<IActionResult> Agregar()
        {
            AgregarModificarRolViewModel rol = new()
            {
                Estado = true,
                Operaciones = new List<OperacionesViewModel>()
            };


            int idRolActual = 0;
            List<Operacion> operacionesRolActual = _context.RolOperaciones.Where(ro => ro.CustomRolId == idRolActual).Select(o => o.Operacion).ToList();

            List<Operacion> operaciones = await _context.Operaciones
                .Include(m => m.Modulo)
                .ThenInclude(c => c.ModuloCategoria)
                .OrderBy(o => o.Modulo.ModuloCategoria.Nombre).ThenBy(o => o.Modulo.Nombre).ThenBy(o => o.Nombre)
                .ToListAsync();

            foreach (var operacion in operaciones)
            {
                OperacionesViewModel opVM = new OperacionesViewModel
                {
                    Id = operacion.Id,
                    Nombre = operacion.Nombre,
                    NombreClave = operacion.NombreClave,
                    Descripcion = operacion.Descripcion,
                    Modulo = operacion.Modulo
                };
                //Marcar seleccionado las operaciones del rol actual
                if (operacionesRolActual.Any(r => r.Id == operacion.Id))
                {
                    opVM.Seleccionado = true;
                }
                rol.Operaciones.Add(opVM);
            }

            //rol.Operaciones = operacionesViewModel;
            return View(rol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(string[] OperacionesSel, AgregarModificarRolViewModel model)
        {
            //Recuperar objetos para que en caso de error devolverlos a la vista
            //Todas las operaciones disponibles
            List<Operacion> operaciones = await _context.Operaciones
                .Include(m => m.Modulo)
                .ThenInclude(c => c.ModuloCategoria)
                .OrderBy(o => o.Modulo.ModuloCategoria.Nombre).ThenBy(o => o.Modulo.Nombre).ThenBy(o => o.Nombre)
                .ToListAsync();

            //Convertir las operaciones (Ids) seleccionadas en la vista a una 
            //lista de RolOperacion
            List<RolOperacion> opSel = new List<RolOperacion>();
            for (int i = 0; i < OperacionesSel.Count(); i++)
            {
                int idOpSel = Int32.Parse(OperacionesSel[i]);
                opSel.Add(new RolOperacion()
                {
                    Operacion = _context.Operaciones.FirstOrDefault(o => o.Id == idOpSel)
                });
            }

            //Agregar todas las Operaciones al modelo y marcar como seleccionadas las elegidas
            foreach (var operacion in operaciones)
            {
                OperacionesViewModel opVM = new OperacionesViewModel
                {
                    Id = operacion.Id,
                    Nombre = operacion.Nombre,
                    NombreClave = operacion.NombreClave,
                    Descripcion = operacion.Descripcion,
                    Modulo = operacion.Modulo,
                };
                //Marcar en true las operaciones seleccionadas
                if (opSel.Any(o => o.Operacion.Id == operacion.Id))
                {
                    opVM.Seleccionado = true;
                }
                model.Operaciones.Add(opVM);
            }

            //Error en caso de seleccionar ninguna operación
            if (OperacionesSel.Count() == 0)
            {
                _notyf.Error("Debe seleccionar al menos una operación para crear el rol.");
                return View(model);
            }

            if (OperacionesSel.Count() == 0)
            {
                _notyf.Error("Debe seleccionar al menos una operación para crear el rol.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                string nombre = model.Nombre;

                try
                {
                    CustomRol rol = new()
                    {
                        Nombre = model.Nombre,
                        Descripcion = model.Descripcion,
                        RolesOperaciones = opSel,
                        Estado = true
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

        public async Task<IActionResult> Modificar(int id)
        {
            //Recuperar la info del rol
            CustomRol r = _context.CustomRoles.FirstOrDefault(r => r.Id == id);
            AgregarModificarRolViewModel rol = new()
            {
                Nombre = r.Nombre,
                Descripcion = r.Descripcion,
                Estado = r.Estado,
                Operaciones = new List<OperacionesViewModel>()
            };


            int idRolActual = id;
            List<Operacion> operacionesRolActual = _context.RolOperaciones.Where(ro => ro.CustomRolId == idRolActual).Select(o => o.Operacion).ToList();

            List<Operacion> operaciones = await _context.Operaciones
                .Include(m => m.Modulo)
                .ThenInclude(c => c.ModuloCategoria)
                .OrderBy(o => o.Modulo.ModuloCategoria.Nombre).ThenBy(o => o.Modulo.Nombre).ThenBy(o => o.Nombre)
                .ToListAsync();

            foreach (var operacion in operaciones)
            {
                OperacionesViewModel opVM = new OperacionesViewModel
                {
                    Id = operacion.Id,
                    Nombre = operacion.Nombre,
                    NombreClave = operacion.NombreClave,
                    Descripcion = operacion.Descripcion,
                    Modulo = operacion.Modulo
                };
                //Marcar seleccionado las operaciones del rol actual
                if (operacionesRolActual.Any(r => r.Id == operacion.Id))
                {
                    opVM.Seleccionado = true;
                }
                rol.Operaciones.Add(opVM);
            }

            return View(rol);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modificar(int id, string[] OperacionesSel, AgregarModificarRolViewModel model)
        {
            //Recuperar objetos para que en caso de error devolverlos a la vista
            //Todas las operaciones disponibles
            List<Operacion> operaciones = await _context.Operaciones
                .Include(m => m.Modulo)
                .ThenInclude(c => c.ModuloCategoria)
                .OrderBy(o => o.Modulo.ModuloCategoria.Nombre).ThenBy(o => o.Modulo.Nombre).ThenBy(o => o.Nombre)
                .ToListAsync();

            //Convertir las operaciones (Ids) seleccionadas en la vista a una 
            //lista de RolOperacion
            List<RolOperacion> opSel = new List<RolOperacion>();
            for (int i = 0; i < OperacionesSel.Count(); i++)
            {
                int idOpSel = Int32.Parse(OperacionesSel[i]);
                opSel.Add(new RolOperacion()
                {
                    Operacion = _context.Operaciones.FirstOrDefault(o => o.Id == idOpSel)
                });
            }

            //Agregar todas las Operaciones al modelo y marcar como seleccionadas las elegidas
            foreach (var operacion in operaciones)
            {
                OperacionesViewModel opVM = new OperacionesViewModel
                {
                    Id = operacion.Id,
                    Nombre = operacion.Nombre,
                    NombreClave = operacion.NombreClave,
                    Descripcion = operacion.Descripcion,
                    Modulo = operacion.Modulo,
                };
                //Marcar en true las operaciones seleccionadas
                if (opSel.Any(o => o.Operacion.Id == operacion.Id))
                {
                    opVM.Seleccionado = true;
                }
                model.Operaciones.Add(opVM);
            }

            //Error en caso de seleccionar ninguna operación
            if (OperacionesSel.Count() == 0)
            {
                _notyf.Error("Debe seleccionar al menos una operación para crear el rol.");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                //Rercuperar el rol actualizar
                CustomRol rolActualizar = await _context.CustomRoles
                    .Include(r => r.RolesOperaciones)
                    .FirstOrDefaultAsync(r => r.Id == id);

                //Error en caso de no encontrar el rol
                if(rolActualizar == null)
                {
                    return NotFound();
                }

                //Eliminar operaciones que ya tenia antes de actualizar las nuevas
                foreach (RolOperacion ro in rolActualizar.RolesOperaciones)
                {
                    _context.RolOperaciones.Remove(ro);
                }
                await _context.SaveChangesAsync();

                string nombre = model.Nombre;

                try
                {
                    //Actualizar las propiedades con las que vienen de la vista
                    rolActualizar.Nombre = model.Nombre;
                    rolActualizar.Descripcion = model.Descripcion;
                    rolActualizar.Estado = model.Estado;
                    rolActualizar.RolesOperaciones = opSel;

                    _context.Update(rolActualizar);
                    await _context.SaveChangesAsync();

                    _notyf.Success($"El rol <b>{nombre}</b> fue modificado exitosamente.", 6);
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
                    if (ex.InnerException != null)
                    {
                        _notyf?.Error(ex.InnerException.Message.Replace("'", "\\'"));
                    }
                    else
                    {
                        Console.WriteLine(ex.Message);
                        _notyf?.Error(ex.Message.Replace("'", "\\'"));
                    }
                    return View(model);
                }
            }

            return View(model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            CustomRol rol = await _context.CustomRoles
                .Include(op => op.Usuarios)
                .FirstOrDefaultAsync(m => m.Id == id);

            try
            {
                if (rol.CantidadUsuarios > 0)
                {
                    _notyf.Error($"El rol <b>{rol.Nombre}</b> no puede ser eliminado porque tiene usuarios asociados.");
                    return RedirectToAction(nameof(Index));
                }

                if (rol != null)
                {
                    _context.CustomRoles.Remove(rol);
                }

                await _context.SaveChangesAsync();

                _notyf.Success($"El rol <b>{rol.Nombre}</b> fue eliminado exitosamente.", 6);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyf.Error($"Ocurrió un error al intentar eliminar el rol <b>{rol.Nombre}</b>: {ex.InnerException.Message.Replace("'", "\\'")}.");
                return RedirectToAction(nameof(Index));
            }

        }

    }
}
