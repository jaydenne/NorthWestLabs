using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NorthWestLabs.Models;

namespace NorthWestLabs.ControllerReference
{
    public class EquiptmentsController : Controller
    {
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: Equiptments
        public ActionResult Index()
        {
            return View(db.Equiptments.ToList());
        }

        // GET: Equiptments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equiptment equiptment = db.Equiptments.Find(id);
            if (equiptment == null)
            {
                return HttpNotFound();
            }
            return View(equiptment);
        }

        // GET: Equiptments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Equiptments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EquiptmentID,EquiptmentName,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] Equiptment equiptment)
        {
            if (ModelState.IsValid)
            {
                db.Equiptments.Add(equiptment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(equiptment);
        }

        // GET: Equiptments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equiptment equiptment = db.Equiptments.Find(id);
            if (equiptment == null)
            {
                return HttpNotFound();
            }
            return View(equiptment);
        }

        // POST: Equiptments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EquiptmentID,EquiptmentName,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] Equiptment equiptment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(equiptment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(equiptment);
        }

        // GET: Equiptments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Equiptment equiptment = db.Equiptments.Find(id);
            if (equiptment == null)
            {
                return HttpNotFound();
            }
            return View(equiptment);
        }

        // POST: Equiptments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Equiptment equiptment = db.Equiptments.Find(id);
            db.Equiptments.Remove(equiptment);
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
