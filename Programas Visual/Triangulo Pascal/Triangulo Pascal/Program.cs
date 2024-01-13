using System;

namespace Triangulo_Pascal
{
    class Program
    {
        static void Main(string[] args)
        {
            int pisos = 0;
            int[] arreglo = new int[1];
            //capturamos un mensaje y lo convertimos en int//
            Console.WriteLine("Introduzca el numero de pisos");
            pisos = Convert.ToInt16(Console.ReadLine());
            /* Declaramos el primer ciclo for que va a recorrer dependiendo
             * de los datos introducidos en la variable pisos */
            for (int i =1 ; i <= pisos; i++)
            {
                /*Colocamos un arreglo con la variable i del ciclo for
                 * que será el tamaño que tendrá cada vez el ciclo for 
                 * se ejecute */
                int[] pascal = new int[i];
                //Ciclo for que se decrementa para formar el triángulo
                for (int j = pisos; j < i; j--)
                {
                    Console.Write(" ");
                }
                // Ciclo for que genera las sumas de las cifras
                for (int k = 0; k < i; k++)
                {
                    //Condición que evalua la variable del ciclo for
                    if (k == 0 || k == (i - 1))
                    {
                        pascal[k] = 1;
                    }
                    else
                    {
                        //sumamos los numeros que estan en cada pocion 
                        //del arreglo para formar el triangulo
                        pascal[k] = arreglo[k] + arreglo[k - 1];
                    }
                    Console.Write("[" + pascal[k] + "]");
                }
                arreglo = pascal;
                Console.WriteLine(" ");
            }
             
             

            Console.ReadLine();


        }
    }
}
