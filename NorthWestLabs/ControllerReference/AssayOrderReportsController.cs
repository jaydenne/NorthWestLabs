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
    public class AssayOrderReportsController : Controller
    {
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: AssayOrderReports
        public ActionResult Index()
        {
            var assayOrderReports = db.AssayOrderReports.Include(a => a.AssayOrder);
            return View(assayOrderReports.ToList());
        }

        // GET: AssayOrderReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssayOrderReport assayOrderReport = db.AssayOrderReports.Find(id);
            if (assayOrderReport == null)
            {
                return HttpNotFound();
            }
            return View(assayOrderReport);
        }

        // GET: AssayOrderReports/Create
        public ActionResult Create()
        {
            ViewBag.AssayOrderID = new SelectList(db.AssayOrders, "AssayOrderID", "ModifiedBy");
            return View();
        }

        // POST: AssayOrderReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssayOrderReportID,AssayOrderID,AttachedFile,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] AssayOrderReport assayOrderReport)
        {
            if (ModelState.IsValid)
            {
                db.AssayOrderReports.Add(assayOrderReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssayOrderID = new SelectList(db.AssayOrders, "AssayOrderID", "ModifiedBy", assayOrderReport.AssayOrderID);
            return View(assayOrderReport);
        }

        // GET: AssayOrderReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssayOrderReport assayOrderReport = db.AssayOrderReports.Find(id);
            if (assayOrderReport == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssayOrderID = new SelectList(db.AssayOrders, "AssayOrderID", "ModifiedBy", assayOrderReport.AssayOrderID);
            return View(assayOrderReport);
        }

        // POST: AssayOrderReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssayOrderReportID,AssayOrderID,AttachedFile,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] AssayOrderReport assayOrderReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assayOrderReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssayOrderID = new SelectList(db.AssayOrders, "AssayOrderID", "ModifiedBy", assayOrderReport.AssayOrderID);
            return View(assayOrderReport);
        }

        // GET: AssayOrderReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssayOrderReport assayOrderReport = db.AssayOrderReports.Find(id);
            if (assayOrderReport == null)
            {
                return HttpNotFound();
            }
            return View(assayOrderReport);
        }

        // POST: AssayOrderReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssayOrderReport assayOrderReport = db.AssayOrderReports.Find(id);
            db.AssayOrderReports.Remove(assayOrderReport);
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
