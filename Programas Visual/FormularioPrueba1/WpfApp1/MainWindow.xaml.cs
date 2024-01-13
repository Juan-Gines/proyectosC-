using Dominio.Modelos;
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
using Dominio.ValoresObjetos;


namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ModeloEmpleado empleado = new ModeloEmpleado();
        private ModeloMoldes moldes = new ModeloMoldes();
        private ModeloOperacion operacion = new ModeloOperacion();
        private ModeloOT ot = new ModeloOT();

        private EstadoPantalla EstadoPantalla { get; set; }

        private Button botonActual = new Button();

        public MainWindow()
        {
            InitializeComponent();

        }

        //METODOS
        private void BotonActivo(object senderBtn)
        {
            if (senderBtn != null)
            {
                botonActual.IsDefault = false;
                botonActual = (Button)senderBtn;
                botonActual.IsDefault = true;
            }
        }

        private void OcultarPanel()
        {
            if (panelDataGrid.IsVisible == true)
            {
                panelDataGrid.Visibility = Visibility.Collapsed;
                panelBtnIzq.Visibility = Visibility.Collapsed;
            }
        }

        private void MostrarPanel()
        {
            if (panelDataGrid.IsVisible == false)
            {
                panelDataGrid.Visibility = Visibility.Visible;
                panelBtnIzq.Visibility = Visibility.Visible;
            }
        }

        //BOTONES INFORMACION DEL DATAGRID

        private void BtnOt_Click(object sender, RoutedEventArgs e)
        {
            BotonActivo(sender);
            iconTitulo.Icon = FontAwesome.Sharp.IconChar.ClipboardList;
            titulo.Text = "OT";
            EstadoPantalla = EstadoPantalla.OT;
            MostrarPanel();
            var query = from o in ot.MostrarOT().Where(o => o.Acabada.Equals(false))
                        join m in moldes.MostrarMoldes() on o.IdMolde equals m.Id
                        select new { o.Id, o.IdMolde, o.Numero, m.Codigo, o.HorasTrabajo, o.Tipo, o.ClienteOT };
            panelDataGrid.ItemsSource = query;
            panelDataGrid.SelectedValuePath = "Id";            
        }

        private void BtnEmpleados_Click(object sender, RoutedEventArgs e)
        {
            BotonActivo(sender);
            iconTitulo.Icon = FontAwesome.Sharp.IconChar.Users;
            titulo.Text = "Empleados";
            EstadoPantalla = EstadoPantalla.Empleados;
            MostrarPanel();
            var query= empleado.MostrarEmpleado().OrderBy(o => o.Nombre);
            panelDataGrid.ItemsSource = query;            
            panelDataGrid.SelectedValuePath = "Id";
        }

        private void BtnMoldes_Click(object sender, RoutedEventArgs e)
        {
            BotonActivo(sender);
            iconTitulo.Icon = FontAwesome.Sharp.IconChar.DiceD6;
            titulo.Text = "Moldes";
            EstadoPantalla = EstadoPantalla.Moldes;
            MostrarPanel();
            panelDataGrid.ItemsSource = moldes.MostrarMoldes().OrderBy(o => o.Codigo);            
        }

        private void BtnOtAcabada_Click(object sender, RoutedEventArgs e)
        {
            BotonActivo(sender);
            iconTitulo.Icon = FontAwesome.Sharp.IconChar.ClipboardCheck;
            titulo.Text = "OT Finalizadas";
            EstadoPantalla = EstadoPantalla.OTfinalizada;
            MostrarPanel();
            var query = from o in ot.MostrarOT().Where(o => o.Acabada.Equals(true))
                        join m in moldes.MostrarMoldes() on o.IdMolde equals m.Id
                        select new { o.Id, o.IdMolde, o.Numero, m.Codigo, o.HorasTrabajo, o.Tipo, o.ClienteOT };
            panelDataGrid.ItemsSource = query;            
        }

        private void HomeClick(object sender, MouseButtonEventArgs e)
        {
            OcultarPanel();
            botonActual.IsDefault = false;
            iconTitulo.Icon = FontAwesome.Sharp.IconChar.Home;
            titulo.Text = "Home";
        }
        //BOTONES BARRA DE TITULO

        private void barraTitulo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void BtnMaximizar_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal) WindowState = WindowState.Maximized;
            else WindowState = WindowState.Normal;
        }
        private void BtnMinimizar_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void barraTitulo_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Normal) WindowState = WindowState.Maximized;
            else WindowState = WindowState.Normal;
        }
        //EVENTOS

        private void panelDataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OcultarPanel();
        }
        //BOTONES DE DERECHA DEL PANEL PRINCIPAL

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {

        }

    }
}
