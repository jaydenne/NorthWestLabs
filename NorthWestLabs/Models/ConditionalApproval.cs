using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    
    [Table("ConditionalApproval")]
    public class ConditionalApproval
    {
        [Key]
        public int ConditionalApprovalID { get; set; }
        public int? WorkOrderID { get; set; }
        public int? AssayID { get; set; }
        public int? TestID { get; set; }
        public int? ClientID { get; set; }
        public Nullable<System.DateTime> ApprovalDate { get; set; }
        public string ApproverName { get; set; }
        public string Comments { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual ProtocolNotebook ProtocolNotebook { get; set; }
        public virtual Test Test { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }
    }
}
