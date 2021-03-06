﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model;
using DataAccessLayer;
using System.IO;
using System.Dynamic;
using Newtonsoft.Json;

namespace RhastyGaming.Controllers
{
    
    public class StudentController : BaseController
    {
        private StudentContext dbStudent = new StudentContext();
        //
        // GET: /Student/
        [Authorize(Roles = "tblcompanyuser")]
        public ActionResult Index()
        {            
            return View();
        }
        [Authorize(Roles = "tblcompanyuser")]
        public ActionResult Edit(int id)
        {
            Student student = dbStudent.GetAllStudent.FirstOrDefault(s => s.ID == id);
            return View(student);
        }
         [Authorize(Roles = "tblcompanyuser")]
        [HttpPost]
        public ActionResult Edit(Student student, int id)
        {
            TryUpdateModel(student);
            if (ModelState.IsValid)
            {
                dbStudent.Update(student, id);
                base.SetUserIDForAudit();
                base.dbAudit.Edit("User has updated a student record");
                return RedirectToAction("Index");
            }
            return View();
        }
         [Authorize(Roles = "tblcompanyuser")]
        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);

                // then save on the server...
                var path = Path.Combine(Server.MapPath("~/uploads/student"), fileName);
                file.SaveAs(path);
                dbStudent.MassUpload(fileName);
                base.SetUserIDForAudit();
                base.dbAudit.Edit("User has uploaded student records");
            }
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "tblcompanyuser, tblemployee")]
        public string Data(int pageSize, int pageNumber, string sortOrder, string searchText = null)
        {
            var skip = (pageNumber - 1) * pageSize;
            int skipResult = Convert.ToInt32(skip);

            IEnumerable<Student> students = IsSearching(searchText);
            
            //----------------
            var listCount = students.Count();
            var list = students.ToList().Skip(skipResult).Take(pageSize);

            dynamic foo = new ExpandoObject();
            foo.total = listCount;
            foo.rows = list;

            string json = JsonConvert.SerializeObject(foo);


            return json;
        }

        private IEnumerable<Student> IsSearching(string searchText)
        {
            IEnumerable<Student> students = dbStudent.GetAllStudent;
            if (searchText != null)
            {
                return students.Where(s => s.Firstname.Contains(searchText) ||
                               s.Lastname.Contains(searchText) ||
                               s.Middlename.Contains(searchText) ||
                               s.Program.Contains(searchText) ||
                               s.StudentNumber.Contains(searchText));
            }
            return students;
        }        
    }
}