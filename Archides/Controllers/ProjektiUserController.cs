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
    public class ProjektiUserController : Controller
    {
        private DBArchidesArchitetureEntities db = new DBArchidesArchitetureEntities();

        // GET: ProjektiUser
        public ActionResult Index()
        {
            var projektiUsers = db.ProjektiUsers.Include(p => p.Projekti).Include(p => p.Useri);
            return View(projektiUsers.ToList());
        }

        // GET: ProjektiUser/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjektiUser projektiUser = db.ProjektiUsers.Find(id);
            if (projektiUser == null)
            {
                return HttpNotFound();
            }
            return View(projektiUser);
        }

        // GET: ProjektiUser/Create
        public ActionResult Create()
        {
            ViewBag.ProjektiID = new SelectList(db.Projektis, "ProjektiID", "Titulli");
            ViewBag.UserID = new SelectList(db.Useris, "UserID", "Emri");
            return View();
        }

        // POST: ProjektiUser/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjektiUserID,ProjektiID,UserID")] ProjektiUser projektiUser)
        {
            if (ModelState.IsValid)
            {
                db.ProjektiUsers.Add(projektiUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjektiID = new SelectList(db.Projektis, "ProjektiID", "Titulli", projektiUser.ProjektiID);
            ViewBag.UserID = new SelectList(db.Useris, "UserID", "Emri", projektiUser.UserID);
            return View(projektiUser);
        }

        // GET: ProjektiUser/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjektiUser projektiUser = db.ProjektiUsers.Find(id);
            if (projektiUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjektiID = new SelectList(db.Projektis, "ProjektiID", "Titulli", projektiUser.ProjektiID);
            ViewBag.UserID = new SelectList(db.Useris, "UserID", "Emri", projektiUser.UserID);
            return View(projektiUser);
        }

        // POST: ProjektiUser/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjektiUserID,ProjektiID,UserID")] ProjektiUser projektiUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projektiUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjektiID = new SelectList(db.Projektis, "ProjektiID", "Titulli", projektiUser.ProjektiID);
            ViewBag.UserID = new SelectList(db.Useris, "UserID", "Emri", projektiUser.UserID);
            return View(projektiUser);
        }

        // GET: ProjektiUser/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjektiUser projektiUser = db.ProjektiUsers.Find(id);
            if (projektiUser == null)
            {
                return HttpNotFound();
            }
            return View(projektiUser);
        }

        // POST: ProjektiUser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjektiUser projektiUser = db.ProjektiUsers.Find(id);
            db.ProjektiUsers.Remove(projektiUser);
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
