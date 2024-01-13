using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Contratos
{
    public interface IRepositorioGenerico <Entidad> where Entidad:class
    {
        int Insertar(Entidad entidad);
        int Editar(Entidad entidad);
        int Borrar(int id);
        IEnumerable<Entidad> GetEntidad();
    }
}
