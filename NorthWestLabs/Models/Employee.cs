using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Position { get; set; }
        public string Location { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        public virtual Authorization Authorization { get; set; }
        public virtual EmployeeBankInfo EmployeeBankInfo { get; set; }
        public virtual EmployeeWageInfo EmployeeWageInfo { get; set; }
    }
}
