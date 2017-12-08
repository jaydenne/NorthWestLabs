using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthWestLabs.Models;
using NorthWestLabs.DAL;

namespace NorthWestLabs.Controllers.Seattle
{
    [Authorize]
    public class ManageInvoicesController : Controller
    {
        NorthwestLabsContext db = new NorthwestLabsContext();
        // GET: Invoices
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewInvoice()
        {
            IEnumerable<WorkOrder> WorkOrderList = db.WorkOrders.ToList();
            IEnumerable<Invoice> InvoiceList = db.Invoices.ToList();
            List<WorkOrder> myWorkOrders = new List<WorkOrder>();

            foreach(WorkOrder order in WorkOrderList)
            {
                Boolean add = true;
                foreach(Invoice item in InvoiceList)
                {
                    if(order.WorkOrderID==item.WorkOrderID)
                    {
                        add = false;
                    }
                }
                if(add)
                {
                    myWorkOrders.Add(order);
                }
            }

            return View(myWorkOrders);
        }
        public ActionResult AddInvoice(int workorderid)
        {
            return View();
        }


       
    }
}