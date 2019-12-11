namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "MaritalStatus", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "MaritalStatus");
        }
    }
}
