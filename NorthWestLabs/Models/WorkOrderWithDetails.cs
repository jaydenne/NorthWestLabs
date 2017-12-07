using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    public class WorkOrderWithDetails
    {
        [DisplayName("Work Order")]
        public WorkOrder workOrder { get; set; }
        public List<AssayOrderWithTestResults> assayOrderWithTestResultsList = new List<AssayOrderWithTestResults>();
    }
}