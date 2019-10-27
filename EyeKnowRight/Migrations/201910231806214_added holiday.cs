namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedholiday : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Holidays", "Month", c => c.DateTime());
            AddColumn("dbo.Holidays", "HolidayName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Holidays", "HolidayName");
            DropColumn("dbo.Holidays", "Month");
        }
    }
}
