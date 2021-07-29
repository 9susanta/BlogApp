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
    public class NewsTypeController : BaseController
    {
        private readonly IOperation<Tblnewstype> _newsType;
        private readonly IclsNewsType _clsNewsType;
        public NewsTypeController(IOperation<Tblnewstype> newsType, IclsNewsType clsNewsType)
        {
            this._newsType = newsType;
            this._clsNewsType = clsNewsType;
        }
        [AuthorizationPrivilege]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetNewsType(int? page, int? pageSize)
        {
            try
            {
                ClsPaged<Tblnewstype> objNewsType = new ClsPaged<Tblnewstype>();
                var model = _newsType.GetAll(i => i.IsDeleted == false).ToList();
                return Json(objNewsType.Get(page, pageSize, model));
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        [HttpGet]
        public JsonResult GetNewsTypes()
        {
            try
            {
                var model = _newsType.GetAll(i => i.IsDeleted == false).ToList();
                return Json(model);
            }
            catch (Exception ex)
            {
               
            }
            return null;
        }
        [HttpPost]
        public JsonResult InsertNewsType(NewsType _newsTyp)
        {
            try
            {
                
                var itemfound = _clsNewsType.CheckNewsType(_newsTyp.NewsTypeName);
                if (itemfound > 0)
                {
                    return Json(new { msg = "This Record is already Exist" });
                }
                Tblnewstype newstyp = new Tblnewstype();
                newstyp.NewsType = _newsTyp.NewsTypeName;
                newstyp.OdiaName = _newsTyp.NewsTypeOdia;
                newstyp.IsMenu = _newsTyp.IsMenu;
                newstyp.IsDeleted = false;
                _newsType.Insert(newstyp);
                _newsType.Save();
                return Json(newstyp);
            }
            catch (Exception ex)
            {
            }
            return null;
        }
        [HttpPost]
        public JsonResult UpdateNewsType(NewsType _newsTyp)
        {
            try
            {
               
                var itemfound = _clsNewsType.CheckNewsType(_newsTyp.NewsTypeName);
                if (itemfound > 0)
                {
                    Tblnewstype newstype = _newsType.GetByID(x => x.Id == _newsTyp.NewsTypeId);
                    newstype.OdiaName = _newsTyp.NewsTypeOdia;
                    newstype.IsMenu = _newsTyp.IsMenu;
                    _newsType.Edit(newstype);
                    _newsType.Save();
                    return Json(new { msg = "This Record is Updated Successfully" });
                }
                else
                {
                    Tblnewstype newstyp = _newsType.GetByID(x => x.Id == _newsTyp.NewsTypeId);
                    newstyp.NewsType = _newsTyp.NewsTypeName;
                    newstyp.OdiaName = _newsTyp.NewsTypeOdia;
                    newstyp.IsMenu = _newsTyp.IsMenu;
                    _newsType.Edit(newstyp);
                    _newsType.Save();
                    return Json(newstyp);
                }
            }
            catch (Exception ex)
            {

            }
            return null;

        }
        [HttpPost]
        public JsonResult DeleteNewsType(int Id)
        {
            try
            {
                Tblnewstype newstyp = _newsType.GetByID(x => x.Id == Id);
                newstyp.IsDeleted = true;
                _newsType.Edit(newstyp);
                _newsType.Save();
                return Json(Id);
            }
            catch (Exception ex)
            {

                           }
            return null;
        }
        public IActionResult menuLayout()
        {
            try
            {               
                var model = _newsType.GetAll(i => i.IsDeleted == false && i.IsMenu == true).ToList();    
                return PartialView("_MenuLayout", model);
                
            }
            catch (Exception ex)
            {
            }
            return null;

        }
    }
}
