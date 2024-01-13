using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Contratos;
using AccesoDatos.Entidades;
using AccesoDatos.Repositorios;
using Dominio.Modelos;
using Dominio.ValoresObjetos;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Dominio.Modelos
{
    public class ModeloOt : IDisposable
    {
        private int id;
        private string numero;
        private int idMolde;
        private int horasTrabajo;
        private string tipo;
        private string cliente;
        private bool acabada;
        List<ModeloOt> listaOt;

        private IOtRepositorio otRepositorio;
        public EstadoEntidad Estado { private get; set; }

        public int Id { get => id; set => id = value; }
        [Required]
        [StringLength(maximumLength:9)]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Inserte solamente números sin espacio")]
        public string Numero { get => numero; set => numero = value; }
        [Required]        
        public int IdMolde { get => idMolde; set => idMolde = value; }
        [Required]
        public int HorasTrabajo { get => horasTrabajo; set => horasTrabajo = value; }
        [Required]
        public string Tipo { get => tipo; set => tipo = value; }
        [RegularExpression("^[a-zA-Zá-úà-ù0-9 ]+$")]
        [StringLength(maximumLength: 50)]
        public string Cliente { get => cliente; set => cliente = value; }        
        public bool Acabada { get => acabada; set => acabada = value; }
       

        public ModeloOt()
        {
            otRepositorio = new OtRepositorio();

        }

        public string GuardarCambios()
        {
            string mensaje = null;
            try
            {
                var OtModeloDatos = new OT
                {
                    Id = id,
                    Numero = Convert.ToInt32(numero),
                    IdMolde = idMolde,
                    HorasTrabajo = horasTrabajo,
                    Tipo = tipo,
                    Cliente = cliente,
                    Acabada = acabada
                };

                switch (Estado)
                {
                    case EstadoEntidad.Añadido:
                        otRepositorio.Insertar(OtModeloDatos);
                        mensaje = "Añadido correctamente";
                        break;
                    case EstadoEntidad.Editado:
                        otRepositorio.Editar(OtModeloDatos);
                        mensaje = "Editado correctamente";
                        break;
                    case EstadoEntidad.Borrado:
                        otRepositorio.Borrar(id);
                        mensaje = "Borrado correctamente";
                        break;
                }
            }
            catch (Exception ex)
            {
                System.Data.SqlClient.SqlException sqlEx = ex as System.Data.SqlClient.SqlException;
                if (sqlEx != null && sqlEx.Number == 2627)
                    mensaje = "Esta orden de trabajo ya está registrada";
                else
                    mensaje = ex.ToString();
            }
            return mensaje;
        }
        public List<ModeloOt> MostrarOt()
        {
            var OtModeloDatos = otRepositorio.GetEntidad();            
            listaOt = new List<ModeloOt>();
            foreach (OT oT in OtModeloDatos)
            {
                listaOt.Add(new ModeloOt
                {
                    id = oT.Id,
                    numero = oT.Numero.ToString(),
                    idMolde = oT.IdMolde,
                    horasTrabajo = oT.HorasTrabajo,
                    tipo = oT.Tipo,
                    cliente = oT.Cliente,
                    acabada=oT.Acabada                    
                });
            }
            
            return listaOt;
        }
        public ModeloOt MostrarOt(int idOt)
        {
            listaOt = MostrarOt();
            return listaOt.Find(m => m.id.Equals(idOt));
        }

        public void Dispose()
        {

        }
    }
}
