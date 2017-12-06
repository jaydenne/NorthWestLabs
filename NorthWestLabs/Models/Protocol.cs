using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
   [Table("Protocol")]
    
    public class Protocol
    {
        
        [Key]
        public int ProtocolID { get; set; }
        public string ProtocolName { get; set; }
        public string ProtocolDescription { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<ProtocolNotebook> ProtocolNotebooks { get; set; }
    }
}
