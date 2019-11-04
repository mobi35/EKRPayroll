namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedovertime : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Overtimes",
                c => new
                    {
                        OvertimePK = c.Int(nullable: false, identity: true),
                        UntilWhatTime = c.DateTime(),
                        Reason = c.String(),
                        UserName = c.String(),
                        DateOfOvertime = c.DateTime(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.OvertimePK);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Overtimes");
        }
    }
}
