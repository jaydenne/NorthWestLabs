using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    public class CompoundAssayOrder
    {
        public Compound compound { get; set; }
        public AssayOrder assayOrder { get; set; }
    }
}