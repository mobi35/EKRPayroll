namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DailyTimeRecords", "FirstTimeIn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DailyTimeRecords", "FirstTimeIn");
        }
    }
}
