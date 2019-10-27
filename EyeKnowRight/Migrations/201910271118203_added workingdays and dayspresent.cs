namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedworkingdaysanddayspresent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyTimeRecords", "WorkingDays", c => c.Int(nullable: false));
            AddColumn("dbo.DailyTimeRecords", "DaysPresent", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyTimeRecords", "DaysPresent");
            DropColumn("dbo.DailyTimeRecords", "WorkingDays");
        }
    }
}
