using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Archides.Models;

namespace Archides.Controllers
{
    public class ProjectController : Controller
    {
        private DBArchidesArchitetureEntities db = new DBArchidesArchitetureEntities();

        // GET: Project
        public ActionResult Gallery()
        {
            var projektis = db.Projektis.Include(p => p.Kategoria).Include(p => p.Qyteti).Include(p => p.Useri);
            return View(projektis.ToList());
        }


        //Admin
        public ActionResult IndexGallery()
        {
            var projektis = db.Projektis.Include(p => p.Kategoria).Include(p => p.Qyteti).Include(p => p.Useri);
            return View(projektis.ToList());
        }

        // GET: Project/Details/5   Admin 
        public ActionResult IndexDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projekti projekti = db.Projektis.Find(id);
            if (projekti == null)
            {
                return HttpNotFound();
            }
            return View(projekti);
        }


        //Client Form
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projekti projekti = db.Projektis.Find(id);
            if (projekti == null)
            {
                return HttpNotFound();
            }
            return View(projekti);
        }

        // GET: Project/Create
        public ActionResult Create()
        {
            ViewBag.KategoriaID = new SelectList(db.Kategorias, "KategoriaID", "Kategoria1");
            ViewBag.Lokacioni = new SelectList(db.Qytetis, "QytetiID", "Qyteti1");
            ViewBag.UserID = new SelectList(db.Useris, "UserID", "Emri");
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjektiID,Titulli,Lokacioni,Viti,Madhesia,KategoriaID,Statusi,Pershkrimi,UploadTime,UserID")] Projekti projekti)
        {
            if (ModelState.IsValid)
            {
                projekti.UploadTime = DateTime.Now;
                db.Projektis.Add(projekti);
                db.SaveChanges();
                return RedirectToAction("Create", "Media");
            }
            
            ViewBag.KategoriaID = new SelectList(db.Kategorias, "KategoriaID", "Kategoria1", projekti.KategoriaID);
            ViewBag.Lokacioni = new SelectList(db.Qytetis, "QytetiID", "Qyteti1", projekti.Lokacioni);
            ViewBag.UserID = new SelectList(db.Useris, "UserID", "Emri", projekti.UserID);
            return View(projekti);
        }

        // GET: Project/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projekti projekti = db.Projektis.Find(id);
            if (projekti == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriaID = new SelectList(db.Kategorias, "KategoriaID", "Kategoria1", projekti.KategoriaID);
            ViewBag.Lokacioni = new SelectList(db.Qytetis, "QytetiID", "Qyteti1", projekti.Lokacioni);
            ViewBag.UserID = new SelectList(db.Useris, "UserID", "Emri", projekti.UserID);
            return View(projekti);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjektiID,Titulli,Lokacioni,Viti,Madhesia,KategoriaID,Statusi,Pershkrimi,UploadTime,UserID")] Projekti projekti)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projekti).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Create", "Media");
            }
            ViewBag.KategoriaID = new SelectList(db.Kategorias, "KategoriaID", "Kategoria1", projekti.KategoriaID);
            ViewBag.Lokacioni = new SelectList(db.Qytetis, "QytetiID", "Qyteti1", projekti.Lokacioni);
            ViewBag.UserID = new SelectList(db.Useris, "UserID", "Emri", projekti.UserID);
            return View(projekti);
        }

        // GET: Project/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projekti projekti = db.Projektis.Find(id);
            if (projekti == null)
            {
                return HttpNotFound();
            }
            return View(projekti);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Projekti projekti = db.Projektis.Find(id);

            db.Media.RemoveRange(projekti.Media);
            db.SaveChanges();

            db.ProjektiUsers.RemoveRange(projekti.ProjektiUsers);
            db.SaveChanges();
            db.Projektis.Remove(projekti);
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
