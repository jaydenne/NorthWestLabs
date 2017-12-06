using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{

    [Table("AssayOrder")]
    public class AssayOrder
    {
        [Key]
        public int AssayOrderID { get; set; }
        public int WorkOrderID { get; set; }
        public int PriorityLevelID { get; set; }
        public int AssayID { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
       public virtual PriorityLevel PriorityLevel { get; set; }
        public virtual ProtocolNotebook ProtocolNotebook { get; set; }
        public virtual WorkOrder WorkOrder { get; set; }

       
       // public virtual ICollection<AssayOrderComment> AssayOrderComments { get; set; }
       // public virtual ICollection<AssayOrderReport> AssayOrderReports { get; set; }
       // public virtual ICollection<TestResult> TestResults { get; set; }
 
    }
}
