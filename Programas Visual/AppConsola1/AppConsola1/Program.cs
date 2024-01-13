using System;

namespace AppConsola1
{
    class Program
    {
        static void Main(string[] args)
        {
            Random numero = new Random();

            int numeroAl = numero.Next(0, 100);

            int count = 0;

            Console.WriteLine("Introduce un número del 1 al 100");

            int numeroPosible;

           do
            {
                count++;

                try
                {

                    numeroPosible = int.Parse(Console.ReadLine());
                }
                catch (Exception e) when (e.GetType() !=typeof(FormatException))
                {
                    Console.WriteLine("Número erroneo, se escoge 0 como numero valido");
                    numeroPosible = 0;
                }
                catch
                {
                    Console.WriteLine("El texto no es válido, se escoge 0 como numero valido");
                    numeroPosible = 0;
                }

                if (numeroPosible < numeroAl) Console.WriteLine($"El número correcto es mayor que: {numeroPosible}");
                
                if (numeroPosible > numeroAl) Console.WriteLine($"El número correcto es menor que: {numeroPosible}");
                
            } while (numeroPosible != numeroAl) ;

                Console.WriteLine($"Correcto, eres un crack el número elejido era: {numeroPosible} y lo has conseguido en {count} veces.");

            Console.ReadLine();
       
        }
    }
}
