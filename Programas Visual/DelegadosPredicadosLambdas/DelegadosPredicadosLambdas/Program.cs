using System;
using System.Collections.Generic;

namespace DelegadosPredicadosLambdas
{
    class Program
    {
        static void Main(string[] args)
        {
            ////creación del objeto que apunta al delegado
            //Delegando elDelegado = new Delegando(MensajeBienvenida.MensajeHola);

            ////Utilización del delegado para usar el metodo mensajeKola
            //elDelegado("Hola acabo de llegar");

            //elDelegado = new Delegando(MensajeDespedida.MensajeAdios);

            //elDelegado("Adios me voy que no llego");

            //CREAMOS LA LIST PARA BUSCAR CON UN PREDICADO
            List<int> numeros = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            List<int> numerosPares = numeros.FindAll(i => i % 2 == 0);

            //Predicate<int> elPredicado = new Predicate<int>(EncuentraPares);

            //List<int> losPares = numeros.FindAll(elPredicado);

            //foreach (int num in numerosPares) Console.WriteLine(num);

            numerosPares.ForEach(num =>Console.WriteLine(num));

            //----------------------------------------------

            List<Persona> gente = new List<Persona>();

            Persona P1 = new Persona();
            P1.Edad = 12;
            P1.Nombre = "Juana";

            Persona P2 = new Persona();
            P2.Edad = 45;
            P2.Nombre = "Pepe";

            Persona P3 = new Persona();
            P3.Edad = 13;
            P3.Nombre = "Ana";

            gente.AddRange(new Persona[] { P1, P2, P3 });

            Predicate<Persona> elPredicate = new Predicate<Persona>(ExisteJuan);

            bool existe = gente.Exists(elPredicate);

            if (existe) Console.WriteLine("Hay gente que se llama Juan");
            else Console.WriteLine("No hay nadie llamado Juan");
        }
        static bool ExisteJuan(Persona persona)
        {
            if (persona.Nombre == "Juan") return true;
            else return false;
        }
        
        ////Definición del objeto delegado
        //delegate void Delegando(string mensa);

        //FUNCION QUE VA A SER DELEGADA PREDICADA
        /*static bool EncuentraPares(int num)
        {
            if (num % 2 == 0) return true;
            else return false;
        }*/
    }
    class Persona
    {
        
        private string nombre;

        private int edad;

        public string Nombre { get => nombre; set => nombre = value; }
        public int Edad { get => edad; set => edad = value; }
    }
    //class MensajeBienvenida
    //{

    //    public static void MensajeHola(string msj)
    //    {

    //        Console.WriteLine("Mensaje de bienvenida: {0}", msj);

    //    }

    //}
    //class MensajeDespedida
    //{

    //    public static void MensajeAdios(string msja)
    //    {

    //        Console.WriteLine("Mensaje de despedida: {0}", msja);
    //    }

    //}
}
