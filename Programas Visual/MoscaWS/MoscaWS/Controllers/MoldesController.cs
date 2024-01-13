using AccesoDatos.Repositorio;
using Dominio.Modelos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WsMosca.Modelos;

namespace MoscaWS.Controllers
{
    public class MoldesController : ApiController
    {
        // GET: api/Empleado/5
        public ResModeloMolde Get(int id)
        {
            ModeloMoldes m = new ModeloMoldes();
            ResModeloMolde mRes = new ResModeloMolde(m.MostrarMolde(id));
            return mRes;
        }

        // GET: api/Empleado/5 
        public ResModeloMoldes Get()
        {
            ModeloMoldes me = new ModeloMoldes();
            List<ModeloMoldes> m = new List<ModeloMoldes>();
            m = me.MostrarMoldes();
            ResModeloMoldes mRes = new ResModeloMoldes(m);
            return mRes;
        }

        // POST: api/Empleado       
        public void Post([FromBody] Moldes molde)
        {
            RepositorioMoldes em = new RepositorioMoldes();
            em.Insertar(molde);
        }


        // PUT: api/Empleado/5
        public void Put([FromBody] Moldes molde)
        {
            RepositorioMoldes em = new RepositorioMoldes();
            em.Actualizar(molde);
        }

        // DELETE: api/Empleado/5
        public void Delete(int id)
        {
            RepositorioMoldes em = new RepositorioMoldes();
            em.Borrar(id);
        }
    }
}
