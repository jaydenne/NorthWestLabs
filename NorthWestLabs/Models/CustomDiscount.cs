
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    [Table("CustomDiscount")]
    
    public class CustomDiscount
    {
        [Key]
        public int CustomDiscountID { get; set; }
        public Nullable<int> WorkOrderID { get; set; }
        public string Description { get; set; }
        public Nullable<decimal> DiscountAmount { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        public virtual WorkOrder WorkOrder { get; set; }
    }
}
