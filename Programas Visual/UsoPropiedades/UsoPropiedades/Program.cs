using System;

namespace UsoPropiedades
{
    class Program
    {
        static void Main(string[] args)
        {
            Empleado Juan = new Empleado("Juan");

            Juan.SALARIO = 1500;

            Juan.SALARIO += 600;

            Console.WriteLine("El salario del empleado es: " + Juan.SALARIO);

        }
    }
    class Empleado
    {
        public Empleado(string nombre)
        {
            this.nombre = nombre;
        }
        /*metodos getter setter usados hasta ahora 
        public double setSalario(double salario)
        {
            if (salario<0)
            {
                Console.WriteLine("el salario no puede ser negativo, se le asigna 0");

                return this.salario = 0;
            }
            else
            {
                return this.salario = salario;
            }
        }
        public double getSalario() => salario;*/

        //CREACION DE PROPIEDAD

        private double evaluaSalario(double salario)
        {
            if (salario < 0) return 0;

            else return salario;
        }
        /*public double SALARIO
        {
            get { return this.salario; }

            set { this.salario = evaluaSalario(value); }
        }*/
        //SIMPLIFICAMOS LA PROPIEDAD

        public double SALARIO
        {
            get => this.salario;

            set => this.salario = evaluaSalario(value); 
        }

        private string nombre;

        private double salario;
    }
}
