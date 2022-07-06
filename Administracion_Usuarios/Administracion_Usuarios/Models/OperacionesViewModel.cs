using Administracion_Usuarios.Data.Entities;

namespace Administracion_Usuarios.Models
{
    public class OperacionesViewModel : Operacion
    {
        public bool Seleccionado { get; set; } = false;
    }
}
