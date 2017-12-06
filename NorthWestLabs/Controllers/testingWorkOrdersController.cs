using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NorthWestLabs.Models;

namespace NorthWestLabs.Controllers
{
    public class testingWorkOrdersController : Controller
    {
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: testingWorkOrders
        public ActionResult Index()
        {
            var workOrders = db.WorkOrders.Include(w => w.Client).Include(w => w.QuoteEstimate);
            return View(workOrders.ToList());
        }

        // GET: testingWorkOrders/Details/5
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
            return View(workOrder);
        }

        // GET: testingWorkOrders/Create
        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName");
            ViewBag.QuoteID = new SelectList(db.QuoteEstimates, "QuoteID", "ModifiedBy");
            return View();
        }

        // POST: testingWorkOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        class myClient
            {
            public int ClientID { get; set; }
            public string CompanyName { get; set; }
            public string ContactFirstName { get; set; }
            public string ContactLastName { get; set; }
            public string Address1 { get; set; }
            public string Address2 { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Country { get; set; }
            public Nullable<int> Zip { get; set; }
            public string Phone { get; set; }
            public string Email { get; set; }
            public string Username { get; set; }
            public string PasswordHash { get; set; }
            public string ModifiedBy { get; set; }
            public System.DateTime ModifiedDate { get; set; }
            public string CreatedBy { get; set; }
            public System.DateTime CreatedDate { get; set; }
        }

        public ActionResult Create([Bind(Include = "WorkOrderID,ClientID,QuoteID,DiscountRate,DateTime,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] WorkOrder workOrder)
        {
            myClient stuff = new myClient();
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

        // GET: testingWorkOrders/Edit/5
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
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName", workOrder.ClientID);
            ViewBag.QuoteID = new SelectList(db.QuoteEstimates, "QuoteID", "ModifiedBy", workOrder.QuoteID);
            return View(workOrder);
        }

        // POST: testingWorkOrders/Edit/5
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

        // GET: testingWorkOrders/Delete/5
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

        // POST: testingWorkOrders/Delete/5
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
