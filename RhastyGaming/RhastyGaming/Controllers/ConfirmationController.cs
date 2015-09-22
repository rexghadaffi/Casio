using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RhastyGaming.Controllers
{
    [Authorize(Roles = "tblemployee")]
    public class ConfirmationController : Controller
    {
        private ConfirmationContext dbConfirmation = new ConfirmationContext();
        private EmployeeContext dbEmployee = new EmployeeContext();
        //
        // GET: /Confirmation/
        public ActionResult Index()
        {
            ViewBag.IsRegistrar = dbEmployee.GetDepartmentName(dbEmployee.GetDepartmentID(User.Identity.Name));
            return View(dbConfirmation.Fetch.Where(c => c.Status == true));
        }
	}
}