using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculadora
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        Operaciones calculo = new Operaciones();

        

        private void Boton0_Click(object sender, RoutedEventArgs e)
        {           

            if (calculo.Pantalla != "") calculo.Pantalla += "0";

            Display.Text = calculo.Pantalla;
        }

        private void Boton1_Click(object sender, RoutedEventArgs e)
        {
            calculo.Pantalla += "1";

            Display.Text = calculo.Pantalla;
        }

        private void Boton2_Click(object sender, RoutedEventArgs e)
        {
            calculo.Pantalla += "2";

            Display.Text = calculo.Pantalla;
        }

        private void Boton3_Click(object sender, RoutedEventArgs e)
        {
            calculo.Pantalla += "3";

            Display.Text = calculo.Pantalla;
        }

        private void Boton4_Click(object sender, RoutedEventArgs e)
        {
            calculo.Pantalla += "4";

            Display.Text = calculo.Pantalla;
        }

        private void Boton5_Click(object sender, RoutedEventArgs e)
        {
            calculo.Pantalla += "5";

            Display.Text = calculo.Pantalla;
        }

        private void Boton6_Click(object sender, RoutedEventArgs e)
        {
            calculo.Pantalla += "6";

            Display.Text = calculo.Pantalla;
        }

        private void Boton7_Click(object sender, RoutedEventArgs e)
        {
            calculo.Pantalla += "7";

            Display.Text = calculo.Pantalla;
        }

        private void Boton8_Click(object sender, RoutedEventArgs e)
        {
            calculo.Pantalla += "8";

            Display.Text = calculo.Pantalla;
        }

        private void Boton9_Click(object sender, RoutedEventArgs e)
        {
            calculo.Pantalla += "9";

            Display.Text = calculo.Pantalla;
        }        

        private void BotonBorrar_Click(object sender, RoutedEventArgs e)
        {
            calculo.Borrar();

            Display.Text = calculo.Pantalla;
        }       

        private void BotonPosNeg_Click(object sender, RoutedEventArgs e)
        {
            calculo.PosNeg();

            Display.Text = calculo.Pantalla;
           
        }

        private void BotonDivision_Click(object sender, RoutedEventArgs e)
        {
            if (calculo.Operador == "") calculo.Operador = "/";            

            calculo.ComparaOperacion();

            calculo.Operador = "/";

            Display.Text = calculo.Resultado;

        }

        private void BotonMultiplicar_Click(object sender, RoutedEventArgs e)
        {

            if(calculo.Operador == "") calculo.Operador="*";

            calculo.ComparaOperacion();

            calculo.Operador = "*";

            Display.Text = calculo.Resultado;
        }
        
        private void BotonResta_Click(object sender, RoutedEventArgs e)
        {

            if (calculo.Operador == "") calculo.Operador = "-";

            calculo.ComparaOperacion();

            calculo.Operador = "-";

            Display.Text = calculo.Resultado;


        }

        private void BotonSuma_Click(object sender, RoutedEventArgs e)
        {
            if (calculo.Operador == "") calculo.Operador = "+";                       

            calculo.ComparaOperacion();

            calculo.Operador = "+";

            Display.Text = calculo.Resultado;
            
        }

        private void BotonIgual_Click(object sender, RoutedEventArgs e)
        {
            calculo.ComparaOperacion();

            Display.Text = calculo.Resultado;

        }

        private void BotonPotencia_Click(object sender, RoutedEventArgs e)
        {
            calculo.Potencia();

            Display.Text = calculo.Resultado;


        }

        private void BotonRaiz_Click(object sender, RoutedEventArgs e)
        {
            calculo.Raiz();

            Display.Text = calculo.Resultado;

        }

        private void BotonPunto_Click(object sender, RoutedEventArgs e)
        {
            if (calculo.Pantalla != "") calculo.Pantalla += ",";

            else calculo.Pantalla += "0,";

            Display.Text = calculo.Pantalla;
        }
    }
}
