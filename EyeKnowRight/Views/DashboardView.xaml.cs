using EyeKnowRight.Models;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;
namespace EyeKnowRight
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    /// 


    public partial class DashboardView : UserControl
    {

        EyeKnowRightDB db = new EyeKnowRightDB();
        public Func<ChartPoint, string> PointLabel { get; set; }
        public DashboardView()
        {
            InitializeComponent();

            PointLabel = chartPoint =>
                string.Format("{0} ({1:P})", chartPoint.Y, chartPoint.Participation);

            DataContext = this;
            DepartmentChart.Series.Clear();
            LiveCharts.SeriesCollection psc = new LiveCharts.SeriesCollection();

            List<Employee> employees = new List<Employee>();

            employees = db.Employees.ToList();

            var employeesSelect = db.Employees.GroupBy(a => a.Department)
               .Select(l => new
               {
                   PayrollViewKey = l.Key,
                   Number = l.Count(),
                   Department = l.FirstOrDefault().Department

               }).ToList();

            foreach (var select in employeesSelect)
            {
                psc.Add(new LiveCharts.Wpf.PieSeries {  DataLabels = true, Values = new LiveCharts.ChartValues<decimal> { select.Number }, Title = select.Department });

            }
         
            foreach (var ps in psc) {
                DepartmentChart.Series.Add(ps);
                        }


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
         
        }
    }
}
