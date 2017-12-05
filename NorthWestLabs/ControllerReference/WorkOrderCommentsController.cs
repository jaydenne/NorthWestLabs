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
    public class WorkOrderCommentsController : Controller
    {
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: WorkOrderComments
        public ActionResult Index()
        {
            var workOrderComments = db.WorkOrderComments.Include(w => w.WorkOrder);
            return View(workOrderComments.ToList());
        }

        // GET: WorkOrderComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrderComment workOrderComment = db.WorkOrderComments.Find(id);
            if (workOrderComment == null)
            {
                return HttpNotFound();
            }
            return View(workOrderComment);
        }

        // GET: WorkOrderComments/Create
        public ActionResult Create()
        {
            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy");
            return View();
        }

        // POST: WorkOrderComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "WorkOrderCommentID,WorkOrderID,CommentType,Comment,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] WorkOrderComment workOrderComment)
        {
            if (ModelState.IsValid)
            {
                db.WorkOrderComments.Add(workOrderComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy", workOrderComment.WorkOrderID);
            return View(workOrderComment);
        }

        // GET: WorkOrderComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrderComment workOrderComment = db.WorkOrderComments.Find(id);
            if (workOrderComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy", workOrderComment.WorkOrderID);
            return View(workOrderComment);
        }

        // POST: WorkOrderComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "WorkOrderCommentID,WorkOrderID,CommentType,Comment,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] WorkOrderComment workOrderComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workOrderComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy", workOrderComment.WorkOrderID);
            return View(workOrderComment);
        }

        // GET: WorkOrderComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkOrderComment workOrderComment = db.WorkOrderComments.Find(id);
            if (workOrderComment == null)
            {
                return HttpNotFound();
            }
            return View(workOrderComment);
        }

        // POST: WorkOrderComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkOrderComment workOrderComment = db.WorkOrderComments.Find(id);
            db.WorkOrderComments.Remove(workOrderComment);
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
