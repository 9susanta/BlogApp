using BlogApp.Common;
using BlogApp.Concrete;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlogApp.Controllers
{
    public class RolesController : BaseController
    {
        private readonly IOperation<Tblrole> _roles;
        private readonly IClsRole _clsRole;
        public RolesController(IOperation<Tblrole> roles, IClsRole clsRole)
        {
            this._roles = roles;
            this._clsRole = clsRole;
        }
        [AuthorizationPrivilege(ClaimType = ClaimTypes.Role, ClaimValue = "Admin,SuperAdmin")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetRole(int? page, int? pageSize)
        {
            try
            {
                ClsPaged<Tblrole> listRoles = new ClsPaged<Tblrole>();
                var model = _roles.GetAll(i => i.IsDeleted == false).ToList();
                return Json(listRoles.Get(page, pageSize, model));
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        [HttpGet]
        public JsonResult GetRoles()
        {
            try
            {
                var model = _roles.GetAll(i => i.IsDeleted == false).ToList();
                return Json(model);
            }
            catch (Exception ex)
            {
                
            }
            return null;
        }
        [HttpPost]
        public JsonResult InsertRole([FromBody] clsRole _role)
        {
            try
            {
      
                var itemfound = _clsRole.CheckRoleName(_role.RoleName);
                if (itemfound > 0)
                {
                    return Json(new { msg = "This Record is already Exist" });
                }
                Tblrole rigt = new Tblrole();
                rigt.RoleName = _role.RoleName;
                rigt.IsDeleted = false;
                _roles.Insert(rigt);
                _roles.Save();
                return Json(rigt);
            }
            catch (Exception ex)
            {
            }
            return null;

        }
        [HttpPost]
        public JsonResult UpdateRole([FromBody]clsRole _role)
        {
            try
            {
                var itemfound = _clsRole.CheckRoleName(_role.RoleName);
                if (itemfound > 0)
                {
                    return Json(new { msg = "This Record is already Exist" });
                }
                Tblrole rigt = _roles.GetByID(x => x.RoleId == _role.RoleId);
                rigt.RoleName = _role.RoleName;
                _roles.Edit(rigt);
                _roles.Save();
                return Json(rigt);
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        [HttpPost]
        public JsonResult DeleteRole([FromBody] clsRole role)
        {
            try
            {
                Tblrole rigt = _roles.GetByID(x => x.RoleId == role.RoleId);
                rigt.IsDeleted = true;
                _roles.Edit(rigt);
                _roles.Save();
                return Json(role.RoleId);
            }
            catch (Exception ex)
            {
        
            }
            return null;
        }
    }
}
