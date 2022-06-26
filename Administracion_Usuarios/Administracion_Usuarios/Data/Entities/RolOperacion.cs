namespace Administracion_Usuarios.Data.Entities
{
    public class RolOperacion
    {
        public int Id { get; set; }

        public Rol Rol { get; set; }

        public Operacion Operacion { get; set; }
    }
}
