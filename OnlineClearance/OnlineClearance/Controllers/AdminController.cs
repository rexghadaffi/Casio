using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineClearance.Models;
using OnlineClearance.DAL;

namespace OnlineClearance.Controllers
{
    public class AdminController : Controller
    {
        private AdminContext dbAdmin = new AdminContext();
        private RolesContext dbRoles = new RolesContext();
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            List<Admin> admins = dbAdmin.GetAllAdmin.ToList();
            return View(admins);
        }
        public ActionResult Create()
        {
            CreateDropDown();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Admin admin)
        {
            TryUpdateModel(admin);
            if (ModelState.IsValid)
            {
                dbAdmin.Insert(admin);
                return RedirectToAction("Index");
            }
            CreateDropDown();
            return View();
        }
        public ActionResult Edit(int id)
        {
            Admin admin = dbAdmin.GetAllAdmin.FirstOrDefault(a => a.ID == id);
            SetDropDown(admin);
            return View(admin);
        }
        [HttpPost]
        public ActionResult Edit(Admin admin, int id)
        {
            TryUpdateModel(admin);
            if (ModelState.IsValid)
            {
                dbAdmin.Update(admin, id);
                return RedirectToAction("Index");
            }
            SetDropDown(admin);
            return View(admin);
        }
        private void CreateDropDown()
        {           
            ViewBag.Select = new SelectList(dbRoles.GetAllRoles, "ID", "Name");    
        }
        private void SetDropDown(Admin admin)
        {
            ViewBag.Select = new SelectList(dbRoles.GetAllRoles, "ID", "Name", admin.RoleID);    
        }
    }
}
