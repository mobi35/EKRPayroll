namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ccxz : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Leaves", "Status", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Leaves", "Status", c => c.Boolean(nullable: false));
        }
    }
}
