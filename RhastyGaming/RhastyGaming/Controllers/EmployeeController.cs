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
    public class EmployeeController : BaseController
    {
        private EmployeeContext dbEmployee = new EmployeeContext();
        private DepartmentContext dbDepartment = new DepartmentContext();
        private RolesContext dbRole = new RolesContext();
        //
        // GET: /Employee/
        public ActionResult Index()
        {
            List<EmployeeList> employees = dbEmployee.GetAllEmployee.ToList();
            return View(employees);
        }
        public ActionResult Create()
        {
            CreateDropDowns();
            return View();
        }
        [HttpPost]
        public ActionResult Create(EmployeeList data)
        {
            CreateDropDowns();
            try
            {
                UpdateModel(data);
                if (ModelState.IsValid)
                {
                    data.Status = true;
                    dbEmployee.Insert(data);
                    base.SetUserIDForAudit();
                    base.dbAudit.Add("User has created a new department head record");
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
            EmployeeList employee = dbEmployee.GetAllEmployee.FirstOrDefault(e => e.ID == id);
            SetDropDowns(employee);
            return View(employee);
        }
        [HttpPost]
        public ActionResult Edit(EmployeeList data, int id)
        {
            SetDropDowns(data);
            try
            {
                UpdateModel(data);
                if (ModelState.IsValid)
                {
                    dbEmployee.Update(data, id);
                    base.SetUserIDForAudit();
                    base.dbAudit.Add("User has updated a department head record");
                    return RedirectToAction("Index");
                }               
                return View(data);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = MessageBox.Error(ex.Message);
                return View(data);
            }
          
        }
        // DropDownList initializer
        private void CreateDropDowns()
        {
            ViewBag.Role = new SelectList(dbRole.GetAllRoles, "ID", "Name");
            ViewBag.Dept = new SelectList(dbDepartment.GetAllDepartment, "ID", "Name"); 
        }
        private void SetDropDowns(EmployeeList employee)
        {
            ViewBag.Role = new SelectList(dbRole.GetAllRoles,
                                            "ID",
                                            "Name",
                                            employee.RoleID);
            ViewBag.Dept = new SelectList(dbDepartment.GetAllDepartment,
                                            "ID",
                                            "Name",
                                            employee.DepartmentID);
        }
	}
}