using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    
    [Table("QuoteItem")]
    public class QuoteItem
    {
        [Key]
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
