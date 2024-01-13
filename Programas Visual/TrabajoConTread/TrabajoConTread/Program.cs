using System;
using System.Threading;

namespace TrabajoConTread
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread t1 = new Thread(MetodoSaludar);
            t1.Start();
            Thread t2 = new Thread(MetodoSaludar);
            t2.Start();
            Console.WriteLine("Hello World!Desde el tread 1");
            Thread.Sleep(500);
            Console.WriteLine("Hello World!Desde el tread 1");
            Thread.Sleep(500);
            Console.WriteLine("Hello World!Desde el tread 1");
            Thread.Sleep(500);
            Console.WriteLine("Hello World!Desde el tread 1");
            Thread.Sleep(500);
            Console.WriteLine("Hello World!Desde el tread 1");
            Thread.Sleep(500);

            //MetodoSaludar();
        }

        static void MetodoSaludar()
        {
            Console.WriteLine("Hello World!Desde el tread 2");
            Thread.Sleep(500);
            Console.WriteLine("Hello World!Desde el tread 2");
            Thread.Sleep(500);
            Console.WriteLine("Hello World!Desde el tread 2");
            Thread.Sleep(500);
            Console.WriteLine("Hello World!Desde el tread 2");
            Thread.Sleep(500);
            Console.WriteLine("Hello World!Desde el tread 2");
            Thread.Sleep(500);
        }
    }
}
