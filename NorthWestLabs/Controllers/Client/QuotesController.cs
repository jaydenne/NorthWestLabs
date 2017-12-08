using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NorthWestLabs.DAL;
using NorthWestLabs.Models;
using System.Net.Mail;
using System.Net;

namespace NorthWestLabs.Controllers
{
    [Authorize]
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
            ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName");
            QuoteEstimateWithItemTemplate myModel = new QuoteEstimateWithItemTemplate();
            myModel.Quote = new QuoteEstimate();
            myModel.Quote.QuoteID=0;

            return View(myModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult NewQuote(int? QuoteID, [Bind(Include = "QuoteID,AssayID")] QuoteItem quoteItem)
        {
            QuoteEstimate MyQuote = new QuoteEstimate();
            if(QuoteID == null||QuoteID==0)
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
            else
            {
                MyQuote.QuoteID = (int)QuoteID;
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
            MyQuote.QuoteItems = new List<QuoteItem>();
            foreach (QuoteItem item in items)
            {
                if (item.QuoteID == MyQuote.QuoteID)
                {
                    item.ProtocolNotebook = db.ProtocolNotebooks.Find(item.AssayID);
                    MyQuote.QuoteItems.Add(item);
                }
            }

                ViewBag.AssayID = new SelectList(db.ProtocolNotebooks, "AssayID", "AssayName", quoteItem.AssayID);

            QuoteEstimateWithItemTemplate myModel = new QuoteEstimateWithItemTemplate();
            myModel.Quote = MyQuote;

            return View(myModel);
        }

        public async System.Threading.Tasks.Task<ActionResult> Submit(int QuoteID)
        {
            QuoteEstimate MyQuote = db.QuoteEstimates.Find(QuoteID);
            IEnumerable<QuoteItem> items = db.QuoteItems.ToList();
            MyQuote.QuoteItems = new List<QuoteItem>();
            foreach (QuoteItem item in items)
            {
                if (item.QuoteID == MyQuote.QuoteID)
                {
                    item.ProtocolNotebook = db.ProtocolNotebooks.Find(item.AssayID);
                    MyQuote.QuoteItems.Add(item);

                }
            }
            MyQuote.Client = db.Clients.Find(MyQuote.ClientID);
            int iCount = 0;
            var body ="<h3>Quote "+MyQuote.QuoteID+"</h3>"+
                "<h4>Northwest Labs</h4>"+
                "<p>Quote prepared for <b>"+MyQuote.Client.CompanyName+"</b></p>"+
                "<h4>"+MyQuote.QuoteDate+"</h4>"+
                "<table class='table'>"+
                    "<tr>" +
                     "<th></th>" +
                      "<th>Assay Name</th>" +
                       "<th>Description</th>"+
                        "<th>Estimated Price</th>"+
                    "</tr>";
                    foreach (var item in MyQuote.QuoteItems)
                            {
                                iCount++;
                body += "<tr>" +
                    "<td>" + iCount + "</td>" +
                    "<td>" + item.ProtocolNotebook.AssayName + "</td>" +
                    "<td>" + item.ProtocolNotebook.Description + "</td>" +
                    "<td>$" + item.Cost + " (Does not include conditional tests)</td>" +
                "</tr>";
                    }
            body += "</table>";

            var message = new MailMessage();
            message.To.Add(new MailAddress(db.Clients.Find(GetClientID()).Email));  // replace with valid value 
            message.From = new MailAddress("J.nelson55@gmail.com", "Northwest Labs");  // replace with valid value
            message.Subject = "Northwest Labs - Quote Request";
            message.Body = string.Format(body);
            message.IsBodyHtml = true;
            //message.Attachments() add attachments

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = "J.nelson55@gmail.com",  // replace with valid value
                    Password = "********"  // replace with valid value
                };
                smtp.Credentials = credential;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.EnableSsl = true;
                await smtp.SendMailAsync(message);
            }
           return View(MyQuote);
        }
        public ActionResult Summary()
        {
            IEnumerable<QuoteEstimate> QuoteList = db.QuoteEstimates.ToList();
            IEnumerable<QuoteItem> QuoteItemList = db.QuoteItems.ToList();
            int ClientID = GetClientID();

            List<QuoteEstimate> ClientQuote = new List<QuoteEstimate>();
            foreach(QuoteEstimate quote in QuoteList)
            {
                if(ClientID==quote.ClientID)
                {
                    ClientQuote.Add(quote);
                }
            }
            foreach(QuoteEstimate quote in ClientQuote)
            {
                quote.QuoteItems = new List<QuoteItem>();
                foreach (QuoteItem item in QuoteItemList)
                {
                    if(item.QuoteID==quote.QuoteID)
                    {
                        quote.QuoteItems.Add(item);
                    }
                }
            }
            return View(QuoteList);
        }
    }
}
