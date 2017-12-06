using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
   [Table("EmployeeWageInfo")]
    
    public class EmployeeWageInfo
    {
        [Key]
        public int EmloyeeID { get; set; }
        public Nullable<System.DateTime> LastIncreaseDate { get; set; }
        public Nullable<decimal> LastIncreaseAmount { get; set; }
        public Nullable<decimal> CurrentWage { get; set; }
        public string WageType { get; set; }
        public bool DirectDeposit { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        }
}
