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
    public class TestResultCommentsController : Controller
    {
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: TestResultComments
        public ActionResult Index()
        {
            var testResultComments = db.TestResultComments.Include(t => t.TestResult);
            return View(testResultComments.ToList());
        }

        // GET: TestResultComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResultComment testResultComment = db.TestResultComments.Find(id);
            if (testResultComment == null)
            {
                return HttpNotFound();
            }
            return View(testResultComment);
        }

        // GET: TestResultComments/Create
        public ActionResult Create()
        {
            ViewBag.TestResultsID = new SelectList(db.TestResults, "TestResultsID", "ModifedBy");
            return View();
        }

        // POST: TestResultComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestResultCommentsID,TestResultsID,Title,Comment,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] TestResultComment testResultComment)
        {
            if (ModelState.IsValid)
            {
                db.TestResultComments.Add(testResultComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TestResultsID = new SelectList(db.TestResults, "TestResultsID", "ModifedBy", testResultComment.TestResultsID);
            return View(testResultComment);
        }

        // GET: TestResultComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResultComment testResultComment = db.TestResultComments.Find(id);
            if (testResultComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.TestResultsID = new SelectList(db.TestResults, "TestResultsID", "ModifedBy", testResultComment.TestResultsID);
            return View(testResultComment);
        }

        // POST: TestResultComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestResultCommentsID,TestResultsID,Title,Comment,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] TestResultComment testResultComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testResultComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TestResultsID = new SelectList(db.TestResults, "TestResultsID", "ModifedBy", testResultComment.TestResultsID);
            return View(testResultComment);
        }

        // GET: TestResultComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestResultComment testResultComment = db.TestResultComments.Find(id);
            if (testResultComment == null)
            {
                return HttpNotFound();
            }
            return View(testResultComment);
        }

        // POST: TestResultComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestResultComment testResultComment = db.TestResultComments.Find(id);
            db.TestResultComments.Remove(testResultComment);
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
