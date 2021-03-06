﻿using DataAccessLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RhastyGaming.Controllers
{
    [Authorize(Roles = "tblemployee")]
    public class AccountabilityController : Controller
    {
        private EmployeeContext dbEmployee = new EmployeeContext();
        private DepartmentContext dbDepartment = new DepartmentContext();
        private AccountabilitiesContext dbContext = new AccountabilitiesContext();
        //
        // GET: /Accountability/
        public ActionResult Index()
        {

            ViewBag.IsRegistrar = dbEmployee.GetDepartmentName(dbEmployee.GetDepartmentID(User.Identity.Name));            
            return View();
        }
        public ActionResult ShowModal(string snum)
        {
            SelectDropDown(dbEmployee.GetDepartmentID(User.Identity.Name));
            if (dbContext.Fetch.Where(s => s.StudentNumber == snum).Count() > 0)
            {
                return PartialView("~/Views/Accountability/Edit.cshtml",
                    dbContext.Fetch.Single(s => s.StudentNumber == snum));
            }
            return PartialView("~/Views/Accountability/Add.cshtml",
                               new Accountability { StudentNumber = snum });
        }
        [HttpPost]
        public ActionResult AddNew(Accountability data)
        {
            if (ModelState.IsValid)
            {
                data.DepartmentID = dbEmployee.GetDepartmentID(User.Identity.Name);
                dbContext.AddAccountability(data);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(int id, Accountability data)
        {
            if (ModelState.IsValid)
            {
                data.DepartmentID = dbEmployee.GetDepartmentID(User.Identity.Name);
                if (data.Status)
                {
                    dbContext.DisableConfirmationCode(data.StudentNumber);
                }
                dbContext.EditAccountability(data, id);
            }
            return RedirectToAction("Index");
        }
        public void SelectDropDown(int departmentID)
        {
            ViewBag.DepartmentID = new SelectList(dbDepartment.GetAllDepartment,
                                           "ID",
                                           "Name",
                                           departmentID);
        }
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                dbContext.MassUpload(file, dbEmployee.GetDepartmentID(User.Identity.Name));
            }
            return RedirectToAction("Index");
        }
    }
}