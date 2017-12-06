using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    [Table("Payment")]    
    public class Payment
    {
        [Key]
        public int PaymentID { get; set; }
        public Nullable<int> InvoiceID { get; set; }
        public int ClientID { get; set; }
        public int PaymentDataID { get; set; }
        public Nullable<decimal> CCAmount { get; set; }
        public Nullable<decimal> CheckAmount { get; set; }
        public Nullable<decimal> BankTransferAmount { get; set; }
        public Nullable<System.DateTime> PaymentDate { get; set; }
        public string RecordedBy { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Invoice Invoice { get; set; }
        public virtual PaymentData PaymentData { get; set; }
    }
}
