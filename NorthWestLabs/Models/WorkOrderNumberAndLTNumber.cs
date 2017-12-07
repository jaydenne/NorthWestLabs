using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    public class WorkOrderNumberAndLTNumber
    {
        public int WorkOrderNumber { get; set; }
        public int LTNumber { get; set; }
        public CompoundAssayOrder template { get; set; }
    }
}