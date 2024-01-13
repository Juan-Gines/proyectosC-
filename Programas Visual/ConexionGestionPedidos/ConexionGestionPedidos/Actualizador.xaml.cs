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
using System.Windows.Shapes;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ConexionGestionPedidos
{
    /// <summary>
    /// Lógica de interacción para Actualizador.xaml
    /// </summary>
    public partial class Actualizador : Window
    {
        SqlConnection miConexionSql;

        private int z;

        public Actualizador(int elId)
        {
            InitializeComponent();

            z = elId;

            string miConexion = ConfigurationManager.ConnectionStrings["ConexionGestionPedidos.Properties.Settings.GestionPedidosConnectionString"].ConnectionString;

            miConexionSql = new SqlConnection(miConexion);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            

            try
            {

                string consulta = "UPDATE CLIENTE SET NOMBRE=@nombre, DIRECCION=@direccion, POBLACION=@poblacion, TELEFONO=@telefono WHERE Id=" + z;

                SqlCommand miComandoSql = new SqlCommand(consulta, miConexionSql);

                miConexionSql.Open();

                miComandoSql.Parameters.AddWithValue("@nombre", nCliente.Text);
                miComandoSql.Parameters.AddWithValue("@direccion", dCliente.Text);
                miComandoSql.Parameters.AddWithValue("@poblacion", pCliente.Text);
                miComandoSql.Parameters.AddWithValue("@telefono", tCliente.Text);

                miComandoSql.ExecuteNonQuery();    

                miConexionSql.Close();
            }
            catch (Exception e2)
            {
                MessageBox.Show(e2.ToString());

            }

            this.Close();

        }
    }
}
