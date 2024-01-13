using System;

namespace EjemploPOO
{
    class Program
    {
        static void Main(string[] args)
        {
            Circulo miCirculo;
             
            miCirculo= new Circulo();

            Console.WriteLine(miCirculo.CalculoArea(5));

            Circulo miCirculo2 = new Circulo();

            Console.WriteLine(miCirculo2.CalculoArea(20));

            ConversorEuroDolar dolares = new ConversorEuroDolar();

            dolares.ValorEuro(1.45);

            Console.WriteLine(dolares.ConvierteEuros(50));
        }
    }
    class Circulo
    {
        private const double PI = 3.1416;

        public double CalculoArea(int radio)
        {
            return PI * radio * radio;
        }
        
    }
    class ConversorEuroDolar
    {
        private double euro = 1.385;

        public double ConvierteEuros(double valor) => valor* euro;

        public void ValorEuro(double valorE)
        {
            if (valorE < 0) euro = 1.385;

            else euro = valorE;           
        }

    }
}
