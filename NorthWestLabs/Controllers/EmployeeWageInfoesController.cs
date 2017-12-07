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
    public class EmployeeWageInfoesController : Controller
    {
        private NorthwestLabsContext db = new NorthwestLabsContext();

        // GET: EmployeeWageInfoes
        public ActionResult Index()
        {
            return View(db.EmployeeWageInfoes.ToList());
        }

        // GET: EmployeeWageInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeWageInfo employeeWageInfo = db.EmployeeWageInfoes.Find(id);
            if (employeeWageInfo == null)
            {
                return HttpNotFound();
            }
            return View(employeeWageInfo);
        }

        // GET: EmployeeWageInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EmployeeWageInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeWageInfoID,EmployeeID,LastIncreaseDate,LastIncreaseAmount,CurrentWage,WageType,DirectDeposit,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] EmployeeWageInfo employeeWageInfo)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeWageInfoes.Add(employeeWageInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(employeeWageInfo);
        }

        // GET: EmployeeWageInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeWageInfo employeeWageInfo = db.EmployeeWageInfoes.Find(id);
            if (employeeWageInfo == null)
            {
                return HttpNotFound();
            }
            return View(employeeWageInfo);
        }

        // POST: EmployeeWageInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeWageInfoID,EmployeeID,LastIncreaseDate,LastIncreaseAmount,CurrentWage,WageType,DirectDeposit,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] EmployeeWageInfo employeeWageInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeWageInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(employeeWageInfo);
        }

        // GET: EmployeeWageInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeWageInfo employeeWageInfo = db.EmployeeWageInfoes.Find(id);
            if (employeeWageInfo == null)
            {
                return HttpNotFound();
            }
            return View(employeeWageInfo);
        }

        // POST: EmployeeWageInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeWageInfo employeeWageInfo = db.EmployeeWageInfoes.Find(id);
            db.EmployeeWageInfoes.Remove(employeeWageInfo);
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
