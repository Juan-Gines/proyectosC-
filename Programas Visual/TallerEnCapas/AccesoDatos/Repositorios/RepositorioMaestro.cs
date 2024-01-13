using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace AccesoDatos.Repositorios
{
    public abstract class RepositorioMaestro:Repositorio
    {
        protected List<SqlParameter> parametros;

        protected int ExecuteNonQuery(string transactSql)
        {
            using (var conexion = GetConexionSql())
            {
                conexion.Open();
                using (var comando=new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandText = transactSql;
                    comando.CommandType = CommandType.Text;

                    foreach(SqlParameter par in parametros)
                    {
                        comando.Parameters.Add(par);
                    }
                    int resultado = comando.ExecuteNonQuery();
                    parametros.Clear();
                    return resultado;
                }
            }
        }

        protected DataTable ExecuteReader(string transactSql)
        {
            using (var conexion = GetConexionSql())
            {
                conexion.Open();
                using (var comando = new SqlCommand())
                {
                    comando.Connection = conexion;
                    comando.CommandText = transactSql;
                    comando.CommandType = CommandType.Text;
                    SqlDataReader lectura = comando.ExecuteReader();
                    using(var tabla=new DataTable())
                    {
                        tabla.Load(lectura);
                        lectura.Dispose();
                        return tabla;
                    }

                }
            }
        }
    }
}
