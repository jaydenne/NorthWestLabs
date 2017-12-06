using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
        
    public class Test
    {
    
        [Key]
        public int TestID { get; set; }
        public int AssayID { get; set; }
        public string Name { get; set; }
        public int Hours { get; set; }
        public decimal BasePrice { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
        //public virtual ICollection<ConditionalApproval> ConditionalApprovals { get; set; }
        public virtual ProtocolNotebook ProtocolNotebook { get; set; }
        //public virtual ICollection<TestCondition> TestConditions { get; set; }
        //public virtual ICollection<TestCondition> TestConditions1 { get; set; }
        //public virtual ICollection<TestEquiptment> TestEquiptments { get; set; }
        //public virtual ICollection<TestMaterial> TestMaterials { get; set; }
        //public virtual ICollection<TestResult> TestResults { get; set; }
    }
}
