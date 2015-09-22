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

        public string IsCleared()
        {
            AccountabilitiesContext dbAccountability = new AccountabilitiesContext();
            int count = dbAccountability.Fetch.Where(a => a.StudentNumber == User.Identity.Name && a.Status == true).Count();
            
            if (count <= 0)
            {
                return dbContext.RetrieveConfirmationCode(User.Identity.Name);                        
            }          
            return "You Still Have Pending Accountabilities";
        }
	}
}