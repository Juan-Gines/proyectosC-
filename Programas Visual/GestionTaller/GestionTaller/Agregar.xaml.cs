using System;
using System.Configuration;
using System.Windows;
using System.Windows.Input;
using System.Text.RegularExpressions;

namespace GestionTaller
{
    /// <summary>
    /// Lógica de interacción para Agregar.xaml
    /// </summary>
    namespace GestionTaller
    {
        using System.Configuration;
        using System.Windows;

        /// <summary>
        /// Lógica de interacción para Agregar.xaml
        /// </summary>
        public partial class Agregar : Window
        {
            using GestionTaller.Data;

public class Agregar
{
    DataClasses1DataContext dataContext;

    public Agregar()
    {
        InitializeComponent();

        string miConexionSql = ConfigurationManager.ConnectionStrings["GestionTaller.Properties.Settings.ConexionGestionTaller"].ConnectionString;

        dataContext = new DataClasses1DataContext(miConexionSql);

        string[] cargo = new[] { "Ajustador", "Encargado", "Fresador", "Ingeniero", "Diseñador" };

        agrCargo.ItemsSource = cargo;
    }
           
 public Agregar()
    {
        InitializeComponent();

        string miConexionSql = ConfigurationManager.ConnectionStrings["GestionTaller.Properties.Settings.ConexionGestionTaller"].ConnectionString;

        dataContext = new DataClasses1DataContext(miConexionSql);

        string[] cargo = new[] { "Ajustador", "Encargado", "Fresador", "Ingeniero", "Diseñador" };

        agrCargo.ItemsSource = cargo;

    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            if (agrNombre.Text != "" && agrCargo.Text != "")
            {

                RegexStringValidator miPatron = new RegexStringValidator(@"^\d{9}");

                try
                {
                    miPatron.Validate(agrTelefono.Text);

                    Empleado nuevoEmpleado = new Empleado { Nombre = agrNombre.Text, Apellido = agrApellido.Text, Cargo = agrCargo.Text, Telefono = agrTelefono.Text };

                    dataContext.Empleado.InsertOnSubmit(nuevoEmpleado);

                    dataContext.SubmitChanges();

                    this.Close();

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

    private void agrTelefono_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
    }

    private void agrNombre_PreviewTextInput(object sender, TextCompositionEventArgs e)
    {
        e.Handled = new Regex("[^a-zA-Zà-ùá-ú]+").IsMatch(e.Text);
    }

   
}
        }
    }
