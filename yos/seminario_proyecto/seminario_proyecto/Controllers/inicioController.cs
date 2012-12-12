using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace seminario_proyecto.Controllers
{
    public class inicioController : Controller
    {
        //
        // GET: /inicio/

        public ActionResult Index()
        {
            return View();
        }

    }
}
