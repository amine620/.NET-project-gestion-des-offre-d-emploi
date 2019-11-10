using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using wafabank.Models;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Net;
using System.Net.Mail;

namespace wafabank.Controllers
{

    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {

            var p = db.Secteurs.ToList();

            return View(p);
        }
       
        public ActionResult Details(int id)
        {
            offre medcine = db.jobs.Include(c => c.Secteur).SingleOrDefault(i => i.Id == id);
            if (medcine == null)
            {
                return HttpNotFound();
            }
            return View(medcine);
        }
        public ActionResult information(int id)
        {
            offre medcine = db.jobs.Include(c => c.Secteur).SingleOrDefault(i => i.Id == id);
            if (medcine == null)
            {
                return HttpNotFound();
            }
            return View(medcine);
        }
        public ActionResult Delete(int? id)
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
                return RedirectToAction("listDemand");
            }
        }

        public ActionResult Apply()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Apply(string Nom, string Prenom, string Message, string CIN, string Adresse,string Email, string tele, int id, HttpPostedFileBase cv)
        {
            Session[" choixeId"] = id;
            var UserId = User.Identity.GetUserId();
            var MedId = (int)(Session[" choixeId"]);
            var check = db.Applies.Where(a => a.jobId == MedId && a.UserId == UserId).ToList();
           

            try
            {
                if(ModelState.IsValid)
                {
                    string path = Path.Combine(Server.MapPath("~/upload"), cv.FileName);
                    cv.SaveAs(path);

                    if (check.Count < 1)
                {
                        
                    var medcine = new Demande();
                    medcine.cv = cv.FileName;
                    medcine.jobId = MedId;
                    medcine.UserId = UserId;
                    medcine.Nom = Nom;
                    medcine.Message = Message;
                    medcine.Prenom = Prenom;
                    medcine.Adresse = Adresse;
                    medcine.CIN = CIN;
                    medcine.Email = Email;
                    medcine.Tele = tele;
                    medcine.Date = DateTime.Now;

                    db.Applies.Add(medcine);
                    db.SaveChanges();
                    ViewBag.Result = "Demande Effectué";
                }
                else
                {
                    ViewBag.Result = "Desolé tu est deja Demandé";

                }

            }
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting  
                        // the current instance as InnerException  
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }

            return View();
        }
       
        public ActionResult Search()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Search(string searchname)
        {
            var result = db.jobs.Where(a => a.poste.Nom.Contains(searchname)).ToList();
            return View(result);
        }
        public ActionResult photo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhotoProfil job = db.Profils.Find(id);
            if (job == null)
            {
                return HttpNotFound();
            }
            return View(job);
        }
       
        public ActionResult listDemand()
        {
            var UserId = User.Identity.GetUserId();
            var Medcine = db.Applies.Where(a => a.UserId == UserId);
            return View(Medcine.ToList());
        }
        [HttpGet]
        public ActionResult Contact()
        {

            return View();
        }
        [HttpPost]
        [Authorize]

        public ActionResult Contact(contact contact)
        {
           
                var Mail = new MailMessage();
                var LoginInfo = new NetworkCredential("mohafidi1998@gmail.com", "Amin.Morid..");
                Mail.From = new MailAddress(contact.Email);
                Mail.To.Add(new MailAddress("mohafidi1998@gmail.com"));
                Mail.Subject = contact.subject;
                Mail.IsBodyHtml = true;
                string body = "Nom : " + contact.Name + "<br>" +
                                "Email : " + contact.Email + "<br>" +
                                "Le Titre : " + contact.subject + "<br>" +
                                "Message : " + contact.Message + "</br>";
                Mail.Body = body;
                var smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.EnableSsl = true;
                smtp.Credentials = LoginInfo;
            try { 
                smtp.Send(Mail);
                return RedirectToAction("Index");
            }catch(Exception ex)
            {
                return RedirectToAction("error");

            }
        }
        public ActionResult error()
        {

            return View();
        }
    }
}