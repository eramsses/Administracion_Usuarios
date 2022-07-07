using System.ComponentModel.DataAnnotations.Schema;

namespace Administracion_Usuarios.Data.Entities
{
    public class RolOperacion
    {
        //public int Id { get; set; }

        [ForeignKey("CustomRol")]
        public int CustomRolId { get; set; }

        [ForeignKey("Operacion")]
        public int OperacionId { get; set; }

        public CustomRol Rol { get; set; }

        public Operacion Operacion { get; set; }
    }
}
