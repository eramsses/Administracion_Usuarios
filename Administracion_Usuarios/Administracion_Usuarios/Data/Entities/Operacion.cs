using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Administracion_Usuarios.Data.Entities
{
    public class Operacion
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [MaxLength(80, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres")]
        public string Nombre { get; set; }

        [Display(Name = "Nombre Clave Único")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string NombreClave { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Descripcion { get; set; }

        //Pertenece a un Módulo
        [JsonIgnore]
        public Modulo Modulo { get; set; }

        //Tiene muchas 
        public ICollection<RolOperacion> RolesOperaciones { get; set; }

        [Display(Name = "Roles")]
        public int CantidadRolesOperaciones => RolesOperaciones == null ? 0 : RolesOperaciones.Count;

    }
}
