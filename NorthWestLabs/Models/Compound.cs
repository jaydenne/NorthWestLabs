using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Sequence Code")]
        public int SequenceCode { get; set; }
        [DisplayName ("Compound Name")]
        public string CompoundName { get; set; }
        public Nullable<int> Quantity { get; set; }
        [DisplayName("Date Arrived")]
        public Nullable<System.DateTime> DateArrived { get; set; }
        [DisplayName("Received By")]
        public string ReceivedBy { get; set; }
        [DisplayName("Date Due")]
        public Nullable<System.DateTime> DateDue { get; set; }
        public string Appearance { get; set; }
        public Nullable<decimal> Weight { get; set; }
        [DisplayName("Molecular Mass")]
        public Nullable<decimal> MolecularMass { get; set; }
        [DisplayName("Confirmation Date")]
        public Nullable<System.DateTime> ConfirmationDate { get; set; }
        [DisplayName("Max Total Dose")]
        public Nullable<decimal> MaxTotalDose { get; set; }
        [DisplayName("Actual Weight")]
        public Nullable<decimal> ActualWeight { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
    }
}
