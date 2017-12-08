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
    public class QuoteEstimatesController : Controller
    {
        private NorthwestLabsContext db = new NorthwestLabsContext();

        // GET: QuoteEstimates
        public ActionResult Index()
        {
            var quoteEstimates = db.QuoteEstimates.Include(q => q.Client);
            return View(quoteEstimates.ToList());
        }

        // GET: QuoteEstimates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuoteEstimate quoteEstimate = db.QuoteEstimates.Find(id);
            if (quoteEstimate == null)
            {
                return HttpNotFound();
            }
            return View(quoteEstimate);
        }

        // GET: QuoteEstimates/Create
        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName");
            return View();
        }

        // POST: QuoteEstimates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuoteID,ClientID,ExpectedDiscount,QuoteDate,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] QuoteEstimate quoteEstimate)
        {
            if (ModelState.IsValid)
            {
                db.QuoteEstimates.Add(quoteEstimate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName", quoteEstimate.ClientID);
            return View(quoteEstimate);
        }

        // GET: QuoteEstimates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuoteEstimate quoteEstimate = db.QuoteEstimates.Find(id);
            if (quoteEstimate == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName", quoteEstimate.ClientID);
            return View(quoteEstimate);
        }

        // POST: QuoteEstimates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuoteID,ClientID,ExpectedDiscount,QuoteDate,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] QuoteEstimate quoteEstimate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quoteEstimate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName", quoteEstimate.ClientID);
            return View(quoteEstimate);
        }

        // GET: QuoteEstimates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuoteEstimate quoteEstimate = db.QuoteEstimates.Find(id);
            if (quoteEstimate == null)
            {
                return HttpNotFound();
            }
            return View(quoteEstimate);
        }

        // POST: QuoteEstimates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuoteEstimate quoteEstimate = db.QuoteEstimates.Find(id);
            db.QuoteEstimates.Remove(quoteEstimate);
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
