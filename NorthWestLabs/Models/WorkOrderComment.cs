using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    [Table("WorkOrderComment")]   
    public partial class WorkOrderComment
    {
        [Key]
        [DisplayName("Work Order Comment ID")]
        public int WorkOrderCommentID { get; set; }
        [DisplayName("Work Order Number")]
        public int WorkOrderID { get; set; }
        [DisplayName("Comment Type")]
        public string CommentType { get; set; }
        public string Comment { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        [DisplayName("Work Order")]
        public virtual WorkOrder WorkOrder { get; set; }
    }
}
