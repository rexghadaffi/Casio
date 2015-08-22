using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OnlineClearance.Models;
using OnlineClearance.DAL;

namespace OnlineClearance.Controllers
{
    public class AuthenticationController : Controller
    {
        //
        // GET: /Authentication/

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Authentication data)
        {
            TryUpdateModel(data);
            try
            {
                AuthenticationContext dbAuth = new AuthenticationContext(data);
                if (dbAuth.IsValidUser())
                {
                    FormsAuthentication.SetAuthCookie(data.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.ErrorMessage = "Invalid Username/Password";
                return View();     
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View();
            }          
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}
