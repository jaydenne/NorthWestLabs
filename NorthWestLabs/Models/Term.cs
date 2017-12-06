using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    
    [Table("Term")]
    public class Term
    {
        [Key]    
        public int TermsID { get; set; }
        public Nullable<decimal> DiscountRate { get; set; }
        public Nullable<int> DiscountDays { get; set; }
        public Nullable<int> FullAmountDays { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        
        //public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
