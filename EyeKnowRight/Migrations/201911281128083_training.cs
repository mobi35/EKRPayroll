namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class training : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Trainings",
                c => new
                    {
                        TrainingPK = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        DateMade = c.DateTime(),
                    })
                .PrimaryKey(t => t.TrainingPK);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Trainings");
        }
    }
}
