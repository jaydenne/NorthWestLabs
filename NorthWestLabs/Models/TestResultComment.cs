using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    [Table("TestResultComment")]
    public class TestResultComment
    {
        [Key]
        public int TestResultCommentsID { get; set; }
        public Nullable<int> TestResultsID { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        public virtual TestResult TestResult { get; set; }
    }
}
