using System;
using System.Collections.Generic;

namespace Colecciones
{
    class Program
    {
        static void Main(string[] args)
        {
            //USAMOS LIST
            List<int> numeros = new List<int>();

            //------------------ejemplo 1 haciendo una lista a mano y imprimiendola

            //numeros.Add(34);

            //numeros.Add(20);

            //numeros.Add(10);

            //numeros.Add(15);

            //for (int i = 0; i < numeros.Count; i++) Console.WriteLine(numeros[i]);

            //----------------------------ejemplo 2 pidiendo el número de elementos de la lista

            //Console.WriteLine("Introduzca cualtos elementos en la lista");

            //int contadorNumeros = Int32.Parse(Console.ReadLine());

            //for (int i= 0; i < contadorNumeros; i++)
            //{
            //    numeros.Add(Int32.Parse(Console.ReadLine()));
            //}
            //Console.WriteLine();
            //foreach (int variable in numeros) Console.WriteLine(variable);

            //Usar list sin saber cuantos elementos en lista ni el contenido

            //Console.WriteLine("Introduzca números en la lista. Introduzca el 0 para salir");

            //int numeroNuevo;

            //do
            //{
            //    numeroNuevo = Int32.Parse(Console.ReadLine());

            //    numeros.Add(numeroNuevo);

            //}
            //while (numeroNuevo != 0);

            //numeros.RemoveAt(numeros.Count - 1);

            //foreach (int variable in numeros) Console.WriteLine(variable);

            //USAMOS LINKEDLIST

            //LinkedList<int> enteros = new LinkedList<int>();

            //foreach (int entero in new int[] { 5, 4, 3, 2, 1 }) enteros.AddLast(entero);

            //foreach (int entero in enteros) Console.WriteLine(entero);

            //Console.WriteLine();

            //enteros.Remove(1);

            //LinkedListNode<int> nodoImportante = new LinkedListNode<int>(15);

            //enteros.AddFirst(nodoImportante);

            //for (LinkedListNode<int> nodo = enteros.First; nodo != null; nodo = nodo.Next) Console.WriteLine(nodo.Value);

            //USAMOS QUEUE

            //Queue<int> numeral = new Queue<int>();

            //foreach (int numero in new int[5] { 1, 2, 3, 4, 5 }) numeral.Enqueue(numero);

            //foreach (int numero in numeral) Console.WriteLine(numero);

            //Console.WriteLine("Borro el número {0}", numeral.Dequeue());

            //foreach (int numero in numeral)
            //{
            //    Console.WriteLine($"{numero} + {numeral.GetEnumerator()}");
            //}
            //int[] copyNumeral = numeral.ToArray();

            //Console.WriteLine("Listando la copia en el array");

            //foreach (int numero in copyNumeral) Console.WriteLine(numero);

            //USAMOS DICTIONARY

            Dictionary<string, int> edades = new Dictionary<string, int>();

            edades.Add("Juan", 25);

            edades.Add("Raquel", 45);

            edades["Ariadna"] = 14;

            edades["Rimbaud"] = 1;

            foreach (KeyValuePair<string, int> edad in edades) Console.WriteLine($"Nombre: {edad.Key} Edad: {edad.Value}");

            
        }
    }
}
