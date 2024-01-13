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
        [DataType(DataType.Date)]        
        public DateTime FechaEntrada { get => fechaEntrada; set => fechaEntrada = value; }
        [Required]
        [DataType(DataType.Date)]        
        public DateTime FechaSalida { get => fechaSalida; set => fechaSalida = value; }
        [Required]        
        public bool EnTaller { get => enTaller; set => enTaller = value; }


        public ModeloMoldes()
        {
            repositorioMoldes = new RepositorioMoldes();
            id = -1;
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
                System.Data.SqlClient.SqlException sqlEx = ex.InnerException.InnerException as System.Data.SqlClient.SqlException;
                if (sqlEx != null && sqlEx.Number == 2627)
                    mensaje = "Este molde ya está registrado";
                else
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
    public class ResModeloMolde
    {
        public bool Ok { get; set; }
        public string Error { get; set; }
        public ModeloMoldes Molde;
        

        public ResModeloMolde(ModeloMoldes molde)
        {

            if (molde.Id > -1)
            {
                Ok = true;
                Error = "";
                this.Molde = molde;
            }
            else
            {
                Ok = false;
                Error = "No se ha encontrado un usuario válido";
                this.Molde = molde;
            }

        }

    }

    public class ResModeloMoldes
    {
        public bool Ok { get; set; }
        public string Error { get; set; }
        public ModeloMoldes Molde;
        public List<ModeloMoldes> Moldes;

        public ResModeloMoldes(List<ModeloMoldes> moldes)
        {
            if (moldes.Any())
            {
                Ok = true;
                Error = "";
                this.Moldes = moldes;
            }
            else
            {
                Ok = false;
                Error = "No se ha encontrado un usuario válido";
                this.Moldes = moldes;
            }

        }
    }
}
