
namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Deductions", "Remarks", c => c.String());
            AddColumn("dbo.Employees", "SickLeaveCredit", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "SickLeaveCredit");
            DropColumn("dbo.Deductions", "Remarks");
        }
    }
}
