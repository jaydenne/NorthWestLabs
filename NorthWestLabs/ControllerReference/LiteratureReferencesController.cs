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
    public class LiteratureReferencesController : Controller
    {
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: LiteratureReferences
        public ActionResult Index()
        {
            var literatureReferences = db.LiteratureReferences.Include(l => l.ProtocolNotebook);
            return View(literatureReferences.ToList());
        }

        // GET: LiteratureReferences/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LiteratureReference literatureReference = db.LiteratureReferences.Find(id);
            if (literatureReference == null)
            {
                return HttpNotFound();
            }
            return View(literatureReference);
        }

        // GET: LiteratureReferences/Create
        public ActionResult Create()
        {
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName");
            return View();
        }

        // POST: LiteratureReferences/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LiteratureReferenceID,AssayID,ReferenceName,Reference,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] LiteratureReference literatureReference)
        {
            if (ModelState.IsValid)
            {
                db.LiteratureReferences.Add(literatureReference);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName", literatureReference.AssayID);
            return View(literatureReference);
        }

        // GET: LiteratureReferences/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LiteratureReference literatureReference = db.LiteratureReferences.Find(id);
            if (literatureReference == null)
            {
                return HttpNotFound();
            }
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName", literatureReference.AssayID);
            return View(literatureReference);
        }

        // POST: LiteratureReferences/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LiteratureReferenceID,AssayID,ReferenceName,Reference,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] LiteratureReference literatureReference)
        {
            if (ModelState.IsValid)
            {
                db.Entry(literatureReference).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName", literatureReference.AssayID);
            return View(literatureReference);
        }

        // GET: LiteratureReferences/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LiteratureReference literatureReference = db.LiteratureReferences.Find(id);
            if (literatureReference == null)
            {
                return HttpNotFound();
            }
            return View(literatureReference);
        }

        // POST: LiteratureReferences/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LiteratureReference literatureReference = db.LiteratureReferences.Find(id);
            db.LiteratureReferences.Remove(literatureReference);
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
