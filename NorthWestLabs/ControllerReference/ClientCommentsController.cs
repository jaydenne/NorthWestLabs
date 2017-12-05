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
    public class ClientCommentsController : Controller
    {
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: ClientComments
        public ActionResult Index()
        {
            var clientComments = db.ClientComments.Include(c => c.Client);
            return View(clientComments.ToList());
        }

        // GET: ClientComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientComment clientComment = db.ClientComments.Find(id);
            if (clientComment == null)
            {
                return HttpNotFound();
            }
            return View(clientComment);
        }

        // GET: ClientComments/Create
        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName");
            return View();
        }

        // POST: ClientComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientCommentsID,ClientID,Title,Comment,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] ClientComment clientComment)
        {
            if (ModelState.IsValid)
            {
                db.ClientComments.Add(clientComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName", clientComment.ClientID);
            return View(clientComment);
        }

        // GET: ClientComments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientComment clientComment = db.ClientComments.Find(id);
            if (clientComment == null)
            {
                return HttpNotFound();
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName", clientComment.ClientID);
            return View(clientComment);
        }

        // POST: ClientComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientCommentsID,ClientID,Title,Comment,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] ClientComment clientComment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(clientComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName", clientComment.ClientID);
            return View(clientComment);
        }

        // GET: ClientComments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClientComment clientComment = db.ClientComments.Find(id);
            if (clientComment == null)
            {
                return HttpNotFound();
            }
            return View(clientComment);
        }

        // POST: ClientComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClientComment clientComment = db.ClientComments.Find(id);
            db.ClientComments.Remove(clientComment);
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
