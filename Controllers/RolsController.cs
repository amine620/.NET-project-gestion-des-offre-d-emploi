using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wafabank.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace wafabank.Controllers
{
    //[Authorize(Roles = ("Administrateurs"))]

    public class RolsController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Rols
        public ActionResult Index()
        {
            return View(db.Roles.ToList());
        }

        // GET: Rols/Details/5
        public ActionResult Details(string id)
        {
            var role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: Rols/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rols/Create
        [HttpPost]
        public ActionResult Create(IdentityRole role)
        {

            if (ModelState.IsValid)
            {
                db.Roles.Add(role);
                db.SaveChanges();
                return RedirectToAction("Index");


            }

            else
            {
                return View(role);
            }
        }


        // GET: Rols/Edit/5
        public ActionResult Edit(string id)
        {
            var role = db.Roles.Find(id);
            if (ModelState.IsValid)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Rols/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,Name")] IdentityRole role)
        {
            if (ModelState.IsValid)
            {
                db.Entry(role).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: Rols/Delete/5
        public ActionResult Delete(string id)
        {
            var role = db.Roles.Find(id);
            if (ModelState.IsValid)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Rols/Delete/5
        [HttpPost]
        public ActionResult Delete(IdentityRole role)
        {

            // TODO: Add delete logic here
            var mrol = db.Roles.Find(role.Id);
            db.Roles.Remove(mrol);
            db.SaveChanges();
            return RedirectToAction("Index");


        }
    }
}
