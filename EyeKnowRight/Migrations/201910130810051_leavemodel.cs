namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class leavemodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leaves", "UserName", c => c.String());
            AddColumn("dbo.Leaves", "TypeOfLeave", c => c.String());
            AddColumn("dbo.Leaves", "StartDate", c => c.DateTime());
            AddColumn("dbo.Leaves", "EndLeave", c => c.DateTime());
            AddColumn("dbo.Leaves", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leaves", "Status");
            DropColumn("dbo.Leaves", "EndLeave");
            DropColumn("dbo.Leaves", "StartDate");
            DropColumn("dbo.Leaves", "TypeOfLeave");
            DropColumn("dbo.Leaves", "UserName");
        }
    }
}
