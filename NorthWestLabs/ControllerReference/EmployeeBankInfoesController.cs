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
    public class EmployeeBankInfoesController : Controller
    {
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: EmployeeBankInfoes
        public ActionResult Index()
        {
            var employeeBankInfoes = db.EmployeeBankInfoes.Include(e => e.Employee);
            return View(employeeBankInfoes.ToList());
        }

        // GET: EmployeeBankInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeBankInfo employeeBankInfo = db.EmployeeBankInfoes.Find(id);
            if (employeeBankInfo == null)
            {
                return HttpNotFound();
            }
            return View(employeeBankInfo);
        }

        // GET: EmployeeBankInfoes/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name");
            return View();
        }

        // POST: EmployeeBankInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,BankAccount,RoutingNumber,AccountType,ModifiedBy,ModifiedDate")] EmployeeBankInfo employeeBankInfo)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeBankInfoes.Add(employeeBankInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name", employeeBankInfo.EmployeeID);
            return View(employeeBankInfo);
        }

        // GET: EmployeeBankInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeBankInfo employeeBankInfo = db.EmployeeBankInfoes.Find(id);
            if (employeeBankInfo == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name", employeeBankInfo.EmployeeID);
            return View(employeeBankInfo);
        }

        // POST: EmployeeBankInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,BankAccount,RoutingNumber,AccountType,ModifiedBy,ModifiedDate")] EmployeeBankInfo employeeBankInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeBankInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "EmployeeID", "Name", employeeBankInfo.EmployeeID);
            return View(employeeBankInfo);
        }

        // GET: EmployeeBankInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeBankInfo employeeBankInfo = db.EmployeeBankInfoes.Find(id);
            if (employeeBankInfo == null)
            {
                return HttpNotFound();
            }
            return View(employeeBankInfo);
        }

        // POST: EmployeeBankInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeBankInfo employeeBankInfo = db.EmployeeBankInfoes.Find(id);
            db.EmployeeBankInfoes.Remove(employeeBankInfo);
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
