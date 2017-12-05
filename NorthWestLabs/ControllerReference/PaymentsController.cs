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
    public class PaymentsController : Controller
    {
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: Payments
        public ActionResult Index()
        {
            var payments = db.Payments.Include(p => p.Client).Include(p => p.Invoice).Include(p => p.PaymentData);
            return View(payments.ToList());
        }

        // GET: Payments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // GET: Payments/Create
        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName");
            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "ModifiedBy");
            ViewBag.PaymentDataID = new SelectList(db.PaymentDatas, "PaymentDataID", "BillingName");
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentID,InvoiceID,ClientID,PaymentDataID,CCAmount,CheckAmount,BankTransferAmount,PaymentDate,RecordedBy,ModifiedBy,ModifiedDate")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName", payment.ClientID);
            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "ModifiedBy", payment.InvoiceID);
            ViewBag.PaymentDataID = new SelectList(db.PaymentDatas, "PaymentDataID", "BillingName", payment.PaymentDataID);
            return View(payment);
        }

        // GET: Payments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName", payment.ClientID);
            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "ModifiedBy", payment.InvoiceID);
            ViewBag.PaymentDataID = new SelectList(db.PaymentDatas, "PaymentDataID", "BillingName", payment.PaymentDataID);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentID,InvoiceID,ClientID,PaymentDataID,CCAmount,CheckAmount,BankTransferAmount,PaymentDate,RecordedBy,ModifiedBy,ModifiedDate")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName", payment.ClientID);
            ViewBag.InvoiceID = new SelectList(db.Invoices, "InvoiceID", "ModifiedBy", payment.InvoiceID);
            ViewBag.PaymentDataID = new SelectList(db.PaymentDatas, "PaymentDataID", "BillingName", payment.PaymentDataID);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Payment payment = db.Payments.Find(id);
            if (payment == null)
            {
                return HttpNotFound();
            }
            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = db.Payments.Find(id);
            db.Payments.Remove(payment);
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
