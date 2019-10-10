namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyTimeRecords", "UserName", c => c.String());
            AddColumn("dbo.DailyTimeRecords", "TimeIn", c => c.DateTime());
            AddColumn("dbo.DailyTimeRecords", "TimeOut", c => c.DateTime());
            AddColumn("dbo.DailyTimeRecords", "Accumulated", c => c.Double(nullable: false));
            AddColumn("dbo.DailyTimeRecords", "Late", c => c.Double(nullable: false));
            AddColumn("dbo.DailyTimeRecords", "OverTime", c => c.Double(nullable: false));
            AddColumn("dbo.DailyTimeRecords", "DateTimeStamps", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyTimeRecords", "DateTimeStamps");
            DropColumn("dbo.DailyTimeRecords", "OverTime");
            DropColumn("dbo.DailyTimeRecords", "Late");
            DropColumn("dbo.DailyTimeRecords", "Accumulated");
            DropColumn("dbo.DailyTimeRecords", "TimeOut");
            DropColumn("dbo.DailyTimeRecords", "TimeIn");
            DropColumn("dbo.DailyTimeRecords", "UserName");
        }
    }
}
