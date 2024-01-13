using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Contratos;
using Entidades;

namespace AccesoDatos.Repositorio
{
    public class RepositorioMoldes : IRepositorioMoldes
    {
        string mensaje;
        
        IEnumerable<Moldes> listaMoldes;

        public string Actualizar(Moldes entidad)
        {
            using (connGestionTaller bd = new connGestionTaller())
            {
                var query = (from em in bd.Moldes
                             where em.Id == entidad.Id
                             select em).Single();

                query.Codigo = entidad.Codigo;
                query.ClienteMolde = entidad.ClienteMolde;
                query.FechaEntrada = entidad.FechaEntrada;
                query.FechaSalida = entidad.FechaSalida;
                query.EnTaller = entidad.EnTaller;
                bd.SaveChanges();
                mensaje = "Se actualizaron los datos del molde correctamente";
            }
            return mensaje;
        }

        public string Borrar(int id)
        {
            using (connGestionTaller bd = new connGestionTaller())
            {
                var query = (from em in bd.Moldes
                             where em.Id == id
                             select em).Single();
                bd.Moldes.Remove(query);
                bd.SaveChanges();
                mensaje = "Se Borraron los datos del molde correctamente";
            }
            return mensaje;
        }

        public IEnumerable<Moldes> GetEntidades()
        {
            using (connGestionTaller bd = new connGestionTaller())
            {
                listaMoldes = bd.Moldes.ToList();
            }
            return listaMoldes;
        }

        public Moldes GetMoldes(int id)
        {
            listaMoldes = GetEntidades();

            return listaMoldes.First(em => em.Id == id);
        }

        public string Insertar(Moldes entidad)
        {
            using (connGestionTaller bd = new connGestionTaller())
            {
                Moldes emp = new Moldes()
                {
                    Codigo = entidad.Codigo,
                    ClienteMolde = entidad.ClienteMolde,
                    FechaEntrada = entidad.FechaEntrada,
                    FechaSalida = entidad.FechaSalida,
                    EnTaller =  entidad.EnTaller
                };
                bd.Moldes.Add(emp);
                bd.SaveChanges();
                mensaje = "Se agregó un nuevo molde correctamente";
            }
            return mensaje;
        }
    }
}
