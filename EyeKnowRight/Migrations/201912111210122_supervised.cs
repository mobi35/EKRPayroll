namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class supervised : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "SupervisedDepartment", c => c.String());
            AddColumn("dbo.Notifications", "UserName", c => c.String());
            AddColumn("dbo.Notifications", "Message", c => c.String());
            AddColumn("dbo.Notifications", "IsRead", c => c.Boolean(nullable: false));
            AddColumn("dbo.Notifications", "NotifiedOn", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notifications", "NotifiedOn");
            DropColumn("dbo.Notifications", "IsRead");
            DropColumn("dbo.Notifications", "Message");
            DropColumn("dbo.Notifications", "UserName");
            DropColumn("dbo.Employees", "SupervisedDepartment");
        }
    }
}
