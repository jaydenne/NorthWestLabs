using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{

    [Table("DiscountReference")]
    public class DiscountReference
    {
        [Key]
        public int DiscountID { get; set; }
        public Nullable<int> VolumeMin { get; set; }
        public Nullable<decimal> DiscountRate { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    }
}
