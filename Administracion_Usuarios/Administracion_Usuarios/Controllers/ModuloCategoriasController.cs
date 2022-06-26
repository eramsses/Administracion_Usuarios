using Administracion_Usuarios.Data;
using Administracion_Usuarios.Data.Entities;
using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Administracion_Usuarios.Controllers
{
    public class ModuloCategoriasController : Controller
    {
        private readonly DataContext _context;
        private readonly INotyfService _notyf;

        public ModuloCategoriasController(DataContext context, INotyfService notyf)
        {
            _context = context;
            _notyf = notyf;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.ModuloCategoria
                .Include(mc => mc.Modulos)
                .ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ModuloCategoria == null)
            {
                return NotFound();
            }

            var moduloCategoria = await _context.ModuloCategoria
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moduloCategoria == null)
            {
                return NotFound();
            }

            return View(moduloCategoria);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ModuloCategoria moduloCategoria)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moduloCategoria);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moduloCategoria);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ModuloCategoria == null)
            {
                return NotFound();
            }

            var moduloCategoria = await _context.ModuloCategoria.FindAsync(id);
            if (moduloCategoria == null)
            {
                return NotFound();
            }
            return View(moduloCategoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ModuloCategoria moduloCategoria)
        {
            if (id != moduloCategoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moduloCategoria);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuloCategoriaExists(moduloCategoria.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(moduloCategoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            ModuloCategoria categoria = await _context.ModuloCategoria
                .Include(m => m.Modulos)
                .FirstOrDefaultAsync(m => m.Id == id);

            try
            {
                if (categoria.CantidadModulos > 0)
                {
                    _notyf.Error($"La categoría <b>{categoria.Nombre}</b> no puede ser eliminado porque tiene módulos asociados.");
                    return RedirectToAction(nameof(Index));
                }

                if (categoria != null)
                {
                    _context.ModuloCategoria.Remove(categoria);
                }

                await _context.SaveChangesAsync();

                _notyf.Success($"La categoría <b>{categoria.Nombre}</b> fue eliminada exitosamente.", 3);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _notyf.Error($"Ocurrió un error al intentar eliminar la categoría <b>{categoria.Nombre}</b>: {ex.InnerException.Message}.", 3);
                return RedirectToAction(nameof(Index));
            }

        }

        private bool ModuloCategoriaExists(int id)
        {
            return _context.ModuloCategoria.Any(e => e.Id == id);
        }

        
    }
}
