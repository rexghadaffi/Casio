using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RhastyGaming.Controllers
{
    public class ReportsController : Controller
    {
        private AuditContext dbAudit = new AuditContext();
        //
        // GET: /Reports/
        public ActionResult Audit()
        {
            return View(dbAudit.GetAllAudit.ToList());
        }
        public ActionResult Transaction()
        {
           return View(dbAudit.GetAllAudit.ToList());
        }
	}
}