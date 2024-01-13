using Dominio.Modelos;
using Dominio.ValoresObjetos;
using PruebaDefinitiva.Manager;
using PruebaDefinitiva.Soporte;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using WsMosca.Modelos;

namespace PruebaDefinitiva
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OTManager ManagerOT = new OTManager();
        private OperacionManager ManagerOperacion = new OperacionManager();
        private EmpleadoManager ManagerEmpleado = new EmpleadoManager();
        private MoldesManager ManagerMolde = new MoldesManager();
        private ModeloEmpleado Empleado = new ModeloEmpleado();
        private ModeloMoldes moldes = new ModeloMoldes();
        private List<ModeloOperacion> listaOperacion = new List<ModeloOperacion>();
        private ModeloOperacion operacion = new ModeloOperacion();
        private ModeloOT ot = new ModeloOT();
        private int idSeleccionada;
        private string mensaje;

        private EstadoPantalla EstadoPantalla { get; set; }


        private Button botonActual = new Button();
        private int idOpeGuar;
        private int numOperE;

        public MainWindow()
        {
            InitializeComponent();

        }

        //METODOS

        //METODOS LISTAS

        private void BotonActivo(object senderBtn)
        {
            if (senderBtn != null)
            {
                botonActual.IsDefault = false;
                botonActual = (Button)senderBtn;
                botonActual.IsDefault = true;
            }
        }

        private void ListaOT()
        {
            var query = from o in ManagerOT.MostrarOT().Where(o => o.Acabada.Equals(false))
                        join m in ManagerMolde.MostrarMoldes() on o.IdMolde equals m.Id
                        orderby o.Numero
                        select new { o.Id, o.IdMolde, Número = o.Numero, Código = m.Codigo, Horas_totales = o.HorasTrabajo, o.Tipo, Cliente = o.ClienteOT };
            panelDataGrid.ItemsSource = query;
            panelDataGrid.SelectedValuePath = "Id";
            if (panelDataGrid.Columns.Count > 0)
            {
                panelDataGrid.Columns[0].Visibility = Visibility.Collapsed;
                panelDataGrid.Columns[1].Visibility = Visibility.Collapsed;
            }
        }

        private void ListaEmpleados()
        {
            var query = ManagerEmpleado.MostrarEmpleado().Where(e => e.Id != 38).OrderBy(o => o.Nombre);
            panelDataGrid.ItemsSource = query;
            panelDataGrid.SelectedValuePath = "Id";
            if (panelDataGrid.Columns.Count > 0)
            {
                panelDataGrid.Columns[0].Visibility = Visibility.Collapsed;
            }
        }

        private void ListaMoldes()
        {
            panelDataGrid.ItemsSource = ManagerMolde.MostrarMoldes().OrderByDescending(o => o.EnTaller).ThenBy(o => o.Codigo);
            panelDataGrid.SelectedValuePath = "Id";
            if (panelDataGrid.Columns.Count > 0)
            {
                panelDataGrid.Columns[0].Visibility = Visibility.Collapsed;
            }
        }

        private void ListaOTAcabada()
        {
            var query = from o in ManagerOT.MostrarOT().Where(o => o.Acabada.Equals(true))
                        join m in ManagerMolde.MostrarMoldes() on o.IdMolde equals m.Id
                        orderby o.Numero
                        select new { o.Id, o.IdMolde, Número = o.Numero, Código = m.Codigo, Horas_totales = o.HorasTrabajo, o.Tipo, Cliente = o.ClienteOT };
            panelDataGrid.ItemsSource = query;
            panelDataGrid.SelectedValuePath = "Id";
            if (panelDataGrid.Columns.Count > 0)
            {
                panelDataGrid.Columns[0].Visibility = Visibility.Collapsed;
                panelDataGrid.Columns[1].Visibility = Visibility.Collapsed;
            }
        }

        //PANEL PRINCIPAL

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

        //PANELES SECUNDARIOS

        private void OcultarPanelEditar()
        {
            switch (EstadoPantalla)
            {
                case EstadoPantalla.NuevaOt:
                    tituloOT.Visibility = Visibility.Collapsed;
                    panelOT.Visibility = Visibility.Collapsed;
                    panelEditaOperaciones.Visibility = Visibility.Collapsed;
                    BtnCerrarOT.Visibility = Visibility.Collapsed;
                    break;
                case EstadoPantalla.NuevaMoldes:
                    tituloMoldes.Visibility = Visibility.Collapsed;
                    panelMoldes.Visibility = Visibility.Collapsed;
                    break;
                case EstadoPantalla.NuevaEmpleados:
                    tituloEmpleados.Visibility = Visibility.Collapsed;
                    panelEmpleados.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void MostrarPanelOT()
        {
            comboMolde.ItemsSource = ManagerMolde.MostrarMoldes();
            comboMolde.SelectedValuePath = "Id";
            comboMolde.DisplayMemberPath = "Codigo";
            operacionEmpleado.ItemsSource = ManagerEmpleado.MostrarEmpleado();
            operacionEmpleado.SelectedValuePath = "Id";
            operacionEmpleado.DisplayMemberPath = "Nombre";
            switch (ot.Estado)
            {
                case EstadoEntidad.Insertado:
                    tituloOT.Visibility = Visibility.Visible;
                    panelOT.Visibility = Visibility.Visible;
                    BtnNuevaOT.Visibility = Visibility.Visible;
                    BtnCerrarOT.Visibility = Visibility.Visible;
                    panelOT.IsEnabled = true;
                    break;
                case EstadoEntidad.Actualizado:
                    tituloOT.Visibility = Visibility.Visible;
                    panelOT.Visibility = Visibility.Visible;
                    BtnNuevaOT.Visibility = Visibility.Collapsed;
                    panelEditaOperaciones.Visibility = Visibility.Visible;
                    BtnCerrarOT.Visibility = Visibility.Visible;
                    tablaOT.IsEnabled = false;
                    tablaOperaciones.IsEnabled = false;
                    break;
            }
        }

        private void CalculoHoras(List<ModeloOperacion> list)
        {
            ot.HorasTrabajo = 0;
            for (int i = 0; i < list.Count; i++)
            {
                ot.HorasTrabajo += list[i].Horas;
                ot.Estado = EstadoEntidad.Actualizado;
                ManagerOT.GuardarCambios(ot);
            }
        }

        private void MostrarPanelEmpleado()
        {
            switch (Empleado.Estado)
            {
                case EstadoEntidad.Insertado:
                    tituloEmpleados.Visibility = Visibility.Visible;
                    panelEmpleados.Visibility = Visibility.Visible;
                    BtnNuevoEmpleado.Visibility = Visibility.Visible;
                    break;
                case EstadoEntidad.Actualizado:
                    tituloEmpleados.Visibility = Visibility.Visible;
                    panelEmpleados.Visibility = Visibility.Visible;
                    BtnNuevoEmpleado.Visibility = Visibility.Collapsed;
                    tablaEmpleado.IsEnabled = false;
                    tablaOperaciones.IsEnabled = false;
                    break;
            }
        }

        private void MostrarPanelMolde()
        {
            switch (moldes.Estado)
            {
                case EstadoEntidad.Insertado:
                    tituloMoldes.Visibility = Visibility.Visible;
                    panelMoldes.Visibility = Visibility.Visible;
                    BtnNuevoMolde.Visibility = Visibility.Visible;
                    break;
                case EstadoEntidad.Actualizado:
                    tituloMoldes.Visibility = Visibility.Visible;
                    panelMoldes.Visibility = Visibility.Visible;
                    BtnNuevoMolde.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void MostrarPanelEditar()
        {
            OcultarPanel();
            switch (EstadoPantalla)
            {
                case EstadoPantalla.NuevaOt:
                    MostrarPanelOT();
                    break;
                case EstadoPantalla.NuevaEmpleados:
                    MostrarPanelEmpleado();
                    break;
                case EstadoPantalla.NuevaMoldes:
                    MostrarPanelMolde();
                    break;
            }
        }

        private void LimpiarPanelOperaciones()
        {
            if (panOperacion1.IsVisible == true) panOperacion1.Visibility = Visibility.Collapsed;
            if (panOperacion2.IsVisible == true) panOperacion2.Visibility = Visibility.Collapsed;
            if (panOperacion3.IsVisible == true) panOperacion3.Visibility = Visibility.Collapsed;
            if (panOperacion4.IsVisible == true) panOperacion4.Visibility = Visibility.Collapsed;
            if (panOperacion5.IsVisible == true) panOperacion5.Visibility = Visibility.Collapsed;
            if (panOperacion6.IsVisible == true) panOperacion6.Visibility = Visibility.Collapsed;
            if (panOperacion7.IsVisible == true) panOperacion7.Visibility = Visibility.Collapsed;
            if (panOperacion8.IsVisible == true) panOperacion8.Visibility = Visibility.Collapsed;
            if (panOperacion9.IsVisible == true) panOperacion9.Visibility = Visibility.Collapsed;
        }

        private void MostrarPanelOperaciones(List<ModeloOperacion> lista)
        {
            if (lista.Count > 0)
            {
                switch (EstadoPantalla)
                {
                    case EstadoPantalla.NuevaEmpleados:

                        try
                        {
                            int imd = lista.Count;
                            int i = 0;
                            while (imd > i)
                            {
                                switch (i)
                                {
                                    case 0:
                                        panOperacion1.Visibility = Visibility.Visible;
                                        panOperacion1.DataContext = lista[0];
                                        margenOp1.DataContext = ManagerMolde.MostrarMolde(ManagerOT.MostrarOT(lista[0].NumeroOT).IdMolde).Codigo;
                                        switch (listaOperacion[0].EstadoOp)
                                        {
                                            case "Sin empezar":
                                                tablaOper1.Background = Brushes.GhostWhite;
                                                break;
                                            case "Activo":
                                                tablaOper1.Background = Brushes.Green;
                                                break;
                                            case "Pausado":
                                                tablaOper1.Background = Brushes.Yellow;
                                                break;
                                            case "Finalizado":
                                                tablaOper1.Background = Brushes.Red;
                                                break;
                                        }
                                        i++;
                                        break;
                                    case 1:
                                        panOperacion2.Visibility = Visibility.Visible;
                                        panOperacion2.DataContext = lista[1];
                                        margenOp2.DataContext = ManagerMolde.MostrarMolde(ManagerOT.MostrarOT(lista[1].NumeroOT).IdMolde).Codigo;
                                        switch (listaOperacion[1].EstadoOp)
                                        {
                                            case "Sin empezar":
                                                tablaOper2.Background = Brushes.GhostWhite;
                                                break;
                                            case "Activo":
                                                tablaOper2.Background = Brushes.Green;
                                                break;
                                            case "Pausado":
                                                tablaOper2.Background = Brushes.Yellow;
                                                break;
                                            case "Finalizado":
                                                tablaOper2.Background = Brushes.Red;
                                                break;
                                        }
                                        i++;
                                        break;
                                    case 2:
                                        panOperacion3.Visibility = Visibility.Visible;
                                        panOperacion3.DataContext = lista[2];
                                        margenOp3.DataContext = ManagerMolde.MostrarMolde(ManagerOT.MostrarOT(lista[2].NumeroOT).IdMolde).Codigo;
                                        switch (listaOperacion[2].EstadoOp)
                                        {
                                            case "Sin empezar":
                                                tablaOper3.Background = Brushes.GhostWhite;
                                                break;
                                            case "Activo":
                                                tablaOper3.Background = Brushes.Green;
                                                break;
                                            case "Pausado":
                                                tablaOper3.Background = Brushes.Yellow;
                                                break;
                                            case "Finalizado":
                                                tablaOper3.Background = Brushes.Red;
                                                break;
                                        }
                                        i++;
                                        break;
                                    case 3:
                                        panOperacion4.Visibility = Visibility.Visible;
                                        panOperacion4.DataContext = lista[3];
                                        margenOp4.DataContext = ManagerMolde.MostrarMolde(ManagerOT.MostrarOT(lista[3].NumeroOT).IdMolde).Codigo;
                                        switch (listaOperacion[3].EstadoOp)
                                        {
                                            case "Sin empezar":
                                                tablaOper4.Background = Brushes.GhostWhite;
                                                break;
                                            case "Activo":
                                                tablaOper4.Background = Brushes.Green;
                                                break;
                                            case "Pausado":
                                                tablaOper4.Background = Brushes.Yellow;
                                                break;
                                            case "Finalizado":
                                                tablaOper4.Background = Brushes.Red;
                                                break;
                                        }
                                        i++;
                                        break;
                                    case 4:
                                        panOperacion5.Visibility = Visibility.Visible;
                                        panOperacion5.DataContext = lista[4];
                                        margenOp5.DataContext = ManagerMolde.MostrarMolde(ManagerOT.MostrarOT(lista[4].NumeroOT).IdMolde).Codigo;
                                        switch (listaOperacion[4].EstadoOp)
                                        {
                                            case "Sin empezar":
                                                tablaOper5.Background = Brushes.GhostWhite;
                                                break;
                                            case "Activo":
                                                tablaOper5.Background = Brushes.Green;
                                                break;
                                            case "Pausado":
                                                tablaOper5.Background = Brushes.Yellow;
                                                break;
                                            case "Finalizado":
                                                tablaOper5.Background = Brushes.Red;
                                                break;
                                        }
                                        i++;
                                        break;
                                    case 5:
                                        panOperacion6.Visibility = Visibility.Visible;
                                        panOperacion6.DataContext = lista[5];
                                        margenOp6.DataContext = ManagerMolde.MostrarMolde(ManagerOT.MostrarOT(lista[5].NumeroOT).IdMolde).Codigo;
                                        switch (listaOperacion[5].EstadoOp)
                                        {
                                            case "Sin empezar":
                                                tablaOper6.Background = Brushes.GhostWhite;
                                                break;
                                            case "Activo":
                                                tablaOper6.Background = Brushes.Green;
                                                break;
                                            case "Pausado":
                                                tablaOper6.Background = Brushes.Yellow;
                                                break;
                                            case "Finalizado":
                                                tablaOper6.Background = Brushes.Red;
                                                break;
                                        }
                                        i++;
                                        break;
                                    case 6:
                                        panOperacion7.Visibility = Visibility.Visible;
                                        panOperacion7.DataContext = lista[6];
                                        margenOp7.DataContext = ManagerMolde.MostrarMolde(ManagerOT.MostrarOT(lista[6].NumeroOT).IdMolde).Codigo;
                                        switch (listaOperacion[6].EstadoOp)
                                        {
                                            case "Sin empezar":
                                                tablaOper7.Background = Brushes.GhostWhite;
                                                break;
                                            case "Activo":
                                                tablaOper7.Background = Brushes.Green;
                                                break;
                                            case "Pausado":
                                                tablaOper7.Background = Brushes.Yellow;
                                                break;
                                            case "Finalizado":
                                                tablaOper7.Background = Brushes.Red;
                                                break;
                                        }
                                        i++;
                                        break;
                                    case 7:
                                        panOperacion8.Visibility = Visibility.Visible;
                                        panOperacion8.DataContext = lista[7];
                                        margenOp8.DataContext = ManagerMolde.MostrarMolde(ManagerOT.MostrarOT(lista[7].NumeroOT).IdMolde).Codigo;
                                        switch (listaOperacion[7].EstadoOp)
                                        {
                                            case "Sin empezar":
                                                tablaOper8.Background = Brushes.GhostWhite;
                                                break;
                                            case "Activo":
                                                tablaOper8.Background = Brushes.Green;
                                                break;
                                            case "Pausado":
                                                tablaOper8.Background = Brushes.Yellow;
                                                break;
                                            case "Finalizado":
                                                tablaOper8.Background = Brushes.Red;
                                                break;
                                        }
                                        i++;
                                        break;
                                    case 8:
                                        panOperacion9.Visibility = Visibility.Visible;
                                        panOperacion9.DataContext = lista[8];
                                        margenOp9.DataContext = ManagerMolde.MostrarMolde(ManagerOT.MostrarOT(lista[8].NumeroOT).IdMolde).Codigo;
                                        switch (listaOperacion[8].EstadoOp)
                                        {
                                            case "Sin empezar":
                                                tablaOper9.Background = Brushes.GhostWhite;
                                                break;
                                            case "Activo":
                                                tablaOper9.Background = Brushes.Green;
                                                break;
                                            case "Pausado":
                                                tablaOper9.Background = Brushes.Yellow;
                                                break;
                                            case "Finalizado":
                                                tablaOper9.Background = Brushes.Red;
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
                    case EstadoPantalla.NuevaOt:

                        try
                        {
                            int imd = lista.Count;
                            int i = 0;
                            while (imd > i)
                            {
                                switch (i)
                                {
                                    case 0:
                                        panOperacion1.Visibility = Visibility.Visible;
                                        panOperacion1.DataContext = lista[0];
                                        margenOp1.DataContext = ManagerEmpleado.MostrarEmpleado(lista[0].NuEmpleado).Nombre;
                                        switch (listaOperacion[0].EstadoOp)
                                        {
                                            case "Sin empezar":
                                                tablaOper1.Background = Brushes.GhostWhite;
                                                break;
                                            case "Activo":
                                                tablaOper1.Background = Brushes.Green;
                                                break;
                                            case "Pausado":
                                                tablaOper1.Background = Brushes.Yellow;
                                                break;
                                            case "Finalizado":
                                                tablaOper1.Background = Brushes.Red;
                                                break;
                                        }
                                        i++;
                                        break;
                                    case 1:
                                        panOperacion2.Visibility = Visibility.Visible;
                                        panOperacion2.DataContext = lista[1];
                                        margenOp2.DataContext = ManagerEmpleado.MostrarEmpleado(lista[1].NuEmpleado).Nombre;
                                        switch (listaOperacion[1].EstadoOp)
                                        {
                                            case "Sin empezar":
                                                tablaOper2.Background = Brushes.GhostWhite;
                                                break;
                                            case "Activo":
                                                tablaOper2.Background = Brushes.Green;
                                                break;
                                            case "Pausado":
                                                tablaOper2.Background = Brushes.Yellow;
                                                break;
                                            case "Finalizado":
                                                tablaOper2.Background = Brushes.Red;
                                                break;
                                        }
                                        i++;
                                        break;
                                    case 2:
                                        panOperacion3.Visibility = Visibility.Visible;
                                        panOperacion3.DataContext = lista[2];
                                        margenOp3.DataContext = ManagerEmpleado.MostrarEmpleado(lista[2].NuEmpleado).Nombre;
                                        switch (listaOperacion[2].EstadoOp)
                                        {
                                            case "Sin empezar":
                                                tablaOper3.Background = Brushes.GhostWhite;
                                                break;
                                            case "Activo":
                                                tablaOper3.Background = Brushes.Green;
                                                break;
                                            case "Pausado":
                                                tablaOper3.Background = Brushes.Yellow;
                                                break;
                                            case "Finalizado":
                                                tablaOper3.Background = Brushes.Red;
                                                break;
                                        }
                                        i++;
                                        break;
                                    case 3:
                                        panOperacion4.Visibility = Visibility.Visible;
                                        panOperacion4.DataContext = lista[3];
                                        margenOp4.DataContext = ManagerEmpleado.MostrarEmpleado(lista[3].NuEmpleado).Nombre;
                                        switch (listaOperacion[3].EstadoOp)
                                        {
                                            case "Sin empezar":
                                                tablaOper4.Background = Brushes.GhostWhite;
                                                break;
                                            case "Activo":
                                                tablaOper4.Background = Brushes.Green;
                                                break;
                                            case "Pausado":
                                                tablaOper4.Background = Brushes.Yellow;
                                                break;
                                            case "Finalizado":
                                                tablaOper4.Background = Brushes.Red;
                                                break;
                                        }
                                        i++;
                                        break;
                                    case 4:
                                        panOperacion5.Visibility = Visibility.Visible;
                                        panOperacion5.DataContext = lista[4];
                                        margenOp5.DataContext = ManagerEmpleado.MostrarEmpleado(lista[4].NuEmpleado).Nombre;
                                        switch (listaOperacion[4].EstadoOp)
                                        {
                                            case "Sin empezar":
                                                tablaOper5.Background = Brushes.GhostWhite;
                                                break;
                                            case "Activo":
                                                tablaOper5.Background = Brushes.Green;
                                                break;
                                            case "Pausado":
                                                tablaOper5.Background = Brushes.Yellow;
                                                break;
                                            case "Finalizado":
                                                tablaOper5.Background = Brushes.Red;
                                                break;
                                        }
                                        i++;
                                        break;
                                    case 5:
                                        panOperacion6.Visibility = Visibility.Visible;
                                        panOperacion6.DataContext = lista[5];
                                        margenOp6.DataContext = ManagerEmpleado.MostrarEmpleado(lista[5].NuEmpleado).Nombre;
                                        switch (listaOperacion[5].EstadoOp)
                                        {
                                            case "Sin empezar":
                                                tablaOper6.Background = Brushes.GhostWhite;
                                                break;
                                            case "Activo":
                                                tablaOper6.Background = Brushes.Green;
                                                break;
                                            case "Pausado":
                                                tablaOper6.Background = Brushes.Yellow;
                                                break;
                                            case "Finalizado":
                                                tablaOper6.Background = Brushes.Red;
                                                break;
                                        }
                                        i++;
                                        break;
                                    case 6:
                                        panOperacion7.Visibility = Visibility.Visible;
                                        panOperacion7.DataContext = lista[6];
                                        margenOp7.DataContext = ManagerEmpleado.MostrarEmpleado(lista[6].NuEmpleado).Nombre;
                                        switch (listaOperacion[6].EstadoOp)
                                        {
                                            case "Sin empezar":
                                                tablaOper7.Background = Brushes.GhostWhite;
                                                break;
                                            case "Activo":
                                                tablaOper7.Background = Brushes.Green;
                                                break;
                                            case "Pausado":
                                                tablaOper7.Background = Brushes.Yellow;
                                                break;
                                            case "Finalizado":
                                                tablaOper7.Background = Brushes.Red;
                                                break;
                                        }
                                        i++;
                                        break;
                                    case 7:
                                        panOperacion8.Visibility = Visibility.Visible;
                                        panOperacion8.DataContext = lista[7];
                                        margenOp8.DataContext = ManagerEmpleado.MostrarEmpleado(lista[7].NuEmpleado).Nombre;
                                        switch (listaOperacion[7].EstadoOp)
                                        {
                                            case "Sin empezar":
                                                tablaOper8.Background = Brushes.GhostWhite;
                                                break;
                                            case "Activo":
                                                tablaOper8.Background = Brushes.Green;
                                                break;
                                            case "Pausado":
                                                tablaOper8.Background = Brushes.Yellow;
                                                break;
                                            case "Finalizado":
                                                tablaOper8.Background = Brushes.Red;
                                                break;
                                        }
                                        i++;
                                        break;
                                    case 8:
                                        panOperacion9.Visibility = Visibility.Visible;
                                        panOperacion9.DataContext = lista[8];
                                        margenOp9.DataContext = ManagerEmpleado.MostrarEmpleado(lista[8].NuEmpleado).Nombre;
                                        switch (listaOperacion[8].EstadoOp)
                                        {
                                            case "Sin empezar":
                                                tablaOper9.Background = Brushes.GhostWhite;
                                                break;
                                            case "Activo":
                                                tablaOper9.Background = Brushes.Green;
                                                break;
                                            case "Pausado":
                                                tablaOper9.Background = Brushes.Yellow;
                                                break;
                                            case "Finalizado":
                                                tablaOper9.Background = Brushes.Red;
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

                }
            }
        }

        private void CambiarTexto()
        {
            switch (EstadoPantalla)
            {
                case EstadoPantalla.NuevaOt:
                    textOperacion1.Text = "Empleado:";
                    margenOp1.Margin = new Thickness(9, 0, 0, 0);
                    textOperacion2.Text = "Empleado:";
                    margenOp2.Margin = new Thickness(9, 0, 0, 0);
                    textOperacion3.Text = "Empleado:";
                    margenOp3.Margin = new Thickness(9, 0, 0, 0);
                    textOperacion4.Text = "Empleado:";
                    margenOp4.Margin = new Thickness(9, 0, 0, 0);
                    textOperacion5.Text = "Empleado:";
                    margenOp5.Margin = new Thickness(9, 0, 0, 0);
                    textOperacion6.Text = "Empleado:";
                    margenOp6.Margin = new Thickness(9, 0, 0, 0);
                    textOperacion7.Text = "Empleado:";
                    margenOp7.Margin = new Thickness(9, 0, 0, 0);
                    textOperacion8.Text = "Empleado:";
                    margenOp8.Margin = new Thickness(9, 0, 0, 0);
                    textOperacion9.Text = "Empleado:";
                    margenOp9.Margin = new Thickness(9, 0, 0, 0);
                    editarOperacion1.Visibility = Visibility.Visible;
                    editarOperacion2.Visibility = Visibility.Visible;
                    editarOperacion3.Visibility = Visibility.Visible;
                    editarOperacion4.Visibility = Visibility.Visible;
                    editarOperacion5.Visibility = Visibility.Visible;
                    editarOperacion6.Visibility = Visibility.Visible;
                    editarOperacion7.Visibility = Visibility.Visible;
                    editarOperacion8.Visibility = Visibility.Visible;
                    editarOperacion9.Visibility = Visibility.Visible;
                    break;
                case EstadoPantalla.NuevaEmpleados:
                    textOperacion1.Text = "Molde:";
                    margenOp1.Margin = new Thickness(28, 0, 0, 0);
                    textOperacion2.Text = "Molde:";
                    margenOp2.Margin = new Thickness(28, 0, 0, 0);
                    textOperacion3.Text = "Molde:";
                    margenOp3.Margin = new Thickness(28, 0, 0, 0);
                    textOperacion4.Text = "Molde:";
                    margenOp4.Margin = new Thickness(28, 0, 0, 0);
                    textOperacion5.Text = "Molde:";
                    margenOp5.Margin = new Thickness(28, 0, 0, 0);
                    textOperacion6.Text = "Molde:";
                    margenOp6.Margin = new Thickness(28, 0, 0, 0);
                    textOperacion7.Text = "Molde:";
                    margenOp7.Margin = new Thickness(28, 0, 0, 0);
                    textOperacion8.Text = "Molde:";
                    margenOp8.Margin = new Thickness(28, 0, 0, 0);
                    textOperacion9.Text = "Molde:";
                    margenOp9.Margin = new Thickness(28, 0, 0, 0);
                    editarOperacion1.Visibility = Visibility.Collapsed;
                    editarOperacion2.Visibility = Visibility.Collapsed;
                    editarOperacion3.Visibility = Visibility.Collapsed;
                    editarOperacion4.Visibility = Visibility.Collapsed;
                    editarOperacion5.Visibility = Visibility.Collapsed;
                    editarOperacion6.Visibility = Visibility.Collapsed;
                    editarOperacion7.Visibility = Visibility.Collapsed;
                    editarOperacion8.Visibility = Visibility.Collapsed;
                    editarOperacion9.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        //METODOS BOTONES DE PANELES DE OPERACIONES

        private void ActivarOperacion(int i)
        {
            if (listaOperacion[i].EstadoOp != "Activo")
            {
                if (!tablaOperaciones.IsEnabled)
                {
                    if (listaOperacion[i].NuEmpleado != 38)
                    {
                        var numOperE = operacion.MostrarOperEmp(listaOperacion[i].NuEmpleado).Where(o => o.EstadoOp != "Finalizado").Count();
                        if (listaOperacion[i].EstadoOp == "Finalizado") numOperE += 1;
                        if (numOperE <= 9)
                        {
                            operacion = listaOperacion[i];
                            operacion.Estado = EstadoEntidad.Actualizado;
                            operacion.EstadoOp = "Activo";
                            operacion.GuardarCambios();
                            if (EstadoPantalla == EstadoPantalla.NuevaOt) listaOperacion = operacion.MostrarOperOT(listaOperacion[i].NumeroOT);
                            else listaOperacion = operacion.MostrarOperEmp(listaOperacion[i].NuEmpleado).Where(li => li.EstadoOp != "Finalizado").ToList();
                            LimpiarPanelOperaciones();
                            MostrarPanelOperaciones(listaOperacion);
                        }
                        else MessageBox.Show("Este empleado ya tiene 9 operaciones en su lista sin finalizar.");
                    }
                    else MessageBox.Show("Edita y asigna un empleado primero.");
                }
                else MessageBox.Show("Primero guarda la operación.");
            }
        }

        private void PausarOperacion(int i)
        {
            if (listaOperacion[i].EstadoOp != "Pausado")
            {
                if (!tablaOperaciones.IsEnabled)
                {
                    if (listaOperacion[i].NuEmpleado != 38)
                    {
                        var numOperE = operacion.MostrarOperEmp(listaOperacion[i].NuEmpleado).Where(o => o.EstadoOp != "Finalizado").Count();
                        if (listaOperacion[i].EstadoOp == "Finalizado") numOperE += 1;
                        if (numOperE <= 9)
                        {
                            operacion = listaOperacion[i];
                            operacion.Estado = EstadoEntidad.Actualizado;
                            operacion.EstadoOp = "Pausado";
                            operacion.GuardarCambios();
                            if (EstadoPantalla == EstadoPantalla.NuevaOt) listaOperacion = operacion.MostrarOperOT(listaOperacion[i].NumeroOT);
                            else listaOperacion = operacion.MostrarOperEmp(listaOperacion[i].NuEmpleado).Where(li => li.EstadoOp != "Finalizado").ToList();
                            LimpiarPanelOperaciones();
                            MostrarPanelOperaciones(listaOperacion);
                        }
                        else MessageBox.Show("Este empleado ya tiene 9 operaciones en su lista sin finalizar.");
                    }
                    else MessageBox.Show("Edita y asigna un empleado primero.");
                }
                else MessageBox.Show("Primero guarda la operación.");
            }
        }

        private void FinalizarOperacion(int i)
        {
            if (listaOperacion[i].EstadoOp != "Finalizado")
            {
                if (!tablaOperaciones.IsEnabled)
                {
                    if (listaOperacion[i].NuEmpleado != 38)
                    {
                        operacion = listaOperacion[i];
                        operacion.Estado = EstadoEntidad.Actualizado;
                        operacion.EstadoOp = "Finalizado";
                        operacion.GuardarCambios();
                        if (EstadoPantalla == EstadoPantalla.NuevaOt) listaOperacion = operacion.MostrarOperOT(listaOperacion[i].NumeroOT);
                        else listaOperacion = operacion.MostrarOperEmp(listaOperacion[i].NuEmpleado).Where(li => li.EstadoOp != "Finalizado").ToList();
                        LimpiarPanelOperaciones();
                        MostrarPanelOperaciones(listaOperacion);
                    }
                    else MessageBox.Show("Edita y asigna un empleado primero.");
                }
                else MessageBox.Show("Primero guarda la operación.");
            }
        }

        private void EditarOperacion(int i)
        {
            if (!tablaOperaciones.IsEnabled)
            {
                operacion = listaOperacion[i];
                operacion.Estado = EstadoEntidad.Actualizado;
                operacionTipo.Text = operacion.TipoOperacion;
                operacionHoras.Text = operacion.Horas.ToString();
                operacionEmpleado.SelectedValue = operacion.NuEmpleado;
                operacionEstado.Text = operacion.EstadoOp;
                idOpeGuar = operacion.NuEmpleado;
                tablaOperaciones.IsEnabled = true;
                operacionEmpleado.IsEnabled = true;
                operacionEstado.IsEnabled = true;
            }
            else MessageBox.Show("Primero guarda la operación.");
        }

        //METODOS TABLAS DE DATOS

        //TABLA MOLDES

        private void LimpiarTablaMoldes()
        {
            moldeCodigo.Clear();
            moldeCliente.Clear();
            moldeFeEntrada.DisplayDate = DateTime.Now;
            moldeFeSalida.DisplayDate = DateTime.Now;
            moldeEnTaller.IsChecked = false;
        }

        //TABLA EMPLEADO

        private void LimpiarTablaEmpleado()
        {
            empleadoNombre.Clear();
            empleadoApellido.Clear();
            empleadoCargo.Text = "";
            empleadoTelefono.Clear();
        }

        //TABLA OT

        private void LimpiarTablaOT()
        {
            otNumero.Clear();
            comboMolde.Text = "";
            otHorasTrabajo.Text = "0";
            otTipo.Text = "";
            otClienteOT.Clear();
            otAcabada.IsChecked = false;
        }

        //TABLA OPERACIONES

        private void LimpiarTablaOperaciones()
        {
            operacionTipo.Text = "";
            operacionHoras.Clear();
            operacionEmpleado.Text = "";
        }

        //BOTONES INFORMACION DEL DATAGRID

        private void BtnOt_Click(object sender, RoutedEventArgs e)
        {
            btnActivar.Visibility = Visibility.Collapsed;
            btnBorrar.Visibility = Visibility.Collapsed;
            btnEditar.Visibility = Visibility.Visible;
            btnNuevo.Visibility = Visibility.Visible;
            BotonActivo(sender);
            iconTitulo.Icon = FontAwesome.Sharp.IconChar.ClipboardList;
            titulo.Text = "OT";
            OcultarPanelEditar();
            LimpiarPanelOperaciones();
            EstadoPantalla = EstadoPantalla.OT;
            MostrarPanel();
            ListaOT();
        }

        private void BtnEmpleados_Click(object sender, RoutedEventArgs e)
        {
            btnActivar.Visibility = Visibility.Collapsed;
            btnBorrar.Visibility = Visibility.Visible;
            btnEditar.Visibility = Visibility.Visible;
            btnNuevo.Visibility = Visibility.Visible;
            BotonActivo(sender);
            iconTitulo.Icon = FontAwesome.Sharp.IconChar.Users;
            titulo.Text = "Empleados";
            OcultarPanelEditar();
            LimpiarPanelOperaciones();
            EstadoPantalla = EstadoPantalla.Empleados;
            MostrarPanel();
            ListaEmpleados();
        }

        private void BtnMoldes_Click(object sender, RoutedEventArgs e)
        {
            btnActivar.Visibility = Visibility.Collapsed;
            btnBorrar.Visibility = Visibility.Visible;
            btnEditar.Visibility = Visibility.Visible;
            btnNuevo.Visibility = Visibility.Visible;
            BotonActivo(sender);
            iconTitulo.Icon = FontAwesome.Sharp.IconChar.DiceD6;
            titulo.Text = "Moldes";
            OcultarPanelEditar();
            LimpiarPanelOperaciones();
            EstadoPantalla = EstadoPantalla.Moldes;
            MostrarPanel();
            ListaMoldes();

        }

        private void BtnOtAcabada_Click(object sender, RoutedEventArgs e)
        {
            btnActivar.Visibility = Visibility.Visible;
            btnBorrar.Visibility = Visibility.Visible;
            btnEditar.Visibility = Visibility.Collapsed;
            btnNuevo.Visibility = Visibility.Collapsed;
            BotonActivo(sender);
            iconTitulo.Icon = FontAwesome.Sharp.IconChar.ClipboardCheck;
            titulo.Text = "OT Finalizadas";
            OcultarPanelEditar();
            LimpiarPanelOperaciones();
            EstadoPantalla = EstadoPantalla.OTfinalizada;
            MostrarPanel();
            ListaOTAcabada();

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

        //BOTONES DE DERECHA DEL PANEL PRINCIPAL

        private void btnNuevo_Click(object sender, RoutedEventArgs e)
        {
            OcultarPanel();

            switch (EstadoPantalla)
            {
                case EstadoPantalla.Empleados:
                    Empleado.Estado = EstadoEntidad.Insertado;
                    EstadoPantalla = EstadoPantalla.NuevaEmpleados;
                    LimpiarTablaEmpleado();
                    MostrarPanelEditar();
                    tablaEmpleado.IsEnabled = true;
                    break;
                case EstadoPantalla.OT:
                    ot.Estado = EstadoEntidad.Insertado;
                    EstadoPantalla = EstadoPantalla.NuevaOt;
                    LimpiarTablaOT();
                    tablaOT.IsEnabled = true;
                    MostrarPanelEditar();
                    break;
                case EstadoPantalla.Moldes:
                    moldes.Estado = EstadoEntidad.Insertado;
                    EstadoPantalla = EstadoPantalla.NuevaMoldes;
                    tablaMolde.IsEnabled = true;
                    LimpiarTablaMoldes();
                    MostrarPanelEditar();
                    break;
            }
        }

        private void btnBorrar_Click(object sender, RoutedEventArgs e)
        {
            if (panelDataGrid.SelectedIndex != -1)
            {
                switch (EstadoPantalla)
                {
                    case EstadoPantalla.Empleados:
                        idSeleccionada = (int)panelDataGrid.SelectedValue;
                        Empleado = ManagerEmpleado.MostrarEmpleado(idSeleccionada);
                        Empleado.Estado = EstadoEntidad.Borrado;
                        mensaje = ManagerEmpleado.GuardarCambios(Empleado);
                        MessageBox.Show(mensaje);
                        ListaEmpleados();
                        break;

                    case EstadoPantalla.Moldes:
                        idSeleccionada = (int)panelDataGrid.SelectedValue;
                        moldes = ManagerMolde.MostrarMolde(idSeleccionada);
                        moldes.Estado = EstadoEntidad.Borrado;
                        var valOT = ManagerOT.MostrarOT().Where(o => o.IdMolde.Equals(moldes.Id)).Count();
                        if (valOT == 0)
                        {
                            mensaje = ManagerMolde.GuardarCambios(moldes);
                            MessageBox.Show(mensaje);
                            ListaMoldes();
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Este molde tiene asignada una OT y no puede borrarse.");
                            break;
                        }
                    case EstadoPantalla.OTfinalizada:
                        idSeleccionada = (int)panelDataGrid.SelectedValue;
                        ot = ManagerOT.MostrarOT(idSeleccionada);
                        ot.Estado = EstadoEntidad.Borrado;
                        MessageBoxResult result = MessageBox.Show("Cuidado si acepta borrará la orden de trabajo definitivamente", "Mensaje importante", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                        switch (result)
                        {
                            case MessageBoxResult.OK:
                                mensaje = ManagerOT.GuardarCambios(ot);
                                MessageBox.Show(mensaje, "Mensaje importante");
                                ListaOTAcabada();
                                break;
                            case MessageBoxResult.Cancel:
                                MessageBox.Show("Se canceló y no se borrará el registro", "Mensaje importante");
                                break;
                        }
                        break;
                }
            }
            else
            {
                MessageBox.Show("Selecciona un registro primero");
            }
        }

        private void btnEditar_Click(object sender, RoutedEventArgs e)
        {
            if (panelDataGrid.SelectedIndex != -1)
            {

                switch (EstadoPantalla)
                {
                    case EstadoPantalla.Empleados:
                        OcultarPanel();
                        EstadoPantalla = EstadoPantalla.NuevaEmpleados;
                        CambiarTexto();
                        idSeleccionada = (int)panelDataGrid.SelectedValue;
                        Empleado = ManagerEmpleado.MostrarEmpleado(idSeleccionada);
                        listaOperacion = operacion.MostrarOperEmp(idSeleccionada).Where(li => li.EstadoOp != "Finalizado").ToList();
                        Empleado.Estado = EstadoEntidad.Actualizado;
                        panelEmpleados.DataContext = Empleado;
                        MostrarPanelOperaciones(listaOperacion);
                        MostrarPanelEditar();
                        break;
                    case EstadoPantalla.OT:
                        OcultarPanel();
                        EstadoPantalla = EstadoPantalla.NuevaOt;
                        CambiarTexto();
                        idSeleccionada = (int)panelDataGrid.SelectedValue;
                        listaOperacion = operacion.MostrarOperOT(idSeleccionada);
                        ot = ManagerOT.MostrarOT(idSeleccionada);
                        panelOT.DataContext = ot;
                        ot.Estado = EstadoEntidad.Actualizado;
                        MostrarPanelOperaciones(listaOperacion);
                        MostrarPanelEditar();
                        comboMolde.Text = ManagerMolde.MostrarMolde(ManagerOT.MostrarOT(idSeleccionada).IdMolde).Codigo;
                        break;
                    case EstadoPantalla.Moldes:
                        OcultarPanel();
                        EstadoPantalla = EstadoPantalla.NuevaMoldes;
                        idSeleccionada = (int)panelDataGrid.SelectedValue;
                        moldes = ManagerMolde.MostrarMolde((int)panelDataGrid.SelectedValue);
                        moldes.Estado = EstadoEntidad.Actualizado;
                        tablaMolde.DataContext = moldes;
                        MostrarPanelEditar();
                        tablaMolde.IsEnabled = true;
                        break;
                    case EstadoPantalla.OTfinalizada:
                        break;
                }
            }
            else
            {
                MessageBox.Show("Selecciona un registro primero");
            }
        }

        private void btnActivar_Click(object sender, RoutedEventArgs e)
        {
            idSeleccionada = (int)panelDataGrid.SelectedValue;
            ot = ManagerOT.MostrarOT(idSeleccionada);
            ot.Estado = EstadoEntidad.Actualizado;
            ot.Acabada = false;
            ManagerOT.GuardarCambios(ot);
            MessageBox.Show("OT activada ya puedes ver y modificar su estado en la lista de OT.");
            ListaOTAcabada();
        }

        // BOTONES PANELES NUEVOS ACTUALIZAR Y EDITAR 


        // BOTONES PANEL ACTUALIZAR MOLDE

        private void BtnNuevoMolde_Click(object sender, RoutedEventArgs e)
        {
            if (tablaMolde.IsEnabled == false)
            {
                tablaMolde.IsEnabled = true;
                LimpiarTablaMoldes();
            }
            else MessageBox.Show("Guarda primero antes de insertar un molde nuevo.");
        }

        private void BtnGuardarMolde_Click(object sender, RoutedEventArgs e)
        {
            if (tablaMolde.IsEnabled == true)
            {

                moldes.Codigo = moldeCodigo.Text;
                moldes.ClienteMolde = moldeCliente.Text;
                moldes.FechaEntrada = Convert.ToDateTime(moldeFeEntrada.SelectedDate);
                moldes.FechaSalida = Convert.ToDateTime(moldeFeSalida.SelectedDate);
                moldes.EnTaller = Convert.ToBoolean(moldeEnTaller.IsChecked);

                bool validoMo = new ValidarDatos(moldes).Validar();
                if (validoMo == true)
                {
                    string resultado = ManagerMolde.GuardarCambios(moldes);
                    MessageBox.Show(resultado);
                    if (moldes.Estado == EstadoEntidad.Actualizado)
                    {
                        BtnMoldes.IsDefault = true;
                        OcultarPanelEditar();
                        MostrarPanel();
                        ListaMoldes();
                        EstadoPantalla = EstadoPantalla.Moldes;
                    }
                    else
                        tablaMolde.IsEnabled = false;
                }
            }

        }

        private void BtnCerrarMolde_Click(object sender, RoutedEventArgs e)
        {
            BtnMoldes.IsDefault = true;
            OcultarPanelEditar();
            MostrarPanel();
            ListaMoldes();
            EstadoPantalla = EstadoPantalla.Moldes;
        }

        // BOTONES PANEL ACTUALIZAR EMPLEADO

        private void BtnNuevoEmpleado_Click(object sender, RoutedEventArgs e)
        {
            if (tablaEmpleado.IsEnabled == false)
            {
                tablaEmpleado.IsEnabled = true;
                LimpiarTablaEmpleado();
                Empleado.Estado = EstadoEntidad.Insertado;
            }
            else MessageBox.Show("Guarda primero antes de insertar un empleado nuevo.");
        }

        private void BtnEditarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            if (tablaEmpleado.IsEnabled == false)
            {
                if (Empleado.Estado == EstadoEntidad.Insertado)
                {
                    Empleado = ManagerEmpleado.MostrarEmpleado().Last();
                    Empleado.Estado = EstadoEntidad.Actualizado;
                }
                tablaEmpleado.IsEnabled = true;

            }
            else MessageBox.Show("Guarda primero antes de modificar un empleado nuevo.");

        }

        private void BtnGuardarEmpleado_Click(object sender, RoutedEventArgs e)
        {
            if (tablaEmpleado.IsEnabled == true)
            {

                Empleado.Nombre = empleadoNombre.Text;
                Empleado.Apellido = empleadoApellido.Text;
                Empleado.Cargo = empleadoCargo.Text;
                Empleado.Telefono = empleadoTelefono.Text;

                bool validoMo = new ValidarDatos(Empleado).Validar();
                if (validoMo == true)
                {
                    string resultado = ManagerEmpleado.GuardarCambios(Empleado);
                    MessageBox.Show(resultado);
                    tablaEmpleado.IsEnabled = false;
                }
            }

        }

        private void BtnCerrarEmpleados_Click(object sender, RoutedEventArgs e)
        {
            BtnEmpleados.IsDefault = true;
            OcultarPanelEditar();
            MostrarPanel();
            LimpiarTablaEmpleado();
            LimpiarPanelOperaciones();
            ListaEmpleados();
            EstadoPantalla = EstadoPantalla.Empleados;
        }

        // BOTONES PANEL ACTUALIZAR OPERACION

        private void BtnNuevaOperacion_Click(object sender, RoutedEventArgs e)
        {
            var numOperO = operacion.MostrarOperOT(ot.Id).Count;
            if (numOperO < 9)
            {
                if (tablaOperaciones.IsEnabled == false)
                {
                    tablaOperaciones.IsEnabled = true;
                    operacionEmpleado.IsEnabled = false;
                    operacionEstado.IsEnabled = false;
                    LimpiarTablaOperaciones();
                    operacionEmpleado.SelectedValue = 38;
                    operacionEstado.Text = "Sin empezar";
                    operacion.Estado = EstadoEntidad.Insertado;
                }
                else MessageBox.Show("Guarda primero antes de insertar un empleado nuevo.");
            }
            else MessageBox.Show("Ya tienes 9 operaciones.");
        }

        private void BtnBorrarOperacion_Click(object sender, RoutedEventArgs e)
        {
            if (tablaOperaciones.IsEnabled == true && operacion.Estado == EstadoEntidad.Actualizado)
            {
                operacion.Estado = EstadoEntidad.Borrado;
                mensaje = operacion.GuardarCambios();
                MessageBox.Show(mensaje);
                tablaOperaciones.IsEnabled = false;
                LimpiarPanelOperaciones();
                LimpiarTablaOperaciones();
                if (ot.Estado == EstadoEntidad.Insertado) ot = ManagerOT.MostrarOT().Last();
                listaOperacion = operacion.MostrarOperOT(ot.Id);
                CalculoHoras(listaOperacion);
                otHorasTrabajo.Text = ot.HorasTrabajo.ToString();
                MostrarPanelOperaciones(listaOperacion);
            }

        }

        private void BtnGuardarOperacion_Click(object sender, RoutedEventArgs e)
        {
            if (tablaOperaciones.IsEnabled == true)
            {

                operacion.TipoOperacion = operacionTipo.Text;
                if (operacionHoras.Text == "") operacion.Horas = 0;
                else operacion.Horas = Convert.ToInt32(operacionHoras.Text);
                if (ot.Estado == EstadoEntidad.Insertado) ot = ManagerOT.MostrarOT().Where(o => o.Numero.Equals(ot.Numero)).Single();
                operacion.NumeroOT = Convert.ToInt32(ot.Id);
                operacion.NuEmpleado = Convert.ToInt32(operacionEmpleado.SelectedValue);
                operacion.EstadoOp = operacionEstado.Text;
                if (operacion.NuEmpleado != 38)
                {
                    numOperE = operacion.MostrarOperEmp(operacion.NuEmpleado).Where(o => o.EstadoOp != "Finalizado").Count();
                    if (idOpeGuar == operacion.NuEmpleado) numOperE -= 1;
                }
                else numOperE = 0;
                if (numOperE < 9)
                {
                    bool validoMo = new ValidarDatos(operacion).Validar();
                    if (validoMo == true)
                    {
                        string resultado = operacion.GuardarCambios();
                        MessageBox.Show(resultado);
                        tablaOperaciones.IsEnabled = false;
                        LimpiarPanelOperaciones();
                        LimpiarTablaOperaciones();
                        listaOperacion = operacion.MostrarOperOT(ot.Id);
                        CalculoHoras(listaOperacion);
                        otHorasTrabajo.Text = ot.HorasTrabajo.ToString();
                        MostrarPanelOperaciones(listaOperacion);
                    }
                }
                else MessageBox.Show("No puedes asignar más de 9 tareas.");
            }
        }

        // BOTONES PANEL ACTUALIZAR OT

        private void BtnNuevaOT_Click(object sender, RoutedEventArgs e)
        {
            if (tablaOT.IsEnabled == false)
            {
                tablaOT.IsEnabled = true;
                LimpiarTablaOT();
                ot.Estado = EstadoEntidad.Insertado;
                LimpiarPanelOperaciones();
            }
            else MessageBox.Show("Guarda primero antes de insertar un empleado nuevo.");
        }

        private void BtnEditarOT_Click(object sender, RoutedEventArgs e)
        {
            if (tablaOT.IsEnabled == false)
            {
                if (ot.Estado == EstadoEntidad.Insertado)
                {
                    ot = ManagerOT.MostrarOT().Last();
                    ot.Estado = EstadoEntidad.Actualizado;
                }
                tablaOT.IsEnabled = true;

            }
            else MessageBox.Show("Guarda primero antes de modificar un empleado nuevo.");
        }

        private void BtnGuardarOT_Click(object sender, RoutedEventArgs e)
        {
            if (tablaOT.IsEnabled == true)
            {
                if (otNumero.Text != "")
                {
                    ot.Numero = Convert.ToInt32(otNumero.Text);
                    ot.IdMolde = (int)comboMolde.SelectedValue;
                    ot.HorasTrabajo = Convert.ToInt32(otHorasTrabajo.Text);
                    ot.Tipo = otTipo.Text;
                    ot.ClienteOT = otClienteOT.Text;
                    ot.Acabada = Convert.ToBoolean(otAcabada.IsChecked);

                    bool validoMo = new ValidarDatos(ot).Validar();
                    if (validoMo == true)
                    {
                        string resultado = ManagerOT.GuardarCambios(ot);
                        MessageBox.Show(resultado);
                        if (resultado != "Esta orden de trabajo ya está registrada")
                        {
                            tablaOT.IsEnabled = false;
                            if (panelEditaOperaciones.IsVisible == false) panelEditaOperaciones.Visibility = Visibility.Visible;
                        }
                        if (ot.Acabada && ot.Estado != EstadoEntidad.Insertado)
                        {
                            listaOperacion = operacion.MostrarOperOT(ot.Id).Where(o => o.EstadoOp != "Finalizado").ToList();
                            if (listaOperacion.Count > 0)
                            {
                                foreach (ModeloOperacion item in listaOperacion)
                                {
                                    item.EstadoOp = "Finalizado";
                                    item.Estado = EstadoEntidad.Actualizado;
                                    item.GuardarCambios();
                                }
                            }
                            listaOperacion = operacion.MostrarOperOT(ot.Id);
                            MostrarPanelOperaciones(listaOperacion);
                        }
                    }
                }
                else MessageBox.Show("Asignale un número a la OT");
            }
        }

        private void BtnCerrarOT_Click(object sender, RoutedEventArgs e)
        {
            BtnOt.IsDefault = true;
            OcultarPanelEditar();
            MostrarPanel();
            LimpiarTablaOT();
            LimpiarPanelOperaciones();
            ListaOT();
            EstadoPantalla = EstadoPantalla.OT;
        }

        // BOTONES PANEL ACTIVAR DESACTIVAR OPERACIONES

        private void activarOperacion1_Click(object sender, RoutedEventArgs e)
        {
            ActivarOperacion(0);
        }

        private void pausarOperacion1_Click(object sender, RoutedEventArgs e)
        {
            PausarOperacion(0);
        }

        private void acabarOperacion1_Click(object sender, RoutedEventArgs e)
        {
            FinalizarOperacion(0);
        }

        private void editarOperacion1_Click(object sender, RoutedEventArgs e)
        {
            EditarOperacion(0);
        }

        private void activarOperacion2_Click(object sender, RoutedEventArgs e)
        {
            ActivarOperacion(1);
        }

        private void pausarOperacion2_Click(object sender, RoutedEventArgs e)
        {
            PausarOperacion(1);
        }

        private void acabarOperacion2_Click(object sender, RoutedEventArgs e)
        {
            FinalizarOperacion(1);
        }

        private void editarOperacion2_Click(object sender, RoutedEventArgs e)
        {
            EditarOperacion(1);
        }

        private void activarOperacion3_Click(object sender, RoutedEventArgs e)
        {
            ActivarOperacion(2);
        }

        private void pausarOperacion3_Click(object sender, RoutedEventArgs e)
        {
            PausarOperacion(2);
        }

        private void acabarOperacion3_Click(object sender, RoutedEventArgs e)
        {
            FinalizarOperacion(2);
        }

        private void editarOperacion3_Click(object sender, RoutedEventArgs e)
        {
            EditarOperacion(2);
        }

        private void activarOperacion4_Click(object sender, RoutedEventArgs e)
        {
            ActivarOperacion(3);
        }

        private void pausarOperacion4_Click(object sender, RoutedEventArgs e)
        {
            PausarOperacion(3);
        }

        private void acabarOperacion4_Click(object sender, RoutedEventArgs e)
        {
            FinalizarOperacion(3);
        }

        private void editarOperacion4_Click(object sender, RoutedEventArgs e)
        {
            EditarOperacion(3);
        }

        private void activarOperacion5_Click(object sender, RoutedEventArgs e)
        {
            ActivarOperacion(4);
        }

        private void pausarOperacion5_Click(object sender, RoutedEventArgs e)
        {
            PausarOperacion(4);
        }

        private void acabarOperacion5_Click(object sender, RoutedEventArgs e)
        {
            FinalizarOperacion(4);
        }

        private void editarOperacion5_Click(object sender, RoutedEventArgs e)
        {
            EditarOperacion(4);
        }

        private void activarOperacion6_Click(object sender, RoutedEventArgs e)
        {
            ActivarOperacion(5);
        }

        private void pausarOperacion6_Click(object sender, RoutedEventArgs e)
        {
            PausarOperacion(5);
        }

        private void acabarOperacion6_Click(object sender, RoutedEventArgs e)
        {
            FinalizarOperacion(5);
        }

        private void editarOperacion6_Click(object sender, RoutedEventArgs e)
        {
            EditarOperacion(5);
        }

        private void activarOperacion7_Click(object sender, RoutedEventArgs e)
        {
            ActivarOperacion(6);
        }

        private void pausarOperacion7_Click(object sender, RoutedEventArgs e)
        {
            PausarOperacion(6);
        }

        private void acabarOperacion7_Click(object sender, RoutedEventArgs e)
        {
            FinalizarOperacion(6);
        }

        private void editarOperacion7_Click(object sender, RoutedEventArgs e)
        {
            EditarOperacion(6);
        }

        private void activarOperacion8_Click(object sender, RoutedEventArgs e)
        {
            ActivarOperacion(7);
        }

        private void pausarOperacion8_Click(object sender, RoutedEventArgs e)
        {
            PausarOperacion(7);
        }

        private void acabarOperacion8_Click(object sender, RoutedEventArgs e)
        {
            FinalizarOperacion(7);
        }

        private void editarOperacion8_Click(object sender, RoutedEventArgs e)
        {
            EditarOperacion(7);
        }

        private void activarOperacion9_Click(object sender, RoutedEventArgs e)
        {
            ActivarOperacion(8);
        }

        private void pausarOperacion9_Click(object sender, RoutedEventArgs e)
        {
            PausarOperacion(8);
        }

        private void acabarOperacion9_Click(object sender, RoutedEventArgs e)
        {
            FinalizarOperacion(8);
        }

        private void editarOperacion9_Click(object sender, RoutedEventArgs e)
        {
            EditarOperacion(8);
        }

        //Validar entrada numeros

        private void soloNumeros_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void textBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Copy ||
                e.Command == ApplicationCommands.Cut ||
                e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        
    }
}
