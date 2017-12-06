using NorthWestLabs.DAL;
using NorthWestLabs.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace NorthWestLabs.Controllers
{
    public class ClientController : Controller
    {
        private NorthwestLabsContext db = new NorthwestLabsContext();

        public int GetClientID()
        {
            int ClientID=0;
            IEnumerable<Client> ClientList = db.Clients.ToList();
            foreach (Client myClient in ClientList)
            {
                if (User.Identity.Name == myClient.Username)
                {
                    ClientID = myClient.ClientID;
                }
            }

            return ClientID;
        } 
        // GET: Client
        public ActionResult Index()
        {

            Client myClient = db.Clients.Find(GetClientID());
           
            return View(myClient);
        }
        
        // GET: Client
        public ActionResult ClientInfo()
        {
            
            Client client = db.Clients.Find(GetClientID());
            

            return View(client);
        }

        // GET: Client
        public ActionResult ClientOrdersSummary()
        {

            List<WorkOrder> NewOrders = new List<WorkOrder>();
            IEnumerable<WorkOrder> workorderList = db.WorkOrders.ToList(); 

            
            foreach (WorkOrder item in workorderList)
            {
                if(item.ClientID == GetClientID())
                {
                    NewOrders.Add(item); 
                }
            }
                return View(NewOrders);
        
        }
        // GET: Client
        public ActionResult ClientOrderStatus(int? ordernum)
        {
            ViewBag.ordernumber = ordernum;

            //need a list of assays where the work order is equal to the ordernum

            List<AssayOrderWithTestResults> AssayOrderList = new List<AssayOrderWithTestResults>();

            //List<AssayOrder> assays = new List<AssayOrder>();

            IEnumerable<AssayOrder> AssayList = db.AssayOrders.ToList();
            IEnumerable<TestResult> TestResList = db.TestResults.ToList();
            //Dictionary<int, TestResult> TestResDictionary = new Dictionary<int, TestResult>();
            foreach (AssayOrder item in AssayList)
            {

                if (item.WorkOrderID == ordernum)
                {
                    AssayOrderWithTestResults assayOrder = new AssayOrderWithTestResults();
                    //assays.Add(item);
                    assayOrder.AssayOrder = item;

                    foreach(TestResult val in TestResList)
                    {
                        if(val.AssayOrderID == item.AssayOrderID)
                        {
                            //TestResDictionary.Add(item.AssayOrderID, val);
                            assayOrder.testResults.Add(val);
                        }
                    }
                    AssayOrderList.Add(assayOrder);                          
                }
            }

            //ViewBag.testresults = TestResDictionary;

            return View(AssayOrderList);
        }
        // GET: Client
        public ActionResult ClientAccount()
        {
            return View();
        }
        // GET: Client
        public ActionResult ClientInvoiceDetails()
        {
            return View();
        }
        // GET: Client
        public ActionResult ClientPortalQuotes()
        {
            return View();
        }

        // GET: Clients2/Edit/5
        public ActionResult ClientInfoEdit()
        {
            if (GetClientID() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = db.Clients.Find(GetClientID());
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients2/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClientInfoEdit([Bind(Include = "ClientID,CompanyName,ContactFirstName,ContactLastName,Address1,Address2,City,State,Country,Zip,Phone,Email,Username,PasswordHash,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }
        // GET: Client
        public ActionResult ClientNewWorkOrder()
        {
            return View();
        }



    }
}