using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    [Table("Authorization")]
    public class Authorization
    {
        [Key]
        public int AuthorizationID { get; set; }
        public int EmployeeID { get; set; }
        public int LevelID { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        }
}
