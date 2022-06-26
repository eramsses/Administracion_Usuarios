using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Administracion_Usuarios.Models
{
    public class AgregarOperacionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nombre { get; set; }

        [Display(Name = "Nombre Clave Único")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string NombreClave { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Descripcion { get; set; }

        [Display(Name = "Módulo")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar una categoría.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ModuloId { get; set; }

        [Display(Name = "Módulo")]
        public IEnumerable<SelectListItem> Modulos { get; set; }

        [Display(Name = "Categoría")]
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar una categoría.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int ModuloCategoriaId { get; set; }

        [Display(Name = "Categoría")]
        public IEnumerable<SelectListItem> ModuloCategorias { get; set; }

    }
}
