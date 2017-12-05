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
    public class TestResultsController : Controller
    {
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: TestResults
        public ActionResult Index()
        {
            var testResults = db.TestResults.Include(t => t.AssayOrder).Include(t => t.Status).Include(t => t.Test).Include(t => t.TestTube);
            return View(testResults.ToList());
        }

        // GET: TestResults/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResult testResult = db.TestResults.Find(id);
            if (testResult == null)
            {
                return HttpNotFound();
            }
            return View(testResult);
        }

        // GET: TestResults/Create
        public ActionResult Create()
        {
            ViewBag.AssayOrderID = new SelectList(db.AssayOrders, "AssayOrderID", "ModifiedBy");
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "StatusName");
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Name");
            ViewBag.TestTubeID = new SelectList(db.TestTubes, "TestTubeID", "Concentration");
            return View();
        }

        // POST: TestResults/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestResultsID,TestID,AssayOrderID,TestTubeID,LTNumber,ScheduleStart,TextFile,NumericFile,StatusID,StatusUpdatedDate,RunAgain,RunNumber,ModifedBy,ModifiedDate,CreatedBy,CreatedDate")] TestResult testResult)
        {
            if (ModelState.IsValid)
            {
                db.TestResults.Add(testResult);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssayOrderID = new SelectList(db.AssayOrders, "AssayOrderID", "ModifiedBy", testResult.AssayOrderID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "StatusName", testResult.StatusID);
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Name", testResult.TestID);
            ViewBag.TestTubeID = new SelectList(db.TestTubes, "TestTubeID", "Concentration", testResult.TestTubeID);
            return View(testResult);
        }

        // GET: TestResults/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResult testResult = db.TestResults.Find(id);
            if (testResult == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssayOrderID = new SelectList(db.AssayOrders, "AssayOrderID", "ModifiedBy", testResult.AssayOrderID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "StatusName", testResult.StatusID);
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Name", testResult.TestID);
            ViewBag.TestTubeID = new SelectList(db.TestTubes, "TestTubeID", "Concentration", testResult.TestTubeID);
            return View(testResult);
        }

        // POST: TestResults/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestResultsID,TestID,AssayOrderID,TestTubeID,LTNumber,ScheduleStart,TextFile,NumericFile,StatusID,StatusUpdatedDate,RunAgain,RunNumber,ModifedBy,ModifiedDate,CreatedBy,CreatedDate")] TestResult testResult)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testResult).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssayOrderID = new SelectList(db.AssayOrders, "AssayOrderID", "ModifiedBy", testResult.AssayOrderID);
            ViewBag.StatusID = new SelectList(db.Status, "StatusID", "StatusName", testResult.StatusID);
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Name", testResult.TestID);
            ViewBag.TestTubeID = new SelectList(db.TestTubes, "TestTubeID", "Concentration", testResult.TestTubeID);
            return View(testResult);
        }

        // GET: TestResults/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResult testResult = db.TestResults.Find(id);
            if (testResult == null)
            {
                return HttpNotFound();
            }
            return View(testResult);
        }

        // POST: TestResults/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestResult testResult = db.TestResults.Find(id);
            db.TestResults.Remove(testResult);
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
