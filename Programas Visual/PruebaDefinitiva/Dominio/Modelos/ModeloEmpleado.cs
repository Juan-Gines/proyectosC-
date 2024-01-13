using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Repositorio;
using System.ComponentModel.DataAnnotations;
using Entidades;
using Dominio.ValoresObjetos;
using AccesoDatos.Contratos;


namespace WsMosca.Modelos
{
    public class ModeloEmpleado : IDisposable
    {
        private int id;
        private string nombre;
        private string apellido;
        private string cargo;
        private string telefono;
        private List<ModeloEmpleado> listaEmpleados;


        public EstadoEntidad Estado;
        private IRepositorioEmpleado repositorioEmpleado;

        public int Id { get => id; set => id = value; }
        [Required(ErrorMessage = "El campo nombre es requerido")]
        [RegularExpression("^[a-zA-Zá-úà-ù ]+$", ErrorMessage = "Por favor introduzca sólo caracteres alfabéticos")]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string Nombre { get => nombre; set => nombre = value; }
        [RegularExpression("^[a-zA-Zá-úà-ù ]+$", ErrorMessage = "Por favor introduzca sólo caracteres alfabéticos")]
        [StringLength(maximumLength: 50)]
        public string Apellido { get => apellido; set => apellido = value; }
        public string Cargo { get => cargo; set => cargo = value; }
        [RegularExpression("(^[0-9]+)", ErrorMessage = "Por favor introduzca un número de teléfono válido")]
        [StringLength(maximumLength: 9, ErrorMessage = "Por favor introduzca un número teléfono de 9 dígitos", MinimumLength = 9)]
        public string Telefono { get => telefono; set => telefono = value; }

        public ModeloEmpleado()
        {
            repositorioEmpleado = new RepositorioEmpleado();
            id = -1;
        }

        public string GuardarCambios()
        {
            string mensaje = null;
            try
            {
                var empleadoModeloDatos = new Empleado()
                {
                    Id = id,
                    Nombre = nombre,
                    Apellido = apellido,
                    Cargo = cargo,
                    Telefono = telefono
                };

                switch (Estado)
                {
                    case EstadoEntidad.Insertado:
                        mensaje = repositorioEmpleado.Insertar(empleadoModeloDatos);
                        break;
                    case EstadoEntidad.Actualizado:
                        mensaje = repositorioEmpleado.Actualizar(empleadoModeloDatos);
                        break;
                    case EstadoEntidad.Borrado:
                        mensaje = repositorioEmpleado.Borrar(id);
                        break;
                }

            }
            catch (Exception ex)
            {
                System.Data.SqlClient.SqlException sqlEx = ex.InnerException.InnerException as System.Data.SqlClient.SqlException;
                if (sqlEx != null && sqlEx.Number == 2627)
                    mensaje = "Este empleado ya está registrado";
                else
                    mensaje = ex.ToString();
            }
            return mensaje;
        }

        public List<ModeloEmpleado> MostrarEmpleado()
        {
            var empleadoModeloDatos = repositorioEmpleado.GetEntidades();
            listaEmpleados = new List<ModeloEmpleado>();
            foreach (Empleado item in empleadoModeloDatos)
            {
                listaEmpleados.Add(new ModeloEmpleado
                {
                    id = item.Id,
                    nombre = item.Nombre,
                    apellido = item.Apellido,
                    cargo = item.Cargo,
                    telefono = item.Telefono
                });
            }
            return listaEmpleados;
        }

        public ModeloEmpleado MostrarEmpleado(int idEm)
        {
            listaEmpleados = MostrarEmpleado();
            if (listaEmpleados.Any(e => e.Id.Equals(idEm)))
            {
                return listaEmpleados.First(e => e.Id.Equals(idEm));
            }
            return new ModeloEmpleado();
        }

        public void Dispose()
        {

        }
    }
    public class ResModeloEmpleado
    {
        public bool Ok { get; set; }
        public string Error { get; set; }
        public ModeloEmpleado Empleado;
        public List<ModeloEmpleado> Empleados;

        public ResModeloEmpleado(ModeloEmpleado Empleado)
        {

            if (Empleado.Id > -1)
            {
                Ok = true;
                Error = "";
                this.Empleado = Empleado;
            }
            else
            {
                Ok = false;
                Error = "No se ha encontrado un usuario válido";
                this.Empleado = Empleado;
            }

        }
       
    }

    public class ResModeloEmpleados
    {
        public bool Ok { get; set; }
        public string Error { get; set; }
        public ModeloEmpleado Empleado;
        public List<ModeloEmpleado> Empleados;

              public ResModeloEmpleados(List<ModeloEmpleado> Empleados)
        {
            if (Empleados.Any())
            {
                Ok = true;
                Error = "";
                this.Empleados = Empleados;
            }
            else
            {
                Ok = false;
                Error = "No se ha encontrado un usuario válido";
                this.Empleados = Empleados;
            }

        }
    }
}
