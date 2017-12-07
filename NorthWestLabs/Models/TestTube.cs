using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Test Tube #")]
        public int TestTubeID { get; set; }
        [DisplayName("Test Tube Number")]
        public int TestTubeNumber { get; set; }
        [DisplayName("LT Number")]
        public int LTNumber { get; set; }
        [DisplayName("Sequence Code")]
        public int SequenceCode { get; set; }
        public string Concentration { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<TestResult> TestResults { get; set; }
    }
}
