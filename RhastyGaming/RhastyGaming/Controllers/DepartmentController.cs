using DataAccessLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RhastyGaming.Controllers
{
    [Authorize(Roles = "tblcompanyuser")]
    public class DepartmentController : BaseController
    {
        private DepartmentContext dbDepartment = new DepartmentContext();
        //
        // GET: /Department/
        public ActionResult Index()
        {
            List<Department> departments = dbDepartment.GetAllDepartment.ToList();
            return View(departments);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Department data)
        {
            TryUpdateModel(data);
            if (ModelState.IsValid)
            {
                dbDepartment.Insert(data);
                base.SetUserIDForAudit();
                base.dbAudit.Add("User has created a new department record");
                return RedirectToAction("Index");
            }
            return View();
        }
        public ActionResult Edit(int id)
        {
            Department department = 
                       dbDepartment.GetAllDepartment.FirstOrDefault(d => d.ID == id);
            return View(department);
        }
        [HttpPost]
        public ActionResult Edit(Department data, int id) 
        {
            TryUpdateModel(data);
            if (ModelState.IsValid)
            {
                dbDepartment.Update(data, id);
                base.SetUserIDForAudit();
                base.dbAudit.Edit("User has updated a department record");
                return RedirectToAction("Index");
            }
            return View();
        }
	}
}