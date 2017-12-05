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
    public class CustomDiscountsController : Controller
    {
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: CustomDiscounts
        public ActionResult Index()
        {
            var customDiscounts = db.CustomDiscounts.Include(c => c.WorkOrder);
            return View(customDiscounts.ToList());
        }

        // GET: CustomDiscounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomDiscount customDiscount = db.CustomDiscounts.Find(id);
            if (customDiscount == null)
            {
                return HttpNotFound();
            }
            return View(customDiscount);
        }

        // GET: CustomDiscounts/Create
        public ActionResult Create()
        {
            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy");
            return View();
        }

        // POST: CustomDiscounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomDiscountID,WorkOrderID,Description,DiscountAmount,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] CustomDiscount customDiscount)
        {
            if (ModelState.IsValid)
            {
                db.CustomDiscounts.Add(customDiscount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy", customDiscount.WorkOrderID);
            return View(customDiscount);
        }

        // GET: CustomDiscounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomDiscount customDiscount = db.CustomDiscounts.Find(id);
            if (customDiscount == null)
            {
                return HttpNotFound();
            }
            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy", customDiscount.WorkOrderID);
            return View(customDiscount);
        }

        // POST: CustomDiscounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomDiscountID,WorkOrderID,Description,DiscountAmount,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] CustomDiscount customDiscount)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customDiscount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy", customDiscount.WorkOrderID);
            return View(customDiscount);
        }

        // GET: CustomDiscounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CustomDiscount customDiscount = db.CustomDiscounts.Find(id);
            if (customDiscount == null)
            {
                return HttpNotFound();
            }
            return View(customDiscount);
        }

        // POST: CustomDiscounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CustomDiscount customDiscount = db.CustomDiscounts.Find(id);
            db.CustomDiscounts.Remove(customDiscount);
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
