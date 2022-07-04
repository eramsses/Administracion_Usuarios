using Administracion_Usuarios.Data.Entities;
using Administracion_Usuarios.Enums;
using System.ComponentModel.DataAnnotations;

namespace Administracion_Usuarios.Models
{
    public class AgregarModificarRolViewModel
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
        public EstadoEnum EstadoEnum { get; set; }

        public IEnumerable<Operacion> Operaciones { get; set; }

        //Tiene muchas 
        public ICollection<RolOperacion> RolesOperaciones { get; set; }

        [Display(Name = "Operaciones")]
        public int CantidadRolesOperaciones => RolesOperaciones == null ? 0 : RolesOperaciones.Count;
    }
}
