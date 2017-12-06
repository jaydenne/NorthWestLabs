using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
   [Table("ClientComment")]
    public class ClientComment
    {
        [Key]
        public int ClientCommentsID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public string Title { get; set; }
        public string Comment { get; set; }
        public string ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        public virtual Client Client { get; set; }
    }
}
