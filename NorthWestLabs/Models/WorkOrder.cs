using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using NorthWestLabs.Models;

namespace NorthWestLabs.Models
{
    [Table("WorkOrder")]
    public class WorkOrder
    {
        [Key]
        public int WorkOrderID { get; set; }
        public int ClientID { get; set; }
        public int? QuoteID { get; set; }
        public decimal DiscountRate { get; set; }
        public System.DateTime DateTime { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }


     
    }
}