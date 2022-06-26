using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Administracion_Usuarios.Data.Entities
{
    public class Modulo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(80, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres")]
        public string Nombre { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Descripcion { get; set; }

        //Pertenece a una categoría
        [JsonIgnore]
        [Display(Name = "Categoría")]
        public ModuloCategoria ModuloCategoria { get; set; }

        //Tiene muchas operaciones
        public ICollection<Operacion> Operaciones { get; set; }

        [Display(Name = "Operaciones")]
        public int CantidadOperaciones => Operaciones == null ? 0 : Operaciones.Count;
    }
}
