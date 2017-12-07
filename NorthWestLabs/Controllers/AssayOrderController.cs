using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NorthWestLabs.DAL;
using NorthWestLabs.Models;

namespace NorthWestLabs.Controllers
{
    public class AssayOrderController : Controller
    {
        private NorthwestLabsContext db = new NorthwestLabsContext();

        // GET: AssayOrder
        public ActionResult Index()
        {
            IEnumerable<AssayOrder> AssayOrderList = db.AssayOrders.ToList();
            return View(AssayOrderList);
        }

        // GET: AssayOrder/Details/5
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

        // GET: AssayOrder/Create
        public ActionResult Create()
        {
            ViewBag.PriorityLevelID = new SelectList(db.PriorityLevels, "PriorityLevelID", "ModifiedBy");
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName");
            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy");
            return View();
        }

        // POST: AssayOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssayOrderID,WorkOrderID,PriorityLevelID,AssayID,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate,CompoundID")] AssayOrder assayOrder)
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

        // GET: AssayOrder/Edit/5
        public ActionResult Edit(int? id, int? WorkOrderID)
        {
            AssayOrderWithTestResults assayOrderWithTestResults = new AssayOrderWithTestResults();
            ViewBag.WorkOrderID = WorkOrderID;
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
            assayOrderWithTestResults.AssayOrder = assayOrder;
            IEnumerable<TestResult> TestResList = db.TestResults.ToList();
                    foreach (TestResult val in TestResList)
                    {
                        if (val.AssayOrderID == assayOrder.AssayOrderID)
                        {
                            assayOrderWithTestResults.testResults.Add(val);
                        }
                    }
            return View(assayOrderWithTestResults);
        }

        // POST: AssayOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssayOrderID,WorkOrderID,PriorityLevelID,AssayID,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate,CompoundID")] AssayOrder assayOrder, int? WorkOrderID)
        {

                       
                AssayOrderWithTestResults assayOrderWithTestResults = new AssayOrderWithTestResults();
            if (ModelState.IsValid)
            {
                db.Entry(assayOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details","WorkOrders", new { id=assayOrder.WorkOrderID});
            }
            ViewBag.PriorityLevelID = new SelectList(db.PriorityLevels, "PriorityLevelID", "ModifiedBy", assayOrder.PriorityLevelID);
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName", assayOrder.AssayID);
            assayOrderWithTestResults.AssayOrder = assayOrder;
            return View(assayOrderWithTestResults);
        }

        // GET: AssayOrder/Delete/5
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

        // POST: AssayOrder/Delete/5
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
