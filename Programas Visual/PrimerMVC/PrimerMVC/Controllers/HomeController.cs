using PrimerMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrimerMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var m = new VistasParciales();

            m.Imagen = "~/Content/FotoCasa.jpg";
            m.Nombre = "Pedro";
            m.Apellido = "Perez";
            m.Direccion = "No conocida";
            m.Contrasena = "nolasabes";

            return View(m);
        }

        //[HttpPost]
        //public ActionResult Index(Persona p)
        //{
        //    return View(p);
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}