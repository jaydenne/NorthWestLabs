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
    public class AssayOrdersController : Controller
    {
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: AssayOrders
        public ActionResult Index()
        {
            var assayOrders = db.AssayOrders.Include(a => a.PriorityLevel).Include(a => a.ProtocolNotebook).Include(a => a.WorkOrder);
            return View(assayOrders.ToList());
        }

        // GET: AssayOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssayOrder assayOrder = db.AssayOrders.Find(id);
            if (assayOrder == null)
            {
                return HttpNotFound();
            }
            return View(assayOrder);
        }

        // GET: AssayOrders/Create
        public ActionResult Create()
        {
            ViewBag.PriorityLevelID = new SelectList(db.PriorityLevels, "PriorityLevelID", "ModifiedBy");
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName");
            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy");
            return View();
        }

        // POST: AssayOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssayOrderID,WorkOrderID,PriorityLevelID,AssayID,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] AssayOrder assayOrder)
        {
            if (ModelState.IsValid)
            {
                db.AssayOrders.Add(assayOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PriorityLevelID = new SelectList(db.PriorityLevels, "PriorityLevelID", "ModifiedBy", assayOrder.PriorityLevelID);
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName", assayOrder.AssayID);
            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy", assayOrder.WorkOrderID);
            return View(assayOrder);
        }

        // GET: AssayOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssayOrder assayOrder = db.AssayOrders.Find(id);
            if (assayOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.PriorityLevelID = new SelectList(db.PriorityLevels, "PriorityLevelID", "ModifiedBy", assayOrder.PriorityLevelID);
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName", assayOrder.AssayID);
            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy", assayOrder.WorkOrderID);
            return View(assayOrder);
        }

        // POST: AssayOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssayOrderID,WorkOrderID,PriorityLevelID,AssayID,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] AssayOrder assayOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assayOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PriorityLevelID = new SelectList(db.PriorityLevels, "PriorityLevelID", "ModifiedBy", assayOrder.PriorityLevelID);
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName", assayOrder.AssayID);
            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy", assayOrder.WorkOrderID);
            return View(assayOrder);
        }

        // GET: AssayOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssayOrder assayOrder = db.AssayOrders.Find(id);
            if (assayOrder == null)
            {
                return HttpNotFound();
            }
            return View(assayOrder);
        }

        // POST: AssayOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssayOrder assayOrder = db.AssayOrders.Find(id);
            db.AssayOrders.Remove(assayOrder);
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
