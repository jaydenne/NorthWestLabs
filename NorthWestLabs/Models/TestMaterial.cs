using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    [Table("TestMaterial")]
    
    public class TestMaterial
    {
        [Key]
        public int TestID { get; set; }
        public int MaterialID { get; set; }
        public Nullable<int> UnitsRequired { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
        public virtual Material Material { get; set; }
        public virtual Test Test { get; set; }
    }
}
