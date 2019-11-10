using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using wafabank.Models;

namespace wafabank.Controllers
{
    [Authorize(Roles = "Admins")]

    public class jobsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: choixes
        public ActionResult Index()
        {
            var USerId = User.Identity.GetUserId();

            var choixes = db.jobs.Include(c => c.Secteur)
                .Include(c => c.poste)
                .Include(c => c.Niveau)
                .Include(c => c.ville);
            return View(choixes.ToList());
        }

        // GET: choixes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            offre choixe = db.jobs.Find(id);
            if (choixe == null)
            {
                return HttpNotFound();
            }
            return View(choixe);
        }

        // GET: choixes/Create
        public ActionResult Create()
        {
            ViewBag.SecteurId = new SelectList(db.Secteurs, "Id", "Nom");
            ViewBag.posteId = new SelectList(db.postes, "Id", "Nom");
            ViewBag.NiveauId = new SelectList(db.Niveaus, "Id", "Nom");
            ViewBag.villeId = new SelectList(db.villes, "Id", "Nom");

            return View();
        }

        // POST: choixes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(offre job, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {

                string path = Path.Combine(Server.MapPath("~/upload"), photo.FileName);
                photo.SaveAs(path);
                job.photo = photo.FileName;
                job.UserId = User.Identity.GetUserId();
                job.DatePost = DateTime.Now;
                db.jobs.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");

            }

            ViewBag.SecteurId = new SelectList(db.Secteurs, "Id", "Nom", job.SecteurId);
            ViewBag.posteId = new SelectList(db.postes, "Id", "Nom", job.posteId);
            ViewBag.NiveauId = new SelectList(db.Niveaus, "Id", "Nom", job.NiveauId);
            ViewBag.villeId = new SelectList(db.villes, "Id", "Nom", job.villeId);

            return View(job);
        }

        // GET: choixes/Edit/5
        public ActionResult Edit(int? id)
        {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                offre job = db.jobs.Find(id);
                if (job == null)
                {
                    return HttpNotFound();
                }
            ViewBag.SecteurId = new SelectList(db.Secteurs, "Id", "Nom");
            ViewBag.posteId = new SelectList(db.postes, "Id", "Nom");
            ViewBag.NiveauId = new SelectList(db.Niveaus, "Id", "Nom");
            ViewBag.villeId = new SelectList(db.villes, "Id", "Nom");
            return View(job);
        }

        // POST: choixes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(offre job, HttpPostedFileBase photo)
        {
            if (ModelState.IsValid)
            {
                string oldpath = job.photo;

                if (photo != null)
                {
                    System.IO.File.Delete(oldpath);
                    string path = Path.Combine(Server.MapPath("~/upload"), photo.FileName);
                    photo.SaveAs(path);
                    job.photo = photo.FileName;
                }

                job.DatePost = DateTime.Now;
                db.Entry(job).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SecteurId = new SelectList(db.Secteurs, "Id", "Nom", job.SecteurId);
            ViewBag.posteId = new SelectList(db.postes, "Id", "Nom", job.posteId);
            ViewBag.SecteurId = new SelectList(db.Niveaus, "Id", "Nom", job.NiveauId);
            ViewBag.SecteurId = new SelectList(db.villes, "Id", "Nom", job.villeId);

            return View(job);
        }

        // GET: choixes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            offre choixe = db.jobs.Find(id);
            if (choixe == null)
            {
                return HttpNotFound();
            }
            return View(choixe);
        }

        // POST: choixes/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            offre choixe = db.jobs.Find(id);
            db.jobs.Remove(choixe);
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
        public ActionResult DeleteApply(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demande medcine = db.Applies.Find(id);
            if (medcine == null)
            {
                return HttpNotFound();
            }
            else
            {

                db.Applies.Remove(medcine);
                db.SaveChanges();
                return RedirectToAction("Les_Demande_Apply");
            }
        }
        public ActionResult Les_Demande_Apply()
        {
            var USerId = User.Identity.GetUserId();
            var jb = from app in db.Applies
                     join job in db.jobs
                     on app.jobId equals job.Id
                     where job.User.Id == USerId
                     select app;
            return View(jb.ToList());
        }
        public ActionResult SendConfirmation(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Demande job = db.Applies.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }

            return View(job);
        }
        [HttpPost]
        [Authorize]

        public ActionResult SendConfirmation(Demande contact,int? id)
        {



            MailMessage ms = new MailMessage("AttijariWafajob@mail.com", contact.Email);
            ms.Subject ="accepte";
            string body = "Le Titre : " + "Demande d'emploi" + "<br>" +
                            "Message : " + "Cher candidat, Votre demande a été  recu  Nous vous appellerons à travers 24 heur Pour fournir plus d'informations" + " </br>";
            ms.Body = body;
            ms.IsBodyHtml = false;
            SmtpClient sm = new SmtpClient();
            sm.Host = "smtp.gmail.com";
            sm.Port = 587;
            sm.EnableSsl = true;
            NetworkCredential nt = new NetworkCredential("mohafidi1998@gmail.com", "Amin.Morid..");
            sm.UseDefaultCredentials = true;
            sm.Credentials = nt;
            try
            {
                sm.Send(ms);
                return RedirectToAction("Les_Demande_Apply");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error");

            }


        }

        public ActionResult Error()
        {
            return View();
        }
    }
}
