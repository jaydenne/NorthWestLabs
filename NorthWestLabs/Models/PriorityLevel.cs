using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
   
    [Table("PriorityLevel")]
    public class PriorityLevel
    {

        [Key]    
        public int PriorityLevelID { get; set; }
        public decimal PriorityRate { get; set; }
        public int WorkDaysProcessing { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
      //  public virtual ICollection<AssayOrder> AssayOrders { get; set; }
    }
}
