using Administracion_Usuarios.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Administracion_Usuarios.Helpers
{
    public class CombosHelper : ICombosHelper
    {
        private readonly DataContext _context;

        public CombosHelper(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboCategoriaModulosAsync()
        {
            List<SelectListItem> modulosCategorias = await _context.ModuloCategoria.Select(c => new SelectListItem
            {
                Text = c.Nombre,
                Value = c.Id.ToString(),
            })
                .OrderBy(c => c.Text)
                .ToListAsync();
            modulosCategorias.Insert(0, new SelectListItem { Text = "Seleccione una categoría", Value = "0" });
            return modulosCategorias;
        }

        public async Task<IEnumerable<SelectListItem>> GetComboModulosAsync(int moduloCategoriaId)
        {
            List<SelectListItem> modulos = await _context.Modulos
                .Where(m => m.ModuloCategoria.Id == moduloCategoriaId)
                .Select(m => new SelectListItem
            {
                Text = m.Nombre,
                Value = m.Id.ToString()
            })
                .OrderBy(m => m.Text)
                .ToListAsync();

            if(moduloCategoriaId == 0)
                modulos.Insert(0, new SelectListItem { Text = "Primero seleccione una categoría", Value = "0" });
            else
                modulos.Insert(0, new SelectListItem { Text = "Seleccione un módulo", Value = "0" });

            return modulos;

        }

       
    }
}
