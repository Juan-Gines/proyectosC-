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
using System.Data.SqlClient;
using System.Data;

namespace ConexionGestionPedidos
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string miConexion = ConfigurationManager.ConnectionStrings["ConexionGestionPedidos.Properties.Settings.GestionPedidosConnectionString"].ConnectionString;

            miConexionSql = new SqlConnection(miConexion);
            
        }

        private void mostrarClientes()
        {
            try
            {

                string consulta = "SELECT * FROM CLIENTE";

                SqlDataAdapter miAdaptador = new SqlDataAdapter(consulta, miConexionSql);

                using (miAdaptador)
                {
                    DataTable clientesTabla = new DataTable();

                    miAdaptador.Fill(clientesTabla);

                    listaClientes.DisplayMemberPath = "nombre";
                    listaClientes.SelectedValuePath = "Id";
                    listaClientes.ItemsSource = clientesTabla.DefaultView;
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.ToString());

            }

        }

        private void mostrarPedidos()
        {
            try
            {

                string consulta = "SELECT * FROM PEDIDO P INNER JOIN CLIENTE C ON C.Id=P.cCliente" + " WHERE C.Id=@ClienteId";

                SqlCommand sqlcomando = new SqlCommand(consulta, miConexionSql);

                SqlDataAdapter miAdaptador = new SqlDataAdapter(sqlcomando);

                using (miAdaptador)
                {
                    sqlcomando.Parameters.AddWithValue("@ClienteId", listaClientes.SelectedValue);

                    DataTable pedidosTabla = new DataTable();

                    miAdaptador.Fill(pedidosTabla);

                    PedidosCliente.DisplayMemberPath = "fechaPedido";
                    PedidosCliente.SelectedValuePath = "Id";
                    PedidosCliente.ItemsSource = pedidosTabla.DefaultView;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());

            }
        }

        private void TodosPedidos()
        {
            try
            {

                string consulta = "SELECT *, CONCAT(CCLIENTE, ' ',FECHAPEDIDO, ' ',FORMAPAGO) AS INFOCOMPLETA  FROM PEDIDO";

                SqlDataAdapter miAdaptador = new SqlDataAdapter(consulta, miConexionSql);

                using (miAdaptador)
                {
                    DataTable tPedidosTabla = new DataTable();

                    miAdaptador.Fill(tPedidosTabla);

                    listaPedidos.DisplayMemberPath = "INFOCOMPLETA";
                    listaPedidos.SelectedValuePath = "Id";
                    listaPedidos.ItemsSource = tPedidosTabla.DefaultView;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());

            }
        }

        SqlConnection miConexionSql;

        //private void listaClientes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    mostrarPedidos();

        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string consulta = "DELETE FROM PEDIDO WHERE ID=@PedidoId";
                        
            SqlCommand miComandoSql = new SqlCommand(consulta, miConexionSql);

            miConexionSql.Open();

            miComandoSql.Parameters.AddWithValue("@PedidoId", listaPedidos.SelectedValue);
                        
            miComandoSql.ExecuteNonQuery();

            miConexionSql.Close();

            TodosPedidos();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string consulta = "INSERT INTO CLIENTE (nombre) VALUES (@Nombre)";

            SqlCommand miComandoSql = new SqlCommand(consulta, miConexionSql);

            miConexionSql.Open();

            miComandoSql.Parameters.AddWithValue("@Nombre", insertarCliente.Text);

            miComandoSql.ExecuteNonQuery();

            miConexionSql.Close();

            mostrarClientes();

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string consulta = "DELETE FROM CLIENTE WHERE ID=@ClienteId";

            SqlCommand miComandoSql = new SqlCommand(consulta, miConexionSql);

            miConexionSql.Open();

            miComandoSql.Parameters.AddWithValue("@ClienteId", listaClientes.SelectedValue);

            miComandoSql.ExecuteNonQuery();

            miConexionSql.Close();

            mostrarClientes();

        }

        private void listaClientes_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            mostrarPedidos();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Actualizador actCliente = new Actualizador((int)listaClientes.SelectedValue);            

            try
            {

                string consulta = "SELECT * FROM CLIENTE WHERE ID=@CliId";

                SqlCommand miComandoSql = new SqlCommand(consulta, miConexionSql);

                SqlDataAdapter miAdaptador = new SqlDataAdapter(miComandoSql);

                using (miAdaptador)
                {
                    miComandoSql.Parameters.AddWithValue("@CliId", listaClientes.SelectedValue);

                    DataTable clientesTabla = new DataTable();

                    miAdaptador.Fill(clientesTabla);

                    actCliente.nCliente.Text = clientesTabla.Rows[0]["nombre"].ToString();
                    actCliente.dCliente.Text = clientesTabla.Rows[0]["direccion"].ToString();
                    actCliente.pCliente.Text = clientesTabla.Rows[0]["poblacion"].ToString();
                    actCliente.tCliente.Text = clientesTabla.Rows[0]["telefono"].ToString();
                }
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.ToString());

            }

            actCliente.ShowDialog();

        }

        private void Window_Activated(object sender, EventArgs e)
        {
            mostrarClientes();

            TodosPedidos();
        }
    }
}
