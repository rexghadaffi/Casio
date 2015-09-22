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
    public class AuthenticationController : BaseController
    {
        //GET: /Authentication/
        public ActionResult Index()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    AuthenticationContext db = new AuthenticationContext(new Authentication
                    {
                        Username = User.Identity.Name,
                        Type = Session["role"].ToString()
                    });
                    return RedirectToAction("Index", db.LandingPage);
                }
                return View();
            }
            catch
            {
                FormsAuthentication.SignOut();
                return View();
            }
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
                    Session["role"] = login.Type;
                    base.dbAudit._userid = dbAdmin.GetIdForUser(login.Username);
                    base.dbAudit.LoggedIn();
                    return RedirectToAction("Index", db.LandingPage);
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
            base.dbAudit._userid = dbAdmin.GetIdForUser(User.Identity.Name);
            base.dbAudit.LoggedOut();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}