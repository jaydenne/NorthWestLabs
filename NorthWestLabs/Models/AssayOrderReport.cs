using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    
    [Table("AssayOrderReport")]
    public class AssayOrderReport
    {
        [Key]
        public int AssayOrderReportID { get; set; }
        public int AssayOrderID { get; set; }
        public byte[] AttachedFile { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
        public virtual AssayOrder AssayOrder { get; set; }
    }
}
