using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    public class AssayOrderWithTestResults
    {
        public AssayOrder AssayOrder { get; set; }
        public List<TestResult> testResults = new List<TestResult>(); //{ get; set; }
    }
}