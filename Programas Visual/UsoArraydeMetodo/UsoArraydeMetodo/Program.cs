using System;

namespace UsoArraydeMetodo
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] miArray = ObtenerDatos();

            Console.WriteLine("Voy a imprimir desde el main");

            foreach (int i in miArray) Console.WriteLine(i);
        }
        static int[] ObtenerDatos()
        {
            Console.WriteLine("Introduzca la capacidad del array");

            String respuesta = Console.ReadLine();

            int numElementos = int.Parse(respuesta);

            int[] datos = new int[numElementos];

            for(int i = 0; i < numElementos; i++)
            {
                Console.WriteLine($"Introduce el valor de la posición {i} del array:");

                respuesta = Console.ReadLine();

                datos[i] = int.Parse(respuesta);
            }

            return datos;
        }
    }
}
