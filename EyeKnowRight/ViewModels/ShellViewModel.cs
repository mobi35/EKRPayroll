﻿using Caliburn.Micro;
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
            foreach (var user in userList)
            {
                Deductions deduction = new Deductions();
                double basicSalaryPerMinute = ((user.Salary / 9) /60);
                foreach (var dtr in dailyTimeRecord)
                  {
                    if(user.UserName == dtr.UserName)
                    {
                        deduction.AllAccumulatedTimeAddition += dtr.Accumulated * basicSalaryPerMinute;
                        deduction.LateDeduction += dtr.Late * basicSalaryPerMinute;
                    }
                 }

                    deduction.TotalSalary = deduction.AllAccumulatedTimeAddition;
                    //userList.Remove(user);\\
                    deduction.PayrollPK = db.Payrolls.FirstOrDefault(a => a.StartPayroll == start && a.EndPayroll == end).PayrollPK;
                    deduction.UserName = user.UserName;
                     deduction.BasicSalary = user.Salary; 
                   
                db.Deductionss.Add(deduction);
                db.SaveChanges();
            }

            }catch(Exception e)
            {

            }

        }

    }
}
