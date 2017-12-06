using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    
    [Table("Compound")]
    public class Compound
    {

        [Key]
        public int CompoundID { get; set; }
        public int LTNumber { get; set; }
        public int SequenceCode { get; set; }
        public string CompoundName { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<System.DateTime> DateArrived { get; set; }
        public string ReceivedBy { get; set; }
        public Nullable<System.DateTime> DateDue { get; set; }
        public string Appearance { get; set; }
        public Nullable<decimal> Weight { get; set; }
        public Nullable<decimal> MolecularMass { get; set; }
        public Nullable<System.DateTime> ConfirmationDate { get; set; }
        public Nullable<decimal> MaxTotalDose { get; set; }
        public Nullable<decimal> ActualWeight { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}
