using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace seminario_proyecto.Models
{
    public class RolView
    {
        public Guid id { set; get; }
        public string nombre { set; get; }
    }
    public class UserListRol
    {
        public Guid id { set; get; }
        public string nombre { set; get; }
        public List<RolView> ListaRoles { set; get; }
    }
}