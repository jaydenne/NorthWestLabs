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
    public class ConditionalApprovalsController : Controller
    {
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: ConditionalApprovals
        public ActionResult Index()
        {
            var conditionalApprovals = db.ConditionalApprovals.Include(c => c.Client).Include(c => c.ProtocolNotebook).Include(c => c.Test).Include(c => c.WorkOrder);
            return View(conditionalApprovals.ToList());
        }

        // GET: ConditionalApprovals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConditionalApproval conditionalApproval = db.ConditionalApprovals.Find(id);
            if (conditionalApproval == null)
            {
                return HttpNotFound();
            }
            return View(conditionalApproval);
        }

        // GET: ConditionalApprovals/Create
        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName");
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName");
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Name");
            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy");
            return View();
        }

        // POST: ConditionalApprovals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConditionalApprovalID,WorkOrderID,AssayID,TestID,ClientID,ApprovalDate,ApproverName,Comments")] ConditionalApproval conditionalApproval)
        {
            if (ModelState.IsValid)
            {
                db.ConditionalApprovals.Add(conditionalApproval);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName", conditionalApproval.ClientID);
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName", conditionalApproval.AssayID);
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Name", conditionalApproval.TestID);
            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy", conditionalApproval.WorkOrderID);
            return View(conditionalApproval);
        }

        // GET: ConditionalApprovals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConditionalApproval conditionalApproval = db.ConditionalApprovals.Find(id);
            if (conditionalApproval == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName", conditionalApproval.ClientID);
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName", conditionalApproval.AssayID);
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Name", conditionalApproval.TestID);
            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy", conditionalApproval.WorkOrderID);
            return View(conditionalApproval);
        }

        // POST: ConditionalApprovals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConditionalApprovalID,WorkOrderID,AssayID,TestID,ClientID,ApprovalDate,ApproverName,Comments")] ConditionalApproval conditionalApproval)
        {
            if (ModelState.IsValid)
            {
                db.Entry(conditionalApproval).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName", conditionalApproval.ClientID);
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName", conditionalApproval.AssayID);
            ViewBag.TestID = new SelectList(db.Tests, "TestID", "Name", conditionalApproval.TestID);
            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy", conditionalApproval.WorkOrderID);
            return View(conditionalApproval);
        }

        // GET: ConditionalApprovals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ConditionalApproval conditionalApproval = db.ConditionalApprovals.Find(id);
            if (conditionalApproval == null)
            {
                return HttpNotFound();
            }
            return View(conditionalApproval);
        }

        // POST: ConditionalApprovals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ConditionalApproval conditionalApproval = db.ConditionalApprovals.Find(id);
            db.ConditionalApprovals.Remove(conditionalApproval);
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
