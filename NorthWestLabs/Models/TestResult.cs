using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    [Table("TestResults")]
    public class TestResult
    {
        [Key]
        public int TestResultsID { get; set; }
        public int TestID { get; set; }
        public int AssayOrderID { get; set; }
        public int TestTubeID { get; set; }
        public int LTNumber { get; set; }
        public Nullable<System.DateTime> ScheduleStart { get; set; }
        public byte[] TextFile { get; set; }
        public byte[] NumericFile { get; set; }
        public int StatusID { get; set; }
        public System.DateTime StatusUpdatedDate { get; set; }
        public bool RunAgain { get; set; }
        public int RunNumber { get; set; }
        public string ModifedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
        public virtual AssayOrder AssayOrder { get; set; }
        public virtual Status Status { get; set; }
        
        //public virtual ICollection<TestResultComment> TestResultComments { get; set; }
        public virtual Test Test { get; set; }
        public virtual TestTube TestTube { get; set; }
    }
}
