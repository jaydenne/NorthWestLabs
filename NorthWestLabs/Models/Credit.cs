using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    [Table("Credit")]
    public class Credit
    {
        [Key]
        public int CreditID { get; set; }
        public int? InvoiceID { get; set; }
        public int ClientID { get; set; }
        public System.DateTime CreditDate { get; set; }
        public string AuthorizedBy { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Invoice Invoice { get; set; }
    }
}
