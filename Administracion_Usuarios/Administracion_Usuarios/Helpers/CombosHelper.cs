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
            modulosCategorias.Insert(0, new SelectListItem { Text = "Seleccione una categoria", Value = "0" });
            return modulosCategorias;
        }
    }
}
