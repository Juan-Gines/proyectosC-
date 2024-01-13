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

namespace ComboBox_CheckBox
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<Capitales> listaCapitales = new List<Capitales>();

            listaCapitales.Add(new Capitales { Nombre = "Barcelona" });
            listaCapitales.Add(new Capitales { Nombre = "Madrid" });
            listaCapitales.Add(new Capitales { Nombre = "Paris" });
            listaCapitales.Add(new Capitales { Nombre = "Londres" });
            listaCapitales.Add(new Capitales { Nombre = "Washington" });
            ListaCap.ItemsSource = listaCapitales;
        }

        private void ActivarTodos_Checked(object sender, RoutedEventArgs e)
        {
            Barcelona.IsChecked = true;
            Madrid.IsChecked = true;
            Paris.IsChecked = true;
            Londres.IsChecked = true;
            Washington.IsChecked = true;
        }

        private void ActivarTodos_Unchecked(object sender, RoutedEventArgs e)
        {
            Barcelona.IsChecked = false;
            Madrid.IsChecked = false;
            Paris.IsChecked = false;
            Londres.IsChecked = false;
            Washington.IsChecked = false;
        }
        private void Individual_Cheked(object sender, RoutedEventArgs e)
        {
            if(Barcelona.IsChecked == true && Madrid.IsChecked == true && Paris.IsChecked == true && Londres.IsChecked == true && Washington.IsChecked == true)
            {
                ActivarTodos.IsChecked = true;
            }
            else
            {
                ActivarTodos.IsChecked = null;
            }
        }
        private void Individual_NoCheked(object sender, RoutedEventArgs e)
        {
            if (Barcelona.IsChecked == false && Madrid.IsChecked == false && Paris.IsChecked == false && Londres.IsChecked == false && Washington.IsChecked == false)
            {
                ActivarTodos.IsChecked = false;
            }
            else
            {
                ActivarTodos.IsChecked = null;
            }
        }
    }
    class Capitales
    {
        public string Nombre { get; set; }
    }
}
