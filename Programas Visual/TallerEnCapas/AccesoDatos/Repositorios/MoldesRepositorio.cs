using AccesoDatos.Contratos;
using AccesoDatos.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Repositorios
{
    public class MoldesRepositorio : RepositorioMaestro, IMoldesRepositorio
    {
        private string borrar = "DELETE FROM Moldes WHERE Id=@Id";

        private string editar = "UPDATE Moldes SET Codigo=@Codigo,Cliente=@Cliente,FechaEntrada=@FechaEntrada,FechaSalida=@FechaSalida,EnTaller=@EnTaller WHERE Id=@Id";

        private string mostrarTodos = "SELECT * FROM Moldes";

        private string insertar = "INSERT INTO Moldes VALUES(@Codigo,@Cliente,@FechaEntrada,@FechaSalida,@EnTaller)";

        public int Borrar(int id)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Id", id));
            return ExecuteNonQuery(borrar);
        }

        public int Editar(Moldes entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Id", entidad.Id));
            parametros.Add(new SqlParameter("@Codigo", entidad.Codigo));
            parametros.Add(new SqlParameter("@Cliente", entidad.Cliente));
            parametros.Add(new SqlParameter("@FechaEntrada", entidad.FechaEntrada));
            parametros.Add(new SqlParameter("@FechaSalida", entidad.FechaSalida));
            parametros.Add(new SqlParameter("@EnTaller", entidad.EnTaller));
            return ExecuteNonQuery(editar);

        }
        
        public IEnumerable<Moldes> GetEntidad()
        {
            var tablaResultada = ExecuteReader(mostrarTodos);
            var listaMoldes = new List<Moldes>();
            foreach (DataRow obj in tablaResultada.Rows)
            {
                listaMoldes.Add(new Moldes
                {
                    Id = Convert.ToInt32(obj[0]),
                    Codigo = obj[1].ToString(),
                    Cliente = obj[2].ToString(),
                    FechaEntrada = Convert.ToDateTime(obj[3]),
                    FechaSalida = Convert.ToDateTime(obj[4]),
                    EnTaller= Convert.ToBoolean(obj[5])

                });
            }
            return listaMoldes;

        }

        public int Insertar(Moldes entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Codigo", entidad.Codigo));
            parametros.Add(new SqlParameter("@Cliente", entidad.Cliente));
            parametros.Add(new SqlParameter("@FechaEntrada", entidad.FechaEntrada));
            parametros.Add(new SqlParameter("@FechaSalida", entidad.FechaSalida));
            parametros.Add(new SqlParameter("@EnTaller", entidad.EnTaller));
            return ExecuteNonQuery(insertar);
        }
    }
}
