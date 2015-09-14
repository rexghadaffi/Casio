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
    [Authorize(Roles = "tblcompanyuser")]
    public class AdminController : BaseController
    {           
        //
        // GET: /Admin/     
        private RolesContext dbRoles = new RolesContext();
    
        public ActionResult Index()
        {
            List<Admin> admins = base.dbAdmin.GetAllAdmin.ToList();
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
                    admin.Status = true;
                    base.dbAdmin.Insert(admin);
                    base.SetUserIDForAudit();
                    base.dbAudit.Add("User has created a new admin account");
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
            Admin admin = base.dbAdmin.GetAllAdmin.FirstOrDefault(a => a.ID == id);
            SetDropDown(admin);
            return View(admin);
        }
        [HttpPost]
        public ActionResult Edit(Admin admin, int id)
        {
            SetDropDown(admin);
            try
            {
                UpdateModel(admin);
                if (ModelState.IsValid)
                {
                    base.dbAdmin.Update(admin, id);
                    base.SetUserIDForAudit();
                    base.dbAudit.Edit("User has updated an admin account record");
                    return RedirectToAction("Index");
                }
               
                return View(admin);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MessageBox.Error(ex.Message);
                return View(admin);
            }          
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