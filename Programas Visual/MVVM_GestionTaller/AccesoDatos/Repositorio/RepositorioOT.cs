using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AccesoDatos.Contratos;
using Entidades;

namespace AccesoDatos.Repositorio
{
    public class RepositorioOT: IRepositorioOT
    {
        string mensaje;

        IEnumerable<OT> listaOT;

        public string Actualizar(OT entidad)
        {
            using (GestionTallerEntities bd = new GestionTallerEntities())
            {
                var query = (from em in bd.OT
                             where em.Id == entidad.Id
                             select em).Single();

                query.Numero = entidad.Numero;
                query.IdMolde = entidad.IdMolde;
                query.HorasTrabajo = entidad.HorasTrabajo;
                query.Tipo = entidad.Tipo;
                query.ClienteOT = entidad.ClienteOT;
                query.Acabada = entidad.Acabada;
                bd.SaveChanges();
                mensaje = "Se actualizó la orden de trabajo correctamente";
            }
            return mensaje;
        }

        public string Borrar(int id)
        {
            using (GestionTallerEntities bd = new GestionTallerEntities())
            {
                var query = (from em in bd.OT
                             where em.Id == id
                             select em).Single();
                bd.OT.Remove(query);
                bd.SaveChanges();
                mensaje = "Se Borró la orden de trabajo correctamente";
            }
            return mensaje;
        }

        public IEnumerable<OT> GetEntidades()
        {

            using (GestionTallerEntities bd = new GestionTallerEntities())
            {
                listaOT = bd.OT.ToList();
            }

            return listaOT;
        }

        public OT GetOT(int id)
        {
            listaOT = GetEntidades();

            return listaOT.First(em => em.Id == id);
        }

        public string Insertar(OT entidad)
        {
            using (GestionTallerEntities bd = new GestionTallerEntities())
            {
                OT emp = new OT()
                {
                    Numero = entidad.Numero,
                    IdMolde = entidad.IdMolde,
                    HorasTrabajo = entidad.HorasTrabajo,
                    Tipo = entidad.Tipo,
                    ClienteOT = entidad.ClienteOT,
                    Acabada = entidad.Acabada
                };
                bd.OT.Add(emp);
                bd.SaveChanges();
                mensaje = "Se agregó una nueva orden de trabajo correctamente";
            }
            return mensaje;
        }
    }
}
