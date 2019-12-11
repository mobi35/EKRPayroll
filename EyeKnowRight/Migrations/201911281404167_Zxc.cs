namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Zxc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmployeeTrainings",
                c => new
                    {
                        EmployeeTrainingPK = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        FullName = c.String(),
                        Training = c.String(),
                        DateOfTraining = c.DateTime(),
                        TrainingStatus = c.String(),
                    })
                .PrimaryKey(t => t.EmployeeTrainingPK);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmployeeTrainings");
        }
    }
}
