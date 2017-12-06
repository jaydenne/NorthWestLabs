using System;
using System.Collections.Generic;
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
        public int ClientID { get; set; }
        public string CompanyName { get; set; }
        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int? Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string ModifiedBy { get; set; }
        public System.DateTime ModifiedDate { get; set; }
        public string CreatedBy { get; set; }
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
