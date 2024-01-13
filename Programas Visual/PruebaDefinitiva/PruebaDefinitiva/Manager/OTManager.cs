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
    public class OTManager
    {
        private string _url = "https://localhost:44378/Api/OT";

        public string GuardarCambios(ModeloOT ot)
        {
            string mensaje = null;
            //try
            //{
            //    var otModeloDatos = new OT()
            //    {
            //        Id = ot.Id,
            //        Numero = ot.Numero,
            //        IdMolde = ot.IdMolde,
            //        HorasTrabajo = ot.HorasTrabajo,
            //        Tipo = ot.Tipo,
            //        ClienteOT = ot.ClienteOT,
            //        Acabada = ot.Acabada
            //    };

                switch (ot.Estado)
                {
                    case EstadoEntidad.Insertado:
                        mensaje = Insertar(ot);
                        break;
                    case EstadoEntidad.Actualizado:
                        mensaje = Actualizar(ot);
                        break;
                    case EstadoEntidad.Borrado:
                        mensaje = Borrar(ot.Id);
                        break;
                }
            //}
            //catch (Exception ex)
            //{
            //    System.Data.SqlClient.SqlException sqlEx = ex.InnerException.InnerException as System.Data.SqlClient.SqlException;
            //    if (sqlEx != null && sqlEx.Number == 2627)
            //        mensaje = "Esta orden de trabajo ya está registrada";
            //    else
            //        mensaje = ex.ToString();
            //}
            return mensaje;

        }
        public string Insertar(ModeloOT ot)
        {
            var url = _url;
            var client = new HttpClient();

            var datastring = JsonConvert.SerializeObject(ot);
            var content = new StringContent(datastring);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            var response = client.PostAsync(url, content);

            if (response.Result.IsSuccessStatusCode)
            {
                return "Nueva ot guardada correctamente";
            }
            else
            {
                return "Esta orden de trabajo ya está registrada";
            }

        }

        public string Actualizar(ModeloOT ot)
        {
            var url = _url;
            var client = new HttpClient();

            var datastring = JsonConvert.SerializeObject(ot);
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


        public List<ModeloOT> MostrarOT()
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
                        if (strReader == null) return new List<ModeloOT>();
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string jsonString = objReader.ReadToEnd();
                            var OT = JsonConvert.DeserializeObject<ResModeloOTs>(jsonString);
                            return OT.ots;
                        }
                    }

                }
            }
            catch (WebException ex)
            {
                // Handle error
                return new List<ModeloOT>();
            }

        }

        public ModeloOT MostrarOT(int idEm)
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
                        if (strReader == null) return new ModeloOT();
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string jsonString = objReader.ReadToEnd();
                            var OT = JsonConvert.DeserializeObject<ResModeloOT>(jsonString);
                            return OT.ot;
                        }
                    }

                }
            }
            catch (WebException ex)
            {
                // Handle error
                return new ModeloOT();
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
