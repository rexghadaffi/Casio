using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RhastyGaming.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        protected AuditContext dbAudit = new AuditContext();
        protected AdminContext dbAdmin = new AdminContext();       
        protected TransactionContext dbTransaction = new TransactionContext();
        protected void SetUserIDForAudit()
        {
            dbAudit._userid = dbAdmin.GetIdForUser(User.Identity.Name);
        }    
	}
}