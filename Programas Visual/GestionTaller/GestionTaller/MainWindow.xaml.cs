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
using System.Configuration;
using System.Text.RegularExpressions;

namespace GestionTaller
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataClasses1DataContext dataContext;

        

        public MainWindow()
        {
            InitializeComponent();

            string miConexionSql = ConfigurationManager.ConnectionStrings["GestionTaller.Properties.Settings.ConexionGestionTaller"].ConnectionString;

            dataContext = new DataClasses1DataContext(miConexionSql);
        }
                
        private void Button_Click(object sender, RoutedEventArgs e)
        {
                                               

            panelLista.ItemsSource = dataContext.Moldes;

            panelLista.Columns[0].Visibility = Visibility.Collapsed;

            titLista.Content = "Moldes en taller";

            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            panelLista.ItemsSource = dataContext.Empleado;

            panelLista.Columns[0].Visibility = Visibility.Collapsed;

            titLista.Content = "Trabajadores";
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            panelLista.ItemsSource = dataContext.OT;

            panelLista.Columns[0].Visibility = Visibility.Collapsed;

            titLista.Content = "Ordenes de trabajo";
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Agregar agregar = new Agregar();

            agregar.ShowDialog();
        }

        private void soloLetras_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^a-zA-Zà-ùá-ú]+").IsMatch(e.Text);
        }
        
        private void soloNumeros_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void InfoEmpleado()
        {
            Empleado edEmpleado = new Empleado();

            
        }

        private void panelLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (titLista.Content)
            {
                case "Trabajadores":
                    if (panelLista.SelectedIndex != -1)
                    {
                        titSeleccion.Content = "Información del trabajador";
                        string[] cargo = new[] { "Ajustador", "Encargado", "Fresador", "Ingeniero", "Diseñador" };
                        modCargo.ItemsSource = cargo;
                        modEmpleado.Visibility = Visibility.Visible;
                        Empleado empleado = panelLista.SelectedItem as Empleado;
                        modNombre.Text = empleado.Nombre;
                        modApellido.Text = empleado.Apellido;
                        modCargo.Text = empleado.Cargo;
                        modTelefono.Text = empleado.Telefono;
                    }
                    else modEmpleado.Visibility = Visibility.Collapsed;
                    break;
              
            }
        }

        private void modificaEmpleado_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (modNombre.Text != "" && modCargo.Text != "")
                {

                    RegexStringValidator miPatron = new RegexStringValidator(@"^\d{9}");

                    try
                    {
                        miPatron.Validate(modTelefono.Text);

                        Empleado empleado = panelLista.SelectedItem as Empleado;

                        empleado.Nombre = modNombre.Text;
                        empleado.Apellido = modApellido.Text;
                        empleado.Cargo = modCargo.Text;
                        empleado.Telefono = modTelefono.Text;
                        dataContext.SubmitChanges();

                        modEmpleado.Visibility = Visibility.Collapsed;

                        panelLista.ItemsSource = dataContext.Empleado;

                        panelLista.Columns[0].Visibility = Visibility.Collapsed;

                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Ingresa un número de telefono válido.");
                    }

                }
                else
                {
                    MessageBox.Show("Introduce un nombre y un cargo por favor.");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void borraEmpleado_Click(object sender, RoutedEventArgs e)
        {
            Empleado empleado = panelLista.SelectedItem as Empleado;                       

            dataContext.Empleado.DeleteOnSubmit(empleado);

            dataContext.SubmitChanges();

            modEmpleado.Visibility = Visibility.Collapsed;

            panelLista.ItemsSource = dataContext.Empleado;

            panelLista.Columns[0].Visibility = Visibility.Collapsed;
        }
    }
}
