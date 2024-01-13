using Dominio.ValoresObjetos;
using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using WsMosca.Modelos;
using Dominio.ValoresObjetos;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PruebaDefinitiva.Manager
{
    public class EmpleadoManager
    {
        
        
        private string _url = "https://localhost:44378/Api/Empleado";

        public string GuardarCambios(ModeloEmpleado empleado)
        {
            string mensaje = null;
            try
            {
                var empleadoModeloDatos = new Empleado()
                {
                    Id = empleado.Id,
                    Nombre = empleado.Nombre,
                    Apellido = empleado.Apellido,
                    Cargo = empleado.Cargo,
                    Telefono = empleado.Telefono
                };

                switch (empleado.Estado)
                {
                    case EstadoEntidad.Insertado:
                        mensaje = Insertar(empleadoModeloDatos);
                        break;
                    case EstadoEntidad.Actualizado:
                        mensaje = Actualizar(empleadoModeloDatos);
                        break;
                    case EstadoEntidad.Borrado:
                        mensaje = Borrar(empleadoModeloDatos.Id);
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

        public string Insertar(Empleado empleado)
        {
            var url = _url ;
            var client = new HttpClient();     

            var datastring = JsonConvert.SerializeObject(empleado);
            var content = new StringContent(datastring);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response= client.PostAsync(url, content);

            if (response.Result.IsSuccessStatusCode)
            {
                return "Nuevo empleado guardado correctamente";
            }
            else
            {
                return "Y una mierda";
            }         

        }

        public string Actualizar(Empleado empleado)
        {
            var url = _url;
            var client = new HttpClient();

            var datastring = JsonConvert.SerializeObject(empleado);
            var content = new StringContent(datastring);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.PutAsync(url, content);

            if (response.Result.IsSuccessStatusCode)
            {
                return "Empleado actualizado correctamente";
            }
            else
            {
                return "Y una mierda";
            }

        }

        public string Borrar(int id)
        {
            var url = _url + "?Id=" + id.ToString();
            var client = new HttpClient();

            //var datastring = JsonConvert.SerializeObject(id);
            //var content = new StringContent(datastring);
            //content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.DeleteAsync(url);

            if (response.Result.IsSuccessStatusCode)
            {
                return "Empleado borrado correctamente";
            }
            else
            {
                return "Y una mierda";
            }

        }


        public List<ModeloEmpleado> MostrarEmpleado()
        {
            var url = _url ;
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return new List<ModeloEmpleado>();
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string jsonString = objReader.ReadToEnd();
                            var Empleado = JsonConvert.DeserializeObject<ResModeloEmpleados>(jsonString);
                            return Empleado.Empleados;
                        }
                    }

                }
            }
            catch (WebException ex)
            {
                // Handle error
                return new List<ModeloEmpleado>();
            }

        }

        public ModeloEmpleado MostrarEmpleado(int idEm)
        {
            var url = _url + "?Id=" + idEm.ToString();
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return new ModeloEmpleado();
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string jsonString = objReader.ReadToEnd();
                            var Empleado = JsonConvert.DeserializeObject<ResModeloEmpleado>(jsonString);
                            return Empleado.Empleado;
                        }
                    }

                }
            }
            catch (WebException ex)
            {
                // Handle error
                return new ModeloEmpleado();
            }



            //listaEmpleados = MostrarEmpleado();
            //if (listaEmpleados.Any(e => e.Id.Equals(idEm)))
            //{
            //    return listaEmpleados.First(e => e.Id.Equals(idEm));
            //}

        }

        public void Dispose()
        {

        }
    }
}