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
    public class AppraisalClass {

        public string Name { get; set; }
        public double Score { get; set; }

    }

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



            DataContext = this;
            LeaveChart.Series.Clear();
            LiveCharts.SeriesCollection leaveCollection = new LiveCharts.SeriesCollection();

          var  leaveEmployee = db.Employees.ToList();
            int personalLeaveCount = 0, maternityLeaveCount =0, paternityLeaveCount = 0, medicalLeaveCount = 0, bereavementLeaveCount = 0, sickLeaveCount = 0;

            foreach (var emp in leaveEmployee){
                personalLeaveCount += emp.PersonalLeave;
                maternityLeaveCount += emp.MaternityLeave;
                paternityLeaveCount += emp.PaternityLeave;
                medicalLeaveCount += emp.MedicalLeave;
                bereavementLeaveCount += emp.BereavementLeave;
                sickLeaveCount += emp.SickLeave;
            }
         
                leaveCollection.Add(new LiveCharts.Wpf.PieSeries { DataLabels = true, Values = new LiveCharts.ChartValues<decimal> { personalLeaveCount }, Title = "Personal Leave" });
                leaveCollection.Add(new LiveCharts.Wpf.PieSeries { DataLabels = true, Values = new LiveCharts.ChartValues<decimal> { maternityLeaveCount }, Title = "Maternity Leave" });
                leaveCollection.Add(new LiveCharts.Wpf.PieSeries { DataLabels = true, Values = new LiveCharts.ChartValues<decimal> { paternityLeaveCount }, Title = "Paternity Leave" });
                leaveCollection.Add(new LiveCharts.Wpf.PieSeries { DataLabels = true, Values = new LiveCharts.ChartValues<decimal> { medicalLeaveCount }, Title = "Medical Leave" });
                leaveCollection.Add(new LiveCharts.Wpf.PieSeries { DataLabels = true, Values = new LiveCharts.ChartValues<decimal> { bereavementLeaveCount }, Title = "Bereavement Leave" });
                leaveCollection.Add(new LiveCharts.Wpf.PieSeries { DataLabels = true, Values = new LiveCharts.ChartValues<decimal> { sickLeaveCount }, Title = "Sick Leave" });



            foreach (var l in leaveCollection)
            {
                LeaveChart.Series.Add(l);
            }






            //// CARTESIAN
            ///

            AppraisalChart.Series.Clear();
            LiveCharts.SeriesCollection appraisalCollections = new LiveCharts.SeriesCollection();

            var employeeList = db.Employees.ToList();
            var appraisalList = db.Evaluations.ToList();

            List<AppraisalClass> appraisalClass = new List<AppraisalClass>();
            foreach (var emp in employeeList)
            {
                double score = 0; 
                foreach (var app in appraisalList)
                {
                    if(emp.UserName == app.UserName)
                    {
                        score += app.TotalScore;
                    }
                }

                appraisalClass.Add(new AppraisalClass
                {
                    Name = emp.UserName,
                    Score = score
                });
            }

            foreach (var eachAppraise in appraisalClass.OrderByDescending(a => a.Score))
            {
                appraisalCollections.Add(new LiveCharts.Wpf.ColumnSeries { DataLabels = true, Values = new LiveCharts.ChartValues<double> { eachAppraise.Score }, Title = eachAppraise.Name });

            }

            foreach(var appcol in appraisalCollections)
            {
                AppraisalChart.Series.Add(appcol);
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
         
        }
    }
}
