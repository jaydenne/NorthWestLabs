using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using NorthWestLabs.DAL;
using NorthWestLabs.Models;

namespace NorthWestLabs.DAL
{
    public class NorthwestLabsContext : DbContext
    {
        public NorthwestLabsContext() : base("NorthwestDB")
        {

        }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<AssayOrder> AssayOrders { get; set; }

        public System.Data.Entity.DbSet<NorthWestLabs.Models.Compound> Compounds { get; set; }

        public System.Data.Entity.DbSet<NorthWestLabs.Models.WorkOrder> WorkOrders { get; set; }

        public System.Data.Entity.DbSet<NorthWestLabs.Models.QuoteEstimate> QuoteEstimates { get; set; }

        //public virtual System.Data.Entity.DbSet<NorthWestLabs.Models.AssayOrder> AssayOrders { get; set; }

        
        public System.Data.Entity.DbSet<NorthWestLabs.Models.Invoice> Invoices { get; set; }

        public System.Data.Entity.DbSet<NorthWestLabs.Models.Term> Terms { get; set; }

        object placeHolderVariable;
        public System.Data.Entity.DbSet<NorthWestLabs.Models.ProtocolNotebook> ProtocolNotebooks { get; set; }

        public System.Data.Entity.DbSet<NorthWestLabs.Models.Protocol> Protocols { get; set; }

        public System.Data.Entity.DbSet<NorthWestLabs.Models.Test> Tests { get; set; }

        /* public virtual DbSet<AssayOrder> AssayOrders { get; set; }
         public virtual DbSet<AssayOrderComment> AssayOrderComments { get; set; }
         public virtual DbSet<AssayOrderReport> AssayOrderReports { get; set; }
         public virtual DbSet<Authorization> Authorizations { get; set; }
         public virtual DbSet<Client> Clients { get; set; }
         public virtual DbSet<ClientComment> ClientComments { get; set; }
         public virtual DbSet<Compound> Compounds { get; set; }
         public virtual DbSet<ConditionalApproval> ConditionalApprovals { get; set; }
         public virtual DbSet<Country> Countries { get; set; }
         public virtual DbSet<Credit> Credits { get; set; }
         public virtual DbSet<CustomDiscount> CustomDiscounts { get; set; }
         public virtual DbSet<DiscountReference> DiscountReferences { get; set; }
         public virtual DbSet<Employee> Employees { get; set; }
         public virtual DbSet<EmployeeBankInfo> EmployeeBankInfoes { get; set; }
         public virtual DbSet<EmployeeWageInfo> EmployeeWageInfoes { get; set; }
         public virtual DbSet<Equiptment> Equiptments { get; set; }
         public virtual DbSet<InvioceItem> InvioceItems { get; set; }
         public virtual DbSet<Invoice> Invoices { get; set; }
         public virtual DbSet<LiteratureReference> LiteratureReferences { get; set; }
         public virtual DbSet<Material> Materials { get; set; }
         public virtual DbSet<Payment> Payments { get; set; }
         public virtual DbSet<PaymentData> PaymentDatas { get; set; }
         public virtual DbSet<PriorityLevel> PriorityLevels { get; set; }
         public virtual DbSet<Protocol> Protocols { get; set; }
         public virtual DbSet<ProtocolNotebook> ProtocolNotebooks { get; set; }
         public virtual DbSet<QuoteEstimate> QuoteEstimates { get; set; }
         public virtual DbSet<QuoteItem> QuoteItems { get; set; }
         public virtual DbSet<State> States { get; set; }
         public virtual DbSet<Status> Status { get; set; }
         public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
         public virtual DbSet<Term> Terms { get; set; }
         public virtual DbSet<TestCondition> TestConditions { get; set; }
         public virtual DbSet<TestEquiptment> TestEquiptments { get; set; }
         public virtual DbSet<TestMaterial> TestMaterials { get; set; }
         public virtual DbSet<TestReport> TestReports { get; set; }
         public virtual DbSet<TestResultComment> TestResultComments { get; set; }
         public virtual DbSet<TestResult> TestResults { get; set; }
         public virtual DbSet<Test> Tests { get; set; }
         public virtual DbSet<TestTube> TestTubes { get; set; }
         public virtual DbSet<WorkOrder> WorkOrders { get; set; }
         public virtual DbSet<WorkOrderComment> WorkOrderComments { get; set; }*/







    }
}