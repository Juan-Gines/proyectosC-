using AccesoDatos.Repositorio;
using Dominio.Modelos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MoscaWS.Controllers
{
    public class OTController : ApiController
    {
        string mensaje="";
        public ResModeloOT Get(int id)
        {
            ModeloOT m = new ModeloOT();
            ResModeloOT mRes = new ResModeloOT(m.MostrarOT(id));
            return mRes;
        }

        // GET: api/Empleado/5 
        public ResModeloOTs Get()
        {
            ModeloOT me = new ModeloOT();
            List<ModeloOT> m = new List<ModeloOT>();
            m = me.MostrarOT();
            ResModeloOTs mRes = new ResModeloOTs(m);
            return mRes;
        }

        // POST: api/Empleado       
        public string Post([FromBody] ModeloOT ot)
        {
            //ModeloOT em = new ModeloOT();
            mensaje=ot.GuardarCambios();
            return mensaje;
        }


        // PUT: api/Empleado/5
        public IHttpActionResult Put([FromBody] ModeloOT ot)
        {
            ModeloOT em = new ModeloOT();
            mensaje=ot.GuardarCambios();
            if (mensaje == "Esta orden de trabajo ya está registrada")
            {
                IHttpActionResult result;

                return result;
            }
        }

        // DELETE: api/Empleado/5
        public void Delete(int id)
        {
            RepositorioOT em = new RepositorioOT();
            em.Borrar(id);
        }
    }
}
