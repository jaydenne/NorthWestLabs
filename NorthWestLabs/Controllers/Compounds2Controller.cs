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
    public class Compounds2Controller : Controller
    {
        private NorthwestLabsContext db = new NorthwestLabsContext();

        // GET: Compounds2
        public ActionResult Index()
        {
            return View(db.Compounds.ToList());
        }

        // GET: Compounds2/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compound compound = db.Compounds.Find(id);
            if (compound == null)
            {
                return HttpNotFound();
            }
            return View(compound);
        }

        // GET: Compounds2/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Compounds2/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CompoundID,LTNumber,SequenceCode,CompoundName,Quantity,DateArrived,ReceivedBy,DateDue,Appearance,Weight,MolecularMass,ConfirmationDate,MaxTotalDose,ActualWeight,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] Compound compound)
        {
            if (ModelState.IsValid)
            {
                db.Compounds.Add(compound);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(compound);
        }

        // GET: Compounds2/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compound compound = db.Compounds.Find(id);
            if (compound == null)
            {
                return HttpNotFound();
            }
            return View(compound);
        }

        // POST: Compounds2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CompoundID,LTNumber,SequenceCode,CompoundName,Quantity,DateArrived,ReceivedBy,DateDue,Appearance,Weight,MolecularMass,ConfirmationDate,MaxTotalDose,ActualWeight,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] Compound compound)
        {
            if (ModelState.IsValid)
            {
                db.Entry(compound).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(compound);
        }

        // GET: Compounds2/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Compound compound = db.Compounds.Find(id);
            if (compound == null)
            {
                return HttpNotFound();
            }
            return View(compound);
        }

        // POST: Compounds2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Compound compound = db.Compounds.Find(id);
            db.Compounds.Remove(compound);
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
