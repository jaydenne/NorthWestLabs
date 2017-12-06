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
    
    public class ProtocolNotebooksController : Controller
    {
        private NorthwestLabsContext db = new NorthwestLabsContext();

        // GET: ProtocolNotebooks
        public ActionResult Index()
        {
            var protocolNotebooks = db.ProtocolNotebooks.Include(p => p.Protocol);
            return View(protocolNotebooks.ToList());
        }

        // GET: ProtocolNotebooks/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProtocolNotebook protocolNotebook = db.ProtocolNotebooks.Find(id);
            if (protocolNotebook == null)
            {
                return HttpNotFound();
            }
            return View(protocolNotebook);
        }

        // GET: ProtocolNotebooks/Create
        public ActionResult Create()
        {
            ViewBag.ProtocolID = new SelectList(db.Protocols, "ProtocolID", "ProtocolName");
            return View();
        }

        // POST: ProtocolNotebooks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssayID,AssayName,ProtocolID,EstDaysToComplete,Description,RequireAnimalTesting,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate,ImageLocation")] ProtocolNotebook protocolNotebook)
        {

            protocolNotebook.CreatedDate = DateTime.Now;
            protocolNotebook.ModifiedDate = DateTime.Now;
            protocolNotebook.CreatedBy = "Jesse";
            protocolNotebook.ModifiedBy = "Jesse";
            if (ModelState.IsValid)
            {
                db.ProtocolNotebooks.Add(protocolNotebook);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProtocolID = new SelectList(db.Protocols, "ProtocolID", "ProtocolName", protocolNotebook.ProtocolID);
            return View(protocolNotebook);
        }

        // GET: ProtocolNotebooks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProtocolNotebook protocolNotebook = db.ProtocolNotebooks.Find(id);
            if (protocolNotebook == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProtocolID = new SelectList(db.Protocols, "ProtocolID", "ProtocolName", protocolNotebook.ProtocolID);
            return View(protocolNotebook);
        }

        // POST: ProtocolNotebooks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssayID,AssayName,ProtocolID,EstDaysToComplete,Description,RequireAnimalTesting,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate,ImageLocation")] ProtocolNotebook protocolNotebook)
        {
            if (ModelState.IsValid)
            {
                db.Entry(protocolNotebook).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProtocolID = new SelectList(db.Protocols, "ProtocolID", "ProtocolName", protocolNotebook.ProtocolID);
            return View(protocolNotebook);
        }

        // GET: ProtocolNotebooks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProtocolNotebook protocolNotebook = db.ProtocolNotebooks.Find(id);
            if (protocolNotebook == null)
            {
                return HttpNotFound();
            }
            return View(protocolNotebook);
        }

        // POST: ProtocolNotebooks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProtocolNotebook protocolNotebook = db.ProtocolNotebooks.Find(id);
            db.ProtocolNotebooks.Remove(protocolNotebook);
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
