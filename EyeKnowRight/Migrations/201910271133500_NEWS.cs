﻿namespace EyeKnowRight.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NEWS : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DailyTimeRecords",
                c => new
                    {
                        DailyTimeRecordPK = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        TimeIn = c.DateTime(),
                        TimeOut = c.DateTime(),
                        FirstTimeIn = c.DateTime(),
                        Accumulated = c.Double(nullable: false),
                        AccumulatedString = c.String(),
                        Late = c.Double(nullable: false),
                        LateString = c.String(),
                        OverTime = c.Double(nullable: false),
                        OverTimeString = c.String(),
                        DateTimeStamps = c.DateTime(),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.DailyTimeRecordPK);
            
            CreateTable(
                "dbo.Deductions",
                c => new
                    {
                        DeductionPK = c.Int(nullable: false, identity: true),
                        PayrollPK = c.Int(nullable: false),
                        BasicSalary = c.Double(nullable: false),
                        LateDeduction = c.Double(nullable: false),
                        OverTimeAddition = c.Double(nullable: false),
                        AllAccumulatedTimeAddition = c.Double(nullable: false),
                        SSSDeduction = c.Double(nullable: false),
                        TinDeduction = c.Double(nullable: false),
                        PagibigDeduction = c.Double(nullable: false),
                        TotalSalary = c.Double(nullable: false),
                        UserName = c.String(),
                        WorkingDays = c.Int(),
                        DaysPresent = c.Int(),
                    })
                .PrimaryKey(t => t.DeductionPK);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeePK = c.Int(nullable: false, identity: true),
                        EmployeeID = c.String(maxLength: 10),
                        FirstName = c.String(),
                        MiddleName = c.String(),
                        LastName = c.String(),
                        UserName = c.String(maxLength: 40),
                        Password = c.String(maxLength: 40),
                        Email = c.String(maxLength: 50),
                        Gender = c.String(maxLength: 50),
                        DateRegistered = c.DateTime(),
                        Age = c.Int(),
                        Address = c.String(maxLength: 100),
                        Position = c.String(maxLength: 50),
                        JobTitle = c.String(),
                        Salary = c.Double(nullable: false),
                        Picture = c.Binary(),
                        BirthDate = c.DateTime(),
                        MyProperty = c.Int(),
                        MaternityLeave = c.Int(nullable: false),
                        PaternityLeave = c.Int(nullable: false),
                        SickLeave = c.Int(nullable: false),
                        BereavementLeave = c.Int(nullable: false),
                        MedicalLeave = c.Int(nullable: false),
                        PersonalLeave = c.Int(nullable: false),
                        NumberOfTries = c.Int(nullable: false),
                        DaysContract = c.Int(nullable: false),
                        SSSNumber = c.String(),
                        TINNumber = c.String(),
                        PagibigNumber = c.String(),
                        LastAppraiseDate = c.DateTime(),
                        Department = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.EmployeePK);
            
            CreateTable(
                "dbo.Evaluations",
                c => new
                    {
                        EvaluationPK = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        DateAppraise = c.DateTime(),
                        Answer1 = c.Int(nullable: false),
                        Answer2 = c.Int(nullable: false),
                        Answer3 = c.Int(nullable: false),
                        Answer4 = c.Int(nullable: false),
                        Answer5 = c.Int(nullable: false),
                        Answer6 = c.Int(nullable: false),
                        Answer7 = c.Int(nullable: false),
                        Answer8 = c.Int(nullable: false),
                        Answer9 = c.Int(nullable: false),
                        Answer10 = c.Int(nullable: false),
                        Answer11 = c.Int(nullable: false),
                        Answer12 = c.Int(nullable: false),
                        Answer13 = c.Int(nullable: false),
                        Answer14 = c.Int(nullable: false),
                        Answer15 = c.Int(nullable: false),
                        Answer16 = c.Int(nullable: false),
                        Answer17 = c.Int(nullable: false),
                        Answer18 = c.Int(nullable: false),
                        Answer19 = c.Int(nullable: false),
                        Answer20 = c.Int(nullable: false),
                        TotalScore = c.Int(nullable: false),
                        Comment = c.String(),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.EvaluationPK);
            
            CreateTable(
                "dbo.Holidays",
                c => new
                    {
                        HolidayPK = c.Int(nullable: false, identity: true),
                        Month = c.DateTime(),
                        HolidayName = c.String(),
                    })
                .PrimaryKey(t => t.HolidayPK);
            
            CreateTable(
                "dbo.Leaves",
                c => new
                    {
                        LeavePK = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        TypeOfLeave = c.String(),
                        ReasonForLeaving = c.String(),
                        StartDate = c.DateTime(),
                        EndLeave = c.DateTime(),
                        Status = c.String(),
                    })
                .PrimaryKey(t => t.LeavePK);
            
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        LoanPK = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.LoanPK);
            
            CreateTable(
                "dbo.Memos",
                c => new
                    {
                        MemoPK = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.MemoPK);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        NotificationPK = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.NotificationPK);
            
            CreateTable(
                "dbo.Payrolls",
                c => new
                    {
                        PayrollPK = c.Int(nullable: false, identity: true),
                        StartPayroll = c.DateTime(),
                        EndPayroll = c.DateTime(),
                        NumberOfWorkingDays = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PayrollPK);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Payrolls");
            DropTable("dbo.Notifications");
            DropTable("dbo.Memos");
            DropTable("dbo.Loans");
            DropTable("dbo.Leaves");
            DropTable("dbo.Holidays");
            DropTable("dbo.Evaluations");
            DropTable("dbo.Employees");
            DropTable("dbo.Deductions");
            DropTable("dbo.DailyTimeRecords");
        }
    }
}