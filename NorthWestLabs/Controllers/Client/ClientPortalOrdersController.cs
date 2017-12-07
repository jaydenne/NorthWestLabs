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
    public class ClientPortalOrdersController : Controller
    {

        private NorthwestLabsContext db = new NorthwestLabsContext();

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


        // GET: ClientPortalOrders
        public ActionResult Index()
        {
            return View();
        }


        // GET: Client
        public ActionResult ClientNewWorkOrder(int? workorderid)
        {

            ViewBag.PriorityLevelID = new SelectList(db.PriorityLevels, "PriorityLevelID", "WorkDaysProcessing");
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName");
            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy");
            ViewBag.CompoundID = new SelectList(db.Compounds, "CompoundID", "CompoundName");
            NewClientOrder newClientOrder = new NewClientOrder();
            if (workorderid != null || workorderid != 0) {
                newClientOrder.workOrder = db.WorkOrders.Find(workorderid);
                    }
            return View(newClientOrder);
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
            if (workorderid == null ||workorderid==0) { 

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
                if(item.WorkOrderID== workOrder.WorkOrderID)
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
            return View("WorkOrderMain",newClientOrder);
        }
        
        [HttpPost]
        public ActionResult WorkOrderMain(NewClientOrder newClientOrder)
        {
           foreach(var item in newClientOrder.compoundAssayOrderList)
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
            foreach(Compound item in compoundList)
            {
                if(item.LTNumber==LTNumber)
                {
                    compound.CompoundName=item.CompoundName;

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

        public ActionResult SubmitOrder(int workorderid)
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
                        if(test.AssayID==item.assayOrder.AssayID)
                        {
                            TestResult newresult = new TestResult();
                        newresult.AssayOrderID = item.assayOrder.AssayOrderID;
                        newresult.CompoundID = item.compound.CompoundID;
                        newresult.TestID = test.TestID;
                        newresult.CreatedBy = "Client " + GetClientID();
                        newresult.CreatedDate = DateTime.Now;
                        newresult.ModifedBy = "Client " + GetClientID();
                        newresult.ModifiedDate = DateTime.Now;
                        newresult.StatusID = 1;
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
                return RedirectToAction("Index","Client");
            


        }


        }
}
