using System;

namespace EjercicioClases
{
    class Program
    {
        static void Main(string[] args)
        {
            Avion miAvion = new Avion();
            
            Coche miCoche = new Coche();

            Console.WriteLine("Probando el avión");
            
            miAvion.ArrancarMotor();

            miAvion.Conducir();

            miAvion.PararMotor();

            Console.WriteLine();

            Console.WriteLine("Probando el coche");

            miCoche.ArrancarMotor();

            miCoche.Conducir();

            miCoche.ArrancarMotor();

            Vehiculo miVehiculo = miCoche;

            Console.WriteLine();

            miVehiculo.Conducir();

            miVehiculo = miAvion;

            miVehiculo.Conducir();
        }
    }
    
    class Vehiculo
    {
        public void ArrancarMotor()
        {
            Console.WriteLine("El motor esta en marcha");
        }
        public void PararMotor()
        {
            Console.WriteLine("El motor está parado");
        }
        public virtual void Conducir()
        {
            Console.WriteLine($"Estoy conduciendo un vehículo");
        }
    }

    class Avion:Vehiculo
    {
        public override void Conducir()
        {
            Console.WriteLine($"Estoy pilotando un avión");
        }

    }

    class Coche:Vehiculo
    {
        public override void Conducir()
        {
            Console.WriteLine($"Estoy conduciendo un coche");
        }

    }
}
