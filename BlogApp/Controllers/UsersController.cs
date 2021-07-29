using BlogApp.Common;
using BlogApp.Concrete;
using BlogApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace BlogApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly IOperation<Tbluser> _users;
        private readonly IUsermanament _usermgnt;
        private readonly INewsOprations _newsOprations;
        public UsersController(IOperation<Tbluser> users, IUsermanament usermgnt, INewsOprations _newsOprations)
        {
            this._users = users;
            this._usermgnt = usermgnt;
            this._newsOprations = _newsOprations;
        }
        [AuthorizationPrivilege]
        public IActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            
            clsUser _users = new clsUser();
            return View(_users);
        }
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Login(clsUser _user, string returnUrl = default(string))
        {
            string rtnUrl = ViewBag.ReturnUrl;
           
            TempData["loginFailedMessage"] = "";
            if (ModelState.IsValid)
            {
                var loginResult = _usermgnt.Login(_user.UserName, new Helper().Encrypt(_user.Password));
                if (loginResult != null)
                {
                    var userSession = new UserSessionModel
                    {
                        UserId = Guid.NewGuid(),
                        DisplayName = loginResult.FullName
                    };

                    var identity = new ClaimsIdentity(AuthenticationHelper.CreateClaim(userSession, loginResult.UserName, loginResult.UserId, loginResult.RoleName.ToString()), CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                   
                    if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
                    return RedirectToAction("Dashbord", "NewsOprations");
                }
                else
                {
                    TempData["loginFailedMessage"] = "Entered UserName and Password is Wrong";
                }
            }
            else
            {
                if (string.IsNullOrEmpty(_user.UserName))
                {
                    TempData["loginFailedMessage"] = "Please Enter Your Username";
                }
                else if (string.IsNullOrEmpty(_user.Password))
                {
                    TempData["loginFailedMessage"] = "Please Enter Your Password";
                }
            }
            return View();
        }
        [AllowAnonymous]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        public IActionResult UserProfile()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetCurrentUser()
        {
            var currentUser = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var UserId = currentUser.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).Select(c => c.Value).SingleOrDefault();
            int UserID = int.Parse(UserId);
            var _user = _newsOprations.getCurrentUserId(UserID);
            return Json(new { user = JsonConvert.SerializeObject(_user) });
        }
        [HttpPost]
        public JsonResult SetCurrentUserInfo(clsUser _user)
        {
            var currentUser = (ClaimsPrincipal)Thread.CurrentPrincipal;
            var UserId = currentUser.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).Select(c => c.Value).SingleOrDefault();
            int UserID = int.Parse(UserId);
            Tbluser user = _users.GetByID(x => x.UserId == UserID);
            user.FullName = _user.FullName;
            user.Email = _user.Email;
            user.Phone = _user.Phone;
            user.DateUpdate = DateTime.Now;
            _users.Edit(user);
            _users.Save();
            return Json(new { user = "Sucess" });
        }
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(UpdateAccount _user)
        {
            string message = "";
            bool status = false;

            if (ModelState.IsValid)
            {
                var currentUser = (ClaimsPrincipal)Thread.CurrentPrincipal;
                var UserId = currentUser.Claims.Where(x => x.Type == ClaimTypes.PrimarySid).Select(c => c.Value).SingleOrDefault();
                int UserID = int.Parse(UserId);
                _user.CurrentPassword = new Helper().Encrypt(_user.CurrentPassword);
                Tbluser user = _users.GetByID(x => x.UserId == UserID && x.Password == _user.CurrentPassword);
                if (user != null)
                {
                    user.Password = new Helper().Encrypt(_user.ConfirmPassword);
                    _users.Edit(user);
                    _users.Save();
                    return RedirectToAction("Logout");
                }
                else
                {
                    TempData["valid"] = "You Enter Wrong Current Password";
                }
            }
            return View();
        }
        public IActionResult ChangePasswordByUser()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePasswordByUser(clsUser _user)
        {
            return View();
        }
        [HttpGet]
        [AuthorizationPrivilege]
        public JsonResult GetUser(int? page, int? pageSize)
        {
            ClsPaged<Tbluser> listUser = new ClsPaged<Tbluser>();
            var model = _users.GetAll(i => i.IsDeleted == false).ToList();
            return Json(listUser.Get(page, pageSize, model));
        }
        [HttpGet]
        public JsonResult GetUsers()
        {
            try
            {
                var model = _users.GetAll(i => i.IsDeleted == false).ToList();
                return Json(model);
            }
            catch (Exception ex)
            {

            }
            return Json(null);
        }
        [HttpPost]
        [AuthorizationPrivilege]
        public JsonResult InsertUser(clsUser _user)
        {
            try
            {
                var usernm = _users.GetByID(x => x.UserName == _user.UserName);
                if (usernm != null)
                {
                    return Json(new { usernm = "UserName already Exist" });
                }
                var emailId = _users.GetByID(x => x.Email == _user.Email);
                if (emailId != null)
                {
                    return Json(new { emailId = "Email already Exist" });
                }
                var mobileNo = _users.GetByID(x => x.Phone == _user.Phone);
                if (mobileNo != null)
                {
                    return Json(new { mobileNo = "Mobile No. already Exist" });
                }
                Tbluser user = new Tbluser();
                user.UserName = _user.UserName;
                user.FullName = _user.FullName;
                user.Email = _user.Email;
                user.Password = new Helper().Encrypt("welcome_123");
                user.Phone = _user.Phone;
                user.RoleId = _user.RoleId;
                user.IsBlocked = false;
                user.DateCreate = DateTime.Now;
                user.IsDeleted = false;
                _users.Insert(user);
                _users.Save();
                return Json(user);
            }
            catch (Exception ex)
            {
            }
            return Json(null);
        }
        [HttpPost]
        public JsonResult UpdateUser(clsUser _user)
        {
            try
            {
                Tbluser user = _users.GetByID(x => x.UserId == _user.UserId);
                user.FullName = _user.FullName;
                user.Email = _user.Email;
                user.Phone = _user.Phone;
                user.DateUpdate = DateTime.Now;
                _users.Edit(user);
                _users.Save();
                return Json(new { usernm = "User Information Updated  Successfully" });
            }
            catch (Exception ex)
            {

            }
            return Json(null);
        }
        [HttpPost]
        public JsonResult DeleteUser(int Id)
        {
            try
            {
                Tbluser user = _users.GetByID(x => x.UserId == Id);
                user.IsDeleted = true;
                _users.Edit(user);
                _users.Save();
                return Json(new { msg = "User Deleted Successfully" });
            }
            catch (Exception ex)
            {

            }
            return Json(null);
        }
        [HttpPost]
        public JsonResult UserReset(int Id)
        {
            try
            {
                Tbluser user = _users.GetByID(x => x.UserId == Id);
                user.Password = new Helper().Encrypt("welcome_123");
                _users.Edit(user);
                _users.Save();
                return Json(new { msg = "User Reset Successfully" });
            }
            catch (Exception ex)
            {

            }
            return Json(null);
        }
        [HttpPost]
        public JsonResult UserBlock(int Id, bool status)
        {
            try
            {
                Tbluser user = _users.GetByID(x => x.UserId == Id);
                user.IsBlocked = !status;
                _users.Edit(user);
                _users.Save();
                return Json(new { msg = status });
            }
            catch (Exception ex)
            {

            }
            return Json(null);
        }
    }
}
