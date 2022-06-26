using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Administracion_Usuarios.Models
{
    public class EditModuloViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nombre { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Descripcion { get; set; }

        [Display(Name = "Categoría")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar una categoría.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ModuloCategoriaId { get; set; }

        [Display(Name = "Categoría")]
        public IEnumerable<SelectListItem> ModuloCategorias { get; set; }
    }
}
