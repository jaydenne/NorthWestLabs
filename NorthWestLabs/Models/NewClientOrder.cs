using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    public class NewClientOrder
    {
        public WorkOrder workOrder { get; set; }
        public List<CompoundAssayOrder> compoundAssayOrderList = new List<CompoundAssayOrder>();
        public CompoundAssayOrder compoundAssayOrderTemplate { get; set; }

    }
}