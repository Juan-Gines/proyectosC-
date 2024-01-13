using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Contratos;
using Entidades;

namespace AccesoDatos.Repositorio
{
    public class RepositorioOperacion : IRepositorioOperacion
    {
        string mensaje;

        IEnumerable<Operacion> listaOperacion;

        public string Actualizar(Operacion entidad)
        {
            using (connGestionTaller bd = new connGestionTaller())
            {
                var query = (from em in bd.Operacion
                             where em.Id == entidad.Id
                             select em).Single();

                query.TipoOperacion = entidad.TipoOperacion;
                query.Horas = entidad.Horas;
                query.NumeroOT = entidad.NumeroOT;
                query.NuEmpleado = entidad.NuEmpleado;
                query.EstadoOp = entidad.EstadoOp;
                bd.SaveChanges();
                mensaje = "Se actualizó la operación correctamente";
            }
            return mensaje;
        }

        public string Borrar(int id)
        {
            using (connGestionTaller bd = new connGestionTaller())
            {
                var query = (from em in bd.Operacion
                             where em.Id == id
                             select em).Single();
                bd.Operacion.Remove(query);
                bd.SaveChanges();
                mensaje = "Se Borró la operación correctamente";
            }
            return mensaje;
        }

        public IEnumerable<Operacion> GetEntidades()
        {
            using (connGestionTaller bd = new connGestionTaller())
            {
                listaOperacion = bd.Operacion.ToList();
            }
            return listaOperacion;
        }

        public Operacion GetOperacion(int id)
        {
            listaOperacion = GetEntidades();

            return listaOperacion.First(em => em.Id == id);
        }

        public string Insertar(Operacion entidad)
        {
            using (connGestionTaller bd = new connGestionTaller())
            {
                Operacion emp = new Operacion()
                {
                    TipoOperacion = entidad.TipoOperacion,
                    Horas = entidad.Horas,
                    NuEmpleado = entidad.NuEmpleado,
                    NumeroOT = entidad.NumeroOT,
                    EstadoOp = entidad.EstadoOp
                };
                bd.Operacion.Add(emp);
                bd.SaveChanges();
                mensaje = "Se agregó una nueva operación correctamente";
            }
            return mensaje;
        }
    }
}
