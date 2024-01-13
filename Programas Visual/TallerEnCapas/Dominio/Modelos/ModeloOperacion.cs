using AccesoDatos.Contratos;
using AccesoDatos.Entidades;
using AccesoDatos.Repositorios;
using Dominio.ValoresObjetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Modelos
{
    public class ModeloOperacion : IDisposable
    {
        private int id;
        private string tipoOperacion;
        private string horas;
        private int numeroOT;
        private int nEmpleado;
        private string estadoOp;

        private List<ModeloOperacion> listaOperaciones;

        private IOperacionRepositorio operacionRepositorio;
        public EstadoEntidad Estado { get; set; }

        public int Id { get => id; set => id = value; }
        [Required]
        public string TipoOperacion { get => tipoOperacion; set => tipoOperacion = value; }
        [Required]
        [RegularExpression("^[0-9]+",ErrorMessage ="Máximo 999 horas")]
        [StringLength(maximumLength:3)]
        public string Horas { get => horas; set => horas = value; }
        public int NumeroOT { get => numeroOT; set => numeroOT = value; }
        public int NEmpleado { get => nEmpleado; set => nEmpleado = value; }
        [Required]
        public string EstadoOp { get => estadoOp; set => estadoOp = value; }

        public ModeloOperacion()
        {
            operacionRepositorio = new OperacionRepositorio();
        }

        public string GuardarCambios()
        {
            string mensaje = null;
            try
            {
                var operacionModeloDatos = new Operacion
                {
                    Id = id,
                    TipoOperacion = tipoOperacion,
                    Horas =Convert.ToInt32(horas),
                    NumeroOT = numeroOT,
                    nEmpleado = nEmpleado,
                    EstadoOp=EstadoOp
                };

                switch (Estado)
                {
                    case EstadoEntidad.Añadido:
                        operacionRepositorio.Insertar(operacionModeloDatos);
                        mensaje = "Añadido correctamente";
                        break;
                    case EstadoEntidad.Editado:
                        operacionRepositorio.Editar(operacionModeloDatos);
                        mensaje = "Editado correctamente";
                        break;
                    case EstadoEntidad.Borrado:
                        operacionRepositorio.Borrar(id);
                        mensaje = "Borrado correctamente";
                        break;
                }

            }
            catch (Exception ex)
            {
                System.Data.SqlClient.SqlException sqlEx = ex as System.Data.SqlClient.SqlException;
                if (sqlEx != null && sqlEx.Number == 2627)
                    mensaje = "Esta operación ya está registrada";
                else
                    mensaje = ex.ToString();
            }
            return mensaje;
        }

        public List<ModeloOperacion> MostrarOperacion()
        {
            var operacionModeloDatos = operacionRepositorio.GetEntidad();
            listaOperaciones = new List<ModeloOperacion>();
            foreach (Operacion operacion in operacionModeloDatos)
            {
                listaOperaciones.Add(new ModeloOperacion
                {
                    id = operacion.Id,
                    tipoOperacion = operacion.TipoOperacion,
                    horas = operacion.Horas.ToString(),
                    numeroOT = operacion.NumeroOT,
                    nEmpleado = operacion.nEmpleado,
                    EstadoOp = operacion.EstadoOp
                }); 
            }
            return listaOperaciones;
        }
        public ModeloOperacion Mostrar1Operacion(int idOp)
        {
            listaOperaciones = MostrarOperacion();
            return listaOperaciones.Find(m => m.Id.Equals(idOp));
        }
        public List<ModeloOperacion> MostrarOperacion(int idOt)
        {
            listaOperaciones = MostrarOperacion();
            return listaOperaciones.FindAll(m => m.NumeroOT.Equals(idOt));
        }
        public List<ModeloOperacion> MostrarOperacionEmp(int idOt)
        {
            listaOperaciones = MostrarOperacion();
            return listaOperaciones.FindAll(m => m.NEmpleado.Equals(idOt));
        }

        public void Dispose()
        {
            
        }
              
    }
}
