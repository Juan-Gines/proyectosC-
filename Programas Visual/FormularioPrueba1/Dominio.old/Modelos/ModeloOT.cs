using AccesoDatos.Contratos;
using AccesoDatos.Repositorio;
using Dominio.ValoresObjetos;
using Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Modelos
{
    public class ModeloOT: IDisposable
    {
        private int id;
        private int numero;
        private int idMolde;
        private int horastrabajo;
        private string tipo;
        private string clienteOT;
        private bool acabada;
        private List<ModeloOT> listaOT;

        public EstadoEntidad Estado;
        private IRepositorioOT repositorioOT;

        public int Id { get => id; set => id = value; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Introduzca un número válido")]        
        public int Numero { get => numero; set => numero = value; }
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Introduzca un número válido")]
        public int IdMolde { get => idMolde; set => idMolde = value; }
        [Required]
        [Range(0, 999, ErrorMessage = "Introduzca un número de 0-999")]
        public int Horastrabajo { get => horastrabajo; set => horastrabajo = value; }
        [Required]
        public string Tipo { get => tipo; set => tipo = value; }
        [RegularExpression("^[a-zA-Zá-úà-ù0-9 ]+$")]
        [StringLength(maximumLength: 50)]
        public string ClienteOT { get => clienteOT; set => clienteOT = value; }
        [Required]
        public bool Acabada { get => acabada; set => acabada = value; }

        public ModeloOT()
        {
            repositorioOT = new RepositorioOT();
        }

        public string GuardarCambios()
        {
            string mensaje = null;
            try
            {
                var otModeloDatos = new OT()
                {
                    Id = id,
                    Numero = numero,
                    IdMolde = idMolde,
                    HorasTrabajo = horastrabajo,
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
                    numero = item.Numero,
                    idMolde = item.IdMolde,
                    horastrabajo = Convert.ToInt32(item.HorasTrabajo),
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
}
