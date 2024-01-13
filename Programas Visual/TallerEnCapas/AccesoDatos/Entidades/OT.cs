using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatos.Entidades
{
    public class OT
    {
        public int Id { get; set; }

        public int Numero { get; set; }

        public int IdMolde { get; set; }

        public int HorasTrabajo { get; set; }

        public string Tipo { get; set; }

        public string Cliente { get; set; }

        public bool Acabada { get; set; }
                
    }
}
