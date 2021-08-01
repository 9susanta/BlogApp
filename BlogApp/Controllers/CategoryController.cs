using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogApp.Controllers
{
    public class CategoryController : Controller
    {
        [Route("category/{categoryId}/{category}")]
        public IActionResult Index(int? categoryId, string category)
        {
            ViewBag.Title = category + " - Khabar Odia";
            var request = HttpContext.Request.Path;
            ViewBag.Url = request;
            ViewBag.ImgUrl = HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + "/Images/logo/default.png";
            ViewBag.Desc = "Khabar Odia is one of the leading web platform which brings up latest,crime,politics,entertainment,sports and many more news from round the globle to its readers in Odia. Odia is one of the oldest indian language which use brodly use by Indian state odisha. Also Khabar Odia is commited to reach more and more odia people with its unbaised news in their langauge only .";
            return View();
        }
    }
}
