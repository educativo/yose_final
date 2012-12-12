using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace seminario_proyecto.Models
{
    public class regTipo
    {
        public int id { get; set; }
        public String nombre { get; set; }
        public String descri { get; set; }
        public String precio { get; set; }
    }
}