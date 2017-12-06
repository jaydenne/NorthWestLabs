using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
   [Table("TestCondition")] 
    public class TestCondition
    {
        [Key]
        public int TestConditionsID { get; set; }
        public Nullable<int> TestID { get; set; }
        public Nullable<int> ConditionalOnID { get; set; }
        public string Condition { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
        public virtual Test Test { get; set; }
        public virtual Test Test1 { get; set; }
    }
}
