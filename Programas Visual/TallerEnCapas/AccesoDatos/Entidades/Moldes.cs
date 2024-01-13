using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class Moldes
    {
        public int Id { get; set; }

        public string Codigo { get; set; }

        public string Cliente { get; set; }

        public DateTime FechaEntrada { get; set; }

        public DateTime FechaSalida { get; set; }

        public bool EnTaller { get; set; }

    }
}
