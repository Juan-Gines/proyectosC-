using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculadora
{
    public class Operaciones
    {
        public string Pantalla 
        {
            get { return pantalla; }

            set { pantalla = value; }
        }
        public double Numero1
        {
            get { return numero1; }

            set { numero1 = value; }
        }
        public double Numero2
        {
            get { return numero2; }

            set { numero2 = value; }
        }
        public string Operador
        {
            get { return operador; }

            set { operador = value; }
        }
        public string Resultado
        {
            get { return resultado; }

            set { resultado = value; }
        }
        public void ComparaOperacion()
        {
            if (numero1 == 0 && pantalla !="" )
            {
                Numero1 = double.Parse(Pantalla);

                Resultado = Pantalla;

                Pantalla = "";
            }
            else if (numero1 !=0 && operador != "" && pantalla !="")
            {
                Numero2 = double.Parse(Pantalla);

                Operacion();

                numero2 = numero1;

                numero1 = double.Parse(Resultado);                                

                Pantalla = "";
            }

        }
        public void PosNeg()
        {
            if (pantalla != "") pantalla = (double.Parse(Pantalla) * -1).ToString();

        }
        public void Borrar()
        {
            numero1 = 0;numero2 = 0;resultado = "";operador = "";pantalla = "";
        }
        public void Potencia()
        {
            if (pantalla != "" && numero1 == 0)
            {
                Numero1 = double.Parse(Pantalla);

                resultado = Math.Pow(numero1,2).ToString();

                Pantalla = "";

            }
            else if (numero1 != 0)
            {
                ComparaOperacion();

                resultado = Math.Pow(double.Parse(resultado),2).ToString();
            }
        }
        public void Raiz()
        {
            if (pantalla != "" && numero1 == 0)
            {
                Numero1 = double.Parse(Pantalla);

                resultado = Math.Sqrt(numero1).ToString();

                Pantalla = "";

            }
            else if (numero1 !=0)
            {
                ComparaOperacion();

                resultado = Math.Sqrt(double.Parse(resultado)).ToString();
            }
        }
        public void Operacion()
        {
            switch (operador)
            {
                case "+":
                    resultado = (numero1 + numero2).ToString();                    
                    break;
                case "-":
                    resultado = (numero1 - numero2).ToString();                   
                    break;
                case "*":
                    resultado = (numero1 * numero2).ToString();                    
                    break;
                case "/":
                    resultado = (numero1 / numero2).ToString();                    
                    break;
            }
        }
        

        private double numero1=0, numero2=0;

        private string pantalla="",operador="",resultado="";
       
    }

    
}
