using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    [Table("AssayOrderComment")]  
    public class AssayOrderComment
    {
        [Key]
        public int AssayOrderCommentsID { get; set; }
        public int AssayOrderID { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModfiedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public virtual AssayOrder AssayOrder { get; set; }
    }
}
