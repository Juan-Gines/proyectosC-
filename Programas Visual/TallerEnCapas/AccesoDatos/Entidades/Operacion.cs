using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class Operacion
    {
        public int Id { get; set; }

        public string TipoOperacion { get; set; }

        public int Horas { get; set; }

        public int NumeroOT { get; set; }

        public int nEmpleado { get; set; }
                
        public string EstadoOp { get; set; }

    }
}
