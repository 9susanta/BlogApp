using BlogApp.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Models;

namespace BlogApp.Controllers
{
    public class InfoController : Controller
    {
        // GET: Info
        private IOperation<Tblcontact> _tblcon = null;
       
        public InfoController(IOperation<Tblcontact> tblcon)
        {
            this._tblcon = tblcon;
        }
        [AuthorizationPrivilege]
        public IActionResult Index()
        {
            return View();
        }
        public JsonResult Details()
        {
            try
            {
                var result = new { Info = JsonConvert.SerializeObject(_tblcon.GetAll(x => x.Id > 0).OrderByDescending(x => x.PostedOn), Formatting.None) };
                return Json(result);
            }
            catch (Exception ex)
            {
                
            }
            return null;
        }
        [HttpPost]
        public JsonResult Delete(decimal Id)
        {
            try
            {
                _tblcon.Delete(x => x.Id == Id);
                _tblcon.Save();
                var result = new { Info = "Success", Formatting.None };
                return Json(result);
            }
            catch (Exception ex)
            {
                
            }
            return null;
        }
        [HttpDelete]
        public JsonResult SeletedDelete(clsInfo _clsInfo)
        {
            try
            {
                var item = _tblcon.GetAll(x => _clsInfo.Ids.Contains(x.Id)).ToList();
                _tblcon.BulkDelete(item);
                _tblcon.Save();
                var result = new { Info = "Success", Formatting.None };
                return Json(result);
            }
            catch (Exception ex)
            {
               
            }
            return null;
        }
    }
}
