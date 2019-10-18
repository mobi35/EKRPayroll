namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class zxc : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "Department", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "Department");
        }
    }
}
