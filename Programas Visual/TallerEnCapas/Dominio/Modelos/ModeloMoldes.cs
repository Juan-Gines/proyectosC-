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
    public class ModeloMoldes : IDisposable
    {
        private int id;
        private string codigo;
        private string cliente;
        private DateTime fechaEntrada;
        private DateTime fechaSalida;
        private bool enTaller;
        List<ModeloMoldes> listaMoldes;

        private IMoldesRepositorio moldesRepositorio;
        public EstadoEntidad Estado { private get; set; }

        public int Id { get => id; set => id = value; }
        [Required]
        [RegularExpression(@"^(Tec-\d{3}-\d{3})-[M0-9]{3}",ErrorMessage ="Inserte un código válido Tec-000-000-M00")]
        [StringLength(maximumLength: 15, MinimumLength = 11)]        
        public string Codigo { get => codigo; set => codigo = value; }
        [RegularExpression("^[a-zA-Zá-úà-ù0-9 ]+$")]
        [StringLength(maximumLength:50)]
        public string Cliente { get => cliente; set => cliente = value; }
        [Required]
        public DateTime FechaEntrada { get => fechaEntrada; set => fechaEntrada = value; }
        [Required]
        public DateTime FechaSalida { get => fechaSalida; set => fechaSalida = value; }
        [Required]           
        public bool EnTaller {  get => enTaller; set => enTaller = value; }

        public ModeloMoldes()
        {
            moldesRepositorio = new MoldesRepositorio();

        }

        public string GuardarCambios()
        {
            string mensaje = null;
            try
            {
                var moldeModeloDatos = new Moldes
                {
                    Id = id,
                    Codigo = codigo,
                    Cliente = cliente,
                    FechaEntrada = fechaEntrada,
                    FechaSalida = fechaSalida,
                    EnTaller = enTaller
                };

                switch (Estado)
                {
                    case EstadoEntidad.Añadido:
                        moldesRepositorio.Insertar(moldeModeloDatos);
                        mensaje = "Añadido correctamente";
                        break;
                    case EstadoEntidad.Editado:
                        moldesRepositorio.Editar(moldeModeloDatos);
                        mensaje = "Editado correctamente";
                        break;
                    case EstadoEntidad.Borrado:
                        moldesRepositorio.Borrar(id);
                        mensaje = "Borrado correctamente";
                        break;
                }

            }
            catch (Exception ex)
            {
                System.Data.SqlClient.SqlException sqlEx = ex as System.Data.SqlClient.SqlException;
                if (sqlEx != null && sqlEx.Number == 2627)
                    mensaje = "Este molde ya está registrado";
                else 
                    mensaje = ex.ToString();
            }
            return mensaje;
        }

        public List<ModeloMoldes> MostrarMoldes()
        {
            var moldeModeloDatos = moldesRepositorio.GetEntidad();
            listaMoldes = new List<ModeloMoldes>();
            foreach (Moldes molde in moldeModeloDatos)
            {
                listaMoldes.Add(new ModeloMoldes
                {
                    id = molde.Id,
                    codigo = molde.Codigo,
                    cliente = molde.Cliente,
                    fechaEntrada = molde.FechaEntrada,
                    fechaSalida = molde.FechaSalida,
                    enTaller =molde.EnTaller
                });
            }
            return listaMoldes;
        }

        public ModeloMoldes MostrarMoldes(int idMolde)
        {
            listaMoldes = MostrarMoldes();
            return listaMoldes.First(m => m.Id.Equals(idMolde));
        }

        public void Dispose()
        {

        }
    }
}
