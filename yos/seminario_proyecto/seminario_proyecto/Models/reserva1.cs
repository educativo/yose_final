using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seminario_proyecto.Models
{
    public class reserva1
    {
        public int id { get; set; }
        public DateTime fechaini { get; set; }
        public DateTime fechafinal { get; set; }
        public int dia { get; set; }
        public string precio { get; set; }
        public string nombreapellido { get; set; }
        public cliper percli { get; set; }
    }
    public class cliper
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