using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace AccesoDatos.Repositorios
{
    public abstract class Repositorio
    {
        private readonly string connectionString;
        public Repositorio()
        {
            connectionString = ConfigurationManager.ConnectionStrings["connGestionPedidos"].ConnectionString;
        }

        protected SqlConnection GetConexionSql()
        {
            return new SqlConnection(connectionString);
        }
    }
}
