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
    public class TestEquiptmentsController : Controller
    {
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: TestEquiptments
        public ActionResult Index()
        {
            var testEquiptments = db.TestEquiptments.Include(t => t.Equiptment).Include(t => t.Test);
            return View(testEquiptments.ToList());
        }

        // GET: TestEquiptments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestEquiptment testEquiptment = db.TestEquiptments.Find(id);
            if (testEquiptment == null)
            {
                return HttpNotFound();
            }
            return View(testEquiptment);
        }

        // GET: TestEquiptments/Create
        public ActionResult Create()
        {
            ViewBag.EquiptmentID = new SelectList(db.Equiptments, "EquiptmentID", "EquiptmentName");
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Name");
            return View();
        }

        // POST: TestEquiptments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestID,EquiptmentID,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] TestEquiptment testEquiptment)
        {
            if (ModelState.IsValid)
            {
                db.TestEquiptments.Add(testEquiptment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EquiptmentID = new SelectList(db.Equiptments, "EquiptmentID", "EquiptmentName", testEquiptment.EquiptmentID);
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Name", testEquiptment.TestID);
            return View(testEquiptment);
        }

        // GET: TestEquiptments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestEquiptment testEquiptment = db.TestEquiptments.Find(id);
            if (testEquiptment == null)
            {
                return HttpNotFound();
            }
            ViewBag.EquiptmentID = new SelectList(db.Equiptments, "EquiptmentID", "EquiptmentName", testEquiptment.EquiptmentID);
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Name", testEquiptment.TestID);
            return View(testEquiptment);
        }

        // POST: TestEquiptments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestID,EquiptmentID,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] TestEquiptment testEquiptment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testEquiptment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EquiptmentID = new SelectList(db.Equiptments, "EquiptmentID", "EquiptmentName", testEquiptment.EquiptmentID);
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Name", testEquiptment.TestID);
            return View(testEquiptment);
        }

        // GET: TestEquiptments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestEquiptment testEquiptment = db.TestEquiptments.Find(id);
            if (testEquiptment == null)
            {
                return HttpNotFound();
            }
            return View(testEquiptment);
        }

        // POST: TestEquiptments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestEquiptment testEquiptment = db.TestEquiptments.Find(id);
            db.TestEquiptments.Remove(testEquiptment);
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
