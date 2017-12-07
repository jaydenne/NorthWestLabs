using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    public class AllEmployeeInfo
    {
        public Employee Employee { get; set; }
        public EmployeeBankInfo BankInfo{get; set;}
        public EmployeeWageInfo WageInfo { get; set; }
    }
}