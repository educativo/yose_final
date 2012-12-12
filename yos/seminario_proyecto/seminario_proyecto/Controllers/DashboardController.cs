using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using seminario_proyecto.Models;

namespace seminario_proyecto.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/
        //[Authorize(Roles = "usuario")]
        public ActionResult Index()
        {
             ConectorDataContext db = new ConectorDataContext();
            //consulta para seleccionar y filtrar los datos iniciales
            List<dashboard2> lista = db.habitacions.Select(a => new dashboard2()
            {
                id = a.id,
                tipo = a.tipo,
                estado= a.estado,
                reservas = a.recervas.Select(b => new res()
                {
                    idHa = b.idhabi,
                    fechainicial = b.fechainiciorecer,
                    fechafinal = b.fechainiciorecer,
                    precio = b.precio,
                    dia = b.fechafinrecer.Subtract(b.fechainiciorecer).Days
                }).ToList()
            }).ToList();
            ViewBag.info = lista;
            DateTime hoy = DateTime.Now;
            int anio = hoy.Year;
            int mes = hoy.Month;
            int dia = hoy.Day;
            string value = "";
            while (value != "Monday")
            {
                DateTime find = new DateTime(anio, mes, dia);
                dia--;
                if (dia < 1)
                {
                    mes--;
                    if (mes < 1)
                    {
                        anio--;
                        mes = 12;
                    }
                    dia = DateTime.DaysInMonth(anio, mes);
                }
                value = find.DayOfWeek.ToString();
            }
            dia++;
            DateTime hh = new DateTime(anio, mes, dia);
            List<DateTime> semanas = new List<DateTime>();
            for (int i = 0; i < 7; i++)
            {
                semanas.Add(hh.AddDays(i));
            }
            ViewBag.fechas = semanas;
            return View();
        }

    }
}
