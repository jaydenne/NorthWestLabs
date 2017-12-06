using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    public class WorkOrderWithDetails
    {
        public WorkOrder workOrder { get; set; }
        public List<AssayOrderWithTestResults> assayOrderWithTestResultsList = new List<AssayOrderWithTestResults>();
    }
}