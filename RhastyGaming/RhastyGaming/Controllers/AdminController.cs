using DataAccessLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Utility;

namespace RhastyGaming.Controllers
{
    public class AdminController : Controller
    {
        //
        // GET: /Admin/
        private AdminContext dbAdmin = new AdminContext();
        private RolesContext dbRoles = new RolesContext();
    
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
            CreateDropDown();
            try
            {
                UpdateModel(admin);
                if (ModelState.IsValid)
                {
                    dbAdmin.Insert(admin);
                    return RedirectToAction("Index");
                }              
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MessageBox.Error(ex.Message);
                return View();
            }
           
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