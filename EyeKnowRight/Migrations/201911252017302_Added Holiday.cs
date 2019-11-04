namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedHoliday : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Holidays", "SalaryInrease", c => c.Single(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Holidays", "SalaryInrease");
        }
    }
}
