using System;

namespace ConceptosPOO
{
    class Program
    {
        static void Main(string[] args)
        {
            realizarTarea();

           
        }
        static void realizarTarea()
        {
            Punto origen = new Punto();

            Punto destino = new Punto(140, 89);

            Punto nuevoPunto = new Punto();

            double distancia= origen.CalculoDistancia(destino);

            Console.WriteLine($"La distancia ente dos puntos es: {distancia}");

            Console.WriteLine($"Número de objetos: {Punto.Contador()}");

        }
    }
}
