using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using seminario_proyecto.Models;

namespace seminario_proyecto.Controllers
{
    public class recervasController : Controller
    {
        //
        // GET: /recervas/
        
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult vecliente()
        {
            ConectorDataContext db = new ConectorDataContext();
            return Json(new { nombres = db.personas.Select(a => a.cliente.nombre+" "+a.apellidos).ToList() });

        }
        public JsonResult Resumen(int idhabi,string fechaini,string fechafin,string nombress)
        {
            ConectorDataContext db = new ConectorDataContext();
            string nombre = (from clid in db.personas where (nombress == (clid.cliente.nombre + " " + clid.apellidos).ToString()) select clid.cliente.nombre).First();
            string apellido = (from clid in db.personas where (nombress == (clid.cliente.nombre + " " + clid.apellidos).ToString()) select clid.apellidos).First();
            string tipo = (from habi in db.habitacions where(idhabi==habi.id) select habi.tipo).First();
            string fechain = fechaini;
            string fechafi = fechafin;
            int idtipo = Convert.ToInt32((from habi in db.habitacions where (idhabi == habi.id) select habi.idtipo).First());
            string precio = ((from tip in db.tipos where (tip.id == idtipo) select tip.precio).First()).ToString();

            string[] cadena = { nombre,apellido, tipo, fechaini, fechafin, precio };
            return Json(cadena);
        }

        [Authorize(Roles = "usuario")]
        public ActionResult nuevareserva()
        {
            ConectorDataContext db = new ConectorDataContext();
            ViewBag.listarecer = from a in db.recervas select a;

            var t = db.habitacions.ToList();
            ViewBag.habit = t;
        
            return View();      
        }
        [HttpPost]
        public ActionResult nuevareserva(reserva1 rec)
        {
            if (ModelState.IsValid)
            {
                ConectorDataContext db = new ConectorDataContext();
                 // var vari = from a in db.recervas select a;

                 ViewBag.listarecer=from a in db.recervas select a;

                var t = db.habitacions.ToList();
                ViewBag.habit = t;
        

                recerva re = new recerva();
                re.idcliente = (from cliid in db.personas where(rec.nombreapellido == (cliid.cliente.nombre+" "+cliid.apellidos).ToString()) select cliid.idcli).First();
                re.idhabi= rec.id;

                string inicio = rec.fechaini.ToString("yyyy-MM-dd");
                string final = rec.fechafinal.ToString("yyyy-MM-dd");

                re.fechainiciorecer = rec.fechaini;
                re.fechafinrecer = rec.fechafinal;
                           
                re.dia=rec.fechafinal.DayOfYear-rec.fechaini.DayOfYear;

                var idtip= (from pre in db.habitacions where(pre.id==re.idhabi) select pre.idtipo).First();
                re.precio = (from prec in db.tipos where (idtip == prec.id) select prec.precio).First().ToString(); 

                /*
                var t = db.habitacions.ToList();
                ViewBag.habit = t;
            
                cliente cli = new cliente();
                habitacion ha = new habitacion();

                int idC = db.habitacions.OrderByDescending(a => a.id).First().id;
                re.idhabi = idC;
                re.fechainiciorecer = rec.fechaini;
                re.fechafinrecer = rec.fechafinal;
                int d = Convert.ToInt32(re.fechafinrecer.Subtract(re.fechainiciorecer));
                re.dia = d;
                re.precio = rec.precio;
                */
                db.recervas.InsertOnSubmit(re);
                db.SubmitChanges();
                ViewBag.Message = "Datos insertados correctamente";
                return View();
                //return RedirectToAction("exito", "habitaciones");
             
            }
           
            return View();
        }
        public ActionResult factura() {


            return View();        
        }
        public ActionResult registrarcliente(reserva1 re)
        {
            if (ModelState.IsValid)
            {
                ConectorDataContext db = new ConectorDataContext();
                cliente cli = new cliente();
                persona per = new persona();

                cli.nombre = re.percli.nombre;
                cli.telefono = re.percli.telefono;
                cli.direccion = re.percli.direccion;
                cli.email = re.percli.email;
                cli.ciudad = re.percli.ciudad;
                cli.estado = re.percli.estado;
                cli.pais = re.percli.pais;
                db.clientes.InsertOnSubmit(cli);

                int idA = db.clientes.OrderByDescending(a => a.id).First().id;

                per.idcli = idA;
                per.apellidos = re.percli.apelli;
                per.pasaporte = re.percli.pasaporte;
                per.comentario = re.percli.comen;
                per.cumpleaños = re.percli.cumple;
                db.personas.InsertOnSubmit(per);
                db.SubmitChanges();
                return Redirect("#");
                //return RedirectToAction("exito", "habitaciones");
            }
            return View();
        }
    }
}
