﻿using NorthWestLabs.DAL;
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



            return View();
        }

        // POST: WorkOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ClientNewWorkOrder([Bind(Include = "WorkOrderID,ClientID,QuoteID,DiscountRate,DateTime,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] WorkOrder workOrder)
        {
            if (ModelState.IsValid)
            {
                workOrder.ClientID = GetClientID();
                workOrder.DateTime = DateTime.Now;
                workOrder.ModifiedBy = "End User";
                workOrder.ModifiedDate = DateTime.Now;
                workOrder.CreatedBy = "End User";
                workOrder.CreatedDate = DateTime.Now;


                db.WorkOrders.Add(workOrder);
                db.SaveChanges();
                return RedirectToAction("OrderAddCompound");
            }

            ViewBag.ClientID = new SelectList(db.Clients, "ClientID", "CompanyName", workOrder.ClientID);
            ViewBag.QuoteID = new SelectList(db.QuoteEstimates, "QuoteID", "ModifiedBy", workOrder.QuoteID);
            return View(workOrder);
        }


        // GET: Client
        public ActionResult OrderAddCompound()
        {
            // Team.teamID = db.Teams.Max(t => t.teamID) + 1;
            // ViewBag.LTNumber = db.Compounds.Max(t => t.LTNumber) + 1;

            return View();
        }
        // POST: Compounds2/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderAddCompound([Bind(Include = "LTNumber,SequenceCode,CompoundName,Quantity,DateArrived,ReceivedBy,DateDue,Appearance,Weight,MolecularMass,ConfirmationDate,MaxTotalDose,ActualWeight,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] Compound compound)
        {
            if (ModelState.IsValid)
            {
                compound.LTNumber = db.Compounds.Max(t => t.LTNumber) + 1;
                compound.SequenceCode = 1;
                compound.ModifiedBy = "End User";
                compound.ModifiedDate = DateTime.Now;
                compound.CreatedBy = "End User";
                compound.CreatedDate = DateTime.Now;
                db.Compounds.Add(compound);
                db.SaveChanges();
                return RedirectToAction("WorkOrderMain");
            }

            return View(compound);
        }

        // GET: Client
        public ActionResult WorkOrderMain()
        {
            return View();
        }

        // GET: Client
        public ActionResult OrderAddAssayOrder()
        {
            

            return View();
        }
        // POST: Compounds2/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OrderAddAssayOrder([Bind(Include = "AssayOrderID,WorkOrderID,PriorityLevelID,AssayID,ModifiedBy,ModifiedDate,CreatedBy,CreatedDate")] AssayOrder assayorder)
        {
            if (ModelState.IsValid)
            {
               
                assayorder.ModifiedBy = "End User";
                assayorder.ModifiedDate = DateTime.Now;
                assayorder.CreatedBy = "End User";
                assayorder.CreatedDate = DateTime.Now;
                db.AssayOrders.Add(assayorder);
                db.SaveChanges();
                return RedirectToAction("WorkOrderMain");
            }

            return View(assayorder);
        }



    }
   }