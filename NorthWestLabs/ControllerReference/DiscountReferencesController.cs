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
    public class DiscountReferencesController : Controller
    {
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: DiscountReferences
        public ActionResult Index()
        {
            return View(db.DiscountReferences.ToList());
        }

        // GET: DiscountReferences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountReference discountReference = db.DiscountReferences.Find(id);
            if (discountReference == null)
            {
                return HttpNotFound();
            }
            return View(discountReference);
        }

        // GET: DiscountReferences/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DiscountReferences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DiscountID,VolumeMin,DiscountRate,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] DiscountReference discountReference)
        {
            if (ModelState.IsValid)
            {
                db.DiscountReferences.Add(discountReference);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(discountReference);
        }

        // GET: DiscountReferences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountReference discountReference = db.DiscountReferences.Find(id);
            if (discountReference == null)
            {
                return HttpNotFound();
            }
            return View(discountReference);
        }

        // POST: DiscountReferences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DiscountID,VolumeMin,DiscountRate,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] DiscountReference discountReference)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discountReference).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(discountReference);
        }

        // GET: DiscountReferences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiscountReference discountReference = db.DiscountReferences.Find(id);
            if (discountReference == null)
            {
                return HttpNotFound();
            }
            return View(discountReference);
        }

        // POST: DiscountReferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiscountReference discountReference = db.DiscountReferences.Find(id);
            db.DiscountReferences.Remove(discountReference);
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
