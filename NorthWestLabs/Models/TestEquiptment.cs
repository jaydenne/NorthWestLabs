using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    [Table("TestEquiptment")]
    public class TestEquiptment
    {
        [Key]
        public int TestID { get; set; }
        [Key]
        public int EquiptmentID { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        public virtual Equiptment Equiptment { get; set; }
        public virtual Test Test { get; set; }
    }
}
