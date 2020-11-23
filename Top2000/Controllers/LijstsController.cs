using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Top2000.Models;

namespace Top2000.Controllers
{
    public class LijstsController : Controller
    { 
        private Top2000Entities1 db = new Top2000Entities1();

        // GET: Lijsts
        public ActionResult Index(string search, string dropdown)
        {
            var lijst = db.Lijst.Include(l => l.Song).Include(l => l.Top2000Jaar1);
            return View(lijst.Where(x => x.Song.titel.StartsWith(search) || x.Song.Artiest.naam.StartsWith(search) || search == null).ToList());
        }

        // GET: Lijsts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lijst lijst = db.Lijst.Find(id);
            if (lijst == null)
            {
                return HttpNotFound();
            }
            return View(lijst);
        }

        // GET: Lijsts/Create
        public ActionResult Create()
        {
            ViewBag.songid = new SelectList(db.Song, "songid", "titel");
            ViewBag.top2000jaar = new SelectList(db.Top2000Jaar, "Jaar", "Titel");
            return View();
        }

        // POST: Lijsts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "songid,top2000jaar,positie")] Lijst lijst)
        {
            if (ModelState.IsValid)
            {
                db.Lijst.Add(lijst);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.songid = new SelectList(db.Song, "songid", "titel", lijst.songid);
            ViewBag.top2000jaar = new SelectList(db.Top2000Jaar, "Jaar", "Titel", lijst.top2000jaar);
            return View(lijst);
        }

        // GET: Lijsts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lijst lijst = db.Lijst.Find(id);
            if (lijst == null)
            {
                return HttpNotFound();
            }
            ViewBag.songid = new SelectList(db.Song, "songid", "titel", lijst.songid);
            ViewBag.top2000jaar = new SelectList(db.Top2000Jaar, "Jaar", "Titel", lijst.top2000jaar);
            return View(lijst);
        }

        // POST: Lijsts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "songid,top2000jaar,positie")] Lijst lijst)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lijst).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.songid = new SelectList(db.Song, "songid", "titel", lijst.songid);
            ViewBag.top2000jaar = new SelectList(db.Top2000Jaar, "Jaar", "Titel", lijst.top2000jaar);
            return View(lijst);
        }

        // GET: Lijsts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lijst lijst = db.Lijst.Find(id);
            if (lijst == null)
            {
                return HttpNotFound();
            }
            return View(lijst);
        }

        // POST: Lijsts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Lijst lijst = db.Lijst.Find(id);
            db.Lijst.Remove(lijst);
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
