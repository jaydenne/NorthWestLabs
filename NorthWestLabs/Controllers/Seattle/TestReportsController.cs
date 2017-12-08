using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthWestLabs.Models;
using NorthWestLabs.DAL;
using System.Net.Mail;
using System.Net;

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
        public async System.Threading.Tasks.Task<ActionResult> CreateWorkOrderReport(int workorderid)
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






   var body = "<div style='width:800px;margin-left:auto; margin-right:auto;'>" +
    "<div style='text-align:center;'>" +
        "<h2> Northwest Labs</h2>" +
        "<h3> Order Number: " + myOrder.workOrder.WorkOrderID + "</h3>" +
    "</div>" +
    "<div>" +
        "<p><b>Client Name:</b> " + myOrder.workOrder.Client.CompanyName + "</p>" +
        "<p><b>Address Line 1:</b> " + myOrder.workOrder.Client.Address1 + "</p>" +
        "<p><b>Address Line 2:</b> " + myOrder.workOrder.Client.Address2 + "</p>" +
        "<p><b>Contact Name:</b> " + myOrder.workOrder.Client.ContactFirstName + " " + myOrder.workOrder.Client.ContactLastName + "</p>" +
        "<p><b>Phone:</b> " + myOrder.workOrder.Client.Phone + "</p>" +
        "<p><b>Email:</b> " + myOrder.workOrder.Client.Email + "</p>" +
    "</div>" +
    "<div>";
            foreach(NorthWestLabs.Models.AssayOrderWithTestResults item in myOrder.assayOrderWithTestResultsList)
            {
                body += "<h3>Compound ID:</h3> " + item.AssayOrder.CompoundID +
                "<div>";
                foreach (NorthWestLabs.Models.TestResult result in item.testResults)
                {
                    body+="<ul>"+
                        "<li>Status: "+result.Status.StatusName+"</li>"+
                        "<li>Test Tube: "+result.TestTubeID+"</li>"+
                        "<li>Test ID: "+result.TestID+"</li>" +
                    "</ul>";

                }
                body += "</div>" +
                "<br />";
            }
            body += "</div><div></div></div>";






            var message = new MailMessage();
            message.To.Add(new MailAddress(myOrder.workOrder.Client.Email));  // replace with valid value 
            message.From = new MailAddress("J.nelson55@gmail.com", "Northwest Labs");  // replace with valid value
            message.Subject = "Northwest Labs - Work Order Report";
            message.Body = string.Format(body);
            message.IsBodyHtml = true;
            //message.Attachments() add attachments

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "J.nelson55@gmail.com",  // replace with valid value
                    Password = "insertpasswordhere"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
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