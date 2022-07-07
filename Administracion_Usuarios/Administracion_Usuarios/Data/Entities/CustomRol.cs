using Administracion_Usuarios.Enums;
using System.ComponentModel.DataAnnotations;

namespace Administracion_Usuarios.Data.Entities
{
    public class CustomRol
    {
        public int Id { get; set; }

        [MaxLength(80, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nombre { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        [Display(Name = "Estado")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public bool Estado { get; set; }

        //Tiene muchas 
        public ICollection<RolOperacion> RolesOperaciones { get; set; }

        [Display(Name = "Operaciones")]
        public int CantidadRolesOperaciones => RolesOperaciones == null ? 0 : RolesOperaciones.Count;

        //Tiene muchos usuarios
        public ICollection<Usuario> Usuarios { get; set; }

        //Solo lectura
        [Display(Name = "Usuarios")]
        public int CantidadUsuarios => Usuarios == null ? 0 : Usuarios.Count;

    }
}
