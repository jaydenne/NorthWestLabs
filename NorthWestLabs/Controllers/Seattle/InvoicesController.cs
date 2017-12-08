using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NorthWestLabs.Controllers.Seattle
{
    public class InvoicesController : Controller
    {
        // GET: Invoices
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewInvoice()
        {

            return View();
        }
        [HttpPost]
        public ActionResult NewInvoice(int )


       
    }
}