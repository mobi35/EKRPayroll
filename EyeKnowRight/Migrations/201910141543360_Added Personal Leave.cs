namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPersonalLeave : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employees", "PersonalLeave", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employees", "PersonalLeave");
        }
    }
}
