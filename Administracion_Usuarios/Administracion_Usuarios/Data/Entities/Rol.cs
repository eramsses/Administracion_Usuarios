using Administracion_Usuarios.Enums;
using System.ComponentModel.DataAnnotations;

namespace Administracion_Usuarios.Data.Entities
{
    public class Rol
    {
        public int Id { get; set; }

        [MaxLength(80, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nombre { get; set; }

        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        [Display(Name = "Estado")]
        public EstadoEnum EstadoEnum { get; set; }
        
    }
}
