using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Contratos
{
    public interface IRepositorioGenerico <Entidad> where Entidad:class
    {
        string Insertar(Entidad entidad);
        string Actualizar(Entidad entidad);
        string Borrar(int id);
        IEnumerable<Entidad> GetEntidades();
    }
}
