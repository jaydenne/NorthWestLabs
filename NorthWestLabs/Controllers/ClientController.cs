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
        private NorthwestLabsEntitiesDB db = new NorthwestLabsEntitiesDB();

        // GET: Client
        public ActionResult Index()
        {
            return View();
        }



        // GET: Client
        public ActionResult ClientInfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //checks to make sure it is a valid client that has been entered
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return HttpNotFound();
            }

            return View(client);
        }


        




        // GET: Client
        public ActionResult ClientOrdersSummary()
        {

            List<WorkOrder> NewOrders = new List<WorkOrder>();
            NewOrders = db.WorkOrders.ToList();

            return View(NewOrders);
        }
        // GET: Client
        public ActionResult ClientOrderStatus(int? ordernum)
        {

            //need a list of assays where the work order is equal to the ordernum
            List<AssayOrder> assays = new List<AssayOrder>();

            IEnumerable<AssayOrder> AssayList = db.AssayOrders.ToList();
            foreach (AssayOrder item in AssayList)
            {
                if (item.WorkOrderID == ordernum)
                {
                    assays.Add(item);
                }
            }



           // WorkOrder workorder = db.WorkOrders.Find(ordernum);
           
            return View(assays);
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

    }
}