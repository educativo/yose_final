using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seminario_proyecto.Models
{
    public class ArchivoFile
    {
        public HttpPostedFileBase archivo { get; set; }
        public string rutafisica {get;set;}
    }

    public class clientes
    {
        public string nombre { get; set; }
        public string telefono { get; set; }
        public string direccion { get; set; }
        public string email { get; set; }
        public string ciudad { get; set; }
        public string estado { get; set; }
        public string pais { get; set; }
        public string contacto { get; set; }
       
        //public string id { get; set; }

    }
   
    public class servicios
    {
        public string servicio { get; set; }
        public string categoria { get; set; }
        public string precio { get; set; }
    }
}