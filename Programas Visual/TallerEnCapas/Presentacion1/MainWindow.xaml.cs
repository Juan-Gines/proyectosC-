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
using Dominio.Modelos;
using Presentacion1.Soporte;
using Presentacion1.ModeloVistas;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ModeloEmpleado empleado = new ModeloEmpleado();
        private ModeloMoldes molde = new ModeloMoldes();
        private ModeloVistaPanel vista = new ModeloVistaPanel();
        private ModeloOt ot = new ModeloOt();
        private ModeloOperacion operacion = new ModeloOperacion();
        private List<ModeloOperacion> listaOperacion = new List<ModeloOperacion>();        
        private int idMoldeOt,idOperacion1, idOperacion2, idOperacion3, idOperacion4, idOperacion5, idOperacion6;
        

        public MainWindow()
        {
            InitializeComponent();
        }

        //METODOS
        private void ListaEmpleados()
        {
            try
            {                
                Panel.ItemsSource = empleado.MostrarEmpleado();                
                Panel.SelectedValuePath = "Id";
                               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ListaMoldes()
        {
            try
            {
                Panel.ItemsSource = molde.MostrarMoldes();                
                Panel.SelectedValuePath = "Id";
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ListaOt()
        {
            try
            {
                Panel.ItemsSource = from ot in ot.MostrarOt()
                                    join mol in molde.MostrarMoldes()
                                    on ot.IdMolde equals mol.Id
                                    where ot.Acabada == false
                                    select new
                                    { ot.Id, ot.IdMolde, ot.Numero, CodigoMolde = mol.Codigo, ot.HorasTrabajo, ot.Tipo, ot.Cliente };
                Panel.SelectedValuePath = "Id";                
                idMoOt.ItemsSource = molde.MostrarMoldes();
                idMoOt.SelectedValuePath = "Id";
                idMoOt.DisplayMemberPath = "Codigo";
                OtOp11Em.ItemsSource = empleado.MostrarEmpleado();
                OtOp11Em.SelectedValuePath = "Id";
                OtOp11Em.DisplayMemberPath = "Nombre";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void ListaOtAcabada()
        {
            try
            {
                Panel.ItemsSource = from ot in ot.MostrarOt()
                                    join mol in molde.MostrarMoldes()
                                    on ot.IdMolde equals mol.Id
                                    where ot.Acabada == true
                                    select new
                                    { ot.Id, ot.IdMolde, ot.Numero, CodigoMolde = mol.Codigo, ot.HorasTrabajo, ot.Tipo, ot.Cliente };
                Panel.SelectedValuePath = "Id";
                Panel.Columns[0].Visibility = Visibility.Collapsed;
                Panel.Columns[1].Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void OcultarPanel()
        {
            panelEmpleado.Visibility = Visibility.Collapsed;
            panelMolde.Visibility = Visibility.Collapsed;
            panelOt.Visibility = Visibility.Collapsed;
        }

        private void OcultarPanelInferior()
        {
            PanelInferior.Visibility = Visibility.Collapsed;
            PanelInferiorDer.Visibility = Visibility.Collapsed;
        }

        private void CambiarTexto()
        {
            switch (vista.Estado)
            {
                case ValidarLista.OT:
                    OpCampo1.Text = "Empleado:";
                    OpCampo2.Text = "Empleado:";
                    OpCampo3.Text = "Empleado:";
                    OpCampo4.Text = "Empleado:";
                    OpCampo5.Text = "Empleado:";
                    OpCampo6.Text = "Empleado:";
                    break;
                case ValidarLista.Empleados:
                    OpCampo1.Text = "Molde:";
                    OpCampo2.Text = "Molde:";
                    OpCampo3.Text = "Molde:";
                    OpCampo4.Text = "Molde:";
                    OpCampo5.Text = "Molde:";
                    OpCampo6.Text = "Molde:";                   
                    break;
            }
        }

        private void RenovarInferior()
        {
            OtOp1Ti.Text = null;
            OtOp1Ho.Text = null;
            OtOp1Em.Text = null;
            OtOp1Es.Text = null;
            OtOp2Ti.Text = null;
            OtOp2Ho.Text = null;
            OtOp2Em.Text = null;
            OtOp2Es.Text = null;
            OtOp3Ti.Text = null;
            OtOp3Ho.Text = null; 
            OtOp3Em.Text = null;
            OtOp3Es.Text = null;
            OtOp4Ti.Text = null;
            OtOp4Ho.Text = null;
            OtOp4Em.Text = null;
            OtOp4Es.Text = null;            
            OtOp5Ti.Text = null;
            OtOp5Ho.Text = null;
            OtOp5Em.Text = null;
            OtOp5Es.Text = null;
            OtOp6Ti.Text = null;
            OtOp6Ho.Text = null;
            OtOp6Em.Text = null;
            OtOp6Es.Text = null;
            OtOp11Ti.Text = null;
            OtOp11Ho.Text = null;
            OtOp11Em.Text = null;
            OtOp11Es.Text = null;
            OtOp1.Background = Brushes.GhostWhite;
            OtOp2.Background = Brushes.GhostWhite;
            OtOp3.Background = Brushes.GhostWhite;
            OtOp4.Background = Brushes.GhostWhite;
            OtOp5.Background = Brushes.GhostWhite;
            OtOp6.Background = Brushes.GhostWhite;
            OtOpBo1.IsEnabled = false;
            OtOpBo2.IsEnabled = false;
            OtOpBo3.IsEnabled = false;
            OtOpBo4.IsEnabled = false;
            OtOpBo5.IsEnabled = false;
            OtOpBo6.IsEnabled = false;
        }

        private void Renovar()
        {
            switch (vista.Estado)
            {
                case ValidarLista.Empleados:
                    panelEmpleado.IsEnabled = false;
                    NoEmpleado.Clear();
                    ApEmpleado.Clear();
                    CaEmpleado.SelectedItem = null;
                    TeEmpleado.Clear();
                    break;
                case ValidarLista.Moldes:
                    panelMolde.IsEnabled = false;
                    coMolde.Clear();
                    clMolde.Clear();
                    chenTaller.IsChecked = false;
                    feMolde.Text = DateTime.Now.ToString();
                    fsMolde.Text = DateTime.Now.ToString();
                    break;
                case ValidarLista.OT:
                    panelOt.IsEnabled = false;
                    nuOt.Clear();
                    idMoOt.SelectedItem = null;
                    tiOt.SelectedItem = null;
                    clOt.Clear();
                    acOt.IsChecked = false;
                    break;
            }
        }
        private void BotBorrarVisible()
        {
            switch (vista.Estado)
            {
                case ValidarLista.OT:
                    OtBoBo1.Visibility = Visibility.Visible;
                    OtBoBo2.Visibility = Visibility.Visible;
                    OtBoBo3.Visibility = Visibility.Visible;
                    OtBoBo4.Visibility = Visibility.Visible;
                    OtBoBo5.Visibility = Visibility.Visible;
                    OtBoBo6.Visibility = Visibility.Visible;
                    break;
                case ValidarLista.Empleados:
                    OtBoBo1.Visibility = Visibility.Collapsed;
                    OtBoBo2.Visibility = Visibility.Collapsed;
                    OtBoBo3.Visibility = Visibility.Collapsed;
                    OtBoBo4.Visibility = Visibility.Collapsed;
                    OtBoBo5.Visibility = Visibility.Collapsed;
                    OtBoBo6.Visibility = Visibility.Collapsed;
                    break;
            }
        }
        private void RellenarInferior()
        {
            switch (vista.Estado)
            {
                case ValidarLista.OT:
                    PanelInferior.Visibility = Visibility.Visible;
                    PanelInferiorDer.Visibility = Visibility.Visible;
                    BtnBorrarOp.IsEnabled = false;
                    BtnGuardarOp.IsEnabled = false;
                    OtOp11.IsEnabled = false;
                    try
                    {
                        if (Panel.SelectedIndex == -1) break;
                        listaOperacion = operacion.MostrarOperacion((int)Panel.SelectedValue);
                        int imd = listaOperacion.Count;
                        int i = 0;
                        while (imd > i)
                        {
                            switch (i)
                            {
                                case 0:
                                    OtOp1Ti.Text = listaOperacion[0].TipoOperacion;
                                    OtOp1Ho.Text = listaOperacion[0].Horas;
                                    if (listaOperacion[0].NEmpleado != 0)
                                    {
                                        OtOp1Em.Text = empleado.MostrarEmpleado(listaOperacion[0].NEmpleado).Nombre;
                                    }
                                    else
                                    {
                                        OtOp1Em.Text = "Sin asignar";
                                    }
                                    OtOp1Es.Text = listaOperacion[0].EstadoOp;
                                    idOperacion1 = listaOperacion[0].Id;
                                    OtOpBo1.IsEnabled = true;
                                    switch (listaOperacion[0].EstadoOp)
                                    {
                                        case "Sin empezar":
                                            OtOp1.Background = Brushes.GhostWhite;
                                            break;
                                        case "Activo":
                                            OtOp1.Background = Brushes.Green;
                                            break;
                                        case "Pausado":
                                            OtOp1.Background = Brushes.Yellow;
                                            break;
                                        case "Finalizado":
                                            OtOp1.Background = Brushes.Red;
                                            break;
                                    }
                                    i++;
                                    break;
                                case 1:
                                    OtOp2Ti.Text = listaOperacion[1].TipoOperacion;
                                    OtOp2Ho.Text = listaOperacion[1].Horas;
                                    if (listaOperacion[1].NEmpleado != 0)
                                    {
                                        OtOp2Em.Text = empleado.MostrarEmpleado(listaOperacion[1].NEmpleado).Nombre;
                                    }
                                    else
                                    {
                                        OtOp2Em.Text = "Sin asignar";
                                    }
                                    OtOp2Es.Text = listaOperacion[1].EstadoOp;
                                    idOperacion2 = listaOperacion[1].Id;
                                    OtOpBo2.IsEnabled = true;
                                    switch (listaOperacion[1].EstadoOp)
                                    {
                                        case "Sin empezar":
                                            OtOp2.Background = Brushes.GhostWhite;
                                            break;
                                        case "Activo":
                                            OtOp2.Background = Brushes.Green;
                                            break;
                                        case "Pausado":
                                            OtOp2.Background = Brushes.Yellow;
                                            break;
                                        case "Finalizado":
                                            OtOp2.Background = Brushes.Red;
                                            break;
                                    }
                                    i++;
                                    break;
                                case 2:
                                    OtOp3Ti.Text = listaOperacion[2].TipoOperacion;
                                    OtOp3Ho.Text = listaOperacion[2].Horas;
                                    if (listaOperacion[2].NEmpleado != 0)
                                    {
                                        OtOp3Em.Text = empleado.MostrarEmpleado(listaOperacion[2].NEmpleado).Nombre;
                                    }
                                    else
                                    {
                                        OtOp3Em.Text = "Sin asignar";
                                    }
                                    OtOp3Es.Text = listaOperacion[2].EstadoOp;
                                    idOperacion3 = listaOperacion[2].Id;
                                    OtOpBo3.IsEnabled = true;
                                    switch (listaOperacion[2].EstadoOp)
                                    {
                                        case "Sin empezar":
                                            OtOp3.Background = Brushes.GhostWhite;
                                            break;
                                        case "Activo":
                                            OtOp3.Background = Brushes.Green;
                                            break;
                                        case "Pausado":
                                            OtOp3.Background = Brushes.Yellow;
                                            break;
                                        case "Finalizado":
                                            OtOp3.Background = Brushes.Red;
                                            break;
                                    }
                                    i++;
                                    break;
                                case 3:
                                    OtOp4Ti.Text = listaOperacion[3].TipoOperacion;
                                    OtOp4Ho.Text = listaOperacion[3].Horas;
                                    if (listaOperacion[3].NEmpleado != 0)
                                    {
                                        OtOp4Em.Text = empleado.MostrarEmpleado(listaOperacion[3].NEmpleado).Nombre;
                                    }
                                    else
                                    {
                                        OtOp4Em.Text = "Sin asignar";
                                    }
                                    OtOp4Es.Text = listaOperacion[3].EstadoOp;
                                    idOperacion4 = listaOperacion[3].Id;
                                    OtOpBo4.IsEnabled = true;
                                    switch (listaOperacion[3].EstadoOp)
                                    {
                                        case "Sin empezar":
                                            OtOp4.Background = Brushes.GhostWhite;
                                            break;
                                        case "Activo":
                                            OtOp4.Background = Brushes.Green;
                                            break;
                                        case "Pausado":
                                            OtOp4.Background = Brushes.Yellow;
                                            break;
                                        case "Finalizado":
                                            OtOp4.Background = Brushes.Red;
                                            break;
                                    }
                                    i++;
                                    break;
                                case 4:
                                    OtOp5Ti.Text = listaOperacion[4].TipoOperacion;
                                    OtOp5Ho.Text = listaOperacion[4].Horas;
                                    if (listaOperacion[4].NEmpleado != 0)
                                    {
                                        OtOp5Em.Text = empleado.MostrarEmpleado(listaOperacion[4].NEmpleado).Nombre;
                                    }
                                    else
                                    {
                                        OtOp5Em.Text = "Sin asignar";
                                    }
                                    OtOp5Es.Text = listaOperacion[4].EstadoOp;
                                    idOperacion5 = listaOperacion[4].Id;
                                    OtOpBo5.IsEnabled = true;
                                    switch (listaOperacion[4].EstadoOp)
                                    {
                                        case "Sin empezar":
                                            OtOp5.Background = Brushes.GhostWhite;
                                            break;
                                        case "Activo":
                                            OtOp5.Background = Brushes.Green;
                                            break;
                                        case "Pausado":
                                            OtOp5.Background = Brushes.Yellow;
                                            break;
                                        case "Finalizado":
                                            OtOp5.Background = Brushes.Red;
                                            break;
                                    }
                                    i++;
                                    break;
                                case 5:
                                    OtOp6Ti.Text = listaOperacion[5].TipoOperacion;
                                    OtOp6Ho.Text = listaOperacion[5].Horas;
                                    if (listaOperacion[5].NEmpleado != 0)
                                    {
                                        OtOp6Em.Text = empleado.MostrarEmpleado(listaOperacion[5].NEmpleado).Nombre;
                                    }
                                    else
                                    {
                                        OtOp6Em.Text = "Sin asignar";
                                    }
                                    OtOp6Es.Text = listaOperacion[5].EstadoOp;
                                    idOperacion6 = listaOperacion[5].Id;
                                    OtOpBo6.IsEnabled = true;
                                    switch (listaOperacion[5].EstadoOp)
                                    {
                                        case "Sin empezar":
                                            OtOp6.Background = Brushes.GhostWhite;
                                            break;
                                        case "Activo":
                                            OtOp6.Background = Brushes.Green;
                                            break;
                                        case "Pausado":
                                            OtOp6.Background = Brushes.Yellow;
                                            break;
                                        case "Finalizado":
                                            OtOp6.Background = Brushes.Red;
                                            break;
                                    }
                                    i++;
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                case ValidarLista.Empleados:

                    PanelInferior.Visibility = Visibility.Visible;
                    try
                    {
                        if (Panel.SelectedIndex == -1) break;                        
                        listaOperacion = operacion.MostrarOperacionEmp((int)Panel.SelectedValue);
                        var listaFiltrada = listaOperacion.FindAll(o => o.EstadoOp != "Finalizado");
                        int imd = listaFiltrada.Count;
                        int i = 0;
                        while (imd > i)
                        {
                            switch (i)
                            {
                                case 0:
                                    OtOp1Ti.Text = listaFiltrada[0].TipoOperacion;
                                    OtOp1Ho.Text = listaFiltrada[0].Horas;                                    
                                    OtOp1Em.Text = molde.MostrarMoldes(ot.MostrarOt(listaFiltrada[0].NumeroOT).IdMolde).Codigo;
                                    OtOp1Es.Text = listaFiltrada[0].EstadoOp;
                                    idOperacion1 = listaFiltrada[0].Id;
                                    OtOpBo1.IsEnabled = true;
                                    switch (listaFiltrada[0].EstadoOp)
                                    {
                                        case "Sin empezar":
                                            OtOp1.Background = Brushes.GhostWhite;
                                            break;
                                        case "Activo":
                                            OtOp1.Background = Brushes.Green;
                                            break;
                                        case "Pausado":
                                            OtOp1.Background = Brushes.Yellow;
                                            break;
                                        case "Finalizado":
                                            OtOp1.Background = Brushes.Red;
                                            break;
                                    }
                                    i++;
                                    break;
                                case 1:
                                    OtOp2Ti.Text = listaFiltrada[1].TipoOperacion;
                                    OtOp2Ho.Text = listaFiltrada[1].Horas;
                                    OtOp2Em.Text = molde.MostrarMoldes(ot.MostrarOt(listaFiltrada[1].NumeroOT).IdMolde).Codigo;
                                    OtOp2Es.Text = listaFiltrada[1].EstadoOp;
                                    idOperacion2 = listaFiltrada[1].Id;
                                    OtOpBo2.IsEnabled = true;
                                    switch (listaFiltrada[1].EstadoOp)
                                    {
                                        case "Sin empezar":
                                            OtOp2.Background = Brushes.GhostWhite;
                                            break;
                                        case "Activo":
                                            OtOp2.Background = Brushes.Green;
                                            break;
                                        case "Pausado":
                                            OtOp2.Background = Brushes.Yellow;
                                            break;
                                        case "Finalizado":
                                            OtOp2.Background = Brushes.Red;
                                            break;
                                    }
                                    i++;
                                    break;
                                case 2:
                                    OtOp3Ti.Text = listaFiltrada[2].TipoOperacion;
                                    OtOp3Ho.Text = listaFiltrada[2].Horas;
                                    OtOp3Em.Text = molde.MostrarMoldes(ot.MostrarOt(listaFiltrada[2].NumeroOT).IdMolde).Codigo;
                                    OtOp3Es.Text = listaFiltrada[2].EstadoOp;
                                    idOperacion3 = listaFiltrada[2].Id;
                                    OtOpBo3.IsEnabled = true;
                                    switch (listaFiltrada[2].EstadoOp)
                                    {
                                        case "Sin empezar":
                                            OtOp3.Background = Brushes.GhostWhite;
                                            break;
                                        case "Activo":
                                            OtOp3.Background = Brushes.Green;
                                            break;
                                        case "Pausado":
                                            OtOp3.Background = Brushes.Yellow;
                                            break;
                                        case "Finalizado":
                                            OtOp3.Background = Brushes.Red;
                                            break;
                                    }
                                    i++;
                                    break;
                                case 3:
                                    OtOp4Ti.Text = listaFiltrada[3].TipoOperacion;
                                    OtOp4Ho.Text = listaFiltrada[3].Horas;
                                    OtOp4Em.Text = molde.MostrarMoldes(ot.MostrarOt(listaFiltrada[3].NumeroOT).IdMolde).Codigo;
                                    OtOp4Es.Text = listaFiltrada[3].EstadoOp;
                                    idOperacion4 = listaFiltrada[4].Id;
                                    OtOpBo4.IsEnabled = true;
                                    switch (listaFiltrada[3].EstadoOp)
                                    {
                                        case "Sin empezar":
                                            OtOp4.Background = Brushes.GhostWhite;
                                            break;
                                        case "Activo":
                                            OtOp4.Background = Brushes.Green;
                                            break;
                                        case "Pausado":
                                            OtOp4.Background = Brushes.Yellow;
                                            break;
                                        case "Finalizado":
                                            OtOp4.Background = Brushes.Red;
                                            break;
                                    }
                                    i++;
                                    break;
                                case 4:
                                    OtOp5Ti.Text = listaFiltrada[4].TipoOperacion;
                                    OtOp5Ho.Text = listaFiltrada[4].Horas;
                                    OtOp5Em.Text = molde.MostrarMoldes(ot.MostrarOt(listaFiltrada[4].NumeroOT).IdMolde).Codigo;
                                    OtOp5Es.Text = listaFiltrada[4].EstadoOp;
                                    idOperacion5 = listaFiltrada[4].Id;
                                    OtOpBo5.IsEnabled = true;
                                    switch (listaFiltrada[4].EstadoOp)
                                    {
                                        case "Sin empezar":
                                            OtOp5.Background = Brushes.GhostWhite;
                                            break;
                                        case "Activo":
                                            OtOp5.Background = Brushes.Green;
                                            break;
                                        case "Pausado":
                                            OtOp5.Background = Brushes.Yellow;
                                            break;
                                        case "Finalizado":
                                            OtOp5.Background = Brushes.Red;
                                            break;
                                    }
                                    i++;
                                    break;
                                case 5:
                                    OtOp6Ti.Text = listaFiltrada[5].TipoOperacion;
                                    OtOp6Ho.Text = listaFiltrada[5].Horas;
                                    OtOp6Em.Text = molde.MostrarMoldes(ot.MostrarOt(listaFiltrada[5].NumeroOT).IdMolde).Codigo;
                                    OtOp6Es.Text = listaFiltrada[5].EstadoOp;
                                    idOperacion6 = listaFiltrada[5].Id;
                                    OtOpBo6.IsEnabled = true;
                                    switch (listaFiltrada[5].EstadoOp)
                                    {
                                        case "Sin empezar":
                                            OtOp6.Background = Brushes.GhostWhite;
                                            break;
                                        case "Activo":
                                            OtOp6.Background = Brushes.Green;
                                            break;
                                        case "Pausado":
                                            OtOp6.Background = Brushes.Yellow;
                                            break;
                                        case "Finalizado":
                                            OtOp6.Background = Brushes.Red;
                                            break;
                                    }
                                    i++;
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                    break;


            }

        }

        private int NuevoMolde(string codigo)
        {


            if (idMoOt.SelectedValue == null && codigo != "")
            {

                molde.Codigo = codigo;
                molde.Cliente = "";
                molde.FechaEntrada = DateTime.Now;
                molde.FechaSalida = DateTime.Now;
                molde.EnTaller = true;

                bool validoMo = new ValidacionDatos(molde).Validar();
                if (validoMo == true)
                {
                    molde.GuardarCambios();
                    var mol = molde.MostrarMoldes().First(m => m.Codigo.Equals(codigo));
                    return mol.Id;
                }
                else return 0;
            }
            else if (idMoOt.SelectedValue == null)
            {
                MessageBox.Show("Introduce un codigo de molde.");
                return 0;
            }
            else return (int)idMoOt.SelectedValue;
        }

        //BOTONES PANEL SUPERIOR    
       
        private void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            switch (vista.Estado)
            {
                case ValidarLista.Empleados:

                    empleado.Nombre = NoEmpleado.Text;
                    empleado.Apellido = ApEmpleado.Text;
                    empleado.Cargo = CaEmpleado.Text;
                    empleado.Telefono = TeEmpleado.Text;

                    bool validoEm = new ValidacionDatos(empleado).Validar();
                    if (validoEm == true)
                    {
                        string resultado = empleado.GuardarCambios();
                        BtnGuardar.IsEnabled = false;
                        MessageBox.Show(resultado);
                        ListaEmpleados();
                        Renovar();
                    }
                    break;
                case ValidarLista.Moldes:

                    molde.Codigo = coMolde.Text;
                    molde.Cliente = clMolde.Text;
                    molde.FechaEntrada = Convert.ToDateTime(feMolde.SelectedDate);
                    molde.FechaSalida = Convert.ToDateTime(fsMolde.SelectedDate);
                    molde.EnTaller = Convert.ToBoolean(chenTaller.IsChecked);

                    bool validoMo = new ValidacionDatos(molde).Validar();
                    if (validoMo == true)
                    {
                        string resultado = molde.GuardarCambios();
                        BtnGuardar.IsEnabled = false;
                        MessageBox.Show(resultado);
                        ListaMoldes();
                        Renovar();
                    }
                    break;
                case ValidarLista.OT:
                    
                    ot.Numero = nuOt.Text;
                    ot.IdMolde = NuevoMolde(idMoOt.Text);
                    ot.HorasTrabajo = 20;
                    ot.Tipo = tiOt.Text;
                    ot.Cliente = clOt.Text;
                    ot.Acabada = Convert.ToBoolean(acOt.IsChecked);
                    if (ot.IdMolde == 0) break;
                    bool validoOt = new ValidacionDatos(ot).Validar();                    
                    if (validoOt == true)
                    {
                        string resultado = ot.GuardarCambios();
                        BtnGuardar.IsEnabled = false;
                        MessageBox.Show(resultado);
                        ListaOt();
                        Renovar();
                    }
                    break;
            }
        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            switch (vista.Estado)
            {
                case ValidarLista.Empleados:
                    if (Panel.SelectedIndex > -1)
                    {
                        panelEmpleado.IsEnabled = true;
                        BtnGuardar.IsEnabled = true;
                        empleado = Panel.SelectedItem as ModeloEmpleado;
                        empleado.Estado = EstadoEntidad.Editado;
                        NoEmpleado.Text = empleado.Nombre;
                        ApEmpleado.Text = empleado.Apellido;
                        CaEmpleado.Text = empleado.Cargo;
                        TeEmpleado.Text = empleado.Telefono;
                    }
                    else MessageBox.Show("Selecciona un empleado");
                    break;
                case ValidarLista.Moldes:
                    if (Panel.SelectedIndex > -1)
                    {

                        panelMolde.IsEnabled = true;
                        BtnGuardar.IsEnabled = true;
                        molde = Panel.SelectedItem as ModeloMoldes;
                        molde.Estado = EstadoEntidad.Editado;
                        coMolde.Text = molde.Codigo;
                        clMolde.Text = molde.Cliente;
                        feMolde.Text = molde.FechaEntrada.ToString();
                        fsMolde.Text = molde.FechaSalida.ToString();
                        chenTaller.IsChecked = molde.EnTaller;
                    }
                    else MessageBox.Show("Selecciona un molde");
                    break;
                case ValidarLista.OT:
                    if (Panel.SelectedIndex > -1)
                    {

                        panelOt.IsEnabled = true;
                        BtnGuardar.IsEnabled = true;
                        ot = ot.MostrarOt((int)Panel.SelectedValue);
                        idMoldeOt = ot.IdMolde;
                        molde = molde.MostrarMoldes(idMoldeOt);
                        molde.Estado = EstadoEntidad.Editado;
                        ot.Estado = EstadoEntidad.Editado;
                        nuOt.Text = ot.Numero;
                        idMoOt.Text = molde.Codigo;
                        tiOt.Text = ot.Tipo;
                        clOt.Text = ot.Cliente;
                        acOt.IsChecked = ot.Acabada;
                    }
                    else MessageBox.Show("Selecciona un molde");
                    break;
            }
        }

        private void BtnNuevo_Click(object sender, RoutedEventArgs e)
        {
            switch (vista.Estado)
            {
                case ValidarLista.Empleados:
                    panelEmpleado.IsEnabled = true;
                    empleado.Estado = EstadoEntidad.Añadido;
                    BtnGuardar.IsEnabled = true;
                    break;
                case ValidarLista.Moldes:
                    panelMolde.IsEnabled = true;
                    molde.Estado = EstadoEntidad.Añadido;
                    BtnGuardar.IsEnabled = true;
                    break;
                case ValidarLista.OT:
                    panelOt.IsEnabled = true;
                    ot.Estado = EstadoEntidad.Añadido;
                    BtnGuardar.IsEnabled = true;
                    break;
            }
        }

        private void BtnBorrar_Click(object sender, RoutedEventArgs e)
        {
            switch (vista.Estado)
            {
                case ValidarLista.Empleados:
                    if (Panel.SelectedIndex > -1)
                    {

                        empleado = Panel.SelectedItem as ModeloEmpleado;
                        empleado.Estado = EstadoEntidad.Borrado;
                        listaOperacion = operacion.MostrarOperacionEmp(empleado.Id);                        
                        foreach(ModeloOperacion item in listaOperacion)
                        {
                            item.Estado = EstadoEntidad.Editado;
                            item.EstadoOp = "Sin empezar";
                            item.NEmpleado = 0;
                            item.GuardarCambios();
                        }
                        string resultado = empleado.GuardarCambios();
                        MessageBox.Show(resultado);
                        ListaEmpleados();
                    }
                    else MessageBox.Show("Selecciona un empleado");
                    break;
                case ValidarLista.Moldes:
                    if (Panel.SelectedIndex > -1)
                    {

                        molde = Panel.SelectedItem as ModeloMoldes;
                        if (ot.MostrarOt().Find(o => o.Id.Equals(molde.Id)) == null)
                        {
                            molde.Estado = EstadoEntidad.Borrado;
                            string resultado = molde.GuardarCambios();
                            MessageBox.Show(resultado);
                            ListaMoldes();
                        }
                        else MessageBox.Show("No puedes borrar un molde si está ligado a una OT");
                        break;
                    }
                    else MessageBox.Show("Selecciona un molde");
                    break;
            }

        }  

        //BOTONES DE VISTAS

        private void BtnListaEmpleados_Click(object sender, RoutedEventArgs e)
        {  
           
            ListaEmpleados();
            vista.Estado = ValidarLista.Empleados;            
            OcultarPanel();
            Renovar();
            RenovarInferior();
            BotBorrarVisible();
            OcultarPanelInferior();            
            CambiarTexto();
            panelEmpleado.Visibility = Visibility.Visible;
            panelEmpleado.IsEnabled = false;
            BtnGuardar.IsEnabled = false;
        }

        private void BtnListaMoldes_Click(object sender, RoutedEventArgs e)
        { 
            
            ListaMoldes();
            vista.Estado = ValidarLista.Moldes;            
            OcultarPanel();
            Renovar();
            RenovarInferior();            
            OcultarPanelInferior();            
            panelMolde.Visibility = Visibility.Visible;
            panelMolde.IsEnabled = false;
            BtnGuardar.IsEnabled = false;

        }

        private void BtnListaOt_Click(object sender, RoutedEventArgs e)
        {        
            
            ListaOt();
            vista.Estado = ValidarLista.OT;            
            OcultarPanel();
            Renovar();
            RenovarInferior();
            BotBorrarVisible();
            OcultarPanelInferior();            
            CambiarTexto();
            panelOt.Visibility = Visibility.Visible;            
            panelOt.IsEnabled = false;
            BtnGuardar.IsEnabled = false;

        }

        private void BtnListaOtAcabada_Click(object sender, RoutedEventArgs e)
        {            
            ListaOtAcabada();
            vista.Estado = ValidarLista.OTFinalizada;
            OcultarPanel();
            Renovar();
            RenovarInferior();
            OcultarPanelInferior();                        
            panelOt.IsEnabled = false;
            BtnGuardar.IsEnabled = false;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ListaEmpleados();
            OcultarPanel();
            vista.Estado = ValidarLista.Empleados;
            panelEmpleado.Visibility = Visibility.Visible;
            panelEmpleado.IsEnabled = false;
            BtnGuardar.IsEnabled = false;
        }

        private void Panel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RenovarInferior();
            RellenarInferior();
            
        }

        //BOTONES PANEL INFERIOR

        private void OtBoAc1_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion1);
            operacion.Estado = EstadoEntidad.Editado;
            operacion.EstadoOp = "Activo";
            string resultado = operacion.GuardarCambios();
            MessageBox.Show(resultado);
            RenovarInferior();
            RellenarInferior();
        }

            private void OtBoPa1_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion1);
            operacion.Estado = EstadoEntidad.Editado;
            operacion.EstadoOp = "Pausado";
            string resultado = operacion.GuardarCambios();
            MessageBox.Show(resultado);
            RenovarInferior();
            RellenarInferior();
        }

        private void OtBoFi1_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion1);
            operacion.Estado = EstadoEntidad.Editado;
            operacion.EstadoOp = "Finalizado";
            string resultado = operacion.GuardarCambios();
            MessageBox.Show(resultado);
            RenovarInferior();
            RellenarInferior();
        }

            private void OtBoBo1_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion1);
            operacion.Estado = EstadoEntidad.Editado;
            BtnGuardarOp.IsEnabled = true;
            BtnBorrarOp.IsEnabled = true;
            OtOp11.IsEnabled = true;
            OtOp11Ti.Text = operacion.TipoOperacion;
            OtOp11Ho.Text = operacion.Horas;
            if (operacion.NEmpleado != 0)
            {
                OtOp11Em.SelectedValue = empleado.MostrarEmpleado(operacion.NEmpleado).Id;
            }
            else
            {
                OtOp11Em.Text = "Sin asignar";
            }
            OtOp11Es.Text = operacion.EstadoOp;
        }       
        

        private void OtBoAc2_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion2);
            operacion.Estado = EstadoEntidad.Editado;
            operacion.EstadoOp = "Activo";
            string resultado = operacion.GuardarCambios();
            MessageBox.Show(resultado);
            RenovarInferior();
            RellenarInferior();
        }

        private void OtBoPa2_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion2);
            operacion.Estado = EstadoEntidad.Editado;
            operacion.EstadoOp = "Pausado";
            string resultado = operacion.GuardarCambios();
            MessageBox.Show(resultado);
            RenovarInferior();
            RellenarInferior();

        }

        private void OtBoFi2_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion3);
            operacion.Estado = EstadoEntidad.Editado;
            operacion.EstadoOp = "Finalizado";
            string resultado = operacion.GuardarCambios();
            MessageBox.Show(resultado);
            RenovarInferior();
            RellenarInferior();
        }

        private void OtBoBo2_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion2);
            operacion.Estado = EstadoEntidad.Editado;
            BtnGuardarOp.IsEnabled = true;
            BtnBorrarOp.IsEnabled = true;
            OtOp11.IsEnabled = true;
            OtOp11Ti.Text = operacion.TipoOperacion;
            OtOp11Ho.Text = operacion.Horas;
            if (operacion.NEmpleado != 0)
            {
                OtOp11Em.SelectedValue = empleado.MostrarEmpleado(operacion.NEmpleado).Id;
            }
            else
            {
                OtOp11Em.Text = "Sin asignar";
            }
            OtOp11Es.Text = operacion.EstadoOp;

        }

        private void OtBoAc3_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion3);
            operacion.Estado = EstadoEntidad.Editado;
            operacion.EstadoOp = "Activo";
            string resultado = operacion.GuardarCambios();
            MessageBox.Show(resultado);
            RenovarInferior();
            RellenarInferior();
        }

        private void OtBoPa3_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion3);
            operacion.Estado = EstadoEntidad.Editado;
            operacion.EstadoOp = "Pausado";
            string resultado = operacion.GuardarCambios();
            MessageBox.Show(resultado);
            RenovarInferior();
            RellenarInferior();
        }

        private void OtBoFi3_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion3);
            operacion.Estado = EstadoEntidad.Editado;
            operacion.EstadoOp = "Finalizado";
            string resultado = operacion.GuardarCambios();
            MessageBox.Show(resultado);
            RenovarInferior();
            RellenarInferior();
        }

        private void OtBoBo3_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion3);
            operacion.Estado = EstadoEntidad.Editado;
            BtnGuardarOp.IsEnabled = true;
            BtnBorrarOp.IsEnabled = true;
            OtOp11.IsEnabled = true;
            OtOp11Ti.Text = operacion.TipoOperacion;
            OtOp11Ho.Text = operacion.Horas;
            if (operacion.NEmpleado != 0)
            {
                OtOp11Em.SelectedValue = empleado.MostrarEmpleado(operacion.NEmpleado).Id;
            }
            else
            {
                OtOp11Em.Text = "Sin asignar";
            }
            OtOp11Es.Text = operacion.EstadoOp;
        }

        private void OtBoAc4_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion4);
            operacion.Estado = EstadoEntidad.Editado;
            operacion.EstadoOp = "Activo";
            string resultado = operacion.GuardarCambios();
            MessageBox.Show(resultado);
            RenovarInferior();
            RellenarInferior();
        }

        private void OtBoPa4_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion4);
            operacion.Estado = EstadoEntidad.Editado;
            operacion.EstadoOp = "Pausado";
            string resultado = operacion.GuardarCambios();
            MessageBox.Show(resultado);
            RenovarInferior();
            RellenarInferior();
        }

        private void OtBoFi4_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion4);
            operacion.Estado = EstadoEntidad.Editado;
            operacion.EstadoOp = "Finalizado";
            string resultado = operacion.GuardarCambios();
            MessageBox.Show(resultado);
            RenovarInferior();
            RellenarInferior();

        }

        private void OtBoBo4_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion4);
            operacion.Estado = EstadoEntidad.Editado;
            BtnGuardarOp.IsEnabled = true;
            BtnBorrarOp.IsEnabled = true;
            OtOp11.IsEnabled = true;
            OtOp11Ti.Text = operacion.TipoOperacion;
            OtOp11Ho.Text = operacion.Horas;
            if (operacion.NEmpleado != 0)
            {
                OtOp11Em.SelectedValue = empleado.MostrarEmpleado(operacion.NEmpleado).Id;
            }
            else
            {
                OtOp11Em.Text = "Sin asignar";
            }
            OtOp11Es.Text = operacion.EstadoOp;
        }

        private void OtBoAc5_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion5);
            operacion.Estado = EstadoEntidad.Editado;
            operacion.EstadoOp = "Activo";
            string resultado = operacion.GuardarCambios();
            MessageBox.Show(resultado);
            RenovarInferior();
            RellenarInferior();

        }

        private void OtBoPa5_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion5);
            operacion.Estado = EstadoEntidad.Editado;
            operacion.EstadoOp = "Pausado";
            string resultado = operacion.GuardarCambios();
            MessageBox.Show(resultado);
            RellenarInferior();
        }

        private void OtBoFi5_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion5);
            operacion.Estado = EstadoEntidad.Editado;
            operacion.EstadoOp = "Finalizado";
            string resultado = operacion.GuardarCambios();
            MessageBox.Show(resultado);
            RenovarInferior();
            RellenarInferior();

        }

        private void OtBoBo5_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion5);
            operacion.Estado = EstadoEntidad.Editado;
            BtnGuardarOp.IsEnabled = true;
            BtnBorrarOp.IsEnabled = true;
            OtOp11.IsEnabled = true;
            OtOp11Ti.Text = operacion.TipoOperacion;
            OtOp11Ho.Text = operacion.Horas;
            if (operacion.NEmpleado != 0)
            {
                OtOp11Em.SelectedValue = empleado.MostrarEmpleado(operacion.NEmpleado).Id;
            }
            else
            {
                OtOp11Em.Text = "Sin asignar";
            }
            OtOp11Es.Text = operacion.EstadoOp;

        }

        private void OtBoAc6_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion6);
            operacion.Estado = EstadoEntidad.Editado;
            operacion.EstadoOp = "Activo";
            string resultado = operacion.GuardarCambios();
            MessageBox.Show(resultado);
            RenovarInferior();
            RellenarInferior();
        }

        private void OtBoPa6_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion6);
            operacion.Estado = EstadoEntidad.Editado;
            operacion.EstadoOp = "Pausado";
            string resultado = operacion.GuardarCambios();
            MessageBox.Show(resultado);
            RenovarInferior();
            RellenarInferior();

        }

        private void OtBoFi6_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion6);
            operacion.Estado = EstadoEntidad.Editado;
            operacion.EstadoOp = "Finalizado";
            string resultado = operacion.GuardarCambios();
            MessageBox.Show(resultado);
            RenovarInferior();
            RellenarInferior();
        }

        private void OtBoBo6_Click(object sender, RoutedEventArgs e)
        {
            operacion = operacion.Mostrar1Operacion(idOperacion6);
            operacion.Estado = EstadoEntidad.Editado;
            BtnGuardarOp.IsEnabled = true;
            BtnBorrarOp.IsEnabled = true;
            OtOp11.IsEnabled = true;
            OtOp11Ti.Text = operacion.TipoOperacion;
            OtOp11Ho.Text = operacion.Horas;
            if (operacion.NEmpleado != 0)
            {
                OtOp11Em.SelectedValue = empleado.MostrarEmpleado(operacion.NEmpleado).Id;
            }
            else
            {
                OtOp11Em.Text = "Sin asignar";
            }
            OtOp11Es.Text = operacion.EstadoOp;           

        }
        private void BtnGuardarOp_Click(object sender, RoutedEventArgs e)
        {
            switch (operacion.Estado)
            {
                case EstadoEntidad.Añadido:
                    operacion.TipoOperacion = OtOp11Ti.Text;
                    operacion.Horas = OtOp11Ho.Text;
                    operacion.NEmpleado = 0;
                    operacion.EstadoOp = "Sin empezar";
                    operacion.NumeroOT = (int)Panel.SelectedValue;
                    int val = operacion.MostrarOperacion(operacion.NumeroOT).Count;
                    if (val > 5)
                    {
                        MessageBox.Show("No puedes guardar más de 6 operaciones por molde");
                        break;
                    }
                    else
                    {
                        bool validoOp = new ValidacionDatos(operacion).Validar();
                        if (validoOp == true)
                        {
                            string resultado = operacion.GuardarCambios();
                            BtnGuardar.IsEnabled = false;
                            MessageBox.Show(resultado);
                            RenovarInferior();
                            RellenarInferior();
                        }
                        break;
                    }
                case EstadoEntidad.Editado:
                    operacion.TipoOperacion = OtOp11Ti.Text;
                    operacion.Horas = OtOp11Ho.Text;
                    if (OtOp11Em.Text == "Sin asignar")
                    {
                        MessageBox.Show("Por favor elija un empleado");
                        break;
                    }
                    else
                    {
                        operacion.NEmpleado = (int)OtOp11Em.SelectedValue;
                    }
                        operacion.EstadoOp = OtOp11Es.Text;
                        var listaOp = operacion.MostrarOperacionEmp((int)OtOp11Em.SelectedValue);
                        var listaFiltrada = listaOp.FindAll(o => o.EstadoOp != "Finalizado");
                        if (listaFiltrada.Count > 5)
                            
                    {
                        MessageBox.Show("No puedes guardar más de 6 operaciones por empleado");
                        break;
                    }
                    bool validoOp2 = new ValidacionDatos(operacion).Validar();
                    if (validoOp2 == true)
                    {
                        string resultado = operacion.GuardarCambios();
                        BtnGuardar.IsEnabled = false;
                        MessageBox.Show(resultado);
                        RenovarInferior();
                        RellenarInferior();
                        
                    }
                    break;
            }
        }
        private void BtnNuevaOp_Click(object sender, RoutedEventArgs e)
        {
            operacion.Estado = EstadoEntidad.Añadido;
            OtOp11.IsEnabled = true;
            BtnGuardarOp.IsEnabled = true;
        }
        private void BtnBorrarOp_Click(object sender, RoutedEventArgs e)
        {
            operacion.Estado = EstadoEntidad.Borrado;
            string resultado = operacion.GuardarCambios();
            MessageBox.Show(resultado);
            RenovarInferior();
            RellenarInferior();

        }

    }
}
