﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using seminario_proyecto.Models;

namespace seminario_proyecto.Controllers
{
    public class AdministradorController : Controller
    {
        //
        // GET: /Administrador/

        public ActionResult Index()
        {
            return View();
        }
       // esto  para  ser autorizado  para adquirir algunas ventanas
        [Authorize(Roles="administrador")]
        public JsonResult addrol(string idUs2, string idRol)
        {
            //string data = idUs2 + idRol;
            Guid _idUs = new Guid(idUs2);
            Guid _idRol = new Guid(idRol);
            ConectorDataContext db = new ConectorDataContext();
            UsersInRole rol = new UsersInRole()
            {
                RoleId = _idRol,
                UserId = _idUs
            };
            int aa = db.UsersInRoles.Where(a => a.RoleId == _idRol && a.UserId == _idUs).Count();
            if (aa == 1)
            {
                return Json(new { success = "Error Este Rol esta Asignado" });
            }
            db.UsersInRoles.InsertOnSubmit(rol);
            db.SubmitChanges();

            string nombrerol = db.Roles.Where(a => a.RoleId == _idRol).First().RoleName;

            return Json(new { success = "Rol Agregado", rol = nombrerol, idRol = _idRol, idUs = _idUs });
        }
    public JsonResult deleteRol(string idRol, string idUser)
    {
        Guid _idRol = new Guid(idRol);
        Guid _idUser = new Guid(idUser);
        ConectorDataContext db = new ConectorDataContext();
        UsersInRole rol = db.UsersInRoles.Where(a => a.RoleId == _idRol && a.UserId == _idUser).First();
        db.UsersInRoles.DeleteOnSubmit(rol);
        db.SubmitChanges();
        return Json(new { success = true });
    }
    public ActionResult getUserRoles()
    {
        ConectorDataContext db = new ConectorDataContext();
        List<UserListRol> Lista = db.Users.
            Select(a => new UserListRol()
            {
                id = a.UserId,
                nombre = a.UserName,
                ListaRoles = db.UsersInRoles.Where(b => b.UserId == a.UserId).
                Select(b => new RolView()
                {
                    id = b.Role.RoleId,
                    nombre = b.Role.RoleName
                }).ToList()
            }).ToList();
        ViewBag.lista = Lista;
        //Consultar la tabla Roles De la base de datos
        List<Role> listaRoles = db.Roles.ToList();
        ViewBag.listRol = listaRoles;
        return View();
    }
    public ActionResult testing()
    {
        return View();
    }
    [HttpPost]
    public ActionResult testing(Testing model)
    {
        return View();
    }


    }
}
