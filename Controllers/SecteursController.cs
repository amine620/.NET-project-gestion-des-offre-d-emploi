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
    public class SecteursController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NomPanleChoixes
        public ActionResult Index()
        {
            return View(db.Secteurs.ToList());
        }

        // GET: NomPanleChoixes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secteur Secteur = db.Secteurs.Find(id);
            if (Secteur == null)
            {
                return HttpNotFound();
            }
            return View(Secteur);
        }

        // GET: NomPanleChoixes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: NomPanleChoixes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nom")] Secteur nomPanleChoixe)
        {
            if (ModelState.IsValid)
            {
                db.Secteurs.Add(nomPanleChoixe);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nomPanleChoixe);
        }

        // GET: NomPanleChoixes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secteur nomPanleChoixe = db.Secteurs.Find(id);
            if (nomPanleChoixe == null)
            {
                return HttpNotFound();
            }
            return View(nomPanleChoixe);
        }

        // POST: NomPanleChoixes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nom")] Secteur nomPanleChoixe)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nomPanleChoixe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nomPanleChoixe);
        }

        // GET: NomPanleChoixes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Secteur nomPanleChoixe = db.Secteurs.Find(id);
            if (nomPanleChoixe == null)
            {
                return HttpNotFound();
            }
            return View(nomPanleChoixe);
        }

        // POST: NomPanleChoixes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Secteur nomPanleChoixe = db.Secteurs.Find(id);
            db.Secteurs.Remove(nomPanleChoixe);
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
