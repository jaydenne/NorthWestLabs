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
    public class InvioceItemsController : Controller
    {
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: InvioceItems
        public ActionResult Index()
        {
            var invioceItems = db.InvioceItems.Include(i => i.Invoice).Include(i => i.ProtocolNotebook).Include(i => i.WorkOrder);
            return View(invioceItems.ToList());
        }

        // GET: InvioceItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvioceItem invioceItem = db.InvioceItems.Find(id);
            if (invioceItem == null)
            {
                return HttpNotFound();
            }
            return View(invioceItem);
        }

        // GET: InvioceItems/Create
        public ActionResult Create()
        {
            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "ModifiedBy");
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName");
            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy");
            return View();
        }

        // POST: InvioceItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "InvoiceItemID,InvoiceID,AssayID,WorkOrderID,Amount,Modified,ModifiedDate,CreatedBy,CreatedDate")] InvioceItem invioceItem)
        {
            if (ModelState.IsValid)
            {
                db.InvioceItems.Add(invioceItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "ModifiedBy", invioceItem.InvoiceID);
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName", invioceItem.AssayID);
            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy", invioceItem.WorkOrderID);
            return View(invioceItem);
        }

        // GET: InvioceItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvioceItem invioceItem = db.InvioceItems.Find(id);
            if (invioceItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "ModifiedBy", invioceItem.InvoiceID);
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName", invioceItem.AssayID);
            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy", invioceItem.WorkOrderID);
            return View(invioceItem);
        }

        // POST: InvioceItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "InvoiceItemID,InvoiceID,AssayID,WorkOrderID,Amount,Modified,ModifiedDate,CreatedBy,CreatedDate")] InvioceItem invioceItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(invioceItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "ModifiedBy", invioceItem.InvoiceID);
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName", invioceItem.AssayID);
            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy", invioceItem.WorkOrderID);
            return View(invioceItem);
        }

        // GET: InvioceItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            InvioceItem invioceItem = db.InvioceItems.Find(id);
            if (invioceItem == null)
            {
                return HttpNotFound();
            }
            return View(invioceItem);
        }

        // POST: InvioceItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            InvioceItem invioceItem = db.InvioceItems.Find(id);
            db.InvioceItems.Remove(invioceItem);
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
