﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Administracion_Usuarios.Data.Entities
{
    public class Operacion
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nombre { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Descripción")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Descripcion { get; set; }

        //Pertenece a un Módulo
        [JsonIgnore]
        public Modulo Modulo { get; set; }

    }
}