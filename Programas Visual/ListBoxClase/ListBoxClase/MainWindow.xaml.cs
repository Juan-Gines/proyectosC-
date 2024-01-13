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

namespace ListBoxClase
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<Poblaciones> listaPob = new List<Poblaciones>();

            listaPob.Add(new Poblaciones { Nombre1 = "Madrid", Nombre2 = "Barcelona", Temperatura1 = 15,Temperatura2 = 17 });
            listaPob.Add(new Poblaciones { Nombre1 = "Sevilla", Nombre2 = "Santiago", Temperatura1 = 28, Temperatura2 = 10 });
            listaPob.Add(new Poblaciones { Nombre1 = "Lérida", Nombre2 = "Girona", Temperatura1 = 22, Temperatura2 = 20 });
            listaPob.Add(new Poblaciones { Nombre1 = "Zaragoza", Nombre2 = "Bilbao", Temperatura1 = 12, Temperatura2 = 19 });
            ListaPoblaciones.ItemsSource = listaPob;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           if(ListaPoblaciones.SelectedItem != null)
            MessageBox.Show((ListaPoblaciones.SelectedItem as Poblaciones).Nombre1 + " " + (ListaPoblaciones.SelectedItem as Poblaciones).Temperatura1 + " ºC " +
                (ListaPoblaciones.SelectedItem as Poblaciones).Nombre2 + " " + (ListaPoblaciones.SelectedItem as Poblaciones).Temperatura2 + " ºC\nDiferencia grados: " +
                (ListaPoblaciones.SelectedItem as Poblaciones).DifTemperatura);
                
        }
    }

    public class Poblaciones
    {
        public string Nombre1 { get; set; }

        public int Temperatura1 { get; set; }

        public string Nombre2 { get; set; }

        public int Temperatura2 { get; set; }

        public int DifTemperatura
        {
            get { return DifTemp() ; }

            set { DifTemp(); }
        }
        private int DifTemp()
        {
            difTemperatura = Temperatura1 - Temperatura2;

            if (difTemperatura < 0) difTemperatura = difTemperatura * -1;

            return difTemperatura;
        }
        
        private int difTemperatura;
    }
}
