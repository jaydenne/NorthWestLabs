using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    
    [Table("WorkOrder")]
    public class WorkOrder
    {
       [Key]   
        public int WorkOrderID { get; set; }
        public int ClientID { get; set; }
        public Nullable<int> QuoteID { get; set; }
        [DisplayName("Discount Rate")]
        public decimal DiscountRate { get; set; }
        [DisplayName("Date")]
        public System.DateTime DateTime { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
       // public virtual ICollection<AssayOrder> AssayOrders { get; set; }
        public virtual Client Client { get; set; }
       // public virtual ICollection<ConditionalApproval> ConditionalApprovals { get; set; }
      //  public virtual ICollection<CustomDiscount> CustomDiscounts { get; set; }
       // public virtual ICollection<InvioceItem> InvioceItems { get; set; }
       // public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual QuoteEstimate QuoteEstimate { get; set; }
        //public virtual ICollection<WorkOrderComment> WorkOrderComments { get; set; }
    }
}
