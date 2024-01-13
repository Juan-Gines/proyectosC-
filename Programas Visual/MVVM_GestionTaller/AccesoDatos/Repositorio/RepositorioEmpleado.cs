using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Contratos;
using Entidades;

namespace AccesoDatos.Repositorio
{
    public class RepositorioEmpleado: IRepositorioEmpleado
    {
        string mensaje;
        
        IEnumerable<Empleado> listaEmpleado;

        public string Actualizar(Empleado entidad)
        {
            using (GestionTallerEntities bd = new GestionTallerEntities())
            {
                var query = (from em in bd.Empleado
                            where em.Id == entidad.Id
                            select em).Single();
                //emp = bd.Empleado.Find(entidad.Id);
                query.Nombre = entidad.Nombre;
                query.Apellido = entidad.Apellido;
                query.Cargo = entidad.Cargo;
                query.Telefono = entidad.Telefono;               
                bd.SaveChanges();
                mensaje = "Se actualizaron los datos del empleado correctamente";
            }
            return mensaje;
        }

        public string Borrar(int id)
        {
            using (GestionTallerEntities bd = new GestionTallerEntities())
            {
                var query = (from em in bd.Empleado
                             where em.Id == id
                             select em).Single();
                bd.Empleado.Remove(query);                
                bd.SaveChanges();
                mensaje = "Se Borraron los datos del empleado correctamente";
            }
            return mensaje;
        }

        public Empleado GetEmpleado(int id)
        {
            listaEmpleado = GetEntidades();

            return listaEmpleado.First(em => em.Id == id);
        }

        public IEnumerable<Empleado> GetEntidades()
        {
            using (GestionTallerEntities bd = new GestionTallerEntities())
            {
                listaEmpleado = bd.Empleado.ToList();
            }
            return listaEmpleado;
        }

        public string Insertar(Empleado entidad)
        {
            using (GestionTallerEntities bd = new GestionTallerEntities())
            {
                Empleado emp = new Empleado()
                {
                    Nombre = entidad.Nombre,
                    Apellido = entidad.Apellido,
                    Cargo = entidad.Cargo,
                    Telefono = entidad.Telefono
                };
                bd.Empleado.Add(emp);
                bd.SaveChanges();
                mensaje = "Se agregó un nuevo empleado correctamente";
            }
            return mensaje;
        }
    }
}
