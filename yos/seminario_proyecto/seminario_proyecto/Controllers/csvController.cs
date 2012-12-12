using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using seminario_proyecto.Models;
using LumenWorks.Framework.IO.Csv;
using System.IO;

namespace seminario_proyecto.Controllers
{
    public class csvController : Controller
    {
        //
        // GET: /csv/ para personas

        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "administrador")]
        public ActionResult upload()
        {
            return View();
        }
        [HttpPost]
        public ActionResult upload(ArchivoFile datos)
        {
            string rutafisica = Server.MapPath("~/csv");
            string rt = rutafisica + @"/" + datos.archivo.FileName;
            datos.archivo.SaveAs(rutafisica + @"/" + datos.archivo.FileName);
            ConectorDataContext db = new ConectorDataContext();
            archivo ar = new archivo()
            {
                rutafisica = rutafisica + @"/" + datos.archivo.FileName,
                fecha = DateTime.Now.ToString()
            };
            db.archivos.InsertOnSubmit(ar);
            db.SubmitChanges();
            CsvReader csv = new CsvReader(new StreamReader(rt), true);
            int total = csv.FieldCount;
            string[] headers = csv.GetFieldHeaders();
            List<cliente> listacli = new List<cliente>();
            while (csv.ReadNextRecord())
            {
                cliente cli = new cliente()
                {

                    nombre = csv[0],
                    telefono = csv[1],
                    direccion = csv[2],
                    email = csv[3],
                    ciudad = csv[4],
                    estado = csv[5],
                    pais = csv[6],
                   // id = csv[8]

                };
                listacli.Add(cli);
                db.clientes.InsertOnSubmit(cli);
                db.SubmitChanges();
            }
            ViewBag.lista = listacli;
            
            return View();
        }
        
        [Authorize(Roles = "administrador")]

        public ActionResult exportarCsv() {
            return View();
        }

        public ActionResult exportar()
        {
            ConectorDataContext db = new ConectorDataContext();
            List<cliente> datos = db.clientes.ToList();
            string nombre = "clientes" + DateTime.Now.ToString().Replace("/", "_").Replace(":", "_") + "csv";
            string ruta = Server.MapPath("~/download");
            StreamWriter stream = System.IO.File.CreateText(ruta + @"\" + nombre);

            foreach (var item in datos)
            {
                stream.WriteLine("{0},{1},{2},{3},{4},{5},{6}", item.id, item.nombre, item.telefono, item.direccion, item.email, item.ciudad, item.estado, item.pais);

            }
            stream.Close();
            return Redirect("/download/" + nombre);

        }

        // para servicios/////////////
        [Authorize(Roles = "administrador")]
        public ActionResult uploadservicios()
        {
            return View();
        }
        [HttpPost]
        public ActionResult uploadservicios(ArchivoFile datos)
        {
            string rutafisica = Server.MapPath("~/csv");
            string rt = rutafisica + @"/" + datos.archivo.FileName;
            datos.archivo.SaveAs(rutafisica + @"/" + datos.archivo.FileName);
            ConectorDataContext db = new ConectorDataContext();
            archivo ar = new archivo()
            {
                rutafisica = rutafisica + @"/" + datos.archivo.FileName,
                fecha = DateTime.Now.ToString()
            };
            db.archivos.InsertOnSubmit(ar);
            db.SubmitChanges();
            CsvReader csv = new CsvReader(new StreamReader(rt), true);
            int total = csv.FieldCount;
            string[] headers = csv.GetFieldHeaders();
            List<servicios> listaser = new List<servicios>();
            while (csv.ReadNextRecord())
            {
                servicios ser = new servicios()
                {

                    servicio= csv[0],
                    categoria = csv[1],
                    precio = csv[2],
                   

                };
                listaser.Add(ser);
            }
            ViewBag.lista = listaser;

            return View();
        }

        [Authorize(Roles = "administrador")]
        public ActionResult exportarCsvservi()
        {
            return View();
        }

        public ActionResult exportarservi()
        {
            ConectorDataContext db = new ConectorDataContext();
            List<servicio> datos = db.servicios.ToList();
            string nombre = "servicios" + DateTime.Now.ToString().Replace("/", "_").Replace(":", "_") + "csv";
            string ruta = Server.MapPath("~/download");
            StreamWriter stream = System.IO.File.CreateText(ruta + @"\" + nombre);

            foreach (var item in datos)
            {
                stream.WriteLine("{0},{1},{2}", item.id, item.servicio_nombre,item.categoria_nombre,item.precio);

            }
            stream.Close();
            return Redirect("/download/" + nombre);

        }
    }



}

