namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ZXXZ : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "SSSNumber", c => c.String());
            AddColumn("dbo.Employees", "TINNumber", c => c.String());
            AddColumn("dbo.Employees", "PagibigNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "PagibigNumber");
            DropColumn("dbo.Employees", "TINNumber");
            DropColumn("dbo.Employees", "SSSNumber");
        }
    }
}
