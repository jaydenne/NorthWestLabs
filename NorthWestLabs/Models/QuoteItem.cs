//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NorthWestLabs.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class QuoteItem
    {
        public int QuoteItemID { get; set; }
        public int QuoteID { get; set; }
        public int AssayID { get; set; }
        public decimal Cost { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
        public virtual ProtocolNotebook ProtocolNotebook { get; set; }
        public virtual QuoteEstimate QuoteEstimate { get; set; }
    }
}
