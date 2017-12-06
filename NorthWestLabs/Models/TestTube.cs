using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    [Table("TestTube")]
    
    public class TestTube
    {
       
        [Key]
        public int TestTubeID { get; set; }
        [Key]
        public int LTNumber { get; set; }
        [Key]
        public int SequenceCode { get; set; }
        public string Concentration { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TestResult> TestResults { get; set; }
    }
}
