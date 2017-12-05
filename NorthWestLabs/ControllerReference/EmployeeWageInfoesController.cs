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
    public class EmployeeWageInfoesController : Controller
    {
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: EmployeeWageInfoes
        public ActionResult Index()
        {
            var employeeWageInfoes = db.EmployeeWageInfoes.Include(e => e.Employee);
            return View(employeeWageInfoes.ToList());
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
            ViewBag.EmloyeeID = new SelectList(db.Employees, "EmployeeID", "Name");
            return View();
        }

        // POST: EmployeeWageInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmloyeeID,LastIncreaseDate,LastIncreaseAmount,CurrentWage,WageType,DirectDeposit,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] EmployeeWageInfo employeeWageInfo)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeWageInfoes.Add(employeeWageInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmloyeeID = new SelectList(db.Employees, "EmployeeID", "Name", employeeWageInfo.EmloyeeID);
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
            ViewBag.EmloyeeID = new SelectList(db.Employees, "EmployeeID", "Name", employeeWageInfo.EmloyeeID);
            return View(employeeWageInfo);
        }

        // POST: EmployeeWageInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmloyeeID,LastIncreaseDate,LastIncreaseAmount,CurrentWage,WageType,DirectDeposit,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] EmployeeWageInfo employeeWageInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeWageInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmloyeeID = new SelectList(db.Employees, "EmployeeID", "Name", employeeWageInfo.EmloyeeID);
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
