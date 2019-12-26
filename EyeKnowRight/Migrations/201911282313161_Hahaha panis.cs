namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Hahahapanis : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyTimeRecords", "leaveEarned", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Employees", "MaternityLeave", c => c.Single(nullable: false));
            AlterColumn("dbo.Employees", "PaternityLeave", c => c.Single(nullable: false));
            AlterColumn("dbo.Employees", "SickLeave", c => c.Single(nullable: false));
            AlterColumn("dbo.Employees", "BereavementLeave", c => c.Single(nullable: false));
            AlterColumn("dbo.Employees", "MedicalLeave", c => c.Single(nullable: false));
            AlterColumn("dbo.Employees", "PersonalLeave", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employees", "PersonalLeave", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "MedicalLeave", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "BereavementLeave", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "SickLeave", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "PaternityLeave", c => c.Int(nullable: false));
            AlterColumn("dbo.Employees", "MaternityLeave", c => c.Int(nullable: false));
            DropColumn("dbo.DailyTimeRecords", "leaveEarned");
        }
    }
}
