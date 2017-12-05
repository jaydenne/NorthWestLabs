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
    public class TestReportsController : Controller
    {
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: TestReports
        public ActionResult Index()
        {
            return View(db.TestReports.ToList());
        }

        // GET: TestReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestReport testReport = db.TestReports.Find(id);
            if (testReport == null)
            {
                return HttpNotFound();
            }
            return View(testReport);
        }

        // GET: TestReports/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestReportsID,TestResultsID,FileData,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] TestReport testReport)
        {
            if (ModelState.IsValid)
            {
                db.TestReports.Add(testReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(testReport);
        }

        // GET: TestReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestReport testReport = db.TestReports.Find(id);
            if (testReport == null)
            {
                return HttpNotFound();
            }
            return View(testReport);
        }

        // POST: TestReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestReportsID,TestResultsID,FileData,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] TestReport testReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(testReport);
        }

        // GET: TestReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestReport testReport = db.TestReports.Find(id);
            if (testReport == null)
            {
                return HttpNotFound();
            }
            return View(testReport);
        }

        // POST: TestReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestReport testReport = db.TestReports.Find(id);
            db.TestReports.Remove(testReport);
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
