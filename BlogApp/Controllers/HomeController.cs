using BlogApp.Common;
using BlogApp.Concrete;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly INewsOprations _newsOperation;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, INewsOprations newsOperation)
        {
            this._newsOperation = newsOperation;
            _logger = logger;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Home - Khabar Odia";
            var request = HttpContext.Request.Path;
            ViewBag.Url = request;
            ViewBag.ImgUrl = HttpContext.Request.Protocol + "://" + HttpContext.Request.Host + "/Images/logo/default.png";
            ViewBag.Desc = "Khabar Odia is one of the leading web platform which brings up latest,crime,politics,entertainment,sports and many more news from round the globle to its readers in Odia. Odia is one of the oldest indian language which use brodly use by Indian state odisha. Also Khabar Odia is commited to reach more and more odia people with its unbaised news in their langauge only .";
            return View();
        }
        [Route("{controller=Home}/{action=Category}/{categoryId}/{category?}")]
        public IActionResult Category(int? categoryId, string category)
        {
            ViewBag.Title = category + " - Khabar Odia";
            var request = HttpContext.Request.Path;
            ViewBag.Url = request;
            ViewBag.ImgUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + "/Images/logo/default.png";
            ViewBag.Desc = "Khabar Odia is one of the leading web platform which brings up latest,crime,politics,entertainment,sports and many more news from round the globle to its readers in Odia. Odia is one of the oldest indian language which use brodly use by Indian state odisha. Also Khabar Odia is commited to reach more and more odia people with its unbaised news in their langauge only .";
            return View();
        }
        public async Task<JsonResult> GetFirstTimeCategoryData(int? categoryId)
        {
            try
            {
                DataSet ds = await Task.Run(() => _newsOperation.GetClientCategory(categoryId.Value));
                var result = new { latest = JsonConvert.SerializeObject(ds.Tables[0], Formatting.None), popular = JsonConvert.SerializeObject(ds.Tables[1], Formatting.None), category = JsonConvert.SerializeObject(ds.Tables[2], Formatting.None), paging = JsonConvert.SerializeObject(ds.Tables[3], Formatting.None) };
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return null;
        }
        public async Task<JsonResult> GetResultSection(int? categoryId, int? pageIndex, string section)
        {
            try
            {
                 DataTable dataTable = await Task.Run(() => _newsOperation.GetSectionData(section, pageIndex.Value, categoryId.Value));
                 var result = new { data = JsonConvert.SerializeObject(dataTable, Formatting.None) };
                 return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return null;

        }
        public async Task<JsonResult> GetCategoryData(int? categoryId, int? page, int? pageSize)
        {
            
            DataSet ds = await Task.Run(() => _newsOperation.GetNews(page.Value, pageSize.Value, categoryId.Value));
            return Json(new { data = JsonConvert.SerializeObject(ds.Tables[0], Formatting.None), totalcount = JsonConvert.SerializeObject(ds.Tables[1], Formatting.None) });
           
        }
        public JsonResult GetCategoryDataByName(string category, int? page, int? pageSize)
        {
            
            //NewsList nl = newsOperation.GetNewsBySearch(page.Value, category);
            //nl.CurrentPages = page.Value;
            //nl.TotalPages = (int)Math.Ceiling((decimal)nl.totalCount / pageSize.Value);
            return Json("");
        }
        public IActionResult Topic(string search)
        {
            return View();
        }
        [Route("article/{category}/{year}/{month}/{id}/{slugurl}", Name = "article")]
        public async Task<IActionResult> News(string category, int? year, int? month, decimal? id, string slugurl)
        {
            try
            {
                
                DataSet ds = await Task.Run(() => _newsOperation.GetNewPost(id.Value, 1));
                if (ds == null)
                {
                    return View();
                }
                ViewBag.Url = HttpContext.Request;
                return View(ds.Tables[0]);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return View();
        }
        
        [HttpGet]
        public async Task<JsonResult> GetNewsData(decimal Id)
        {
            try
            {
                DataSet ds = await Task.Run(() => _newsOperation.GetNewPost(Id, 2));
                if (ds == null)
                {
                    return null;
                }
                var result = new { latest = JsonConvert.SerializeObject(ds.Tables[0], Formatting.None), popular = JsonConvert.SerializeObject(ds.Tables[1], Formatting.None), related = JsonConvert.SerializeObject(ds.Tables[2], Formatting.None), Prev = JsonConvert.SerializeObject(ds.Tables[3], Formatting.None), Next = JsonConvert.SerializeObject(ds.Tables[4], Formatting.None) };
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return null;
        }
        public IActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public IActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public async Task<JsonResult> GetHomePage()
        {
            try
            {
                DataSet ds = await Task.Run(() => _newsOperation.GetHomePage());
                if (ds == null)
                {
                    return null;
                }
                var result = new { latest = JsonConvert.SerializeObject(ds.Tables[0], Formatting.None), datacat = JsonConvert.SerializeObject(ds.Tables[1], Formatting.None), svmenu = JsonConvert.SerializeObject(ds.Tables[2], Formatting.None), plrnw = JsonConvert.SerializeObject(ds.Tables[3], Formatting.None), phtnw = JsonConvert.SerializeObject(ds.Tables[4], Formatting.None) };
                return Json(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return null;
        }
        public async Task<JsonResult> GetTopic(string topic, int? pageIndex = 1)
        {
            try
            {
                
                var item = await Task.Run(() => _newsOperation.GetNewsBySearch(pageIndex.Value, topic));
                return Json(new { latestdata = JsonConvert.SerializeObject(item.Tables[0]), populardata = JsonConvert.SerializeObject(item.Tables[1]), data = JsonConvert.SerializeObject(item.Tables[2]), count = JsonConvert.SerializeObject(item.Tables[3]) });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return null;
        }
        public JsonResult SendEmail(Tblcontact tbl)
        {
            try
            {
                tbl.PostedOn = DateTime.Now;
                _newsOperation.Send(tbl);
                return Json(new { latestdata = "Success" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
            return null;
        }
    }
}
