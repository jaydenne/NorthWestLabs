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
    public class PaymentDatasController : Controller
    {
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: PaymentDatas
        public ActionResult Index()
        {
            var paymentDatas = db.PaymentDatas.Include(p => p.Client);
            return View(paymentDatas.ToList());
        }

        // GET: PaymentDatas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentData paymentData = db.PaymentDatas.Find(id);
            if (paymentData == null)
            {
                return HttpNotFound();
            }
            return View(paymentData);
        }

        // GET: PaymentDatas/Create
        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName");
            return View();
        }

        // POST: PaymentDatas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PaymentDataID,ClientID,BillingName,Address1,Address2,City,State,CC_4Digits,ExpDate,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] PaymentData paymentData)
        {
            if (ModelState.IsValid)
            {
                db.PaymentDatas.Add(paymentData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName", paymentData.ClientID);
            return View(paymentData);
        }

        // GET: PaymentDatas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentData paymentData = db.PaymentDatas.Find(id);
            if (paymentData == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName", paymentData.ClientID);
            return View(paymentData);
        }

        // POST: PaymentDatas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentDataID,ClientID,BillingName,Address1,Address2,City,State,CC_4Digits,ExpDate,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] PaymentData paymentData)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentData).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName", paymentData.ClientID);
            return View(paymentData);
        }

        // GET: PaymentDatas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentData paymentData = db.PaymentDatas.Find(id);
            if (paymentData == null)
            {
                return HttpNotFound();
            }
            return View(paymentData);
        }

        // POST: PaymentDatas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentData paymentData = db.PaymentDatas.Find(id);
            db.PaymentDatas.Remove(paymentData);
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
