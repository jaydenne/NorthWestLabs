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


    [Authorize]
    public class ClientController : Controller
    {
        private NorthwestLabsContext db = new NorthwestLabsContext();
        public Boolean IsClient()
        {
            Boolean client = false;
            IEnumerable<Client> ClientList = db.Clients.ToList();
            foreach (Client item in ClientList)
            {
                if (User.Identity.Name == item.Username)
                {
                    client = true;
                }
            }

            return client;
        }

        public int GetClientID()
        {
            int ClientID = 0;
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
            if (!IsClient())
            {
                RedirectToAction("Index","Home");
            }

            Client myClient = db.Clients.Find(GetClientID());

            return View(myClient);
        }

        // GET: Client
        public ActionResult ClientInfo()
        {
            if (!IsClient())
            {
                RedirectToAction("Index");
            }

            Client client = db.Clients.Find(GetClientID());


            return View(client);
        }

        // GET: Client
        public ActionResult ClientOrdersSummary()
        {
            if (!IsClient())
            {
                RedirectToAction("Index");
            }

            List<WorkOrder> NewOrders = new List<WorkOrder>();
            IEnumerable<WorkOrder> workorderList = db.WorkOrders.ToList();


            foreach (WorkOrder item in workorderList)
            {
                if (item.ClientID == GetClientID())
                {
                    NewOrders.Add(item);
                }
            }
            return View(NewOrders);

        }
        // GET: Client
        public ActionResult ClientOrderStatus(int? ordernum)
        {
            if (!IsClient())
            {
                RedirectToAction("Index");
            }
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

                    foreach (TestResult val in TestResList)
                    {
                        if (val.AssayOrderID == item.AssayOrderID)
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
            decimal AccTotal = 0;
           
            if (!IsClient())
            {
                RedirectToAction("Index");
            }
            return View();
            
            List<AccountInvoiceData> totalactinvoice = new List<AccountInvoiceData>(); 
            IEnumerable<Invoice> AllInvoices = db.Invoices.ToList();
            IEnumerable<InvioceItem> ItemList = db.InvoiceItems.ToList();
          foreach(Invoice item in AllInvoices)
            {
                if(item.ClientID == GetClientID())
                {
                    AccountInvoiceData acctdata = new AccountInvoiceData();
                    acctdata.Invoice = item;
                        foreach(InvioceItem val in ItemList)
                    {
                        if(val.InvoiceID == item.InvoiceID)
                        {
                            acctdata.invoiceitems.Add(val);
                            AccTotal = AccTotal +  (decimal)val.Amount; 
                        }
                        
                    }
                    totalactinvoice.Add(acctdata);
                }
            }
            ViewBag.Total = AccTotal;
               return View(totalactinvoice);
        }
        // GET: Client
        public ActionResult ClientInvoiceDetails()
        {
            if (!IsClient())
            {
                RedirectToAction("Index");
            }
            return View();
        }
        // GET: Client
        public ActionResult ClientPortalQuotes()
        {
            if (!IsClient())
            {
                RedirectToAction("Index");
            }
            return View();
        }

        // GET: Clients2/Edit/5
        public ActionResult ClientInfoEdit()
        {
            if (!IsClient())
            {
                RedirectToAction("Index");
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
            if (!IsClient())
            {
                RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(client);
        }


        



    }
}