using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Administracion_Usuarios.Data.Entities
{
    public class Modulo
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nombre { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Descripcion { get; set; }

        //Pertenece a una categoría
        [JsonIgnore]
        public ModuloCategoria Categoria { get; set; }

        //Tiene muchas operaciones
        public ICollection<Operacion> Operaciones { get; set; }

        [Display(Name = "Operaciones en Módulo")]
        public int CantidadOperaciones => Operaciones == null ? 0 : Operaciones.Count;
    }
}
