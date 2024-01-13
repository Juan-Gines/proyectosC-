using System;
using System.Collections.Generic;
using System.Linq;

namespace Trabajo_Con_Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            //int[] numerosEnteros = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //IEnumerable<int> numerosPares = from numero in numerosEnteros where numero % 2 == 0 select numero;

            //foreach (int i in numerosPares) Console.WriteLine(i);

            ControlEmpresaEmpleado control = new ControlEmpresaEmpleado();

            control.GetCeo();

            Console.WriteLine();

            control.GetEmpresa1();

            Console.WriteLine();

            control.GetSueldoAlto();

        }
    }

    class ControlEmpresaEmpleado
    {
        public ControlEmpresaEmpleado()
        {
            listaEmpresas = new List<Empresa>();

            listaEmpleados = new List<Empleado>();

            listaEmpresas.Add(new Empresa{ Nombre = "Google",Id = 1});

            listaEmpresas.Add(new Empresa { Nombre = "Tecsymold", Id = 2 });

            listaEmpleados.Add(new Empleado { Id = 1, Cargo = "Ceo", Nombre = "Dany Mateo", Salario = 150000, IdEmpresa = 1 });

            listaEmpleados.Add(new Empleado { Id = 2, Cargo = "Ceo", Nombre = "Juan Ginés", Salario = 1500000, IdEmpresa = 2 });

            listaEmpleados.Add(new Empleado { Id = 3, Cargo = "co-Ceo", Nombre = "Miriam Aroca", Salario = 150001, IdEmpresa = 1 });

            listaEmpleados.Add(new Empleado { Id = 4, Cargo = "co-Ceo", Nombre = "Raquel Rascón", Salario = 150002, IdEmpresa = 2 });
        }

        public void GetCeo()
        {
            IEnumerable<Empleado> ce = from Empleado in listaEmpleados where Empleado.Cargo == "Ceo" select Empleado;

            foreach (Empleado empleado in ce) empleado.InfoEmpleado();
        }
        public void GetSueldoAlto()
        {
            IEnumerable<Empleado> ce = from Empleado in listaEmpleados where Empleado.Salario > 150000 select Empleado;

            foreach (Empleado empleado in ce) empleado.InfoEmpleado();
        }
        public void GetEmpresa1()
        {
            IEnumerable<Empleado> ce = from Empleado in listaEmpleados where Empleado.IdEmpresa == 1 select Empleado;

            foreach (Empleado empleado in ce) empleado.InfoEmpleado();
        }

        public List<Empresa> listaEmpresas ;

        public List<Empleado> listaEmpleados;
    }

    class Empresa
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public void InfoEmpresa() => Console.WriteLine("Empresa: {0} con Id {1}", Nombre, Id);
    }
    
    class Empleado
    {
        public string Nombre { get; set; }

        public int Id { get; set; }

        public double Salario { get; set; }

        public string Cargo { get; set; }

        public int IdEmpresa { get; set; }

        public void InfoEmpleado() => Console.WriteLine("Empleado {0} número {1} con cargo {2}, tiene un salario {3} y trabaja en {4}", Nombre, Id, Cargo, Salario, IdEmpresa);

       
    }

}
