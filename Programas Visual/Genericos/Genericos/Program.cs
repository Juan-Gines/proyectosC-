using System;

namespace Genericos
{
    class Program
    {
        static void Main(string[] args)
        {
            AlmacenObjetos<Empleado> archivo = new AlmacenObjetos<Empleado>(4);

            //archivo.AgregarDatos("Juan");

            //archivo.AgregarDatos("Manuel");

            //archivo.AgregarDatos("Ariadna");

            //archivo.AgregarDatos("Raquel");

            //String nombrePersona = (string)archivo.getDatos(2);

            archivo.AgregarDatos(new Empleado(1500));

            archivo.AgregarDatos(new Empleado(2500));

            archivo.AgregarDatos(new Empleado(3500));

            archivo.AgregarDatos(new Empleado(4500));

            Empleado salarioEmpleado = archivo.getDatos(2);

            Console.WriteLine(salarioEmpleado.getSalario());
        }
    }
    class AlmacenObjetos <T>
    {
        public AlmacenObjetos(int z)
        {
            listaObjetos = new T[z]; 

        }
        public void AgregarDatos(T agregar)
        {
            listaObjetos[i] = agregar;

            i++;
        }
        public T getDatos(int p)
        {
            return listaObjetos[p];
        }

        private int i = 0;

        private T[] listaObjetos;

    }
    class Empleado
    {
        public Empleado(double salario)
        {
            this.salario = salario;
        }
        public double getSalario()
        {
            return salario;
        }

        private double salario;
    }
}
