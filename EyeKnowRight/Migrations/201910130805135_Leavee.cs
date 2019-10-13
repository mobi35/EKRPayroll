namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Leavee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "MaternityLeave", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "PaternityLeave", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "SickLeave", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "BereavementLeave", c => c.Int(nullable: false));
            AddColumn("dbo.Employees", "MedicalLeave", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "MedicalLeave");
            DropColumn("dbo.Employees", "BereavementLeave");
            DropColumn("dbo.Employees", "SickLeave");
            DropColumn("dbo.Employees", "PaternityLeave");
            DropColumn("dbo.Employees", "MaternityLeave");
        }
    }
}
