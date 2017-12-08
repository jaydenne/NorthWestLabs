using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    public class QuoteEstimateWithItemTemplate
    {
        public QuoteEstimate Quote { get; set; }
        public QuoteItem template { get; set; }
    }
}