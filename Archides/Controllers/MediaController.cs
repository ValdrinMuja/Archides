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
    public class MediaController : Controller
    {
        private DBArchidesArchitetureEntities db = new DBArchidesArchitetureEntities();

        // GET: Media
        public ActionResult Index()
        {
            var media = db.Media.Include(m => m.LlojiArkitektura).Include(m => m.MediaType).Include(m => m.Projekti);
            return View(media.ToList());
        }

        // GET: Media/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medium medium = db.Media.Find(id);
            if (medium == null)
            {
                return HttpNotFound();
            }
            return View(medium);
        }

        // GET: Media/Create
        public ActionResult Create()
        {
            ViewBag.LlojiArkitekturaID = new SelectList(db.LlojiArkitekturas, "LlojiArkitekturaID", "LlojiArkitektura1");
            ViewBag.MediaTypeID = new SelectList(db.MediaTypes, "MediaTypeID", "MediaType1");
            ViewBag.ProjektiID = new SelectList(db.Projektis, "ProjektiID", "Titulli");
            return View();
        }
        
        // POST: Media/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MediaID,ProjektiID,MediaTypeID,LlojiArkitekturaID,MediaPath")] Medium medium, IEnumerable<HttpPostedFileBase> ProjectImages)
        {
            if (ModelState.IsValid)
            {
                foreach (var ProjectImage in ProjectImages)
                {
                    if (ProjectImage != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(ProjectImage.FileName);
                        string extension = Path.GetExtension(ProjectImage.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        medium.MediaPath = "~/images/ProjectImages/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/images/ProjectImages/"), fileName);
                        ProjectImage.SaveAs(fileName);
                    }
                    db.Media.Add(medium);
                    db.SaveChanges();
                }
                return RedirectToAction("IndexGallery","Project");
            }

            ViewBag.LlojiArkitekturaID = new SelectList(db.LlojiArkitekturas, "LlojiArkitekturaID", "LlojiArkitektura1", medium.LlojiArkitekturaID);
            ViewBag.MediaTypeID = new SelectList(db.MediaTypes, "MediaTypeID", "MediaType1", medium.MediaTypeID);
            ViewBag.ProjektiID = new SelectList(db.Projektis, "ProjektiID", "Titulli", medium.ProjektiID);
            return View(medium);
        }

        // GET: Media/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medium medium = db.Media.Find(id);
            if (medium == null)
            {
                return HttpNotFound();
            }
            ViewBag.LlojiArkitekturaID = new SelectList(db.LlojiArkitekturas, "LlojiArkitekturaID", "LlojiArkitektura1", medium.LlojiArkitekturaID);
            ViewBag.MediaTypeID = new SelectList(db.MediaTypes, "MediaTypeID", "MediaType1", medium.MediaTypeID);
            ViewBag.ProjektiID = new SelectList(db.Projektis, "ProjektiID", "Titulli", medium.ProjektiID);
            return View(medium);
        }

        // POST: Media/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MediaID,ProjektiID,MediaTypeID,LlojiArkitekturaID,MediaPath")] Medium medium)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medium).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.LlojiArkitekturaID = new SelectList(db.LlojiArkitekturas, "LlojiArkitekturaID", "LlojiArkitektura1", medium.LlojiArkitekturaID);
            ViewBag.MediaTypeID = new SelectList(db.MediaTypes, "MediaTypeID", "MediaType1", medium.MediaTypeID);
            ViewBag.ProjektiID = new SelectList(db.Projektis, "ProjektiID", "Titulli", medium.ProjektiID);
            return View(medium);
        }

        // GET: Media/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medium medium = db.Media.Find(id);
            if (medium == null)
            {
                return HttpNotFound();
            }
            return View(medium);
        }

        // POST: Media/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Medium medium = db.Media.Find(id);
            db.Media.Remove(medium);
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
