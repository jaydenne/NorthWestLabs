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
        public DbSet<AssayOrder> AssayOrders { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<TestResult> TestResults { get; set; }
        public virtual DbSet<InvioceItem> InvoiceItems { get; set; }
        public virtual DbSet<Term> Terms { get; set; }
        public virtual DbSet<TestTube> TestTubes { get; set; }
        public System.Data.Entity.DbSet<NorthWestLabs.Models.Compound> Compounds { get; set; }

        public System.Data.Entity.DbSet<NorthWestLabs.Models.WorkOrder> WorkOrders { get; set; }

        public System.Data.Entity.DbSet<NorthWestLabs.Models.QuoteEstimate> QuoteEstimates { get; set; }

      
        public System.Data.Entity.DbSet<NorthWestLabs.Models.Invoice> Invoices { get; set; }

      //  public System.Data.Entity.DbSet<NorthWestLabs.Models.Term> Terms { get; set; }

        public System.Data.Entity.DbSet<NorthWestLabs.Models.ProtocolNotebook> ProtocolNotebooks { get; set; }

        public System.Data.Entity.DbSet<NorthWestLabs.Models.Protocol> Protocols { get; set; }

        public System.Data.Entity.DbSet<NorthWestLabs.Models.Test> Tests { get; set; }

        public System.Data.Entity.DbSet<NorthWestLabs.Models.PriorityLevel> PriorityLevels { get; set; }

        public System.Data.Entity.DbSet<NorthWestLabs.Models.EmployeeWageInfo> EmployeeWageInfoes { get; set; }

        public System.Data.Entity.DbSet<NorthWestLabs.Models.EmployeeBankInfo> EmployeeBankInfoes { get; set; }
    }
}