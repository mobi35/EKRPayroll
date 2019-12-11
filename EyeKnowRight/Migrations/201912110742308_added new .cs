namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addednew : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "Status", c => c.Boolean(nullable: false));
            AddColumn("dbo.Positions", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Positions", "Status");
            DropColumn("dbo.Departments", "Status");
        }
    }
}
