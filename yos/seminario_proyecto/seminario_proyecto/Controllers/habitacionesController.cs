using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using seminario_proyecto.Models;

namespace seminario_proyecto.Controllers
{
    public class habitacionesController : Controller
    {
        //
        // GET: /habitaciones/

        public ActionResult Index()
        {
            return View();
        }


        public ActionResult exito()
        {
            return View();
        }

        public ActionResult exitoman()
        {
            return View();
        }
        [Authorize(Roles = "administrador")]
        public ActionResult registrar()
        {
            //List<tipo> lista = new List<tipo>();
            //lista.Add(new tipo());
            ConectorDataContext db = new ConectorDataContext();
            var t= db.tipos.ToList();
            ViewBag.tipo = t;
            return View();
        }

    [HttpPost]
        public ActionResult registrar(RegHabitacion newhabitacion)
        {
            
            if (ModelState.IsValid)
            {
                ConectorDataContext db = new ConectorDataContext();
                var t = db.tipos.ToList();
                ViewBag.tipo = t;
                habitacion habi = new habitacion();

               // habi.idtipo = newhabitacion.nombre;

                //ti.nombre = newhabitacion.nombre;
                //ti.descripcion = newhabitacion.descripcion;
                //ti.precio = newhabitacion.precio;
               // db.tipos.InsertOnSubmit(ti);
                db.SubmitChanges();
                int idB = db.tipos.OrderByDescending(a => a.id).First().id;
                habi.idtipo = idB;
                
                habi.estado = newhabitacion.estado;
                Convert.ToString(newhabitacion.tip);
                string al = newhabitacion.tip;
                    
                habi.tipo = al;
                db.habitacions.InsertOnSubmit(habi);
                db.SubmitChanges();
                return Redirect("../habitaciones/mostrarhabitacion");
            }
            return View();
        }

//  registrar tipo
        [Authorize(Roles = "administrador")]
    public ActionResult ReTipo()
    {
               return View();
    }

    [HttpPost]
    public ActionResult ReTipo(regTipo newhabitacion)
    {

        if (ModelState.IsValid)
        {
            ConectorDataContext db = new ConectorDataContext();

            tipo ti = new tipo();
            
            // habi.idtipo = newhabitacion.nombre;

            //ti.nombre = newhabitacion.nombre;
            //ti.descripcion = newhabitacion.descripcion;
            //ti.precio = newhabitacion.precio;
            // db.tipos.InsertOnSubmit(ti);


            ti.nombre = newhabitacion.nombre;
            ti.dercripcion = newhabitacion.descri;
            ti.precio = Convert.ToDouble(newhabitacion.precio); 
            db.tipos.InsertOnSubmit(ti);
            db.SubmitChanges();
            return Redirect("../habitaciones/mostrartipo");
        }
        return View();
    }

    //registrar mantenimiento
        [Authorize(Roles = "administrador")]
    public ActionResult regMantencion()
    {
        //lista.Add(new tipo());
        ConectorDataContext db = new ConectorDataContext();
        var t = db.habitacions.ToList();
        ViewBag.habit  = t;
        return View();
    }
    [HttpPost]
    public ActionResult regMantencion(mantencion newmantencion)
    {

        if (ModelState.IsValid)
        {
            ConectorDataContext db = new ConectorDataContext();
            var t = db.habitacions.ToList();
            ViewBag.habit = t;
               
            mantenimiento man = new mantenimiento();

            int idC = db.habitacions.OrderByDescending(a => a.id).First().id;
            man.idhabi = idC;
            man.fechaini = newmantencion.fecha;
            man.fechafin = newmantencion.fecha2;
            
            db.mantenimientos.InsertOnSubmit(man);
            db.SubmitChanges();

            return Redirect("../habitaciones/mostrarmantencion");
        }
        return View();
    }

    // actualizar datos DE HABITACION

    [Authorize(Roles = "administrador")]
    public ActionResult actualizarhabi(string id)
    {
        ConectorDataContext dt = new ConectorDataContext();
        int ID = Convert.ToInt32(id);

        if ((from i in dt.habitacions where i.id == ID select i).ToList().Count == 0)
        {
            ViewBag.flag = 0;
        }

        else
        {
            var x = (from i in dt.habitacions
                     where i.id == ID
                     select new
                     {   id=i.id,
                         ti=i.tipo,
                         estado=i.estado
  
                     }).ToArray()[0];

            ViewBag.id = x.id;
            ViewBag.tipo = x.ti;
            ViewBag.estado = x.estado;
            ViewBag.flag = 1;
        }


        return View();
    }
    [HttpPost]
    public ActionResult actualizar(RegHabitacion ha)
    {
        ConectorDataContext db = new ConectorDataContext();
        
        tipo t = db.tipos.Single(u => u.id == ha.id);
        habitacion h = db.habitacions.Single(u => u.id == ha.id);
        h.tipo = ha.tip;
        h.estado = ha.estado;
        db.SubmitChanges();
        return Redirect("../habitaciones/mostrarhabitacion");
    }

        // ELIMINAR  HABITACION

        [Authorize(Roles = "administrador")]

    public ActionResult eliminarhabi()
    {
        ViewBag.flag = 0;
        return View();
    }

    [HttpPost]
    public ActionResult eliminarhabi(RegHabitacion ha)
    {
        ConectorDataContext dt = new ConectorDataContext();


        if ((from i in dt.habitacions where i.id == ha.id select i).ToList().Count == 0)
        {
            ViewBag.flag = 0;
        }

        else
        {
            var x = (from i in dt.tipos
                     join j in dt.habitacions on i.id equals j.idtipo
                     where i.id == ha.id
                     select new
                     {
                         id = i.id,
                         ti = j.tipo,
                         estado = j.estado

                     }).ToArray()[0];

            ViewBag.id = x.id;
            ViewBag.tipo = x.ti;
            ViewBag.estado = x.estado;
            ViewBag.flag = 1;
        }


        return View();
    }
   
    public ActionResult eliminar(string ID)
    {
        int id = Convert.ToInt32(ID);
        
        ConectorDataContext db = new ConectorDataContext();
        
        habitacion h = db.habitacions.Single(u => u.id == id);
        db.habitacions.DeleteOnSubmit(h);
        
        db.SubmitChanges();
        return Redirect("/habitaciones/mostrarhabitacion");
    }

        //actulizar tipo
    [Authorize(Roles = "administrador")]
    public ActionResult actualizartipo(string id)
    {
        ConectorDataContext dt = new ConectorDataContext();
        int ID = Convert.ToInt32(id);

        if ((from i in dt.tipos where i.id == ID select i).ToList().Count == 0)
        {
            ViewBag.flag = 0;
        }

        else
        {
            var x = (from i in dt.tipos
                   
                     where i.id == ID
                     select new
                     {
                         id = i.id,
                         nombre = i.nombre,
                         descripcion = i.dercripcion,
                         precio = i.precio

                     }).ToArray()[0];

            ViewBag.id = x.id;
            ViewBag.nombre = x.nombre;
            ViewBag.descripcion = x.descripcion;
            ViewBag.precio = x.precio;
            ViewBag.flag = 1;
        }


        return View();
    }
    [HttpPost]
    public ActionResult actualizar2(regTipo ti)
    {
        ViewBag.hola = ti.id;
        ConectorDataContext db = new ConectorDataContext();

        tipo t = db.tipos.Single(u => u.id == ti.id);
        //habitacion h = db.habitacions.Single(u => u.id == ha.id);



        t.nombre = ti.nombre;
        t.dercripcion = ti.descri;
        Convert.ToString(ti.precio);
        string al = ti.precio;
        ti.precio = al;
        //ti.precio = Convert.ToDouble(ti.precio);
        db.SubmitChanges();
        return Redirect("../habitaciones/mostrartipo");
    }

//eliminar tipo

   [Authorize(Roles = "administrador")]
    public ActionResult eliminartipo()
    {
        ViewBag.flag = 0;
        return View();
    }

    [HttpPost]
    public ActionResult eliminartipo(regTipo ti)
    {
        ConectorDataContext dt = new ConectorDataContext();


        if ((from i in dt.tipos where i.id == ti.id select i).ToList().Count == 0)
        {
            ViewBag.flag = 0;
        }

        else
        {
            var x = (from i in dt.tipos

                     where i.id == ti.id
                     select new
                     {
                         id = i.id,
                         nombre = i.nombre,
                         descripcion = i.dercripcion,
                         precio = i.precio

                     }).ToArray()[0];

            ViewBag.id = x.id;
            ViewBag.nombre = x.nombre;
            ViewBag.descripcion = x.descripcion;
            ViewBag.precio = x.precio;
            ViewBag.flag = 1;
        }


        return View();
    }
   
    public ActionResult eliminar2(string ID)
    {
        int id = Convert.ToInt32(ID);
        ConectorDataContext db = new ConectorDataContext();

        tipo t = db.tipos.Single(u => u.id == id);
        db.tipos.DeleteOnSubmit(t);

        db.SubmitChanges();

        return Redirect("/habitaciones/mostrartipo");
    }


        // ACTUALIZAR MANTENIMIENTO
        [Authorize(Roles = "administrador")]
    public ActionResult actualizarman(string id)
    {
        ConectorDataContext dt = new ConectorDataContext();
        int ID = Convert.ToInt32(id);

        if ((from i in dt.mantenimientos where i.id == ID select i).ToList().Count == 0)
        {
            ViewBag.flag = 0;
        }

        else
        {
            var x = (from i in dt.mantenimientos
                     
                     where i.id == ID
                     select new
                     {
                         id = i.id,
                         fe1=i.fechaini,
                         fe2=i.fechafin,
                     }).ToArray()[0];

            ViewBag.id = x.id;
            ViewBag.fecha1=x.fe1;
            ViewBag.fecha2=x.fe2;
            ViewBag.flag = 1;
        }


        return View();
    }
    [HttpPost]
    public ActionResult actualizar3(mantencion ma)
    {
     
        ConectorDataContext db = new ConectorDataContext();

        
       // habitacion h = db.habitacions.Single(u => u.id == ma.id);
        mantenimiento m = db.mantenimientos.Single(u => u.id == ma.id);


        m.fechaini = ma.fecha;
        m.fechafin = ma.fecha2;
        db.SubmitChanges();
        return Redirect("../habitaciones/mostrarmantencion");
    }

    // ELIMINAR  mantenimeito
[Authorize(Roles = "administrador")]
    public ActionResult eliminarman()
    {
        ViewBag.flag = 0;
        return View();
    }

    [HttpPost]
    public ActionResult eliminarman(mantencion ma)
    {
        ConectorDataContext dt = new ConectorDataContext();


        if ((from i in dt.mantenimientos where i.id == ma.id select i).ToList().Count == 0)
        {
            ViewBag.flag = 0;
        }

        else
        {
            var x = (from i in dt.habitacions
                     join j in dt.mantenimientos on i.id equals j.idhabi
                     where i.id == ma.id
                     select new
                     {
                         id = i.id,
                         fe1 = j.fechaini,
                         fe2 = j.fechafin,
                     }).ToArray()[0];

            ViewBag.id = x.id;
            ViewBag.fecha1 = x.fe1;
            ViewBag.fecha2 = x.fe2;
            ViewBag.flag = 1;
        }


        return View();
    }
   
    public ActionResult eliminar3(string ID)
    {
        int id = Convert.ToInt32(ID);
        ConectorDataContext db = new ConectorDataContext();

        mantenimiento m = db.mantenimientos.Single(u => u.id == id);
        db.mantenimientos.DeleteOnSubmit(m);

        db.SubmitChanges();

        return Redirect("/habitaciones/mostrarmantencion");
    }
        //MOSTRAR LAS TABLAS

    public ActionResult mostrartipo()
    {
        ConectorDataContext db = new ConectorDataContext();
        ViewBag.lista = from i in db.tipos select i;
        return View();
    }

    public ActionResult mostrarhabitacion()
    {
        ConectorDataContext db = new ConectorDataContext();
        ViewBag.lista = from i in db.habitacions select i;
        return View();
    }
    public ActionResult mostrarmantencion()
    {
        ConectorDataContext db = new ConectorDataContext();
        ViewBag.lista = from i in db.mantenimientos select i;
        return View();
    }

   }

}
