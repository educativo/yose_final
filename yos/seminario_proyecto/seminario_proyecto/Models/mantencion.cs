using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace seminario_proyecto.Models
{
    public class mantencion
    {
        public String fecha { get; set; }
        public String fecha2 { get; set; }
        public int id { get; set; }
    }
}