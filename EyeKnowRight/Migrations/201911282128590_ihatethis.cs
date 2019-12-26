namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ihatethis : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notifications", "NotificationToWho", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "NotificationToWho");
        }
    }
}
