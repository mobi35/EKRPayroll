namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asda : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmployeeTrainings", "TimeOfTraining", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmployeeTrainings", "TimeOfTraining");
        }
    }
}
