using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrimerMVC.Models
{
    public class VistasParciales
    {
        
        public string Imagen { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        [RegularExpression("^[a-zA-Zá-úà-ùä-ü ]+")]
        public string Nombre { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        [RegularExpression("^[a-zA-Zá-úà-ùä-ü ]+")]
        public string Apellido { get; set; }
        public string Direccion { get; set; }
        public string Contrasena { get; set; }
    }
}