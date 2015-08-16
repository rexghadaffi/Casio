using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using DAL;

namespace OnlineClearance.Controllers
{
    public class AdminController : Controller
    {
        private AdminContext dbAdmin = new AdminContext();
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            List<Admin> admins = dbAdmin.GetAllAdmin.ToList();
            return View(admins);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Admin admin)
        {
            TryUpdateModel(admin);
            if (ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
