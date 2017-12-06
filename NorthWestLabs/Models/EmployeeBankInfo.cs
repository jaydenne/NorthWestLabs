using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
   
    [Table("EmployeeBankInfo")]
    public class EmployeeBankInfo
    {
        [Key]
        public int EmployeeBankInfoID { get; set; }
        public int EmployeeID { get; set; }
        public string BankAccount { get; set; }
        public string RoutingNumber { get; set; }
        public string AccountType { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        }
}
