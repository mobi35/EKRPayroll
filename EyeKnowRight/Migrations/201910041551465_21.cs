namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _21 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DailyTimeRecords", "TimeIn", c => c.DateTime());
            AlterColumn("dbo.DailyTimeRecords", "TimeOut", c => c.DateTime());
            AlterColumn("dbo.DailyTimeRecords", "DateTimeStamps", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.DailyTimeRecords", "DateTimeStamps", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DailyTimeRecords", "TimeOut", c => c.DateTime(nullable: false));
            AlterColumn("dbo.DailyTimeRecords", "TimeIn", c => c.DateTime(nullable: false));
        }
    }
}
