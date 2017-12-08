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
    public class SeattleController : Controller
    {
        private NorthwestLabsContext db = new NorthwestLabsContext();

        public int GetEmployeeID()
        {
            int EmployeeID = 0;
            IEnumerable<Employee> EmployeeList = db.Employees.ToList();
            foreach (Employee myEmployee in EmployeeList)
            {
                if (User.Identity.Name == myEmployee.UserName)
                {
                    EmployeeID = myEmployee.EmployeeID;
                }
            }

            return EmployeeID;
        }

        public ActionResult SeattleIndex()
        {

            Employee myEmployee = db.Employees.Find(GetEmployeeID());

            return View(myEmployee);
        }

        public ActionResult OrderSummary(int? id)
        {
            var workOrders = db.WorkOrders.Include(w => w.Client).Include(w => w.QuoteEstimate);
            List<AssayOrderWithTestResults> AssayOrderList = new List<AssayOrderWithTestResults>();

            IEnumerable<AssayOrder> AssayList = db.AssayOrders.ToList();
            IEnumerable<TestResult> TestResList = db.TestResults.ToList();

            foreach(AssayOrder item  in AssayList)
            {
                if (item.WorkOrderID == id)
                {
                    AssayOrderWithTestResults assayOrder = new AssayOrderWithTestResults();
                    assayOrder.AssayOrder = item;

                    foreach(TestResult val in TestResList)
                    {
                        if (val.AssayOrderID == item.AssayOrderID)
                        {
                            assayOrder.testResults.Add(val);
                        }
                    }
                    AssayOrderList.Add(assayOrder);
                } 
            }

            return View(AssayOrderList);
        }

        // GET: ManageClients
        public ActionResult ClientIndex()
        {
            return View(db.Clients.ToList());
        }

        // GET: ManageClients/Details/5
        public ActionResult ClientDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: ManageClients/Create
        public ActionResult CreateClient()
        {
            return View();
        }

        // POST: ManageClients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateClient([Bind(Include = "CompanyName,ContactFirstName,ContactLastName,Address1,Address2,City,State,Country,Zip,Phone,Email,Username,PasswordHash,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] Client client)
        {
            if (ModelState.IsValid)
            {
                client.CreatedBy = User.Identity.Name;
                client.CreatedDate = DateTime.Now;
                client.ModifiedBy = User.Identity.Name;
                client.ModifiedDate = DateTime.Now;
                

                db.Clients.Add(client);
                db.SaveChanges();
                return RedirectToAction("SeattleIndex");
            }

            return View(client);
        }

        // GET: ManageClients/Edit/5
        public ActionResult EditClients(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: ManageClients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditClients([Bind(Include = "ClientID,CompanyName,ContactFirstName,ContactLastName,Address1,Address2,City,State,Country,Zip,Phone,Email,Username,PasswordHash,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ClientIndex");
            }
            return View(client);
        }

        // GET: ManageClients/Delete/5
        public ActionResult DeleteClients(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: ManageClients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedClient(int id)
        {
            Client client = db.Clients.Find(id);
            db.Clients.Remove(client);
            db.SaveChanges();
            return RedirectToAction("ClientIndex");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ManagementReports()
        {
            return View();
        }
        public ActionResult EmployeeQuotes()
        {
            return View();
        }
        public ActionResult DownloadResults()
        {
            return View();
        }
    }
}