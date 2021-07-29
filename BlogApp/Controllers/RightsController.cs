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
    public class RightsController : BaseController
    {
        private IOperation<Tblright> _rights;
        public RightsController(IOperation<Tblright> rights)
        {
            this._rights = rights;
        }
        [AuthorizationPrivilege]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetRight(int? page, int? pageSize)
        {
            try
            {
                ClsPaged<Tblright> listRights = new ClsPaged<Tblright>();
                var model = _rights.GetAll(i => i.IsDeleted == false).ToList();
                return Json(listRights.Get(page, pageSize, model));
            }
            catch (Exception ex)
            {
                
            }
            return Json(null);
        }
        [HttpGet]
        public JsonResult GetRights()
        {
            try
            {
                var model = _rights.GetAll(i => i.IsDeleted == false).ToList();
                return Json(model);
            }
            catch (Exception ex)
            {

            }
            return Json(null);
        }
        [HttpPost]
        public JsonResult InsertRight(clsRights _right)
        {
            try
            {
                Tblright rigt = new Tblright();
                rigt.RightsName = _right.RightsName;
                rigt.IsDeleted = false;
                _rights.Insert(rigt);
                _rights.Save();
                return Json(rigt);
            }
            catch (Exception ex)
            {
            }
            return Json(null);
        }
        [HttpPost]
        public JsonResult UpdateRight(clsRights _right)
        {
            try
            {
                Tblright rigt = _rights.GetByID(x => x.RightsId == _right.RightsId);
                rigt.RightsName = _right.RightsName;
                _rights.Edit(rigt);
                _rights.Save();
                return Json(rigt);
            }
            catch (Exception ex)
            {

            }
            return Json(null);
        }
        [HttpPost]
        public JsonResult DeleteRight(int Id)
        {
            try
            {
                Tblright rigt = _rights.GetByID(x => x.RightsId == Id);
                rigt.IsDeleted = true;
                _rights.Edit(rigt);
                _rights.Save();
                return Json(Id);
            }
            catch (Exception ex)
            {
            }
            return Json(null);
        }
    }
}
