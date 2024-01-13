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

namespace PracticaRadioButton
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

        private void Rojo(object sender, RoutedEventArgs e)
        {
            eRojo.Visibility = Visibility.Visible;
            eAmarillo.Visibility = Visibility.Hidden;
            eVerde.Visibility = Visibility.Hidden;
        }
        private void Amarillo(object sender, RoutedEventArgs e)
        {
            eAmarillo.Visibility = Visibility.Visible;
            eRojo.Visibility = Visibility.Hidden;
            eVerde.Visibility = Visibility.Hidden;
        }
        private void Verde(object sender, RoutedEventArgs e)
        {
            eVerde.Visibility = Visibility.Visible;
            eAmarillo.Visibility = Visibility.Hidden;
            eRojo.Visibility = Visibility.Hidden;
        }
    }
}
