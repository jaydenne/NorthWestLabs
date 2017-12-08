using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthWestLabs.DAL;
using NorthWestLabs.Models;

namespace NorthWestLabs.Controllers
{
    
    public class QuotesController : Controller
    {
        // GET: Quotes
        NorthwestLabsContext db = new NorthwestLabsContext();
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



        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewQuote()
        {
            QuoteEstimate MyQuote = new QuoteEstimate();

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewQuote(int? QuoteID, [Bind(Include = "QuoteID,AssayID,Cost,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] QuoteItem quoteItem)
        {
            QuoteEstimate MyQuote = new QuoteEstimate();
            if(QuoteID == null)
            {
                MyQuote.ClientID = GetClientID();
                MyQuote.CreatedBy = "Client " + GetClientID();
                MyQuote.CreatedDate = DateTime.Now;
                MyQuote.ModifiedBy = "Client " + GetClientID();
                MyQuote.ModifiedDate = DateTime.Now;
                MyQuote.QuoteDate = DateTime.Now;
                db.QuoteEstimates.Add(MyQuote);
                db.SaveChanges();
            }

            QuoteItem MyItem = new QuoteItem();
            MyItem.QuoteID = MyQuote.QuoteID;
            MyItem.AssayID = quoteItem.AssayID;
            MyItem.CreatedBy = "Client " + GetClientID();
            MyItem.CreatedDate = DateTime.Now;
            MyItem.ModifiedBy = "Client " + GetClientID();
            MyItem.ModifiedDate = DateTime.Now;
            MyItem.Cost = 0;
            IEnumerable<Test> testList = db.Tests.ToList();
            foreach (Test test in testList)
            {
                if(test.AssayID==MyItem.AssayID)
                {
                   MyItem.Cost += (test.BasePrice + (test.Hours * 40));
                }
            }
            db.QuoteItems.Add(MyItem);
            db.SaveChanges();
            IEnumerable<QuoteItem> items = db.QuoteItems.ToList();
            foreach (QuoteItem item in items)
            {
                if (item.QuoteID == MyQuote.QuoteID)
                {
                    MyQuote.QuoteItems.Add(item);
                }
            }
            return View(MyQuote);
        }





        // POST: WorkOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClientNewWorkOrder(FormCollection form, int? workorderid,
            [Bind(Include = "LTNumber,SequenceCode,CompoundName,Quantity,DateArrived,ReceivedBy,DateDue,Appearance,Weight,MolecularMass,ConfirmationDate,MaxTotalDose,ActualWeight,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] Compound compound,
            [Bind(Include = "AssayOrderID,WorkOrderID,PriorityLevelID,AssayID,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate,CompoundID")] AssayOrder assayorder)
        {
            ViewBag.PriorityLevelID = new SelectList(db.PriorityLevels, "PriorityLevelID", "WorkDaysProcessing");
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName");
            WorkOrder workOrder = new WorkOrder();
            if (workorderid == null || workorderid == 0)
            {

                workOrder.ClientID = GetClientID();
                workOrder.CreatedBy = "Client " + GetClientID();
                workOrder.ModifiedBy = "Client " + GetClientID();
                workOrder.DateTime = DateTime.Now;
                workOrder.CreatedDate = DateTime.Now;
                workOrder.ModifiedDate = DateTime.Now;
                workOrder.DiscountRate = 0; //calculate discount for customer if it applies here
                db.WorkOrders.Add(workOrder);
                db.SaveChanges();
            }
            else
            {
                workOrder = db.WorkOrders.Find(workorderid);
            }

            compound.CompoundName = form["CompoundName"];
            compound.LTNumber = db.Compounds.Max(t => t.LTNumber) + 1;
            compound.SequenceCode = 1;
            compound.ModifiedBy = "Client " + GetClientID();
            compound.ModifiedDate = DateTime.Now;
            compound.CreatedBy = "Client " + GetClientID();
            compound.CreatedDate = DateTime.Now;
            db.Compounds.Add(compound);
            db.SaveChanges();

            assayorder.WorkOrderID = workOrder.WorkOrderID;
            assayorder.CompoundID = compound.CompoundID;
            assayorder.ModifiedBy = "Client " + GetClientID();
            assayorder.ModifiedDate = DateTime.Now;
            assayorder.CreatedBy = "Client " + GetClientID();
            assayorder.CreatedDate = DateTime.Now;
            db.AssayOrders.Add(assayorder);
            db.SaveChanges();

            NewClientOrder newClientOrder = new NewClientOrder();
            IEnumerable<AssayOrder> allAssay = db.AssayOrders.ToList();
            foreach (AssayOrder item in allAssay)
            {
                if (item.WorkOrderID == workOrder.WorkOrderID)
                {
                    CompoundAssayOrder myCompoundAssayOrder = new CompoundAssayOrder();
                    myCompoundAssayOrder.assayOrder = item;
                    myCompoundAssayOrder.compound = db.Compounds.Find(item.CompoundID);
                    newClientOrder.compoundAssayOrderList.Add(myCompoundAssayOrder);
                }
            }


            newClientOrder.workOrder = workOrder;
            foreach (var item in newClientOrder.compoundAssayOrderList)
            {
                item.assayOrder.ProtocolNotebook = db.ProtocolNotebooks.Find(item.assayOrder.AssayID);
                item.assayOrder.PriorityLevel = db.PriorityLevels.Find(item.assayOrder.PriorityLevelID);
            }
            return View("WorkOrderMain", newClientOrder);
        }

        [HttpPost]
        public ActionResult WorkOrderMain(NewClientOrder newClientOrder)
        {
            foreach (var item in newClientOrder.compoundAssayOrderList)
            {
                item.assayOrder.ProtocolNotebook = db.ProtocolNotebooks.Find(item.assayOrder.AssayID);
            }
            return View(newClientOrder);
        }
        public ActionResult ClientAddAssay(int workorderid, int LTNumber)
        {
            ViewBag.PriorityLevelID = new SelectList(db.PriorityLevels, "PriorityLevelID", "WorkDaysProcessing");
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName");
            WorkOrderNumberAndLTNumber nums = new WorkOrderNumberAndLTNumber();
            nums.LTNumber = LTNumber;
            nums.WorkOrderNumber = workorderid;
            return View(nums);
        }
        [HttpPost]
        public ActionResult ClientAddAssay(int workorderid, int LTNumber,
            [Bind(Include = "AssayOrderID,WorkOrderID,PriorityLevelID,AssayID,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate,CompoundID")] AssayOrder assayorder)
        {
            ViewBag.PriorityLevelID = new SelectList(db.PriorityLevels, "PriorityLevelID", "WorkDaysProcessing");
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName");
            Compound compound = new Compound();
            IEnumerable<Compound> compoundList = db.Compounds.ToList();
            foreach (Compound item in compoundList)
            {
                if (item.LTNumber == LTNumber)
                {
                    compound.CompoundName = item.CompoundName;

                }
            }

            compound.LTNumber = LTNumber;
            compound.SequenceCode = 1;//needs function to increment sequence code
            compound.ModifiedBy = "Client " + GetClientID();
            compound.ModifiedDate = DateTime.Now;
            compound.CreatedBy = "Client " + GetClientID();
            compound.CreatedDate = DateTime.Now;
            db.Compounds.Add(compound);
            db.SaveChanges();

            assayorder.WorkOrderID = workorderid;
            assayorder.CompoundID = compound.CompoundID;
            assayorder.ModifiedBy = "Client " + GetClientID();
            assayorder.ModifiedDate = DateTime.Now;
            assayorder.CreatedBy = "Client " + GetClientID();
            assayorder.CreatedDate = DateTime.Now;
            db.AssayOrders.Add(assayorder);
            db.SaveChanges();

            NewClientOrder newClientOrder = new NewClientOrder();
            newClientOrder.workOrder = db.WorkOrders.Find(workorderid);
            IEnumerable<AssayOrder> allAssay = db.AssayOrders.ToList();
            foreach (AssayOrder item in allAssay)
            {
                if (item.WorkOrderID == newClientOrder.workOrder.WorkOrderID)
                {
                    CompoundAssayOrder myCompoundAssayOrder = new CompoundAssayOrder();
                    myCompoundAssayOrder.assayOrder = item;
                    myCompoundAssayOrder.compound = db.Compounds.Find(item.CompoundID);
                    newClientOrder.compoundAssayOrderList.Add(myCompoundAssayOrder);
                }
            }

            foreach (var item in newClientOrder.compoundAssayOrderList)
            {
                item.assayOrder.ProtocolNotebook = db.ProtocolNotebooks.Find(item.assayOrder.AssayID);
                item.assayOrder.PriorityLevel = db.PriorityLevels.Find(item.assayOrder.PriorityLevelID);
            }


            return View("WorkOrderMain", newClientOrder);
        }

        public async System.Threading.Tasks.Task<ActionResult> SubmitOrder(int workorderid)
        {
            ViewBag.PriorityLevelID = new SelectList(db.PriorityLevels, "PriorityLevelID", "WorkDaysProcessing");
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName");
            WorkOrder workOrder = db.WorkOrders.Find(workorderid);
            NewClientOrder newClientOrder = new NewClientOrder();
            IEnumerable<AssayOrder> allAssay = db.AssayOrders.ToList();
            foreach (AssayOrder item in allAssay)
            {
                if (item.WorkOrderID == workOrder.WorkOrderID)
                {
                    CompoundAssayOrder myCompoundAssayOrder = new CompoundAssayOrder();
                    myCompoundAssayOrder.assayOrder = item;
                    myCompoundAssayOrder.compound = db.Compounds.Find(item.CompoundID);
                    newClientOrder.compoundAssayOrderList.Add(myCompoundAssayOrder);
                }
            }
            newClientOrder.workOrder = workOrder;
            IEnumerable<Test> TestList = db.Tests.ToList();
            foreach (var item in newClientOrder.compoundAssayOrderList)
            {
                item.assayOrder.ProtocolNotebook = db.ProtocolNotebooks.Find(item.assayOrder.AssayID);
                item.assayOrder.PriorityLevel = db.PriorityLevels.Find(item.assayOrder.PriorityLevelID);
                foreach (var test in TestList)
                {
                    if (test.AssayID == item.assayOrder.AssayID)
                    {
                        TestResult newresult = new TestResult();
                        newresult.AssayOrderID = item.assayOrder.AssayOrderID;
                        newresult.CompoundID = item.compound.CompoundID;
                        newresult.TestID = test.TestID;
                        newresult.CreatedBy = "Client " + GetClientID();
                        newresult.CreatedDate = DateTime.Now;
                        newresult.ModifedBy = "Client " + GetClientID();
                        newresult.ModifiedDate = DateTime.Now;
                        newresult.StatusID = 1; //set status to Work Order Received
                        newresult.StatusUpdatedDate = DateTime.Now;
                        TestTube mytube = new TestTube();
                        mytube.CompoundID = item.compound.CompoundID;
                        mytube.CreatedBy = "System";
                        mytube.CreatedDate = DateTime.Now;
                        db.TestTubes.Add(mytube);

                        newresult.TestTubeID = mytube.TestTubeID;
                        db.TestResults.Add(newresult);
                        db.SaveChanges();
                    }
                }

            }
            //Function to send email

            var body = "<h2>Order Confirmed</h2>" +
            "<p>Order Date:" + newClientOrder.workOrder.DateTime + "</p>" +
            "<br />";
            Dictionary<int, NorthWestLabs.Models.Compound> LTNumberList = new Dictionary<int, NorthWestLabs.Models.Compound>();
            foreach (NorthWestLabs.Models.CompoundAssayOrder item in newClientOrder.compoundAssayOrderList)
            {
                if (!LTNumberList.ContainsKey(item.compound.LTNumber))
                {
                    LTNumberList.Add(item.compound.LTNumber, item.compound);
                }
            }

            foreach (var LtNumber in LTNumberList)
            {
                //<!--Display of compound and assay details-->
                body += "<h3>Compound" + LtNumber.Key + " | " + LtNumber.Value.CompoundName + "</h3>" +
                "<table class='table'>" +
                    "<tr>" +
                        "<th>Assay Name</th>" +
                        "<th>LTNumber</th>" +
                    "</tr>";

                foreach (NorthWestLabs.Models.CompoundAssayOrder item in newClientOrder.compoundAssayOrderList)
                {
                    if (item.assayOrder.Compound.LTNumber == LtNumber.Key)
                    {
                        body += "<tr>" +
                            "<td>" + item.assayOrder.ProtocolNotebook.AssayName + "</td>" +
                            "<td>" + LtNumber.Key + "-" + item.compound.SequenceCode + "</td>" +
                        "</tr>";
                    }
                }
                body += "</table>";
            }






            var message = new MailMessage();
            message.To.Add(new MailAddress("Avery.Awerkamp@gmail.com"/*db.Clients.Find(GetClientID()).Email)*/));  // replace with valid value 
            message.From = new MailAddress("J.nelson55@gmail.com", "Northwest Labs");  // replace with valid value
            message.Subject = "Northwest Labs - Order Confirmation";
            message.Body = string.Format(body);
            message.IsBodyHtml = true;
            //message.Attachments() add attachments

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "J.nelson55@gmail.com",  // replace with valid value
                    Password = "LOCKDOWN"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }





            return View(newClientOrder);
        }



    }

























}
}