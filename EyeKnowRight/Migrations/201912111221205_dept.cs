namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dept : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        DepartmentPK = c.Int(nullable: false, identity: true),
                        DepartmentName = c.String(),
                        SupervisorName = c.String(),
                    })
                .PrimaryKey(t => t.DepartmentPK);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Departments");
        }
    }
}
