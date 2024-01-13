using System;

namespace UsoArrays
{
    class Program
    {
        static void Main(string[] args)
        {
            //array implicito

            var valores = new[] { 21, 244, 55, 4, 25.2, 221.2 };

            //array de objetos

            Personas ariadna = new Personas("Ariadna", 15);

            Personas []arrayPersonas = new Personas[2];

            arrayPersonas[0] = new Personas("Raquel", 46);

            arrayPersonas[1] = ariadna;

            //array de clase o tipo anónimo

            var perros = new[]
            {
                new{Nombre="Nala",Edad=10},
                new{Nombre="Rimbaud",Edad=1},
                new{Nombre="Tini",Edad=3}
            };

            //bucle para recorrer arrays
            
            for(int i = 0; i < valores.Length; i++)
            {
                Console.WriteLine(valores[i]);
            }

            //bucle para recorrer arrays de objetos

            for (int i = 0; i < arrayPersonas.Length; i++)
            {
                Console.WriteLine(arrayPersonas[i].getPersonas());
            }

            //bucle foreach

            foreach (Personas contador in arrayPersonas)
            {
                Console.WriteLine(contador.getPersonas());
            }

            foreach (var contador in valores) Console.WriteLine(contador);

            foreach (var contador in perros) Console.WriteLine(contador);
        }
    }
    class Personas
    {
        public Personas(string nombre,int edad)
        {
            this.nombre = nombre;

            this.edad = edad;
        }
        
        public string getPersonas()
        {
            return $"Nombre: {nombre} Edad: {edad}";
        }

        private string nombre;

        private int edad;

    }
}
