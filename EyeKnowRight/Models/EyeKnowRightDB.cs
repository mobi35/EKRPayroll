using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeKnowRight.Models
{


    public partial class EyeKnowRightDB : DbContext
    {
        public EyeKnowRightDB()
            : base("name=EyeKnowRightDB")
        {
        }

        public virtual DbSet<DailyTimeRecord> DailyTimeRecords { get; set; }
        public virtual DbSet<Deductions> Deductionss { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Evaluation> Evaluations { get; set; }
        public virtual DbSet<Holiday> Holidays { get; set; }
        public virtual DbSet<Leave> Leaves { get; set; }
        public virtual DbSet<Loan> Loans { get; set; }
        public virtual DbSet<Memo> Memos { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Payroll> Payrolls { get; set; }
      
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
         
        }
    }
}
