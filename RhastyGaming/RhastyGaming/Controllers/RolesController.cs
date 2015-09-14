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
    public class RolesController : Controller
    {
        RolesContext dbRoles = new RolesContext();
        //
        // GET: /Roles/
        public ActionResult Index()
        {
            return View(dbRoles.GetAllRoles.ToList());
        }    

        //
        // GET: /Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Roles/Create
        [HttpPost]
        public ActionResult Create(Roles data)
        {
            TryUpdateModel(data);
            if (ModelState.IsValid)
            {
                dbRoles.Insert(data);
                return RedirectToAction("Index");
            }
            return View();
        }

        //
        // GET: /Roles/Edit/5
        public ActionResult Edit(int id)
        {
            return View(dbRoles.GetAllRoles.FirstOrDefault(r => r.ID == id));
        }

        //
        // POST: /Roles/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Roles data)
        {
            TryUpdateModel(data);
            if (ModelState.IsValid)
            {
                dbRoles.Update(data, id);
                return RedirectToAction("Index");
            }
            return View();
        }   
    }
}
