using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    [Table("State")]
    
    public class State
    {
        [Key]
        public string StateCode { get; set; }
        public string StateName { get; set; }
    }
}
