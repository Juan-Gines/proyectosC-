using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Dominio.ValoresObjetos;
using AccesoDatos.Contratos;
using AccesoDatos.Repositorio;
using Entidades;

namespace Dominio.Modelos
{
    public class ModeloOperacion: IDisposable
    {
        private int id;
        private string tipoOperacion;
        private int horas;
        private int numeroOT;
        private int nuEmpleado;
        private string estadoOp;
        private List<ModeloOperacion> listaOperacion;

        public EstadoEntidad Estado;
        private IRepositorioOperacion repositorioOperario;

        public int Id { get => id; set => id = value; }
        [Required]
        public string TipoOperacion { get => tipoOperacion; set => tipoOperacion = value; }
        [Required]
        [Range(0,999)]
        public int Horas { get => horas; set => horas = value; }
        [Required]
        public int NumeroOT { get => numeroOT; set => numeroOT = value; }
        public int NuEmpleado { get => nuEmpleado; set => nuEmpleado = value; }
        [Required]
        public string EstadoOp { get => estadoOp; set => estadoOp = value; }

        public ModeloOperacion()
        {
            repositorioOperario = new RepositorioOperacion();
        }

        public string GuardarCambios()
        {
            string mensaje = null;
            try
            {
                var operacionModeloDatos = new Operacion()
                {
                    Id = id,
                    TipoOperacion = tipoOperacion,
                    Horas = horas,
                    NumeroOT = numeroOT,
                    NuEmpleado = nuEmpleado,
                    EstadoOp = estadoOp                    
                };

                switch (Estado)
                {
                    case EstadoEntidad.Insertado:
                        mensaje = repositorioOperario.Insertar(operacionModeloDatos);
                        break;
                    case EstadoEntidad.Actualizado:
                        mensaje = repositorioOperario.Actualizar(operacionModeloDatos);
                        break;
                    case EstadoEntidad.Borrado:
                        mensaje = repositorioOperario.Borrar(id);
                        break;
                }

            }
            catch (Exception ex)
            {
                mensaje = ex.ToString();
            }
            return mensaje;
        }

        public List<ModeloOperacion> MostrarOperacion()
        {
            var empleadoModeloDatos = repositorioOperario.GetEntidades();
            listaOperacion = new List<ModeloOperacion>();
            foreach (Operacion item in empleadoModeloDatos)
            {
                listaOperacion.Add(new ModeloOperacion
                {
                    id = item.Id,
                    tipoOperacion = item.TipoOperacion,
                    horas = Convert.ToInt32(item.Horas),
                    numeroOT = item.NumeroOT,
                    nuEmpleado = Convert.ToInt32(item.NuEmpleado),
                    estadoOp = item.EstadoOp                    
                });
            }
            return listaOperacion;
        }

        public List<ModeloOperacion> MostrarOperEmp(int idEm)
        {
            listaOperacion = MostrarOperacion();
            return listaOperacion.FindAll(e => e.NuEmpleado.Equals(idEm));
        }

        public List<ModeloOperacion> MostrarOperOT(int idOT)
        {
            listaOperacion = MostrarOperacion();
            return listaOperacion.FindAll(e => e.NumeroOT.Equals(idOT));
        }

        public void Dispose()
        {

        }
    }
}
