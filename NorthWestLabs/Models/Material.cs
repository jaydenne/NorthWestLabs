using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
   [Table("Material")]
    
    public class Material
    {
        [Key]
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public Nullable<decimal> PricePerUnit { get; set; }
        public string UnitName { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
    
        //public virtual ICollection<TestMaterial> TestMaterials { get; set; }
    }
}
