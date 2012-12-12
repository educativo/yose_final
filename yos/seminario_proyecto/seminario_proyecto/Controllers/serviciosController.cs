using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using seminario_proyecto.Models;

namespace seminario_proyecto.Controllers
{
    public class serviciosController : Controller
    {
        //
        // GET: /servicios/

        public ActionResult Index()
        {
            return View();
        }

        //registrar  categoria
        [Authorize(Roles = "administrador")]
        public ActionResult regcategoria()
        {
            ConectorDataContext db = new ConectorDataContext();
            ViewBag.lista1 = from a in db.categorias select a;
           
            return View();
        }

        [HttpPost]
        public ActionResult regcategoria(cate newcate)
        {

            if (ModelState.IsValid)
            {
                ConectorDataContext db = new ConectorDataContext();
                ViewBag.lista1 = from a in db.categorias select a;
           
                categoria ca = new categoria();

                ca.nombre = newcate.nombre_cate;
                db.categorias.InsertOnSubmit(ca);
                db.SubmitChanges();
                return Redirect("../servicios/regcategoria");
            }
            return View();
            
        }

        //registrar servicios/////////////
        [Authorize(Roles = "administrador")]

        public ActionResult regservicios()
        {
            //List<tipo> lista = new List<tipo>();
            //lista.Add(new tipo());
            ConectorDataContext db = new ConectorDataContext();
            ViewBag.lista2 = from a in db.servicios select a;
           
            var t = db.categorias.ToList();
            ViewBag.cate = t;
            return View();
        }

        [HttpPost]
        public ActionResult regservicios(servicios1 newservi)
        {

            if (ModelState.IsValid)
            {
                ConectorDataContext db = new ConectorDataContext();
                ViewBag.lista2 = from a in db.servicios select a;
            

                var t = db.categorias.ToList();
                ViewBag.cate = t;
                servicio ser = new servicio();

                 db.SubmitChanges();
                int idB = db.categorias.OrderByDescending(a => a.id).First().id;
                ser.idcate = idB;

                ser.servicio_nombre = newservi.servicio;
                ser.precio = newservi.precio;
                Convert.ToString(newservi.categoria);
                string al = newservi.categoria;

                ser.categoria_nombre = al;
                db.servicios.InsertOnSubmit(ser);
                db.SubmitChanges();
                return Redirect("../servicios/regservicios");
            }
            return View();
        }

        public ActionResult eliminar1(string ID)
        {
            int id = Convert.ToInt32(ID);
            ConectorDataContext db = new ConectorDataContext();

            categoria m = db.categorias.Single(u => u.id == id);
            db.categorias.DeleteOnSubmit(m);

            db.SubmitChanges();

            return Redirect("/servicios/regcategoria");
        }
        public ActionResult eliminar2(string ID)
        {
            int id = Convert.ToInt32(ID);
            ConectorDataContext db = new ConectorDataContext();

            servicio m = db.servicios.Single(u => u.id == id);
            db.servicios.DeleteOnSubmit(m);

            db.SubmitChanges();

            return Redirect("/servicios/regservicios");
        }

        
    }
}
