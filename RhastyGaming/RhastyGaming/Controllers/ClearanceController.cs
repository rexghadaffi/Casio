using DataAccessLayer;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RhastyGaming.Controllers
{
    [Authorize(Roles = "tblstudent")]
    public class ClearanceController : Controller
    {
        private ClearanceContext dbContext = new ClearanceContext();
        //
        // GET: /Clearance/
        public ActionResult Index()
        {
            ClearanceViewModel vm = dbContext.Fetch(User.Identity.Name);
            ViewBag.Lastname = vm.Student.Firstname + " " + vm.Student.Lastname;
            return View(vm);
        }
	}
}