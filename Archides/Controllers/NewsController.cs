using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Archides.Models;
using System.IO;

namespace Archides.Controllers
{
    public class NewsController : Controller
    {
        private DBArchidesArchitetureEntities db = new DBArchidesArchitetureEntities();

        // GET: News Client
        public ActionResult Index()
        {
            var lajmis = db.Lajmis.Include(l => l.Useri);
            return View(lajmis.ToList());
        }


        //News Admin
        public ActionResult IndexNews()
        {
            var lajmis = db.Lajmis.Include(l => l.Useri);
            return View(lajmis.ToList());
        }

        // GET: News/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lajmi lajmi = db.Lajmis.Find(id);
            if (lajmi == null)
            {
                return HttpNotFound();
            }
            return View(lajmi);
        }

        // GET: News/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Useris, "UserID", "Emri");
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LajmiID,FotoPath,VideoPath,UploadTime,Titulli,Pershkrimi,UserID")] Lajmi lajmi, HttpPostedFileBase NewsImage)
        {
            if (ModelState.IsValid)
            {
                if(NewsImage !=null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(NewsImage.FileName);
                    string extension = Path.GetExtension(NewsImage.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    lajmi.FotoPath = "~/images/NewsImages/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/images/NewsImages"), fileName);
                    NewsImage.SaveAs(fileName);
                }
                lajmi.UploadTime = DateTime.Now;
                db.Lajmis.Add(lajmi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.Useris, "UserID", "Emri", lajmi.UserID);
            return View(lajmi);
        }

        // GET: News/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lajmi lajmi = db.Lajmis.Find(id);
            if (lajmi == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Useris, "UserID", "Emri", lajmi.UserID);
            return View(lajmi);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LajmiID,FotoPath,VideoPath,UploadTime,Titulli,Pershkrimi,UserID")] Lajmi lajmi ,HttpPostedFileBase NewsImage)
        {
            if (ModelState.IsValid)
            {

                if (NewsImage != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(NewsImage.FileName);
                    string extension = Path.GetExtension(NewsImage.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    lajmi.FotoPath = "~/images/NewsImages/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/images/NewsImages/"), fileName);
                    NewsImage.SaveAs(fileName);
                }


                db.Entry(lajmi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Useris, "UserID", "Emri", lajmi.UserID);
            return View(lajmi);
        }

        // GET: News/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lajmi lajmi = db.Lajmis.Find(id);
            if (lajmi == null)
            {
                return HttpNotFound();
            }
            return View(lajmi);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lajmi lajmi = db.Lajmis.Find(id);
            db.Lajmis.Remove(lajmi);
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
