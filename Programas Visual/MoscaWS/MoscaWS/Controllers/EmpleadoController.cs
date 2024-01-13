
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WsMosca.Modelos;
using AccesoDatos.Repositorio;
using Entidades;

namespace MoscaWS.Controllers
{
    public class EmpleadoController : ApiController
    {

        // GET: api/Empleado/5
           public ResModeloEmpleado Get(int id)
        {
            ModeloEmpleado m = new ModeloEmpleado();
            ResModeloEmpleado mRes = new ResModeloEmpleado(m.MostrarEmpleado(id));
            return mRes;
        }

        // GET: api/Empleado/5 
        public ResModeloEmpleados Get()
        {
            ModeloEmpleado me = new ModeloEmpleado();
            List<ModeloEmpleado> m = new List<ModeloEmpleado>();
            m = me.MostrarEmpleado();
            ResModeloEmpleados mRes = new ResModeloEmpleados(m);
            return mRes;
        }

        // POST: api/Empleado       
        public void Post([FromBody] Empleado empleado)
        {
            RepositorioEmpleado em = new RepositorioEmpleado();     
            em.Insertar(empleado);
        }


        // PUT: api/Empleado/5
        public void Put([FromBody] Empleado empleado)
        {
            RepositorioEmpleado em = new RepositorioEmpleado();
            em.Actualizar(empleado);
        }

        // DELETE: api/Empleado/5
        public void Delete(int id)
        {
            RepositorioEmpleado em = new RepositorioEmpleado();
            em.Borrar(id);
        }
    }
}
