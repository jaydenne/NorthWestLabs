using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthWestLabs.Models;
using NorthWestLabs.DAL;

namespace NorthWestLabs.Controllers.Seattle
{
    public class TestReportsController : Controller
    {
        // GET: TestReports

        NorthwestLabsContext db = new NorthwestLabsContext();

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult WorkOrderReport()
        {


            return View(db.WorkOrders.ToList());
        }
        public ActionResult CreateWorkOrderReport(int workorderid)
        {
            IEnumerable<AssayOrder> assayOrderList = db.AssayOrders.ToList();
            IEnumerable<TestResult> ResultsList = db.TestResults.ToList();
            List<AssayOrder> myAssays = new List<AssayOrder>();
            WorkOrderWithDetails myOrder = new WorkOrderWithDetails();
            myOrder.workOrder = db.WorkOrders.Find(workorderid);
            myOrder.workOrder.Client = db.Clients.Find(myOrder.workOrder.ClientID);
            foreach(AssayOrder assay in assayOrderList)
            {
                if(assay.WorkOrderID==workorderid)
                {
                    AssayOrderWithTestResults myItem = new AssayOrderWithTestResults();
                    myItem.AssayOrder = assay;
                    foreach(TestResult myTest in ResultsList)
                    {
                        if(myTest.AssayOrderID==assay.AssayOrderID)
                        {
                            myItem.testResults.Add(myTest);
                        }
                    }
                    myOrder.assayOrderWithTestResultsList.Add(myItem);
                }
            }
            return View(myOrder);
        }


        public ActionResult CustomReport()
        {

            return View(new RichTextEditorViewModel());
        }
        [HttpPost]
        public ActionResult CustomReport(RichTextEditorViewModel richtext)
        {

            return RedirectToAction("CustomReportPrint",richtext);
        }
        public ActionResult CustomReportPrint(RichTextEditorViewModel richText)
        {
            return View(richText);
        }
    }
}