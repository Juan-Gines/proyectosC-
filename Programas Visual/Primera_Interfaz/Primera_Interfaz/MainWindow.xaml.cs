using System;
using System.Windows;

namespace Primera_Interfaz
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Le has dado al boton 2");
        }        

        private void StackPanel_PreviewMouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Console.WriteLine("Le has dado al boton panel");
        }
    }
}
