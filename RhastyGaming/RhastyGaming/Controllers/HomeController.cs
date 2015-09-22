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
    public class HomeController : Controller
    {
        private StudentContext dbStudent = new StudentContext();
        private AccountabilitiesContext dbAccountability = new AccountabilitiesContext();
        private EmployeeContext dbEmployee = new EmployeeContext();
        private AdminContext dbAdmin = new AdminContext();
        //
        // GET: /Home/
        public ActionResult Index()
        {
            IQueryable<Student> students = dbStudent.GetAllStudent.AsQueryable();
            IQueryable<Admin> admins = dbAdmin.GetAllAdmin.AsQueryable();
            IQueryable<Employee> employees = dbEmployee.GetAllEmployee.AsQueryable();
            //Students
            ViewBag.ActiveStudents = students.Where(s => s.Status == true).Count();
            ViewBag.InactiveStudents = students.Where(s => s.Status == false).Count();
            ViewBag.Uncleared = dbAccountability.Fetch.Where(a => a.Status == true).Count();
            //Enrolled 
            ViewBag.TwentyOSix = students.Where(sy => sy.SchoolYear.Contains("2005-2006")).Count();
            ViewBag.TwentyOSeven = students.Where(sy => sy.SchoolYear.Contains("2006-2007")).Count();
            ViewBag.TwentyOEight = students.Where(sy => sy.SchoolYear.Contains("2007-2008")).Count();
            ViewBag.TwentyONine = students.Where(sy => sy.SchoolYear.Contains("2008-2009")).Count();
            ViewBag.TwentyOTen = students.Where(sy => sy.SchoolYear.Contains("2009-2010")).Count();
            ViewBag.TwentyOEleven = students.Where(sy => sy.SchoolYear.Contains("2010-2011")).Count();
            ViewBag.TwentyOTwelven = students.Where(sy => sy.SchoolYear.Contains("2011-2012")).Count();
            ViewBag.TwentyOThirteen = students.Where(sy => sy.SchoolYear.Contains("2012-2013")).Count();
            ViewBag.TwentyOFourteen = students.Where(sy => sy.SchoolYear.Contains("2013-2014")).Count();
            ViewBag.TwentyOFifteen = students.Where(sy => sy.SchoolYear.Contains("2014-2015")).Count();
            //Admins
            ViewBag.ActiveAdmin = admins.Where(admin => admin.Status == true).Count();
            ViewBag.InactiveAdmin = admins.Where(admin => admin.Status == false).Count();
            ViewBag.TotalAdmin = admins.Count();
            //Employee
            ViewBag.ActiveEmployee = employees.Where(e => e.Status == true).Count();
            ViewBag.InactiveEmployee = employees.Where(e => e.Status == false).Count();
            ViewBag.TotalEmployee = employees.Count();
            return View();
        }
	}
}