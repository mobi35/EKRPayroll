using Caliburn.Micro;
using EyeKnowRight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeKnowRight.ViewModels
{
    public class ShellViewModel : Screen
    {
        EyeKnowRightDB db = new EyeKnowRightDB();
        //  private readonly IWindowManager windowManager;
        public ShellViewModel()
        {


            var getPayroll = db.Payrolls.ToList();
            var getUserList = db.Employees.ToList();

            if (getPayroll.Count == 0)
            {

                if (DateTime.Now.Day >= 26 ||  DateTime.Now.Day <= 10 )
                {
                    Payroll payroll = new Payroll();
                    payroll.StartPayroll = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 26);
                    payroll.IsActive = true;
                    payroll.EndPayroll = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10);
                    db.Payrolls.Add(payroll);
                    foreach(var list in getUserList)
                    {
                        UpdateDailyTimeRecord(list.UserName);
                    }
                    db.SaveChanges();
                }
                else if (DateTime.Now.Day >= 11 && DateTime.Now.Day <= 25)
                {
                    Payroll payroll = new Payroll();
                    payroll.StartPayroll = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 11);
                    payroll.EndPayroll = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 25);
                    payroll.IsActive = true;
                    db.Payrolls.Add(payroll);
                    foreach (var list in getUserList)
                    {
                        UpdateDailyTimeRecord(list.UserName);
                    }
                    db.SaveChanges();
                }

            }
            else if (DateTime.Now.Day == 25)
            {
             
                bool isExist25 = false;

                foreach (var payroll in getPayroll)
                {
                    if (payroll.IsActive && payroll.EndPayroll == new DateTime(DateTime.Now.Year, DateTime.Now.Month, 25))
                    {
                        db.Payrolls.Where(a => a.PayrollPK == payroll.PayrollPK).FirstOrDefault().IsActive = false;
                        db.SaveChanges();
                        isExist25 = true;
                    }

                }
              
                if (isExist25)
                {
                    Payroll payroll = new Payroll();
                    payroll.StartPayroll = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 26);
                    payroll.EndPayroll = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(1).Month, 10);
                    payroll.IsActive = true;
                    db.Payrolls.Add(payroll);
                    
                    foreach (var list in getUserList)
                    {
                        UpdateDailyTimeRecord(list.UserName);
                    }
                    db.SaveChanges();
                }
            }
            else if (DateTime.Now.Day == 10)
            {
               
                bool isExist11 = false;

                foreach (var payroll in getPayroll)
                {
                    if (payroll.EndPayroll == new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10))
                    {
                        isExist11 = true;
                       
                    }
                }

                if (!isExist11)
                {
                    Payroll payroll = new Payroll();
                    payroll.StartPayroll = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 11);
                    payroll.EndPayroll = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 25);
                    db.Payrolls.Add(payroll);
                    foreach (var list in getUserList)
                    {
                        UpdateDailyTimeRecord(list.UserName);
                    }
                    db.SaveChanges();
                }
            }

        }

        public void UpdateDailyTimeRecord(string username)
        {
            var dateNow = DateTime.Now;
            var firstCutOffDate = DateTime.Now;
           
            if (dateNow.Day >= 11 && dateNow.Day <= 25)
            {

                for (int i = 11; i <= 25; i++)
                {
                    DailyTimeRecord dailyTimeRecord = new DailyTimeRecord();
                    dailyTimeRecord.DateTimeStamps = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i);
                    dailyTimeRecord.UserName = username;
                    db.DailyTimeRecords.Add(dailyTimeRecord);
                    db.SaveChanges();
                }
            }
            else
            {

                var daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

                for (int i = 26; i <= daysInMonth; i++)
                {
                    DailyTimeRecord dailyTimeRecord = new DailyTimeRecord();
                    dailyTimeRecord.DateTimeStamps = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i);
                    dailyTimeRecord.UserName = username;
                    db.DailyTimeRecords.Add(dailyTimeRecord);
                    db.SaveChanges();
                }



                for (int i = 1; i <= 10; i++)
                {
                    DailyTimeRecord dailyTimeRecord = new DailyTimeRecord();
                    dailyTimeRecord.DateTimeStamps = new DateTime(DateTime.Now.Year, DateTime.Now.Month, i);
                    dailyTimeRecord.UserName = username;
                    db.DailyTimeRecords.Add(dailyTimeRecord);
                    db.SaveChanges();
                }
            }


        }

    }
}
