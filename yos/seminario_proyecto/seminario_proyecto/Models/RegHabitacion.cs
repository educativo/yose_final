using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace seminario_proyecto.Models
{
    public class RegHabitacion
    {
        public int id { get; set; }
        public String tip { get; set; }
        public String estado { get; set; }
        
    }
    
}