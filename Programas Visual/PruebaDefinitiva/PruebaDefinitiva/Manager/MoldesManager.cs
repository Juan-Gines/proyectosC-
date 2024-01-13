using Dominio.Modelos;
using Dominio.ValoresObjetos;
using Entidades;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PruebaDefinitiva.Manager
{
    public class MoldesManager
    {
        private string _url = "https://localhost:44378/Api/Moldes";

        public string GuardarCambios(ModeloMoldes molde)
        {
            string mensaje = null;
            try
            {
                var moldeModeloDatos = new Moldes()
                {
                    Id = molde.Id,
                    Codigo = molde.Codigo,
                    ClienteMolde = molde.ClienteMolde,
                    FechaEntrada = molde.FechaEntrada,
                    FechaSalida = molde.FechaSalida,
                    EnTaller = molde.EnTaller
                };

                switch (molde.Estado)
                {
                    case EstadoEntidad.Insertado:
                        mensaje = Insertar(moldeModeloDatos);
                        break;
                    case EstadoEntidad.Actualizado:
                        mensaje = Actualizar(moldeModeloDatos);
                        break;
                    case EstadoEntidad.Borrado:
                        mensaje = Borrar(moldeModeloDatos.Id);
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

        public string Insertar(Moldes molde)
        {
            var url = _url;
            var client = new HttpClient();

            var datastring = JsonConvert.SerializeObject(molde);
            var content = new StringContent(datastring);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.PostAsync(url, content);

            if (response.Result.IsSuccessStatusCode)
            {
                return "Nuevo molde guardado correctamente";
            }
            else
            {
                return "Y una mierda";
            }

        }

        public string Actualizar(Moldes molde)
        {
            var url = _url;
            var client = new HttpClient();

            var datastring = JsonConvert.SerializeObject(molde);
            var content = new StringContent(datastring);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.PutAsync(url, content);

            if (response.Result.IsSuccessStatusCode)
            {
                return "Molde actualizado correctamente";
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
                return "Molde borrado correctamente";
            }
            else
            {
                return "Y una mierda";
            }

        }


        public List<ModeloMoldes> MostrarMoldes()
        {
            var url = _url;
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
                        if (strReader == null) return new List<ModeloMoldes>();
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string jsonString = objReader.ReadToEnd();
                            var Moldes = JsonConvert.DeserializeObject<ResModeloMoldes>(jsonString);
                            return Moldes.Moldes;
                        }
                    }

                }
            }
            catch (WebException ex)
            {
                // Handle error
                return new List<ModeloMoldes>();
            }

        }

        public ModeloMoldes MostrarMolde(int idEm)
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
                        if (strReader == null) return new ModeloMoldes();
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string jsonString = objReader.ReadToEnd();
                            var Moldes = JsonConvert.DeserializeObject<ResModeloMolde>(jsonString);
                            return Moldes.Molde;
                        }
                    }

                }
            }
            catch (WebException ex)
            {
                // Handle error
                return new ModeloMoldes();
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
