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
    public class WorkOrdersController : Controller
    {
        private NorthwestLabsContext db = new NorthwestLabsContext();

        // GET: WorkOrders
        public ActionResult Index()
        {
            var workOrders = db.WorkOrders.Include(w => w.Client).Include(w => w.QuoteEstimate);
            return View(db.WorkOrders.ToList());
        }

        // GET: WorkOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrder workOrder = db.WorkOrders.Find(id);
            if (workOrder == null)
            {
                return HttpNotFound();
            }
            var workOrders = db.WorkOrders.Include(w => w.Client).Include(w => w.QuoteEstimate);
            
            WorkOrderWithDetails workOrderWithDetails = new WorkOrderWithDetails();
            workOrderWithDetails.workOrder = workOrder;
            IEnumerable<AssayOrder> AssayList = db.AssayOrders.ToList();
            IEnumerable<TestResult> TestResList = db.TestResults.ToList();

            foreach (AssayOrder item in AssayList)
            {
                if (item.WorkOrderID == id)
                {
                    AssayOrderWithTestResults assayOrder = new AssayOrderWithTestResults();
                    assayOrder.AssayOrder = item;

                    foreach (TestResult val in TestResList)
                    {
                        if (val.AssayOrderID == item.AssayOrderID)
                        {
                            assayOrder.testResults.Add(val);
                        }
                    }
                    workOrderWithDetails.assayOrderWithTestResultsList.Add(assayOrder);
                }
            }
            return View(workOrderWithDetails);

        }

        // GET: WorkOrders/Create
        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName");
            ViewBag.QuoteID = new SelectList(db.QuoteEstimates, "QuoteID", "ModifiedBy");
            return View();
        }

        // POST: WorkOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkOrderID,ClientID,QuoteID,DiscountRate,DateTime,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] WorkOrder workOrder)
        {
            if (ModelState.IsValid)
            {
                db.WorkOrders.Add(workOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName", workOrder.ClientID);
            ViewBag.QuoteID = new SelectList(db.QuoteEstimates, "QuoteID", "ModifiedBy", workOrder.QuoteID);
            return View(workOrder);
        }

        // GET: WorkOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrder workOrder = db.WorkOrders.Find(id);
            if (workOrder == null)
            {
                return HttpNotFound();
            }
            var workOrders = db.WorkOrders.Include(w => w.Client).Include(w => w.QuoteEstimate);

            WorkOrderWithDetails workOrderWithDetails = new WorkOrderWithDetails();
            workOrderWithDetails.workOrder = workOrder;
            IEnumerable<AssayOrder> AssayList = db.AssayOrders.ToList();
            IEnumerable<TestResult> TestResList = db.TestResults.ToList();

            foreach (AssayOrder item in AssayList)
            {
                if (item.WorkOrderID == id)
                {
                    AssayOrderWithTestResults assayOrder = new AssayOrderWithTestResults();
                    assayOrder.AssayOrder = item;

                    foreach (TestResult val in TestResList)
                    {
                        if (val.AssayOrderID == item.AssayOrderID)
                        {
                            assayOrder.testResults.Add(val);
                        }
                    }
                    workOrderWithDetails.assayOrderWithTestResultsList.Add(assayOrder);
                }
            }
                ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName", workOrder.ClientID);
                ViewBag.QuoteID = new SelectList(db.QuoteEstimates, "QuoteID", "ModifiedBy", workOrder.QuoteID);
            
                return View(workOrderWithDetails);
        }

        // POST: WorkOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkOrderID,ClientID,QuoteID,DiscountRate,DateTime,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] WorkOrder workOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName", workOrder.ClientID);
            ViewBag.QuoteID = new SelectList(db.QuoteEstimates, "QuoteID", "ModifiedBy", workOrder.QuoteID);
            return View(workOrder);
        }

        // GET: WorkOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrder workOrder = db.WorkOrders.Find(id);
            if (workOrder == null)
            {
                return HttpNotFound();
            }
            return View(workOrder);
        }

        // POST: WorkOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkOrder workOrder = db.WorkOrders.Find(id);
            db.WorkOrders.Remove(workOrder);
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
