using AccesoDatos.Contratos;
using AccesoDatos.Repositorio;
using Dominio.ValoresObjetos;
using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class ModeloOT: IDisposable
    {
        private int id;
        private int idMolde;
        private int horasTrabajo;
        private string tipo;
        private string clienteOT;
        private bool acabada;
        private List<ModeloOT> listaOT;

        public EstadoEntidad Estado;
        private IRepositorioOT repositorioOT;

        public int Id {  get => id; set => id = value; }
        [Required]
        [RegularExpression("^[0-9]+")]
        [Range(0, int.MaxValue, ErrorMessage = "Introduzca un número válido")]        
        public int Numero { get; set; }
        [Required]        
        [Range(0, int.MaxValue, ErrorMessage = "Introduzca un número válido")]
        public int IdMolde {  get => idMolde; set => idMolde = value; }
        [Required]
        [Range(0, 999, ErrorMessage = "Introduzca un número de 0-999")]        
        public int HorasTrabajo { get => horasTrabajo; set => horasTrabajo = value; }
        [Required]        
        public string Tipo { get => tipo; set => tipo = value; }
        [RegularExpression("^[a-zA-Zá-úà-ù0-9 ]+$")]
        [StringLength(maximumLength: 50)]       
        public string ClienteOT { get => clienteOT; set => clienteOT = value; }
        [Required]
        public bool Acabada {  get => acabada; set => acabada = value; }

        public ModeloOT()
        {
            repositorioOT = new RepositorioOT();
            id = -1;
        }

        public string GuardarCambios()
        {
            string mensaje = null;
            try
            {
                var otModeloDatos = new OT()
                {
                    Id = id,
                    Numero = Numero,
                    IdMolde = idMolde,
                    HorasTrabajo = horasTrabajo,
                    Tipo = tipo,
                    ClienteOT = clienteOT,
                    Acabada = acabada                    
                };

                switch (Estado)
                {
                    case EstadoEntidad.Insertado:
                        mensaje = repositorioOT.Insertar(otModeloDatos);
                        break;
                    case EstadoEntidad.Actualizado:
                        mensaje = repositorioOT.Actualizar(otModeloDatos);
                        break;
                    case EstadoEntidad.Borrado:
                        mensaje = repositorioOT.Borrar(id);
                        break;
                }

            }
            catch (Exception ex)
            {
                SqlException sqlEx = ex.InnerException.InnerException as SqlException;
                if (sqlEx != null && sqlEx.Number == 2627)
                    mensaje = "Esta orden de trabajo ya está registrada";
                else
                    mensaje = ex.ToString();
            }
            return mensaje;
        }

        public List<ModeloOT> MostrarOT()
        {
            var empleadoModeloDatos = repositorioOT.GetEntidades();
            listaOT = new List<ModeloOT>();
            foreach (OT item in empleadoModeloDatos)
            {
                listaOT.Add(new ModeloOT
                {
                    id = item.Id,
                    Numero = item.Numero,
                    idMolde = item.IdMolde,
                    horasTrabajo = Convert.ToInt32(item.HorasTrabajo),
                    tipo = item.Tipo,
                    clienteOT=item.ClienteOT,
                    acabada=item.Acabada
                });
            }
            return listaOT;
        }

        public ModeloOT MostrarOT(int idOT)
        {
            listaOT = MostrarOT();
            return listaOT.First(e => e.Id.Equals(idOT));
        }

        public void Dispose()
        {

        }
    }
    public class ResModeloOT
    {
        public bool Ok { get; set; }
        public string Error { get; set; }
        public ModeloOT ot;


        public ResModeloOT(ModeloOT ot)
        {

            if (ot.Id > -1)
            {
                Ok = true;
                Error = "";
                this.ot = ot;
            }
            else
            {
                Ok = false;
                Error = "No se ha encontrado una OT válida";
                this.ot = ot;
            }

        }

    }

    public class ResModeloOTs
    {
        public bool Ok { get; set; }
        public string Error { get; set; }
        public ModeloOT ot;
        public List<ModeloOT> ots;

        public ResModeloOTs(List<ModeloOT> ots)
        {
            if (ots.Any())
            {
                Ok = true;
                Error = "";
                this.ots = ots;
            }
            else
            {
                Ok = false;
                Error = "No se ha encontrado un usuario válido";
                this.ots = ots;
            }

        }
    }
}
