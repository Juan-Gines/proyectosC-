using System;
using System.Collections.Generic;
using System.Text;

namespace ConceptosPOO
{
    class Punto
    {
        public Punto()
        {
            x = 0;
            y = 0;
            contador++;            
        }
        public Punto(int x,int y)
        {
            this.x = x;
            this.y = y;
            contador++;
        }
        public double CalculoDistancia(Punto otroPunto)
        {
            int xDif = this.x - otroPunto.x;
            int yDif = this.y - otroPunto.y;
            double distanciaPuntos = Math.Sqrt(Math.Pow(xDif, 2) + Math.Pow(yDif, 2));
            return distanciaPuntos;

        }
        public static int Contador() => contador;      

        private int x, y;

        private static int contador = 0;
    }
}
