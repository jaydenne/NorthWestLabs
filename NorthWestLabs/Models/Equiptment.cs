using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    [Table("Equiptment")]
    public class Equiptment
    { 
        [Key]
        public int EquiptmentID { get; set; }
        public string EquiptmentName { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
        //public virtual ICollection<TestEquiptment> TestEquiptments { get; set; }
    }
}
