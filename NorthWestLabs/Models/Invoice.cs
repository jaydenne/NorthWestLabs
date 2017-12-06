using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    
    [Table("Invoice")]
    public class Invoice
    {
        
        [Key]   
        public int InvoiceID { get; set; }
        public int ClientID { get; set; }
        public Nullable<System.DateTime> InvoiceDate { get; set; }
        public int TermsID { get; set; }
        public int WorkOrderID { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Term Term { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }

        // public virtual ICollection<Credit> Credits { get; set; }
        //public virtual ICollection<InvioceItem> InvioceItems { get; set; }
        // public virtual ICollection<Payment> Payments { get; set; }
    }
}
