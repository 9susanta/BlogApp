using BlogApp.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApp.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading;
using System.Drawing;
using System.Drawing.Drawing2D;
using Newtonsoft.Json;
using BlogApp.Common;
using Microsoft.Extensions.Configuration;

namespace BlogApp.Controllers
{
    public class NewsOprationsController : BaseController
    {
        private readonly IOperation<Newspost> _newsPost;
        
        private readonly IOperation<Tblnewstype> _newsType;

        private readonly IWebHostEnvironment _hostEnvironment;

        private readonly INewsOprations _newsOprations;

        private readonly IOperation<Schdulepostconfig> _schdulePostConfig;
        private readonly IapiPlugin _iapiPlugin;

        private readonly IConfiguration _configuration;

        public NewsOprationsController(IapiPlugin iapiPlugin, IConfiguration configuration,IOperation<Newspost> newsPost, IOperation<Tblnewstype> newsType, IWebHostEnvironment hostEnvironment, INewsOprations newsOprations, IOperation<Schdulepostconfig> schdulePostConfig)
        {
            this._newsPost = newsPost;
            this._newsType = newsType;
            this._hostEnvironment = hostEnvironment;
            this._newsOprations = newsOprations;
            this._schdulePostConfig = schdulePostConfig;
            this._iapiPlugin = iapiPlugin;
            this._configuration = configuration;
        }
        [AuthorizationPrivilege]
        public IActionResult Index()
        {
            return View();
        }
        [AuthorizationPrivilege]
        public IActionResult Dashbord()
        {
            return View();
        }
        [AuthorizationPrivilege]
        public IActionResult Analytics()
        {
            return View();
        }
        
        [HttpPost]
        public ContentResult HeaderImageUpload(IFormCollection data, IFormFile source)
        {
           Newspost nps = new Newspost();
            try
            {
                int NewsId = 0;
                string FolderName = "";
                string path = "";
                
                
                if (HttpContext.Request.Form["newsID"].ToString()=="0")
                {
                    NewsId = _newsOprations.getNewsNextId();
                    FolderName = DateTime.Now.Date.ToString("ddMMyyyy");
                    path = this._hostEnvironment.WebRootPath+"/Uploads/" + FolderName + "/" + NewsId;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    else if(Directory.Exists(path))
                    {
                        try
                        {
                            foreach (var item in Directory.GetFiles(path))
                            {
                                FileInfo fi3 = new FileInfo(item);
                                if (fi3.Exists)
                                    fi3.Delete();
                            }
                        }
                        catch (Exception)
                        {
                        }
                       
                    }
                }
                else
                {             
                    decimal newsID = Convert.ToDecimal(HttpContext.Request.Form["newsID"].ToString());
                    nps = _newsOprations.GetNewsposts(newsID);
                    string ImageName = "";
                    if (nps != null)
                    {
                            DeleteImage(nps.HeaderImageName);
                            ImageName = nps.HeaderImageName;
                            FolderName = ImageName.Split('/')[0];
                            NewsId = int.Parse(ImageName.Split('/')[1]);
                    }
                    
                    path = this._hostEnvironment.WebRootPath+"/Uploads/" + FolderName + "/" + NewsId;
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }
                string ImageUrl = "";
                string filename = "";
                string imgId = DateTime.Now.Hour + "" + DateTime.Now.Minute;
                
                
                 ImageUrl = Path.Combine(path, "Img_" + imgId + ".jpg");
                 Stream strm = data.Files.FirstOrDefault().OpenReadStream();
                 int flstatus = GenerateThumbnails(0.2, strm, ImageUrl, 400, 210);
                // filename = source.FileName;
                 CreateThumbnail(strm, NewsId, FolderName, imgId);
                 CreateThumbnail279(strm, NewsId, FolderName, imgId);
                 CreateThumbnail210(strm, NewsId, FolderName, imgId);
                
                if (HttpContext.Request.Form["newsID"].ToString() != "0")
                {
                    string Image = FolderName + "/" + NewsId + "/" + "Img_" + imgId + ".jpg";
                    
                        decimal newsID = Convert.ToDecimal(HttpContext.Request.Form["newsID"].ToString());
                        Newspost newsPost = _newsPost.GetByID(x => x.Id == newsID);
                        newsPost.HeaderImageName = Image;
                        newsPost.Thumbnail86 = Image.Replace("Img", "Thumbnail_86x64");
                        newsPost.Thumbnail210 = Image.Replace("Img", "Thumbnail_210x136");
                        newsPost.Thumbnail279 = Image.Replace("Img", "Thumbnail_279x220");
                        _newsPost.Edit(newsPost);
                         _newsPost.Save();
                }
                return Content(FolderName + "/" + NewsId + "/" + "Img_" + imgId + ".jpg");
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        private void CreateThumbnail(Stream strm, int NewsId, string FolderName, string imgId)
        {
            string path = this._hostEnvironment.WebRootPath + "/Uploads/" + FolderName + "/" + NewsId;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string ImageUrl = Path.Combine(path, "Thumbnail_86x64_" + imgId + ".jpg");
            
            int flstatus = GenerateThumbnails(0.1, strm, ImageUrl, 86, 64);
        }
        private void CreateThumbnail279(Stream strm, int NewsId, string FolderName, string imgId)
        {
            string path = this._hostEnvironment.WebRootPath + "/Uploads/" + FolderName + "/" + NewsId;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string ImageUrl = Path.Combine(path, "Thumbnail_279x220_" + imgId + ".jpg");
            
            int flstatus = GenerateThumbnails(0.1, strm, ImageUrl, 240, 130);
        }
        private void CreateThumbnail210(Stream strm, int NewsId, string FolderName, string imgId)
        {
            string path = this._hostEnvironment.WebRootPath + "/Uploads/" + FolderName + "/" + NewsId;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string ImageUrl = Path.Combine(path, "Thumbnail_210x136_" + imgId + ".jpg");
            
            int flstatus = GenerateThumbnails(0.1, strm, ImageUrl, 160, 103);
        }
        private int GenerateThumbnails(double scaleFactor, Stream sourcePath, string targetPath, int newWidth, int newHeight)
        {
            try
            {
                using (var image = Image.FromStream(sourcePath))
                {
                    var thumbnailImg = new Bitmap(newWidth, newHeight);
                    var thumbGraph = Graphics.FromImage(thumbnailImg);
                    thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                    thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                    thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                    thumbGraph.DrawImage(image, imageRectangle);
                    thumbnailImg.Save(targetPath, image.RawFormat);
                    thumbGraph.Dispose();
                    thumbnailImg.Dispose();
                    image.Dispose();
                    return 1;
                }

            }
            catch (Exception ex)
            {
            }
            return 0;
        }

        [HttpPost]
        public JsonResult NewsPost([FromBody]ClsPost clsPost)
        {
            try
            {
                var currentUser = HttpContext.User;
                var UserId = currentUser.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).Select(c => c.Value).SingleOrDefault();
                var Role = currentUser.Claims.Where(x => x.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();
                clsPost.IsReviewed = false;
                clsPost.CreatedBy = int.Parse(UserId);
                if (clsPost.IsSchedule == false)
                {
                    if (Role == "SuperAdmin" || Role == "Admin")
                    {
                        clsPost.ReviewedBy = int.Parse(UserId);
                        clsPost.IsReviewed = true;
                    }
                }
                clsPost.PostedOn = DateTime.Now;
                clsPost.PostedDate = DateTime.Now;
                clsPost.PostedYear = DateTime.Now.Year;
                clsPost.PostedMonth = DateTime.Now.Month;
                clsPost.SlugUrl = UrlGenerator.GetUrl(clsPost.EnglishTitle);
                
                decimal i = _newsOprations.NewsPost(clsPost);
                if (i > 0)
                {
                    Task.Factory.StartNew(() => _newsOprations.UpdateData());

                    if (clsPost.IsSchedule == true)
                    {
                        try
                        {
                            Schdulepostconfig schPostConfig = new Schdulepostconfig();
                            if (schPostConfig != null)
                            {
                                schPostConfig.PostId = long.Parse(i.ToString());
                                schPostConfig.PostedOn = DateTime.Now;
                                schPostConfig.ScheduleTime = DateTime.Now.AddHours(clsPost.Delay);
                                schPostConfig.TimeDelay = clsPost.Delay;
                                _schdulePostConfig.Insert(schPostConfig);
                                _schdulePostConfig.Save();
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    if (clsPost.IsSchedule == false)
                    {
                        if (Role == "SuperAdmin" || Role == "Admin")
                        {
                            if (clsPost.IsFacebookPublish == true)
                            {
                                string category = _newsType.GetByID(x => x.Id == clsPost.CategoryId).NewsType;
                                var webadrs = _configuration.GetSection("webid");
                                Task.Factory.StartNew(() => _iapiPlugin.pagePublish(clsPost.OdiaTitle, (webadrs + "/article/" + category + "/" + clsPost.PostedYear + "/" + clsPost.PostedMonth + "/" + i + "/" + clsPost.SlugUrl)));
                            }
                        }
                    }
                    return Json(new { msg = "News Posted Successfully..." });
                }
                else
                {
                    return Json(new { msg = "News Posted UnSuccessful..." });
                }
            }
            catch (Exception ex)
            {
            }
            return Json(new { msg = "News Posted UnSuccessful..." });
        }

        [HttpPost]
        public JsonResult ImageDelete([FromBody] ClsImage clsImage)
        {
            DeleteImage(clsImage.imagePath);
            return Json(null);
        }
        [HttpGet]
        public JsonResult GetAllNews(int? page, DateTime? startdt, DateTime? enddt)
        {
            try
            {
                var currentUser = HttpContext.User;
                var UserId = currentUser.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).Select(c => c.Value).SingleOrDefault();
                var Role = currentUser.Claims.Where(x => x.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();
                int currentuseid = int.Parse(UserId);
                if (Role == "SuperAdmin" || Role == "Admin")
                {
                    currentuseid = 0;
                }
                var item = _newsOprations.GetAllNews(0, currentuseid, 0, page.Value, startdt, enddt);
                if (item != null)
                {
                    return Json(new { data = JsonConvert.SerializeObject(item.Tables[0]), total_pages = JsonConvert.SerializeObject(item.Tables[1])});
                }
                else
                {
                    return Json(null);
                }
            }
            catch (Exception ex)
            {

            }
            return Json(null);
        }
        public JsonResult GetNewsById(int? Id)
        {
            try
            {
                Newspost newstyp = _newsPost.GetByID(x => x.Id == Id);

                Schdulepostconfig schPostConfig = _schdulePostConfig.GetByID(x => x.PostId == Id);

                return Json(new { _newspost = newstyp, _npsc = schPostConfig });
            }
            catch (Exception ex)
            {

            }
            return null;
        }
        public JsonResult DeletePost(int? Id)
        {
            try
            {
                Newspost newstyp = _newsPost.GetByID(x => x.Id == Id);
                if (newstyp != null)
                {
                    _newsPost.Delete(x => x.Id == Id);
                    _newsPost.Save();

                    Task.Factory.StartNew(() => _newsOprations.UpdateData());
                    if (!string.IsNullOrEmpty(newstyp.HeaderImageName))
                    {
                        DeleteImage(newstyp.HeaderImageName);
                    }
                }
                try
                {
                    Schdulepostconfig schPostConfig = _schdulePostConfig.GetByID(x => x.PostId == Id);
                    if (schPostConfig != null)
                    {
                        _schdulePostConfig.Delete(x => x.PostId == Id);
                        _schdulePostConfig.Save();
                    }
                }
                catch (Exception ex)
                {

                }
                return Json(new { msg = "Post Deleted Successfully" });
            }
            catch (Exception ex)
            {
                
            }
            return Json(new { msg = "Post Deletion Unsucessful" });
        }

        [HttpPost]
        public JsonResult UpdatePost([FromBody]ClsPost clsPost)
        {
            try
            {
                var currentUser = HttpContext.User;
                var UserId = currentUser.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).Select(c => c.Value).SingleOrDefault();
                var Role = currentUser.Claims.Where(x => x.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();
                clsPost.IsReviewed = false;
                if (clsPost.IsSchedule == false)
                {
                    if (Role == "SuperAdmin" || Role == "Admin")
                    {
                        clsPost.ReviewedBy = int.Parse(UserId);
                        clsPost.IsReviewed = true;
                    }
                }
                clsPost.Modified = DateTime.Now;
                bool IsReviewed = false;
                
                
                    Newspost newsPost = _newsPost.GetByID(x => x.Id == clsPost.Id);
                    IsReviewed = newsPost.IsReviewed.Value;
                    newsPost.EnglishTitle = clsPost.EnglishTitle;
                    newsPost.OdiaTitle = clsPost.OdiaTitle;
                    newsPost.EngShortDesc = clsPost.ODShortDesc;
                    newsPost.OdshortDesc = clsPost.ODShortDesc;
                    newsPost.SeoMeta = clsPost.SeoMeta;
                    newsPost.Tags = clsPost.Tags;
                    newsPost.CategoryId = clsPost.CategoryId;
                    newsPost.HeaderImageName = clsPost.ImageName;
                    newsPost.Odcontent = clsPost.ODContent;
                    newsPost.IsReviewed = clsPost.IsReviewed;
                    newsPost.ReviewedBy = clsPost.ReviewedBy;
                    newsPost.ModifiedOn = clsPost.Modified;
                    newsPost.Thumbnail86 = clsPost.ImageName.Replace("Img", "Thumbnail_86x64");
                    newsPost.Thumbnail210 = clsPost.ImageName.Replace("Img", "Thumbnail_210x136");
                    newsPost.Thumbnail279 = clsPost.ImageName.Replace("Img", "Thumbnail_279x220");
                    newsPost.SlugUrl = UrlGenerator.GetUrl(clsPost.EnglishTitle);
                    _newsPost.Edit(newsPost);
                    _newsPost.Save();
                    //if(IsReviewed==false)
                    {
                        if (Role == "SuperAdmin" || Role == "Admin")
                        {
                            string category = _newsType.GetByID(x => x.Id == newsPost.CategoryId).NewsType;
                            var webadrs = _configuration.GetSection("webid");
                            Task.Factory.StartNew(() => _iapiPlugin.pagePublish(newsPost.OdiaTitle, (webadrs + "/article/" + category + "/" + newsPost.PostedYear + "/" + newsPost.PostedMonth + "/" + clsPost.Id + "/" + newsPost.SlugUrl)));
                        }
                    }
                
                Task.Factory.StartNew(() => _newsOprations.UpdateData());
                if (clsPost.IsSchedule == true)
                {
                    try
                    {
                        Schdulepostconfig schPostConfig = _schdulePostConfig.GetByID(x => x.PostId == clsPost.Id);
                        if (schPostConfig != null)
                        {
                            schPostConfig.PostId = clsPost.Id;
                            schPostConfig.PostedOn = DateTime.Now;
                            schPostConfig.ScheduleTime = DateTime.Now.AddHours(clsPost.Delay);
                            schPostConfig.TimeDelay = clsPost.Delay;
                            _schdulePostConfig.Edit(schPostConfig);
                            _schdulePostConfig.Save();
                        }
                        else
                        {
                            Schdulepostconfig scPostConfig = new Schdulepostconfig();
                            scPostConfig.PostId = clsPost.Id;
                            scPostConfig.PostedOn = DateTime.Now;
                            scPostConfig.ScheduleTime = DateTime.Now.AddHours(clsPost.Delay);
                            scPostConfig.TimeDelay = clsPost.Delay;
                            _schdulePostConfig.Insert(scPostConfig);
                            _schdulePostConfig.Save();
                        }
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
                else
                {
                    try
                    {
                        Schdulepostconfig schPostConfig = _schdulePostConfig.GetByID(x => x.PostId == clsPost.Id);
                        if (schPostConfig != null)
                        {
                            _schdulePostConfig.Delete(x => x.PostId == clsPost.Id);
                            _schdulePostConfig.Save();
                        }
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
                return Json(new { msg = "Post Updated Successfully" });
            }
            catch (Exception ex)
            {
                ;
            }
            return Json(new { msg = "Post Updation Unsucessful" });
        }
        [HttpPost]
        public JsonResult ApprovePost(int? Id)
        {
            try
            {
               
               
                    var currentUser = (ClaimsPrincipal)Thread.CurrentPrincipal;
                    var UserId = currentUser.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).Select(c => c.Value).SingleOrDefault();
                    Newspost newsPost = _newsPost.GetByID(x => x.Id == Id);
                    newsPost.IsReviewed = true;
                    newsPost.ReviewedBy = int.Parse(UserId);
                     _newsPost.Edit(newsPost);
                     _newsPost.Save();
                    string category = _newsType.GetByID(x => x.Id == newsPost.CategoryId).NewsType;
                    var webadrs = _configuration.GetSection("webid");
                    Task.Factory.StartNew(() => _iapiPlugin.pagePublish(newsPost.OdiaTitle, (webadrs + "/article/" + category + "/" + newsPost.PostedYear + "/" + newsPost.PostedMonth + "/" + Id + "/" + newsPost.SlugUrl)));
                
                Task.Factory.StartNew(() => _newsOprations.UpdateData());

                return Json(new { msg = "Post Approved Successfully" });
            }
            catch (Exception ex)
            {
               
            }
            return Json(new { msg = "Post Approve Unsucessful" });
        }
        public async System.Threading.Tasks.Task<JsonResult> GetAnalytics(string mode = "mine")
        {
            try
            {
                var currentUser = HttpContext.User;
                var UserId = currentUser.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).Select(c => c.Value).SingleOrDefault();
                if (mode != "mine")
                {
                    UserId = "0";
                }
                var item = await Task.Run(() => _newsOprations.GetAnalytics(Convert.ToInt32(UserId)));
                return Json(new { TotPst = JsonConvert.SerializeObject(item.Tables[0]), TotView = JsonConvert.SerializeObject(item.Tables[1]), TdayYday = JsonConvert.SerializeObject(item.Tables[2]), Wkly = JsonConvert.SerializeObject(item.Tables[3]), Monthly = JsonConvert.SerializeObject(item.Tables[4]), DayView = JsonConvert.SerializeObject(item.Tables[5]), WkView = JsonConvert.SerializeObject(item.Tables[6]), MonthView = JsonConvert.SerializeObject(item.Tables[7]) });
            }
            catch (Exception ex)
            {
               
            }
            return null;
        }

        private void DeleteImage(string imagePath)
        {
            try
            {
                string path = this._hostEnvironment.WebRootPath+"/Uploads/" + imagePath;
                FileInfo fi = new FileInfo(path);
                if (fi.Exists)
                    fi.Delete();
                string path1 = this._hostEnvironment.WebRootPath+"/Uploads/" + imagePath.Replace("Img", "Thumbnail_210x136");
                FileInfo fi1 = new FileInfo(path1);
                if (fi1.Exists)
                    fi1.Delete();
                string path2 = this._hostEnvironment.WebRootPath+"/Uploads/" + imagePath.Replace("Img", "Thumbnail_279x220");
                FileInfo fi2 = new FileInfo(path2);
                if (fi2.Exists)
                    fi2.Delete();
                string path3 = this._hostEnvironment.WebRootPath+"/Uploads/" + imagePath.Replace("Img", "Thumbnail_86x64");
                FileInfo fi3 = new FileInfo(path3);
                if (fi3.Exists)
                    fi3.Delete();
            }
            catch (Exception ex)
            {
                
            }
        }

        [HttpPost]
        public JsonResult UploadFile(IList<IFormFile> files)
        {
            string ImageUrl = string.Empty;
            string filename = string.Empty;
            Newspost nps = new Newspost();
            try
            {
                int NewsId = 0;
                string FolderName = "";
                string path = "";
                if (Request.Query["NewsId"] == "0")
                {   
                     NewsId = _newsPost.GetAll(x => x.PostedDate.Value.Year == DateTime.Now.Year && x.PostedDate.Value.Month == DateTime.Now.Month && x.PostedDate.Value.Day == DateTime.Now.Day).ToList().Count;
                    
                    NewsId = NewsId + 1;

                    FolderName = DateTime.Now.Date.ToString("ddMMyyyy");
                    path = this._hostEnvironment.WebRootPath+"/Uploads/" + FolderName + "/" + NewsId + "/Content";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }
                else
                {
                    
                    {
                        decimal newsID = Convert.ToDecimal(Request.Query["NewsId"]);
                        nps = _newsPost.GetByID(x => x.Id == newsID);
                        string ImageName = "";
                        if (nps != null)
                        {
                            ImageName = nps.HeaderImageName;
                            FolderName = ImageName.Split('/')[0];
                            NewsId = int.Parse(ImageName.Split('/')[1]);
                        }
                    }
                    path = this._hostEnvironment.WebRootPath+"/Uploads/" + FolderName + "/" + NewsId + "/Content";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }

                string imgId = DateTime.Now.Minute + "" + DateTime.Now.Second;
                if (files.Count>0)
                {
                    
                    if (files[0]!=null)
                    {
                        ImageUrl = Path.Combine(path, "Img_" + imgId + ".jpg");
                        Stream strm = files[0].OpenReadStream();
                        int flstatus = GenerateThumbnails(0.2, strm, ImageUrl, 400, 210);
                        filename = HttpContext.Request.Protocol + "://" + HttpContext.Request.PathBase + "/Uploads/" + FolderName + "/" + NewsId + "/Content/Img_" + imgId + ".jpg";
                    }
                }
            }
            catch (Exception ex)
            {
               
            }

            return Json(Convert.ToString(filename));
        }
        [HttpPost]
        public JsonResult DeleteContent(string ImgUrl)
        {
            try
            {
                ImgUrl = ImgUrl.Replace(HttpContext.Request.Protocol + "://" + HttpContext.Request.PathBase, "");
                string path = this._hostEnvironment.WebRootPath+"/"+ ImgUrl;
                FileInfo fi = new FileInfo(path);
                if (fi.Exists)
                    fi.Delete();
                return Json("Image Deleted Successfully");
            }
            catch (Exception ex)
            {
                
            }
            return Json("");
        }
    }
}
