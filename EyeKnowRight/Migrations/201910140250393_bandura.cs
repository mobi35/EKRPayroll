namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bandura : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "NumberOfTries", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "NumberOfTries");
        }
    }
}
