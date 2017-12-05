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
    public class AssayOrderCommentsController : Controller
    {
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: AssayOrderComments
        public ActionResult Index()
        {
            var assayOrderComments = db.AssayOrderComments.Include(a => a.AssayOrder);
            return View(assayOrderComments.ToList());
        }

        // GET: AssayOrderComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssayOrderComment assayOrderComment = db.AssayOrderComments.Find(id);
            if (assayOrderComment == null)
            {
                return HttpNotFound();
            }
            return View(assayOrderComment);
        }

        // GET: AssayOrderComments/Create
        public ActionResult Create()
        {
            ViewBag.AssayOrderID = new SelectList(db.AssayOrders, "AssayOrderID", "ModifiedBy");
            return View();
        }

        // POST: AssayOrderComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssayOrderCommentsID,AssayOrderID,Title,Comment,ModifiedBy,ModfiedDate,CreatedBy,CreatedDate")] AssayOrderComment assayOrderComment)
        {
            if (ModelState.IsValid)
            {
                db.AssayOrderComments.Add(assayOrderComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssayOrderID = new SelectList(db.AssayOrders, "AssayOrderID", "ModifiedBy", assayOrderComment.AssayOrderID);
            return View(assayOrderComment);
        }

        // GET: AssayOrderComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssayOrderComment assayOrderComment = db.AssayOrderComments.Find(id);
            if (assayOrderComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssayOrderID = new SelectList(db.AssayOrders, "AssayOrderID", "ModifiedBy", assayOrderComment.AssayOrderID);
            return View(assayOrderComment);
        }

        // POST: AssayOrderComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssayOrderCommentsID,AssayOrderID,Title,Comment,ModifiedBy,ModfiedDate,CreatedBy,CreatedDate")] AssayOrderComment assayOrderComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(assayOrderComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssayOrderID = new SelectList(db.AssayOrders, "AssayOrderID", "ModifiedBy", assayOrderComment.AssayOrderID);
            return View(assayOrderComment);
        }

        // GET: AssayOrderComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssayOrderComment assayOrderComment = db.AssayOrderComments.Find(id);
            if (assayOrderComment == null)
            {
                return HttpNotFound();
            }
            return View(assayOrderComment);
        }

        // POST: AssayOrderComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AssayOrderComment assayOrderComment = db.AssayOrderComments.Find(id);
            db.AssayOrderComments.Remove(assayOrderComment);
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
