using Caliburn.Micro;
using EyeKnowRight.Models;
using EyeKnowRight.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeKnowRight.ViewModels
{
    public class ShellViewModel : Screen
    {
        private string typeOfReports;

        public string TypeOfReports
        {
            get { return typeOfReports; }
            set { typeOfReports = value; }
        }

        private DateTime? startDate;

        public DateTime? StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        private DateTime? endDate;

        public DateTime? EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }


        private int numberOfEntries;

        public int NumberOfEntries
        {
            get { return numberOfEntries; }
            set { numberOfEntries = value; }
        }

        EyeKnowRightDB db = new EyeKnowRightDB();
        public ShellViewModel()
        {
            ///// 

            var getUserList = db.Employees.ToList();
            var leave = db.Leaves.ToList();
            TimeSpan? remainingLeave;
            foreach (var emp in getUserList)
            {
                foreach (var l in leave)
                {
                    if (emp.UserName == l.UserName && DateTime.Now.Date >= l.StartDate && DateTime.Now.Date <= l.EndLeave.Value.AddHours(24))
                    {
                        remainingLeave = l.EndLeave - DateTime.Now.Date;
                        if (l.EndLeave == l.StartDate)
                        {
                            emp.RemainingLeave = 1;
                        }
                        else
                        {
                            emp.RemainingLeave = (int)remainingLeave.Value.TotalDays;
                        }

                        if ((int)remainingLeave.Value.TotalDays < 0)
                        {
                            emp.RemainingLeave = 0;
                        }
                        db.SaveChanges();
                        break;
                    }
                }
            }

            var getPayroll = db.Payrolls.ToList();
          

            if (getPayroll.Count == 0)
            {
              
                if (DateTime.Now.Day >= 26 )
                {
                    Payroll payroll = new Payroll();
                    payroll.StartPayroll = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 26);
                    payroll.EndPayroll = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(1).Month, 10);
                    payroll.IsActive = true;
                    db.Payrolls.Add(payroll);
                    db.SaveChanges();

                }else if (DateTime.Now.Day <= 10)
                {
                    Payroll payroll = new Payroll();
                    payroll.StartPayroll = new DateTime(DateTime.Now.Year, DateTime.Now.AddMonths(-1).Month, 26);
                    payroll.EndPayroll = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10);
                    payroll.IsActive = true;
                    db.Payrolls.Add(payroll);
                    db.SaveChanges();
                }
                else if (DateTime.Now.Day >= 11 && DateTime.Now.Day <= 25)
                {
                    Payroll payroll = new Payroll();
                    payroll.StartPayroll = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 11);
                    payroll.IsActive = true;
                    payroll.EndPayroll = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 25);
                    db.Payrolls.Add(payroll);
                 
                    db.SaveChanges();
                }

            }
            else if (DateTime.Now.Day == 26)
            {
             
                bool isExist25 = false;

                foreach (var payroll in getPayroll)
                {
                    if (payroll.IsActive && payroll.EndPayroll == new DateTime(DateTime.Now.Year, DateTime.Now.Month, 25))
                    {
                       
                        db.Payrolls.Where(a => a.PayrollPK == payroll.PayrollPK).FirstOrDefault().IsActive = false;
                        db.SaveChanges();
                        ComputeSalary(payroll.StartPayroll, payroll.EndPayroll);
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

                    foreach (var user in db.Employees.ToList())
                    {
                        user.LastAppraiseDate = null;
                        
                        db.SaveChanges();
                    }
                   
                    db.SaveChanges();
                }
            }
            else if (DateTime.Now.Day == 11)
            {
             
                bool isExist11 = false;

                foreach (var payroll in getPayroll)
                {
                    if (payroll.IsActive && payroll.EndPayroll == new DateTime(DateTime.Now.Year, DateTime.Now.Month, 10))
                    {
                        db.Payrolls.Where(a => a.PayrollPK == payroll.PayrollPK).FirstOrDefault().IsActive = false;
                        db.SaveChanges();
                        isExist11 = true;
                        ComputeSalary(payroll.StartPayroll, payroll.EndPayroll);
                    }
                }

                if (isExist11)
                {

                    Payroll payroll = new Payroll();
                    payroll.StartPayroll = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 11);
                    payroll.EndPayroll = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 25);
                    payroll.IsActive = true;
                    db.Payrolls.Add(payroll);
                  
                    db.SaveChanges();
                }
            }

        }



        public void ComputeSalary(DateTime? start, DateTime? end)
        {
            try { 
            var dailyTimeRecord = db.DailyTimeRecords.Where(a => a.DateTimeStamps >= start && a.DateTimeStamps <= end).ToList();
            var userList = db.Employees.ToList();
            var getHoliday = db.Holidays.ToList();
            foreach (var user in userList)
            {
                Deductions deduction = new Deductions();
                double basicSalaryPerMinute = ((user.Salary / 9) /60);
                    int daysPresent = 0;
                foreach (var dtr in dailyTimeRecord)
                  {
                      

                            foreach (var hol in getHoliday)
                        {
                            if (dtr.Accumulated >= 400) { 
                            if (hol.Month.Value.Month == dtr.DateTimeStamps.Value.Month && hol.Month.Value.Day == dtr.DateTimeStamps.Value.Day )
                            {
                                //This is a holiday.
                                dtr.Remarks += " " + hol.HolidayName;
                                    if (hol.SalaryInrease == 30)
                                    {
                                        double percentage = 0.30 * dtr.Accumulated;
                                        // salary increase
                                        dtr.Accumulated += percentage;
                                        
                                    }
                                    else
                                    {
                                        double percentage = 1.0 * dtr.Accumulated;
                                        dtr.Accumulated += percentage;
                                    }
                            }
                            }

                        }

                        if (user.UserName == dtr.UserName)
                    {
                        if (dtr.TimeOut != null)
                            daysPresent++;
                            deduction.AllAccumulatedTimeAddition += dtr.Accumulated * basicSalaryPerMinute;
                        deduction.LateDeduction += dtr.Late * basicSalaryPerMinute;
                        if (user.SickLeaveCredit != 0)
                        {
                                deduction.AllAccumulatedTimeAddition += (540 * user.SickLeaveCredit) * basicSalaryPerMinute;
                                deduction.Remarks += $" Added {user.SickLeave} Sick Leave/s";
                                user.SickLeaveCredit = 0;
                                db.SaveChanges();
                        }
                    }
                 }
                    deduction.TotalSalary = deduction.AllAccumulatedTimeAddition;
                    deduction.PayrollPK = db.Payrolls.FirstOrDefault(a => a.StartPayroll == start && a.EndPayroll == end).PayrollPK;
                    deduction.UserName = user.UserName;
                    if (end.Value.AddDays(1).Date.Day == 26 )
                    {
                        deduction.SSSDeduction = 1800;
                        deduction.PagibigDeduction = 100;
                        deduction.TotalSalary -= 1800;
                        deduction.TotalSalary -= 100;
                    }
                    deduction.BasicSalary = user.Salary; 
                    db.Deductionss.Add(deduction);
                    db.SaveChanges();
            }
            }catch(Exception e)
            {

            }

        }

        public void ReportClick()
        {
           

        }
       

    }
}
