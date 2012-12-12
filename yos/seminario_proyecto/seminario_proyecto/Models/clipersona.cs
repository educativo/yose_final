using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace seminario_proyecto.Models
{
    public class clipersona
    {
        public int id { get; set; }
        public String nombre { get; set; }
        public String apelli { get; set; }
        public String pasaporte { get; set; }
        public String telefono { get; set; }
        public String email { get; set; }
        public String direccion { get; set; }
        public String ciudad { get; set; }
        public String estado { get; set; }
        public String pais { get; set; }
        public String contacto { get; set; }
        public String comen { get; set; }
        public String cumple { get; set; }

       // public String nit { get; set; }
       // public int ni { get; set; }
        //public String pago { get; set; }
    }
}