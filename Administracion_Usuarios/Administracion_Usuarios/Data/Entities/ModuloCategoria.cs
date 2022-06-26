using System.ComponentModel.DataAnnotations;

namespace Administracion_Usuarios.Data.Entities
{
    public class ModuloCategoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nombre { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Descripcion { get; set; }

        //Tiene muchos módulos
        public ICollection<Modulo> Modulos { get; set; }

        [Display(Name = "Módulos")]
        public int CantidadModulos => Modulos == null ? 0 : Modulos.Count;
    }
}
