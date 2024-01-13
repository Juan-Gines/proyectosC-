using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccesoDatos.Contratos;
using AccesoDatos.Entidades;
using AccesoDatos.Repositorios;
using Dominio.ValoresObjetos;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace Dominio.Modelos
{
    public class ModeloEmpleado : IDisposable
    {
        private int id;
        private string nombre;
        private string apellido;
        private string cargo;
        private string telefono;
        private List<ModeloEmpleado> listaEmpleados;

        private IEmpleadoRepositorio empleadoRepositorio;
        public EstadoEntidad Estado { get; set; }

        //PROPIEDADES//MODELO DE VISTA//VALIDAR DATOS

        public int Id { get => id; set => id = value; }
        [Required(ErrorMessage ="El campo nombre es requerido")]
        [RegularExpression("^[a-zA-Zá-úà-ù ]+$",ErrorMessage ="Por favor introduzca sólo caracteres alfabéticos")]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string Nombre { get => nombre; set => nombre = value; }
        [RegularExpression("^[a-zA-Zá-úà-ù ]+$", ErrorMessage = "Por favor introduzca sólo caracteres alfabéticos")]
        [StringLength(maximumLength: 50)]
        public string Apellido { get => apellido; set => apellido = value; }
        [Required(ErrorMessage = "El campo cargo es requerido")]
        [RegularExpression("^[a-zA-Zá-úà-ù ]+$", ErrorMessage = "Por favor introduzca sólo caracteres alfabéticos")]
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        public string Cargo { get => cargo; set => cargo = value; }
        [RegularExpression("(^[0-9]+)",ErrorMessage ="Por favor introduzca un número de teléfono válido")]
        [StringLength(maximumLength: 9, ErrorMessage ="Por favor introduzca un número teléfono de 9 dígitos",MinimumLength =9)]
        public string Telefono { get => telefono; set => telefono = value; }

        public ModeloEmpleado()
        {
            empleadoRepositorio = new EmpleadoRepositorio();

        }

        public string GuardarCambios()
        {
            string mensaje=null;
            try
            {
                var empleadoModeloDatos = new Empleado();
                empleadoModeloDatos.Id = id;
                empleadoModeloDatos.Nombre = nombre;
                empleadoModeloDatos.Apellido = apellido;
                empleadoModeloDatos.Cargo = cargo;
                empleadoModeloDatos.Telefono = telefono;

                switch (Estado)
                {
                    case EstadoEntidad.Añadido:
                        empleadoRepositorio.Insertar(empleadoModeloDatos);
                        mensaje = "Añadido correctamente";
                        break;
                    case EstadoEntidad.Editado:
                        empleadoRepositorio.Editar(empleadoModeloDatos);
                        mensaje = "Editado correctamente";
                        break;
                    case EstadoEntidad.Borrado:
                        empleadoRepositorio.Borrar(id);
                        mensaje = "Borrado correctamente";
                        break;
                }

            }catch(Exception ex)
            {
                System.Data.SqlClient.SqlException sqlEx = ex as System.Data.SqlClient.SqlException;
                if (sqlEx != null && sqlEx.Number == 2627)
                    mensaje = "Este empleado ya está registrado";
                else
                    mensaje = ex.ToString();
            }
            return mensaje;
        }

        public List<ModeloEmpleado> MostrarEmpleado()
        {
            var empleadoModeloDatos = empleadoRepositorio.GetEntidad();
            listaEmpleados = new List<ModeloEmpleado>();
            foreach(Empleado empleado in empleadoModeloDatos)
            {
                listaEmpleados.Add(new ModeloEmpleado
                {
                    id = empleado.Id,
                    nombre = empleado.Nombre,
                    apellido = empleado.Apellido,
                    cargo = empleado.Cargo,
                    telefono = empleado.Telefono
                });
            }
            return listaEmpleados;
        }
        
        public ModeloEmpleado MostrarEmpleado(int idEm)
        {
            listaEmpleados = MostrarEmpleado();
            return listaEmpleados.First(e => e.Id.Equals(idEm));
        }

        public void Dispose()
        {
            
        }

        
    }
}
