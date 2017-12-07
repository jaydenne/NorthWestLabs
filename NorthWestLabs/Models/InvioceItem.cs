using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    [Table("InvioceItem")]
    public class InvioceItem
    {
        [Key]
        public int InvoiceItemID { get; set; }
        public int InvoiceID { get; set; }
        public int AssayID { get; set; }
        public int WorkOrderID { get; set; }
        public Nullable<decimal> Amount { get; set; }
        public string Modified { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
        public virtual Invoice Invoice { get; set; }
        public virtual ProtocolNotebook ProtocolNotebook { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }
    }
}
