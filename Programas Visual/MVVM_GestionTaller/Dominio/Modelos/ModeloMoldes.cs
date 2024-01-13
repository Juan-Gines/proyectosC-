using AccesoDatos.Contratos;
using AccesoDatos.Repositorio;
using Entidades;
using Dominio.ValoresObjetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dominio.Modelos
{
    public class ModeloMoldes
    {


        private int id;
        private string codigo;
        private string clienteMolde;
        private DateTime fechaEntrada;
        private DateTime fechaSalida;
        private bool enTaller;
        private List<ModeloMoldes> listaMoldes;

        public EstadoEntidad Estado;
        private IRepositorioMoldes repositorioMoldes;

        public int Id { get => id; set => id = value; }
        [Required]
        [RegularExpression(@"^(Tec-\d{3}-\d{3})-[M0-9]{3}", ErrorMessage = "Inserte un código válido Tec-000-000-M00")]
        [StringLength(maximumLength: 15, MinimumLength = 11)]
        public string Codigo { get => codigo; set => codigo = value; }
        [RegularExpression("^[a-zA-Zá-úà-ù0-9 ]+$")]
        [StringLength(maximumLength: 50)]
        public string ClienteMolde { get => clienteMolde; set => clienteMolde = value; }
        [Required]
        public DateTime FechaEntrada { get => fechaEntrada; set => fechaEntrada = value; }
        [Required]
        public DateTime FechaSalida { get => fechaSalida; set => fechaSalida = value; }
        [Required]
        public bool EnTaller { get => enTaller; set => enTaller = value; }


        public ModeloMoldes()
        {
            repositorioMoldes = new RepositorioMoldes();
        }

        public string GuardarCambios()
        {
            string mensaje = null;
            try
            {
                var moldesModeloDatos = new Moldes()
                {
                    Id = id,
                    Codigo = codigo,
                    ClienteMolde = clienteMolde,
                    FechaEntrada = fechaEntrada,
                    FechaSalida = fechaEntrada,
                    EnTaller=enTaller
                };

                switch (Estado)
                {
                    case EstadoEntidad.Insertado:
                        mensaje = repositorioMoldes.Insertar(moldesModeloDatos);                        
                        break;
                    case EstadoEntidad.Actualizado:                        
                        mensaje = repositorioMoldes.Actualizar(moldesModeloDatos);
                        break;
                    case EstadoEntidad.Borrado:                        
                        mensaje = repositorioMoldes.Borrar(id);
                        break;
                }

            }
            catch (Exception ex)
            {
                mensaje = ex.ToString();
            }
            return mensaje;
        }

        public List<ModeloMoldes> MostrarMoldes()
        {
            var empleadoModeloDatos = repositorioMoldes.GetEntidades();
            listaMoldes = new List<ModeloMoldes>();
            foreach (Moldes item in empleadoModeloDatos)
            {
                listaMoldes.Add(new ModeloMoldes
                {
                    id = item.Id,
                    codigo = item.Codigo,
                    clienteMolde = item.ClienteMolde,
                    fechaEntrada = Convert.ToDateTime(item.FechaEntrada),
                    fechaSalida = Convert.ToDateTime(item.FechaSalida),
                    enTaller = Convert.ToBoolean(item.EnTaller)
                });
            }
            return listaMoldes;
        }

        public ModeloMoldes MostrarMolde(int idMo)
        {
            listaMoldes = MostrarMoldes();
            return listaMoldes.First(e => e.Id.Equals(idMo));
        }

        public void Dispose()
        {

        }
    }
}
