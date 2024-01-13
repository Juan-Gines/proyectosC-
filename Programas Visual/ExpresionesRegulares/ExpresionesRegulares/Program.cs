using System;
using System.Text.RegularExpressions;

namespace ExpresionesRegulares
{
    class Program
    {
        static void Main(string[] args)
        {
            string mensaje="Hola amigos me llamo juan mi teléfono es el +34 123-45-67 y mi código postal es 08207";

            string patron = "[J]";

            Regex miRegex = new Regex(patron);

            MatchCollection match = miRegex.Matches(mensaje);

            if (match.Count > 0) Console.WriteLine("Encontramos una J mayúscula");
            else Console.WriteLine("No hay J en el texto");
                
        }
    }
}
