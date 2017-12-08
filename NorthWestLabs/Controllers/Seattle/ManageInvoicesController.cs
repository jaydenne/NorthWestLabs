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
            IEnumerable<Test> testList = db.Tests.ToList();
            IEnumerable<AssayOrder> assayOrderList = db.AssayOrders.ToList();           
            Invoice myInvoice = new Invoice();
            WorkOrder myWorkOrder = db.WorkOrders.Find(workorderid);
            myInvoice.InvoiceDate = DateTime.Now;
            myInvoice.ClientID = myWorkOrder.ClientID;
            myInvoice.WorkOrderID = myWorkOrder.WorkOrderID;
            myInvoice.InvoiceDate = DateTime.Now;
            myInvoice.ModifiedDate = DateTime.Now;
            myInvoice.TermsID = 2;//Set function to allow user to change terms
            db.Invoices.Add(myInvoice);

            foreach(AssayOrder item in assayOrderList)
            {
                if(item.WorkOrderID==myInvoice.WorkOrderID)
                {
                    InvioceItem myItem = new InvioceItem();
                    myItem.WorkOrderID = item.WorkOrderID;
                    myItem.InvoiceID = myInvoice.InvoiceID;
                    myItem.AssayID = item.AssayID;
                    myItem.Amount = 0;
                    myItem.CreatedDate = DateTime.Now;
                    myItem.ModifiedDate = DateTime.Now;
                    foreach(Test myTest in testList)
                    {
                        if(myTest.AssayID==myItem.AssayID)
                        {
                            myItem.Amount+=(myTest.BasePrice+(myTest.Hours*40));
                        }
                    }
                    db.InvoiceItems.Add(myItem);
                }
            }
            db.SaveChanges();

            return RedirectToAction("Index");
        }


       
    }
}