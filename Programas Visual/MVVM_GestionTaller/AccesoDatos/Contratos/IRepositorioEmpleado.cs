using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace AccesoDatos.Contratos
{
    public interface IRepositorioEmpleado: IRepositorioGenerico <Empleado>
    {
        Empleado GetEmpleado(int id);
    }
}
