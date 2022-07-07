using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Administracion_Usuarios.Data.Entities
{
    public class Usuario : IdentityUser
    {
        [Display(Name = "Nombres")]
        [MaxLength(80, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nombres { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(80, ErrorMessage = "El campo {0} debe tener máximo {1} caractéres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Apellidos { get; set; }

        [Display(Name = "Foto")]
        public Guid ImagenId { get; set; }

        [Display(Name = "Foto")]
        public string ImageFullPath => ImagenId == Guid.Empty
            ? $"~/imagenes/noimage.png"
            : $"~imagenes/usuarios/{ImagenId}";

        [Display(Name = "Usuario")]
        public string FullName => $"{Nombres} {Apellidos}";

        public bool Estado { get; set; }

        //Pertenece a un Rol
        [JsonIgnore]
        public CustomRol CustomRol { get; set; }

        
    }
}
