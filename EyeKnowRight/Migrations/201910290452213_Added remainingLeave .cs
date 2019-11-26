namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedremainingLeave : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "RemainingLeave", c => c.Int(nullable: false));
        }
        public override void Down()
        {
            DropColumn("dbo.Employees", "RemainingLeave");
        }
    }
}
