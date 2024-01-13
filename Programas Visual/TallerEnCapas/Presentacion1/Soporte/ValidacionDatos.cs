using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Presentacion1.Soporte
{
    public class ValidacionDatos
    {
        private ValidationContext contexto;
        private List<ValidationResult> resultado;
        private bool valido;
        private string mensaje;

        public ValidacionDatos(object instancia)
        {
            contexto = new ValidationContext(instancia);
            resultado = new List<ValidationResult>();
            valido = Validator.TryValidateObject(instancia, contexto, resultado,true);
        }
        public bool Validar()
        {
            if (valido == false)
            {
                foreach(ValidationResult item in resultado)
                {
                    mensaje += item.ErrorMessage + "\n";
                }
                System.Windows.MessageBox.Show(mensaje);
            }
            return valido;
        }
    }
}
