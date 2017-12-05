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
    public class TestConditionsController : Controller
    {
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: TestConditions
        public ActionResult Index()
        {
            var testConditions = db.TestConditions.Include(t => t.Test).Include(t => t.Test1);
            return View(testConditions.ToList());
        }

        // GET: TestConditions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestCondition testCondition = db.TestConditions.Find(id);
            if (testCondition == null)
            {
                return HttpNotFound();
            }
            return View(testCondition);
        }

        // GET: TestConditions/Create
        public ActionResult Create()
        {
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Name");
            ViewBag.ConditionalOnID = new SelectList(db.Tests, "TestID", "Name");
            return View();
        }

        // POST: TestConditions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TestConditionsID,TestID,ConditionalOnID,Condition,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] TestCondition testCondition)
        {
            if (ModelState.IsValid)
            {
                db.TestConditions.Add(testCondition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Name", testCondition.TestID);
            ViewBag.ConditionalOnID = new SelectList(db.Tests, "TestID", "Name", testCondition.ConditionalOnID);
            return View(testCondition);
        }

        // GET: TestConditions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestCondition testCondition = db.TestConditions.Find(id);
            if (testCondition == null)
            {
                return HttpNotFound();
            }
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Name", testCondition.TestID);
            ViewBag.ConditionalOnID = new SelectList(db.Tests, "TestID", "Name", testCondition.ConditionalOnID);
            return View(testCondition);
        }

        // POST: TestConditions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TestConditionsID,TestID,ConditionalOnID,Condition,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] TestCondition testCondition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(testCondition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Name", testCondition.TestID);
            ViewBag.ConditionalOnID = new SelectList(db.Tests, "TestID", "Name", testCondition.ConditionalOnID);
            return View(testCondition);
        }

        // GET: TestConditions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TestCondition testCondition = db.TestConditions.Find(id);
            if (testCondition == null)
            {
                return HttpNotFound();
            }
            return View(testCondition);
        }

        // POST: TestConditions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TestCondition testCondition = db.TestConditions.Find(id);
            db.TestConditions.Remove(testCondition);
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
