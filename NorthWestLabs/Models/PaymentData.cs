using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    [Table("PaymentData")]    
    public class PaymentData
    {
        [Key]
        public int PaymentDataID { get; set; }
        public int ClientID { get; set; }
        public string BillingName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public Nullable<int> CC_4Digits { get; set; }
        public Nullable<System.DateTime> ExpDate { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
        public virtual Client Client { get; set; }
        
        //public virtual ICollection<Payment> Payments { get; set; }
    }
}
