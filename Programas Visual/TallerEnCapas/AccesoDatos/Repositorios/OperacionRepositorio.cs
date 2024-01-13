using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Contratos;
using AccesoDatos.Entidades;

namespace AccesoDatos.Repositorios
{
    public class OperacionRepositorio : RepositorioMaestro, IOperacionRepositorio
    {
        private string borrar = "DELETE FROM Operacion WHERE Id=@Id";

        private string editar = "UPDATE Operacion SET TipoOperacion=@TipoOperacion,Horas=@Horas,NumeroOT=@NumeroOT,nEmpleado=@nEmpleado,EstadoOp=@EstadoOp WHERE Id=@Id";

        private string mostrarTodos = "SELECT * FROM Operacion";

        private string insertar = "INSERT INTO Operacion VALUES(@TipoOperacion,@Horas,@NumeroOT,@nEmpleado,@EstadoOp)";

        public int Borrar(int id)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Id", id));
            return ExecuteNonQuery(borrar);
        }

        public int Editar(Operacion entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Id", entidad.Id));
            parametros.Add(new SqlParameter("@TipoOperacion", entidad.TipoOperacion));
            parametros.Add(new SqlParameter("@Horas", entidad.Horas));
            parametros.Add(new SqlParameter("@NumeroOT", entidad.NumeroOT));
            parametros.Add(new SqlParameter("@nEmpleado", entidad.nEmpleado));
            parametros.Add(new SqlParameter("@EstadoOp", entidad.EstadoOp));
            return ExecuteNonQuery(editar);
        }

        public IEnumerable<Operacion> GetEntidad()
        {
            var tablaResultada = ExecuteReader(mostrarTodos);
            var listaOperacion = new List<Operacion>();
            foreach (DataRow obj in tablaResultada.Rows)
            {
                listaOperacion.Add(new Operacion
                {
                    Id = Convert.ToInt32(obj[0]),
                    TipoOperacion = obj[1].ToString(),
                    Horas = Convert.ToInt32(obj[2]),
                    NumeroOT = Convert.ToInt32(obj[3]),
                    nEmpleado = Convert.ToInt32(obj[4]),
                    EstadoOp = obj[5].ToString()
                });
            }
            return listaOperacion;
        }

        public int Insertar(Operacion entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@TipoOperacion", entidad.TipoOperacion));
            parametros.Add(new SqlParameter("@Horas", entidad.Horas));
            parametros.Add(new SqlParameter("@NumeroOT", entidad.NumeroOT));
            parametros.Add(new SqlParameter("@nEmpleado", entidad.nEmpleado));
            parametros.Add(new SqlParameter("@EstadoOp", entidad.EstadoOp));
            return ExecuteNonQuery(insertar);
        }
    }
}
