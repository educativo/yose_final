using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace seminario_proyecto.Models
{
    public class dashboard2
    {
        public int id { set; get; }
        public string tipo { set; get; }
        public string estado { get; set; }
        public List<res> reservas { set; get; }
    }
    public class res
    {
        public int idHa { set; get; }
        public DateTime fechainicial { set; get; }
        public DateTime fechafinal { set; get; }
        public int dia { set; get; }
        public string descripcion { set; get; }
        public string precio { set; get; }
        public string estado { set; get; }
    }

}