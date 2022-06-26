using Microsoft.AspNetCore.Mvc.Rendering;

namespace Administracion_Usuarios.Helpers
{
    public interface ICombosHelper
    {
        Task<IEnumerable<SelectListItem>> GetComboCategoriaModulosAsync();

        Task<IEnumerable<SelectListItem>> GetComboModulosAsync();

    }
}
