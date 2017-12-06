using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{

    [Table("ProtocolNotebook")]
    public class ProtocolNotebook
    {
    
        [Key]
        public int AssayID { get; set; }
        public string AssayName { get; set; }
        public Nullable<int> ProtocolID { get; set; }
        public Nullable<int> EstDaysToComplete { get; set; }
        public string Description { get; set; }
        public Nullable<bool> RequireAnimalTesting { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public string ImageLocation { get; set; }
    
        //public virtual ICollection<AssayOrder> AssayOrders { get; set; }
        //public virtual ICollection<ConditionalApproval> ConditionalApprovals { get; set; }
        //public virtual ICollection<InvioceItem> InvioceItems { get; set; }
        //public virtual ICollection<LiteratureReference> LiteratureReferences { get; set; }
        public virtual Protocol Protocol { get; set; }
        //public virtual ICollection<QuoteItem> QuoteItems { get; set; }
        //public virtual ICollection<Test> Tests { get; set; }
    }
}
