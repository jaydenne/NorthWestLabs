using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    [Table("LiteratureReference")]
   
    public class LiteratureReference
    {
        [Key]
        public int LiteratureReferenceID { get; set; }
        public Nullable<int> AssayID { get; set; }
        public string ReferenceName { get; set; }
        public string Reference { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
        public virtual ProtocolNotebook ProtocolNotebook { get; set; }
    }
}
