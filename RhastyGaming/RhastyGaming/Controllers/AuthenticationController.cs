using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using DataAccessLayer;
using System.Web.Security;

namespace RhastyGaming.Controllers
{
    public class AuthenticationController : Controller
    {

        //GET: /Authentication/
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(Authentication login)
        {
            try
            {
                UpdateModel(login);
                AuthenticationContext db = new AuthenticationContext(login);
                if (db.Message == "")
                {
                    FormsAuthentication.SetAuthCookie(login.Username, false);
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.ErrorMessage = db.Message;
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