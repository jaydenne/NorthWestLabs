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
        public ActionResult ClientNewWorkOrder()
        {

            ViewBag.PriorityLevelID = new SelectList(db.PriorityLevels, "PriorityLevelID", "WorkDaysProcessing");
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName");
            ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy");
            ViewBag.CompoundID = new SelectList(db.Compounds, "CompoundID", "CompoundName");
            NewClientOrder newClientOrder = new NewClientOrder();
            return View(newClientOrder);

        }

        // POST: WorkOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClientNewWorkOrder(
            [Bind(Include = "LTNumber,SequenceCode,CompoundName,Quantity,DateArrived,ReceivedBy,DateDue,Appearance,Weight,MolecularMass,ConfirmationDate,MaxTotalDose,ActualWeight,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] Compound compound,
            [Bind(Include = "AssayOrderID,WorkOrderID,PriorityLevelID,AssayID,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate,CompoundID")] AssayOrder assayorder)
        {
            ViewBag.PriorityLevelID = new SelectList(db.PriorityLevels, "PriorityLevelID", "WorkDaysProcessing");
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName");

            WorkOrder workOrder = new WorkOrder();
            workOrder.ClientID = GetClientID();
            workOrder.CreatedBy = "Client "+GetClientID();
            workOrder.ModifiedBy = "Client "+GetClientID();
            workOrder.DateTime = DateTime.Now;
            workOrder.CreatedDate = DateTime.Now;
            workOrder.ModifiedDate = DateTime.Now;
            workOrder.DiscountRate = 0; //calculate discount for customer if it applies here
            db.WorkOrders.Add(workOrder);
            db.SaveChanges();

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
            CompoundAssayOrder compoundAssayOrder = new CompoundAssayOrder();
            compoundAssayOrder.assayOrder = assayorder;
            compoundAssayOrder.compound = compound;
            newClientOrder.workOrder = workOrder;
            return RedirectToAction("WorkOrderMain",newClientOrder);
        }
        
        public ActionResult WorkOrderMain(NewClientOrder newClientOrder)
        {
            return View();
        }


        /*if (ModelState.IsValid)
        {

            compound.LTNumber = db.Compounds.Max(t => t.LTNumber) + 1;
            compound.SequenceCode = 1;
            compound.ModifiedBy = "End User";
            compound.ModifiedDate = DateTime.Now;
            compound.CreatedBy = "End User";
            compound.CreatedDate = DateTime.Now;
            db.Compounds.Add(compound);
            db.WorkOrders.Add(workOrder);


            assayorder.ModifiedBy = "End User";
            assayorder.ModifiedDate = DateTime.Now;
            assayorder.CreatedBy = "End User";
            assayorder.CreatedDate = DateTime.Now;
            db.AssayOrders.Add(assayorder);
            db.SaveChanges();
            return RedirectToAction("WorkOrderMain");*/

        /*
                        db.WorkOrders.Add(workOrder);
                        db.SaveChanges();
                        return RedirectToAction("OrderAddCompound");*/
        // }
        /*
            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName", workOrder.ClientID);
            ViewBag.QuoteID = new SelectList(db.QuoteEstimates, "QuoteID", "ModifiedBy", workOrder.QuoteID);
            return View(workOrder);*/








        // GET: Client
        //public ActionResult OrderAddCompound()
        //{
        //    // Team.teamID = db.Teams.Max(t => t.teamID) + 1;
        //    // ViewBag.LTNumber = db.Compounds.Max(t => t.LTNumber) + 1;

        //    return View();
        //}
        //// POST: Compounds2/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult OrderAddCompound([Bind(Include = "LTNumber,SequenceCode,CompoundName,Quantity,DateArrived,ReceivedBy,DateDue,Appearance,Weight,MolecularMass,ConfirmationDate,MaxTotalDose,ActualWeight,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] Compound compound)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        compound.LTNumber = db.Compounds.Max(t => t.LTNumber) + 1;
        //        compound.SequenceCode = 1;
        //        compound.ModifiedBy = "End User";
        //        compound.ModifiedDate = DateTime.Now;
        //        compound.CreatedBy = "End User";
        //        compound.CreatedDate = DateTime.Now;
        //        db.Compounds.Add(compound);
        //        db.SaveChanges();
        //        return RedirectToAction("WorkOrderMain");
        //    }

        //    return View(compound);
        //}

        //// GET: Client
        //public ActionResult WorkOrderMain()
        //{
        //    return View();
        //}

        //// GET: Client
        //public ActionResult OrderAddAssayOrder()
        //{
        //    ViewBag.PriorityLevelID = new SelectList(db.PriorityLevels, "PriorityLevelID", "WorkDaysProcessing");
        //    ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName");
        //    ViewBag.WorkOrderID = new SelectList(db.WorkOrders, "WorkOrderID", "ModifiedBy");
        //    ViewBag.CompoundID = new SelectList(db.Compounds, "CompoundID", "CompoundName");
        //    ViewBag.hello = db.PriorityLevels.ToList();

        //    return View();
        //}
        //// POST: Compounds2/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult OrderAddAssayOrder([Bind(Include = "AssayOrderID,WorkOrderID,PriorityLevelID,AssayID,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate,CompoundID")] AssayOrder assayorder)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        assayorder.ModifiedBy = "End User";
        //        assayorder.ModifiedDate = DateTime.Now;
        //        assayorder.CreatedBy = "End User";
        //        assayorder.CreatedDate = DateTime.Now;
        //        db.AssayOrders.Add(assayorder);
        //        db.SaveChanges();
        //        return RedirectToAction("WorkOrderMain");
        //    }

        //    return View(assayorder);
        //}



    }
}
