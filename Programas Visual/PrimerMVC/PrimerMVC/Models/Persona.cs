using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PrimerMVC.Models
{
    public class Persona
    {
        [Required]
        [StringLength(maximumLength:50,MinimumLength =3)]
        [RegularExpression("^[a-zA-Zá-úà-ùä-ü ]+")]
        public string Nombre { get; set; }
        [Required]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        [RegularExpression("^[a-zA-Zá-úà-ùä-ü ]+")]
        public string Apellidos { get; set; }
        [Required]
        public string Sexo { get; set; }
        [Required]
        public string Edad { get; set; }        
        public bool Vive { get; set; }

    }
}