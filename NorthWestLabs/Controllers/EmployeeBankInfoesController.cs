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
    public class EmployeeBankInfoesController : Controller
    {
        private NorthwestLabsContext db = new NorthwestLabsContext();

        // GET: EmployeeBankInfoes
        public ActionResult Index()
        {
            return View(db.EmployeeBankInfoes.ToList());
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
            return View();
        }

        // POST: EmployeeBankInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeBankInfoID,EmployeeID,BankAccount,RoutingNumber,AccountType,ModifiedBy,ModifiedDate")] EmployeeBankInfo employeeBankInfo)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeBankInfoes.Add(employeeBankInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            return View(employeeBankInfo);
        }

        // POST: EmployeeBankInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeBankInfoID,EmployeeID,BankAccount,RoutingNumber,AccountType,ModifiedBy,ModifiedDate")] EmployeeBankInfo employeeBankInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeBankInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
