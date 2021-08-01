using BlogApp.Common;
using BlogApp.Concrete;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Controllers
{
    public class UserManagementController : Controller
    {
        private readonly IOperation<Tblrole> _userrole;
        public UserManagementController(IOperation<Tblrole> userrole)
        {
            this._userrole = userrole;
        }
        [AuthorizationPrivilege]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetRole(int? page, int? pageSize)
        {
            ClsPaged<Tblrole> listRole = new ClsPaged<Tblrole>();
            var model = _userrole.GetAll(i => i.IsDeleted == false).ToList();
            return Json(listRole.Get(page, pageSize, model));
        }
        [HttpPost]
        public JsonResult InsertRole([FromBody]clsRole _role)
        {
            Tblrole lang = new Tblrole();
            lang.RoleName = _role.RoleName;
            lang.IsDeleted = false;
            _userrole.Insert(lang);
            _userrole.Save();
            return Json(lang);
        }
        [HttpPost]
        public JsonResult UpdateRole([FromBody]clsRole _role)
        {
            Tblrole lang = _userrole.GetByID(x => x.RoleId == _role.RoleId);
            lang.RoleName = _role.RoleName;
            _userrole.Edit(lang);
            _userrole.Save();
            return Json(lang);
        }
        [HttpPost]
        public JsonResult DeleteRole(int? Id)
        {
            Tblrole lang = _userrole.GetByID(x => x.RoleId == Id);
            lang.IsDeleted = true;
            _userrole.Save();
            return Json(Id);
        }
    }
}
