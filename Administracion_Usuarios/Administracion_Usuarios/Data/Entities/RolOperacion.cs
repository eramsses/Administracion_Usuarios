using System.ComponentModel.DataAnnotations.Schema;

namespace Administracion_Usuarios.Data.Entities
{
    public class RolOperacion
    {
        //public int Id { get; set; }

        [ForeignKey("Rol")]
        public int RolId { get; set; }

        [ForeignKey("Operacion")]
        public int OperacionId { get; set; }

        public Rol Rol { get; set; }

        public Operacion Operacion { get; set; }
    }
}
