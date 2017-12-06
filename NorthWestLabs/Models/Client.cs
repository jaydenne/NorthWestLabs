using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace NorthWestLabs.Models
{
    [Table("Client")]
    public class Client
    {

        [Key]
        [DisplayName("Client ID")]
        public int ClientID { get; set; }
        [DisplayName ("Company Name")]
        public string CompanyName { get; set; }
        [DisplayName("Contact First Name")]
        public string ContactFirstName { get; set; }
        [DisplayName("Contact Last Name")]
        public string ContactLastName { get; set; }
        [DisplayName("Address 1")]
        public string Address1 { get; set; }
        [DisplayName("Address 2")]
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int? Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        [DisplayName("Password Hash")]
        public string PasswordHash { get; set; }
        [DisplayName("Modified By")]
        public string ModifiedBy { get; set; }
        [DisplayName("Modified Date")]
        public System.DateTime ModifiedDate { get; set; }
        [DisplayName("Created By")]
        public string CreatedBy { get; set; }
        [DisplayName("Created Date")]
        public System.DateTime CreatedDate { get; set; }

/*
        public virtual ICollection<ClientComment> ClientComments { get; set; }
        public virtual ICollection<ConditionalApproval> ConditionalApprovals { get; set; }
        public virtual ICollection<Credit> Credits { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<PaymentData> PaymentDatas { get; set; }
        public virtual ICollection<QuoteEstimate> QuoteEstimates { get; set; }
        public virtual ICollection<WorkOrder> WorkOrders { get; set; }
        */


    }
}
