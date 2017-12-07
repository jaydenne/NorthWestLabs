using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    public class AccountInvoiceData
    {
        public Invoice Invoice { get; set; }
        public List<InvioceItem> invoiceitems = new List<InvioceItem>(); //{ get; set; }

    }
}