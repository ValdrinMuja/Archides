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
    public class TeamController : Controller
    {
        private DBArchidesArchitetureEntities db = new DBArchidesArchitetureEntities();

        // GET: Team
        public ActionResult OurTeam()
        {
            var useris = db.Useris.Include(u => u.Qyteti).Include(u => u.Roli);
            return View(useris.ToList());
        }

        //iadminit
        public ActionResult IndexTeam()
        {
            var useris = db.Useris.Include(u => u.Qyteti).Include(u => u.Roli);
            return View(useris.ToList());
        }

        // GET: Team/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Useri useri = db.Useris.Find(id);
            if (useri == null)
            {
                return HttpNotFound();
            }
            return View(useri);
        }

        // GET: Team/Create
        public ActionResult Create()
        {
            ViewBag.Vendlindja = new SelectList(db.Qytetis, "QytetiID", "Qyteti1");
            ViewBag.RoliID = new SelectList(db.Rolis, "RoliID", "Roli1");
            return View();
        }

        // POST: Team/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Emri,Mbiemri,Gjinia,Vendlindja,Datelindja,Email,Telefoni,Username,Password,Pershkrimi,Foto,RoliID")] Useri useri, HttpPostedFileBase UserImage)
        {
            if (ModelState.IsValid)
            {

                    if (UserImage != null)
                    {
                        string fileName = Path.GetFileNameWithoutExtension(UserImage.FileName);
                        string extension = Path.GetExtension(UserImage.FileName);
                        fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                        useri.Foto = "~/images/UserImages/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/images/UserImages/"), fileName);
                        UserImage.SaveAs(fileName);
                    }

                db.Useris.Add(useri);
                db.SaveChanges();
                return RedirectToAction("Team");
            }

            ViewBag.Vendlindja = new SelectList(db.Qytetis, "QytetiID", "Qyteti1", useri.Vendlindja);
            ViewBag.RoliID = new SelectList(db.Rolis, "RoliID", "Roli1", useri.RoliID);
            return View(useri);
        }

        // GET: Team/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Useri useri = db.Useris.Find(id);
            if (useri == null)
            {
                return HttpNotFound();
            }
            ViewBag.Vendlindja = new SelectList(db.Qytetis, "QytetiID", "Qyteti1", useri.Vendlindja);
            ViewBag.RoliID = new SelectList(db.Rolis, "RoliID", "Roli1", useri.RoliID);
            return View(useri);
        }

        // POST: Team/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Emri,Mbiemri,Gjinia,Vendlindja,Datelindja,Email,Telefoni,Username,Password,Pershkrimi,Foto,RoliID")] Useri useri, HttpPostedFileBase UserImage)
        {
            if (ModelState.IsValid)
            {

                if (UserImage != null)
                {
                    string fileName = Path.GetFileNameWithoutExtension(UserImage.FileName);
                    string extension = Path.GetExtension(UserImage.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                    useri.Foto = "~/images/UserImages/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/images/UserImages/"), fileName);
                    UserImage.SaveAs(fileName);
                }

                db.Entry(useri).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("IndexTeam");
            }
            ViewBag.Vendlindja = new SelectList(db.Qytetis, "QytetiID", "Qyteti1", useri.Vendlindja);
            ViewBag.RoliID = new SelectList(db.Rolis, "RoliID", "Roli1", useri.RoliID);
            return View(useri);
        }

        // GET: Team/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Useri useri = db.Useris.Find(id);
            if (useri == null)
            {
                return HttpNotFound();
            }
            return View(useri);
        }

        // POST: Team/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Useri useri = db.Useris.Find(id);
            db.Useris.Remove(useri);
            db.SaveChanges();
            return RedirectToAction("IndexTeam");
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
