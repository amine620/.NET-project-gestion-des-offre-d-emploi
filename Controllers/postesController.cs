using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using wafabank.Models;

namespace wafabank.Controllers
{
    public class postesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: postes
        public ActionResult Index()
        {
            return View(db.postes.ToList());
        }

        // GET: postes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            poste poste = db.postes.Find(id);
            if (poste == null)
            {
                return HttpNotFound();
            }
            return View(poste);
        }

        // GET: postes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: postes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom")] poste poste)
        {
            if (ModelState.IsValid)
            {
                db.postes.Add(poste);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(poste);
        }

        // GET: postes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            poste poste = db.postes.Find(id);
            if (poste == null)
            {
                return HttpNotFound();
            }
            return View(poste);
        }

        // POST: postes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom")] poste poste)
        {
            if (ModelState.IsValid)
            {
                db.Entry(poste).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(poste);
        }

        // GET: postes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            poste poste = db.postes.Find(id);
            if (poste == null)
            {
                return HttpNotFound();
            }
            return View(poste);
        }

        // POST: postes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            poste poste = db.postes.Find(id);
            db.postes.Remove(poste);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
