using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using seminario_proyecto.Models;

namespace seminario_proyecto.Controllers
{
    public class clienteController : Controller
    {
       //
        // GET: /cliente/

        public ActionResult Index()
        {
            return View();
        }

       

        //registrar persona
        [Authorize(Roles ="administrador")]
        public ActionResult RegistrarPersona()
        {
            return View();

        }
        [HttpPost]
        public ActionResult RegistrarPersona(cliper newcliente)
        {
            if (ModelState.IsValid)
            {
                ConectorDataContext db = new ConectorDataContext();
                cliente cli = new cliente();
                persona per = new persona();  

                cli.nombre = newcliente.nombre;
                cli.telefono = newcliente.telefono;
                cli.direccion = newcliente.direccion;
                cli.email = newcliente.email;
                cli.ciudad = newcliente.ciudad;
                cli.estado = newcliente.estado;
                cli.pais = newcliente.pais;
                db.clientes.InsertOnSubmit(cli);
                
                 int idA = db.clientes.OrderByDescending(a => a.id).First().id;

                 per.idcli = idA;
                 per.apellidos = newcliente.apelli;
                 per.pasaporte = newcliente.pasaporte;
                 per.comentario = newcliente.comen;
                 per.cumpleaños = newcliente.cumple;
                 db.personas.InsertOnSubmit(per);
                 db.SubmitChanges();
                 return Redirect("../cliente/mostrarcliente");
                 //return RedirectToAction("exito", "habitaciones");
            }
            return View();
        }
        [Authorize(Roles = "administrador")]
            //registrar agencia
         public ActionResult RegistrarAgencia()
        {
            return View();

        }
            [HttpPost]
            public ActionResult RegistrarAgencia(cliagencia newcliente)
            {
                if (ModelState.IsValid)
                {
                    ConectorDataContext db = new ConectorDataContext();
                    cliente cli = new cliente();
                    agencia age = new agencia();

                    cli.nombre = newcliente.nombre;
                    cli.telefono = newcliente.telefono;
                    cli.direccion = newcliente.direccion;
                    cli.email = newcliente.email;
                    cli.ciudad = newcliente.ciudad;
                    cli.estado = newcliente.estado;
                    cli.pais = newcliente.pais;
                    db.clientes.InsertOnSubmit(cli);

                    int idB = db.clientes.OrderByDescending(a => a.id).First().id;

                    age.idcli = idB;
                    age.nit = newcliente.nit;
                    age.contacto = newcliente.contacto;
                    db.agencias.InsertOnSubmit(age);
                    db.SubmitChanges();
                    return Redirect("../cliente/mostraragencia");

                }
                return View();
        }
        [Authorize(Roles = "administrador")]
        
        //registrar  empresa
            public ActionResult RegistrarEmpresa()
        {
            return View();

        }
        [HttpPost]
        public ActionResult RegistrarEmpresa(cliempresa newcliente)
        {
            if (ModelState.IsValid)
            {
                ConectorDataContext db = new ConectorDataContext();
                cliente cli = new cliente();
                empresa emp = new empresa();

                cli.nombre = newcliente.nombre;
                cli.telefono = newcliente.telefono;
                cli.direccion = newcliente.direccion;
                cli.email = newcliente.email;
                cli.ciudad = newcliente.ciudad;
                cli.estado = newcliente.estado;
                cli.pais = newcliente.pais;
                db.clientes.InsertOnSubmit(cli);

                 int idC = db.clientes.OrderByDescending(a => a.id).First().id;

                 emp.idcli = idC;
                 emp.nit = newcliente.nit;
                 emp.pago = newcliente.pago;
                 emp.contacto = newcliente.contacto;
                 db.empresas.InsertOnSubmit(emp);
                 db.SubmitChanges();
                 return Redirect("../cliente/mostrarempresa");
            }
            return View();
               // return RedirectToAction("insertar", "cliente");
          }
        public ActionResult exito()
        {
            return View();
        }

      // actualizar datos////////////////////////////////////////////////
        [Authorize(Roles = "administrador")]

        public ActionResult actualizarpersona(string id)
        {
            ConectorDataContext dt = new ConectorDataContext();
            int ID = Convert.ToInt32(id);
            

            if ((from i in dt.clientes where i.id==ID select i).ToList().Count == 0) {
                ViewBag.flag = 0;
            }

            else
            {
                var x = (from i in dt.clientes
                         join j in dt.personas on i.id equals j.idcli
                         where i.id==ID
                         select new { 
                             nombre=i.nombre,
                             telefono=i.telefono,
                             id=i.id,
                             apellido=j.apellidos,
                             pasaporte=j.pasaporte,
                             email=i.email,
                             direccion=i.direccion,
                             ciudad=i.ciudad,
                             estado=i.estado,
                             pais=i.pais,
                             cumple=j.cumpleaños,
                             comen=j.comentario
                         }).ToArray()[0];
                
                ViewBag.id = x.id;
                ViewBag.nombre = x.nombre;
                ViewBag.apellido = x.apellido;
                ViewBag.telefono = x.telefono;
                ViewBag.pasaporte = x.pasaporte;
                ViewBag.email = x.email;
                ViewBag.direccion = x.direccion;
                ViewBag.ciudad = x.ciudad;
                ViewBag.estado = x.estado;
                ViewBag.pais = x.pais;
                ViewBag.comentario = x.comen;
                ViewBag.cumpleaños = x.cumple;
                ViewBag.flag = 1;
            }

            
            return View();
        }
        [HttpPost]
        public ActionResult actualizar1(cliper cli) {
          //  ViewBag.hola = cli.id;
            ConectorDataContext db = new ConectorDataContext();
            cliente c = db.clientes.Single(u => u.id == cli.id);
            persona p = db.personas.Single(u => u.idcli==cli.id);

            c.nombre = cli.nombre;
            c.telefono = cli.telefono;
            c.direccion = cli.direccion;
            c.email = cli.email;
            c.estado = cli.estado;
            c.ciudad = cli.ciudad;
            c.pais = cli.pais;
            c.estado=cli.estado;

            p.apellidos=cli.apelli;
            p.pasaporte=cli.pasaporte;
            p.cumpleaños=cli.cumple;
            p.comentario=cli.comen;

            db.SubmitChanges();
            return Redirect("../cliente/mostrarcliente");
        }
        //eliminar la tablas
        [Authorize(Roles = "administrador")]

        public ActionResult eliminarpersona()
        {
            ViewBag.flag = 0;
            return View();
        }
        [HttpPost]
        public ActionResult eliminarpersona(cliper cli)
        {
            ConectorDataContext dt = new ConectorDataContext();


            if ((from i in dt.clientes where i.nombre == cli.nombre select i).ToList().Count == 0)
            {
                ViewBag.flag = 0;
            }

            else
            {
                var x = (from i in dt.clientes
                         join j in dt.personas on i.id equals j.idcli
                         where i.nombre == cli.nombre
                         select new
                         {
                             telefono = i.telefono,
                             id = i.id,
                             apellido=j.apellidos,
                             pasaporte = j.pasaporte,
                             email = i.email,
                             direccion = i.direccion,
                             ciudad = i.ciudad,
                             estado = i.estado,
                             pais = i.pais,
                             cumple = j.cumpleaños,
                             comen = j.comentario
                         }).ToArray()[0];

                ViewBag.id = x.id;
                ViewBag.apellido = x.apellido;
                ViewBag.telefono = x.telefono;
                ViewBag.pasaporte = x.pasaporte;
                ViewBag.email = x.email;
                ViewBag.direccion = x.direccion;
                ViewBag.ciudad = x.ciudad;
                ViewBag.estado = x.estado;
                ViewBag.pais = x.pais;
                ViewBag.comentario = x.comen;
                ViewBag.cumpleaños = x.cumple;
                ViewBag.flag = 1;
            }

           
            return View();
        }

       
        public ActionResult eliminar(string ID)
        {
            int id = Convert.ToInt32(ID);
            ConectorDataContext db = new ConectorDataContext();
            cliente c = db.clientes.Single(u => u.id == id);
            persona p = db.personas.Single(u => u.idcli==id);
            db.personas.DeleteOnSubmit(p);
            db.SubmitChanges();

            db.clientes.DeleteOnSubmit(c);
            db.SubmitChanges();
            return Redirect("/cliente/mostrarcliente");
        }

        //actulizar agencia////////
        [Authorize(Roles = "administrador")]
        public ActionResult actualizaragencia(string id)
        {
            ConectorDataContext dt = new ConectorDataContext();
            int ID = Convert.ToInt32(id);


            if ((from i in dt.clientes where i.id == ID select i).ToList().Count == 0)
            {
                ViewBag.flag = 0;
            }

            else
            {
                var x = (from i in dt.clientes
                         join j in dt.agencias on i.id equals j.idcli
                         where i.id == ID
                         select new
                         {
                             nombre = i.nombre,
                             nit=j.nit,
                             telefono = i.telefono,
                             id = i.id,
                             email = i.email,
                             direccion = i.direccion,
                             ciudad = i.ciudad,
                             estado = i.estado,
                             pais = i.pais,
                             contacto=j.contacto,
                         }).ToArray()[0];

                ViewBag.id = x.id;
                ViewBag.nombre = x.nombre;
                ViewBag.nit = x.nit;
                ViewBag.telefono = x.telefono;
                ViewBag.email = x.email;
                ViewBag.direccion = x.direccion;
                ViewBag.ciudad = x.ciudad;
                ViewBag.estado = x.estado;
                ViewBag.pais = x.pais;
                ViewBag.contacto = x.contacto;
                ViewBag.flag = 1;
            }


            return View();
        }
        [HttpPost]
        public ActionResult actualizarage(cliagencia cli)
        {
            //  ViewBag.hola = cli.id;
            ConectorDataContext db = new ConectorDataContext();
            cliente c = db.clientes.Single(u => u.id == cli.id);
            agencia a = db.agencias.Single(u => u.idcli == cli.id);

            c.nombre = cli.nombre;
            c.telefono = cli.telefono;
            c.direccion = cli.direccion;
            c.email = cli.email;
            c.estado = cli.estado;
            c.ciudad = cli.ciudad;
            c.pais = cli.pais;


            a.nit = cli.nit;
            a.contacto = cli.contacto; 
           
            db.SubmitChanges();
            return Redirect("../cliente/mostraragencia");
        }

        //elimnar agencia///////////
        [Authorize(Roles = "administrador")]
        public ActionResult eliminaragencia()
        {
            ViewBag.flag = 0;
            return View();
        }
        [HttpPost]
        public ActionResult eliminaragencia(cliagencia cli)
        {
            ConectorDataContext dt = new ConectorDataContext();


            if ((from i in dt.clientes where i.nombre == cli.nombre select i).ToList().Count == 0)
            {
                ViewBag.flag = 0;
            }

            else
            {
                var x = (from i in dt.clientes
                         join j in dt.agencias on i.id equals j.idcli
                         where i.nombre == cli.nombre
                         select new
                         {   nit=j.nit,
                             telefono = i.telefono,
                             id = i.id,
                             email = i.email,
                             direccion = i.direccion,
                             ciudad = i.ciudad,
                             estado = i.estado,
                             pais = i.pais,
                             contacto=j.contacto,
                         }).ToArray()[0];

                ViewBag.id = x.id;
                ViewBag.nit = x.nit;
                ViewBag.telefono = x.telefono;
                ViewBag.email = x.email;
                ViewBag.direccion = x.direccion;
                ViewBag.ciudad = x.ciudad;
                ViewBag.estado = x.estado;
                ViewBag.pais = x.pais;
                ViewBag.contacto = x.contacto;
                ViewBag.flag = 1;
            }


            return View();
        }


        public ActionResult eliminarage(string ID)
        {
            int id = Convert.ToInt32(ID);
            ConectorDataContext db = new ConectorDataContext();
            cliente c = db.clientes.Single(u => u.id == id);
            agencia p = db.agencias.Single(u => u.idcli == id);
            db.agencias.DeleteOnSubmit(p);
            db.SubmitChanges();

            db.clientes.DeleteOnSubmit(c);
            db.SubmitChanges();
            return Redirect("/cliente/mostraragencia");
        }

       
        //actulizar empresa///////////
        [Authorize(Roles = "administrador")]
        public ActionResult actualizarempresa(string id)
        {
            ConectorDataContext dt = new ConectorDataContext();
            int ID = Convert.ToInt32(id);


            if ((from i in dt.clientes where i.id == ID select i).ToList().Count == 0)
            {
                ViewBag.flag = 0;
            }

            else
            {
                var x = (from i in dt.clientes
                         join j in dt.empresas on i.id equals j.idcli
                         where i.id == ID
                         select new
                         {
                             nombre = i.nombre,
                             nit = j.nit,
                             telefono = i.telefono,
                             id = i.id,
                             email = i.email,
                             direccion = i.direccion,
                             ciudad = i.ciudad,
                             estado = i.estado,
                             pais = i.pais,
                             pago=j.pago,
                             contacto = j.contacto,
                         }).ToArray()[0];

                ViewBag.id = x.id;
                ViewBag.nombre = x.nombre;
                ViewBag.nit = x.nit;
                ViewBag.telefono = x.telefono;
                ViewBag.email = x.email;
                ViewBag.direccion = x.direccion;
                ViewBag.ciudad = x.ciudad;
                ViewBag.estado = x.estado;
                ViewBag.pais = x.pais;
                ViewBag.pago = x.pago;
                ViewBag.contacto = x.contacto;
                ViewBag.flag = 1;
            }


            return View();
        }
        [HttpPost]
        public ActionResult actualizaremp(cliempresa cli)
        {
            //  ViewBag.hola = cli.id;
            ConectorDataContext db = new ConectorDataContext();
            cliente c = db.clientes.Single(u => u.id == cli.id);
            empresa a = db.empresas.Single(u => u.idcli == cli.id);

            c.nombre = cli.nombre;
            c.telefono = cli.telefono;
            c.direccion = cli.direccion;
            c.email = cli.email;
            c.estado = cli.estado;
            c.ciudad = cli.ciudad;
            c.pais = cli.pais;


            a.nit = cli.nit;
            a.pago = cli.pago;
            a.contacto = cli.contacto; 

            db.SubmitChanges();
            return Redirect("../cliente/mostrarempresa");
        }

        //elimnar empresa///////////
        [Authorize(Roles = "administrador")]
        public ActionResult eliminarempresa()
        {
            ViewBag.flag = 0;
            return View();
        }
        [HttpPost]
        public ActionResult eliminarempresa(cliempresa cli)
        {
            ConectorDataContext dt = new ConectorDataContext();


            if ((from i in dt.clientes where i.nombre == cli.nombre select i).ToList().Count == 0)
            {
                ViewBag.flag = 0;
            }

            else
            {
                var x = (from i in dt.clientes
                         join j in dt.empresas on i.id equals j.idcli
                         where i.nombre == cli.nombre
                         select new
                         {
                             nit = j.nit,
                             telefono = i.telefono,
                             id = i.id,
                             email = i.email,
                             direccion = i.direccion,
                             ciudad = i.ciudad,
                             estado = i.estado,
                             pais = i.pais,
                             pago=j.pago,
                             contacto = j.contacto,
                         }).ToArray()[0];

                ViewBag.id = x.id;
                ViewBag.nit = x.nit;
                ViewBag.telefono = x.telefono;
                ViewBag.email = x.email;
                ViewBag.direccion = x.direccion;
                ViewBag.ciudad = x.ciudad;
                ViewBag.estado = x.estado;
                ViewBag.pais = x.pais;
                ViewBag.pago = x.pago;
                ViewBag.contacto = x.contacto;
                ViewBag.flag = 1;
            }


            return View();
        }


        public ActionResult eliminaremp(string ID)
        {
            int id = Convert.ToInt32(ID);
            ConectorDataContext db = new ConectorDataContext();
            cliente c = db.clientes.Single(u => u.id == id);
            empresa p = db.empresas.Single(u => u.idcli == id);
            db.empresas.DeleteOnSubmit(p);
            db.SubmitChanges();

            db.clientes.DeleteOnSubmit(c);
            db.SubmitChanges();
            return Redirect("/cliente/mostrarempresa");
        }

       
        
        //listar  datos///////////////
        public ActionResult mostrarcliente()
        {
            ConectorDataContext db = new ConectorDataContext();
            ViewBag.lista = (from a in db.clientes join j in db.personas on a.id equals j.idcli select a).ToList();
            return View();
        }

        public ActionResult mostrarempresa()
        {
            ConectorDataContext db = new ConectorDataContext();
            ViewBag.lista1 = (from b in db.clientes join h in db.empresas on b.id equals h.idcli select b).ToList();
            return View();
        }

        public ActionResult mostraragencia()
        {
            ConectorDataContext db = new ConectorDataContext();
            ViewBag.lista2 = (from c in db.clientes join k in db.agencias on c.id equals k.idcli select c).ToList();
            return View();
        }
        }
    }



