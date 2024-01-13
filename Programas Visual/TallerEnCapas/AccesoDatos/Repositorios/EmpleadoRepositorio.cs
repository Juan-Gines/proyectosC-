using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Contratos;
using AccesoDatos.Entidades;
using System.Data.SqlClient;
using System.Data;

namespace AccesoDatos.Repositorios
{
    public class EmpleadoRepositorio : RepositorioMaestro, IEmpleadoRepositorio
    {
        private string borrar = "DELETE FROM Empleado WHERE Id=@Id";

        private string editar = "UPDATE Empleado SET Nombre=@Nombre,Apellido=@Apellido,Cargo=@Cargo,Telefono=@Telefono WHERE Id=@Id";

        private string mostrarTodos = "SELECT * FROM Empleado";

        private string insertar = "INSERT INTO Empleado VALUES(@Nombre,@Apellido,@Cargo,@Telefono)";

        public int Borrar(int id)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Id", id));            
            return ExecuteNonQuery(borrar);
        }

        public int Insertar(Empleado entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Nombre", entidad.Nombre));
            parametros.Add(new SqlParameter("@Apellido", entidad.Apellido));
            parametros.Add(new SqlParameter("@Cargo", entidad.Cargo));
            parametros.Add(new SqlParameter("@Telefono", entidad.Telefono));
            return ExecuteNonQuery(insertar);
        }

        public IEnumerable<Empleado> GetEntidad()
        {
            var tablaResultada = ExecuteReader(mostrarTodos);
            var listaEmpleados = new List<Empleado>();
            foreach(DataRow obj in tablaResultada.Rows)
            {
                listaEmpleados.Add(new Empleado
                {
                    Id = Convert.ToInt32(obj[0]),
                    Nombre = obj[1].ToString(),
                    Apellido = obj[2].ToString(),
                    Cargo = obj[3].ToString(),
                    Telefono = obj[4].ToString()
                });
            }
            return listaEmpleados;
        }
                
        public int Editar(Empleado entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Id", entidad.Id));
            parametros.Add(new SqlParameter("@Nombre", entidad.Nombre));
            parametros.Add(new SqlParameter("@Apellido", entidad.Apellido));
            parametros.Add(new SqlParameter("@Cargo", entidad.Cargo));
            parametros.Add(new SqlParameter("@Telefono", entidad.Telefono));
            return ExecuteNonQuery(editar);
        }
    }
}
