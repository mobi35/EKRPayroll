namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Evaluations", "DateAppraise", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Evaluations", "DateAppraise", c => c.DateTime(nullable: false));
        }
    }
}
