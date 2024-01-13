using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Entidades;
using AccesoDatos.Contratos;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Documents;

namespace AccesoDatos.Repositorios
{
    public class OtRepositorio : RepositorioMaestro, IOtRepositorio
    {
        private string borrar = "DELETE FROM OT WHERE Id=@Id";

        private string editar = "UPDATE OT SET Numero=@Numero,IdMolde=@IdMolde,HorasTrabajo=@HorasTrabajo,Tipo=@Tipo,Cliente=@Cliente,Acabada=@Acabada WHERE Id=@Id";

        private string mostrarTodos = "SELECT * FROM OT";        

        private string insertar = "INSERT INTO OT VALUES(@Numero,@IdMolde,@HorasTrabajo,@Tipo,@Cliente,@Acabada)";              

        public int Borrar(int id)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Id", id));
            return ExecuteNonQuery(borrar);
        }

        public int Editar(OT entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Id", entidad.Id));
            parametros.Add(new SqlParameter("@Numero", entidad.Numero));
            parametros.Add(new SqlParameter("@IdMolde", entidad.IdMolde));
            parametros.Add(new SqlParameter("@HorasTrabajo", entidad.HorasTrabajo));
            parametros.Add(new SqlParameter("@Tipo", entidad.Tipo));
            parametros.Add(new SqlParameter("@Cliente", entidad.Cliente));
            parametros.Add(new SqlParameter("@Acabada", entidad.Acabada));
            return ExecuteNonQuery(editar);

        }

        public IEnumerable<OT> GetEntidad()
        {
            var tablaResultada = ExecuteReader(mostrarTodos);            
            var listaOt = new List<OT>();
            foreach (DataRow obj in tablaResultada.Rows)
            {
                listaOt.Add(new OT
                {
                    Id = Convert.ToInt32(obj[0]),
                    Numero = Convert.ToInt32(obj[1]),
                    IdMolde = Convert.ToInt32(obj[2]),
                    HorasTrabajo = Convert.ToInt32(obj[3]),
                    Tipo = obj[4].ToString(),
                    Cliente = obj[5].ToString(),
                    Acabada = Convert.ToBoolean(obj[6]),                    
                });               
            }           
            
            return listaOt;
        }
        
               
        public int Insertar(OT entidad)
        {
            parametros = new List<SqlParameter>();
            parametros.Add(new SqlParameter("@Numero", entidad.Numero));
            parametros.Add(new SqlParameter("@IdMolde", entidad.IdMolde));
            parametros.Add(new SqlParameter("@HorasTrabajo", entidad.HorasTrabajo));
            parametros.Add(new SqlParameter("@Tipo", entidad.Tipo));
            parametros.Add(new SqlParameter("@Cliente", entidad.Cliente));
            parametros.Add(new SqlParameter("@Acabada", entidad.Acabada));
            return ExecuteNonQuery(insertar);
        }
    }
}
