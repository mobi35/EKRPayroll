namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class xxcz : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leaves", "ReasonForLeaving", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leaves", "ReasonForLeaving");
        }
    }
}
