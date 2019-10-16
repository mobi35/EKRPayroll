namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Evaluations", "UserName", c => c.String());
            AddColumn("dbo.Evaluations", "DateAppraise", c => c.DateTime(nullable: false));
            AddColumn("dbo.Evaluations", "Answer1", c => c.Int(nullable: false));
            AddColumn("dbo.Evaluations", "Answer2", c => c.Int(nullable: false));
            AddColumn("dbo.Evaluations", "Answer3", c => c.Int(nullable: false));
            AddColumn("dbo.Evaluations", "Answer4", c => c.Int(nullable: false));
            AddColumn("dbo.Evaluations", "Answer5", c => c.Int(nullable: false));
            AddColumn("dbo.Evaluations", "Answer6", c => c.Int(nullable: false));
            AddColumn("dbo.Evaluations", "Answer7", c => c.Int(nullable: false));
            AddColumn("dbo.Evaluations", "Answer8", c => c.Int(nullable: false));
            AddColumn("dbo.Evaluations", "Answer9", c => c.Int(nullable: false));
            AddColumn("dbo.Evaluations", "Answer10", c => c.Int(nullable: false));
            AddColumn("dbo.Evaluations", "Answer11", c => c.Int(nullable: false));
            AddColumn("dbo.Evaluations", "Answer12", c => c.Int(nullable: false));
            AddColumn("dbo.Evaluations", "Answer13", c => c.Int(nullable: false));
            AddColumn("dbo.Evaluations", "Answer14", c => c.Int(nullable: false));
            AddColumn("dbo.Evaluations", "Answer15", c => c.Int(nullable: false));
            AddColumn("dbo.Evaluations", "Answer16", c => c.Int(nullable: false));
            AddColumn("dbo.Evaluations", "Answer17", c => c.Int(nullable: false));
            AddColumn("dbo.Evaluations", "Answer18", c => c.Int(nullable: false));
            AddColumn("dbo.Evaluations", "Answer19", c => c.Int(nullable: false));
            AddColumn("dbo.Evaluations", "Answer20", c => c.Int(nullable: false));
            AddColumn("dbo.Evaluations", "TotalScore", c => c.Int(nullable: false));
            AddColumn("dbo.Evaluations", "Comment", c => c.String());
            AddColumn("dbo.Evaluations", "Remarks", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Evaluations", "Remarks");
            DropColumn("dbo.Evaluations", "Comment");
            DropColumn("dbo.Evaluations", "TotalScore");
            DropColumn("dbo.Evaluations", "Answer20");
            DropColumn("dbo.Evaluations", "Answer19");
            DropColumn("dbo.Evaluations", "Answer18");
            DropColumn("dbo.Evaluations", "Answer17");
            DropColumn("dbo.Evaluations", "Answer16");
            DropColumn("dbo.Evaluations", "Answer15");
            DropColumn("dbo.Evaluations", "Answer14");
            DropColumn("dbo.Evaluations", "Answer13");
            DropColumn("dbo.Evaluations", "Answer12");
            DropColumn("dbo.Evaluations", "Answer11");
            DropColumn("dbo.Evaluations", "Answer10");
            DropColumn("dbo.Evaluations", "Answer9");
            DropColumn("dbo.Evaluations", "Answer8");
            DropColumn("dbo.Evaluations", "Answer7");
            DropColumn("dbo.Evaluations", "Answer6");
            DropColumn("dbo.Evaluations", "Answer5");
            DropColumn("dbo.Evaluations", "Answer4");
            DropColumn("dbo.Evaluations", "Answer3");
            DropColumn("dbo.Evaluations", "Answer2");
            DropColumn("dbo.Evaluations", "Answer1");
            DropColumn("dbo.Evaluations", "DateAppraise");
            DropColumn("dbo.Evaluations", "UserName");
        }
    }
}
