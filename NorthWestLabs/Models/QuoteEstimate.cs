using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    [Table("QuoteEstimate")]    
    public class QuoteEstimate
    {
        [Key]
        public int QuoteID { get; set; }
        public int ClientID { get; set; }
        public Nullable<decimal> ExpectedDiscount { get; set; }
        public Nullable<System.DateTime> QuoteDate { get; set; }
        [DisplayName("Modified By")]
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        public virtual Client Client { get; set; }
        
        //public virtual ICollection<QuoteItem> QuoteItems { get; set; }
        
        //public virtual ICollection<WorkOrder> WorkOrders { get; set; }
    }
}
