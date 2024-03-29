﻿using EyeKnowRight.Models;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EyeKnowRight.Views
{
    /// <summary>
    /// Interaction logic for PayrollPrintView.xaml
    /// </summary>
    public partial class PayrollPrintView : Window
    {

        EyeKnowRightDB db = new EyeKnowRightDB();
        public PayrollPrintView(int payrollPK)
        {
            InitializeComponent();


            var getPayroll = db.Payrolls.FirstOrDefault(a => a.PayrollPK == 1);
            //var deductions = db.Deductionss.Where(a => a.PayrollPK == getPayroll.PayrollPK).ToList();


            var deductionsJoin = db.Deductionss.Join(db.Payrolls, id => id.PayrollPK, forId => forId.PayrollPK, (id, forId) => new { id = id, forId = forId })
                .Where(a => a.id.PayrollPK == payrollPK && a.forId.PayrollPK == payrollPK)
                .GroupBy(a => a.id.UserName)
                .Select(l => new
                {
                    PayrollViewKey = l.Key,
                    UserName = l.FirstOrDefault().id.UserName,
                    LateDeduction = l.FirstOrDefault().id.LateDeduction,
                    BasicSalary = l.FirstOrDefault().id.BasicSalary,
                    Pagibig = l.FirstOrDefault().id.PagibigDeduction,
                    Accumulated = l.FirstOrDefault().id.AllAccumulatedTimeAddition,
                    SSS = l.FirstOrDefault().id.SSSDeduction,
                    TIN = l.FirstOrDefault().id.TinDeduction,
                    TotalSalary = l.FirstOrDefault().id.TotalSalary,
                    PayrollStart = l.FirstOrDefault().forId.StartPayroll,
                    PayrollEnd = l.FirstOrDefault().forId.EndPayroll

                }).ToList();

            var otherJoin = deductionsJoin.Join(db.Employees, id => id.UserName, forId => forId.UserName, (id, forId) => new { id = id, forId = forId }).Where(a => a.id.UserName == a.forId.UserName).GroupBy(a => a.id.UserName)
                .Select(l => new
                {
                    PayrollViewKey = l.Key,
                    UserName = l.FirstOrDefault().id.UserName,
                    LateDeduction = l.FirstOrDefault().id.LateDeduction,
                    BasicSalary = l.FirstOrDefault().id.BasicSalary,
                    Pagibig = l.FirstOrDefault().id.Pagibig,
                    Accumulated = l.FirstOrDefault().id.Accumulated,
                    SSS = l.FirstOrDefault().id.SSS,
                    TIN = l.FirstOrDefault().id.TIN,
                    TotalSalary = l.FirstOrDefault().id.TotalSalary,
                    PayrollStart = l.FirstOrDefault().id.PayrollStart,
                    PayrollEnd = l.FirstOrDefault().id.PayrollEnd,
                    FirstName = l.FirstOrDefault().forId.FirstName,
                    LastName = l.FirstOrDefault().forId.LastName
                }).ToList();

            ReportViewerDemo.Reset();
            DataTable dt = ToDataTable(otherJoin);
            ReportDataSource ds = new ReportDataSource("dataset", dt);
            ReportViewerDemo.LocalReport.DataSources.Add(ds);
            ReportViewerDemo.LocalReport.ReportEmbeddedResource = "EyeKnowRight.Reports.PayrollReport.rdlc";
            ReportViewerDemo.RefreshReport();
        }

        public static DataTable ToDataTable<T>(List<T> items)
        {
            DataTable dataTable = new DataTable(typeof(T).Name);

            //Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                //Defining type of data column gives proper data table 
                var type = (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) ? Nullable.GetUnderlyingType(prop.PropertyType) : prop.PropertyType);
                //Setting column names as Property names
                dataTable.Columns.Add(prop.Name, type);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    //inserting property values to datatable rows
                    values[i] = Props[i].GetValue(item, null);
                }
                dataTable.Rows.Add(values);
            }
            //put a breakpoint here and check datatable
            return dataTable;
        }
    }
}
