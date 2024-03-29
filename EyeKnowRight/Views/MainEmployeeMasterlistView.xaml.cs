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
    public partial class MainEmployeeMasterlistView : Window
    {

        EyeKnowRightDB db = new EyeKnowRightDB();
        public MainEmployeeMasterlistView(dynamic reportObject, dynamic date)
        {
            InitializeComponent();

            ReportViewerDemo.Reset();
            DataTable dt = ToDataTable(reportObject);
           
            ReportDataSource ds = new ReportDataSource("employeemasterlist", dt);
      
            ReportViewerDemo.LocalReport.DataSources.Add(ds);
         
            ReportViewerDemo.LocalReport.ReportEmbeddedResource = "EyeKnowRight.Reports.EmployeeMasterlist.rdlc";
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
