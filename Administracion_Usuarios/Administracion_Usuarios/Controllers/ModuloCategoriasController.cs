using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Administracion_Usuarios.Data;
using Administracion_Usuarios.Data.Entities;

namespace Administracion_Usuarios.Controllers
{
    public class ModuloCategoriasController : Controller
    {
        private readonly DataContext _context;

        public ModuloCategoriasController(DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
              return View(await _context.ModuloCategoria.ToListAsync());
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

        public async Task<IActionResult> Delete(int? id)
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ModuloCategoria == null)
            {
                return Problem("Entity set 'DataContext.ModuloCategoria'  is null.");
            }
            var moduloCategoria = await _context.ModuloCategoria.FindAsync(id);
            if (moduloCategoria != null)
            {
                _context.ModuloCategoria.Remove(moduloCategoria);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ModuloCategoriaExists(int id)
        {
          return _context.ModuloCategoria.Any(e => e.Id == id);
        }
    }
}
