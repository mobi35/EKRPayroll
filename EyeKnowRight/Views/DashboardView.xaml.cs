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
        public string[] TotalAbsenceLabel { get; set; }

        public string[] TotalPerformingLabel { get; set; }
        EyeKnowRightDB db = new EyeKnowRightDB();
        public Func<ChartPoint, string> PointLabel { get; set; }
        public DashboardView()
        {
            InitializeComponent();



            TopEmployee.Series.Clear();
            LiveCharts.SeriesCollection topPerformingCollections = new LiveCharts.SeriesCollection();


            var dtrSelect = db.DailyTimeRecords.Join(db.Employees, id => id.DailyTimeRecordPK, foreign =>
            foreign.EmployeePK, (primaryId, foreignId) => new { PrimaryID = primaryId, ForeignID = foreignId })
                .GroupBy(a => a.PrimaryID.UserName)

                .Select(l => new
                {
                    DtrID = l.Key,
                    Number = l.Count(),
                    SumEach = l.Sum(a => a.PrimaryID.Accumulated),
                    Name = l.FirstOrDefault().ForeignID.UserName,
                    Department = l.FirstOrDefault().ForeignID.Department
                }).OrderByDescending(a => a.SumEach).ToList();

            // var dtrList = db.DailyTimeRecords.ToList();


            bool c1 = false, c2 = false, c3 = false, c4 = false, c5 = false, c6 = false, c7 = false;
            int v1 = 0, v2 = 0, v3 = 0, v4 = 0, v5 = 0, v6 = 0, v7 = 0;
            foreach (var dtr in dtrSelect)
            {
                if (dtr.Department == "Sales and Marketing" && c1 == false)
                {
                    v1 = (int)dtr.SumEach;
                    c1 = true;
                }
                if (dtr.Department == "Creative" && c2 == false)
                {
                    v2 = (int)dtr.SumEach;
                    c2 = true;
                }
                if (dtr.Department == "IT" && c3 == false)
                {
                    v3 = (int)dtr.SumEach;
                    c3 = true;
                }
                if (dtr.Department == "Production" && c4 == false)
                {
                    v4 = (int)dtr.SumEach;
                    c4 = true;
                }
                if (dtr.Department == "Inventory" && c5 == false)
                {
                    v5 = (int)dtr.SumEach;
                    c5 = true;
                }
                if (dtr.Department == "Public Relations" && c6 == false)
                {
                    v6= (int)dtr.SumEach;
                    c6 = true;
                }

                if (dtr.Department == "Human Resources" && c7 == false)
                {
                    v7 = (int)dtr.SumEach;
                    c7 = true;
                }

              
            }

            topPerformingCollections.Add(new LiveCharts.Wpf.ColumnSeries { DataLabels = true, Values = new LiveCharts.ChartValues<int> { v1, v2, v3, v4, v5, v6, v7 }, Title = "Departments" });

            TotalPerformingLabel = new[] { "Sales and Marketing", "Creative", "IT", "Production", "Inventory", "Public Relations", "Human Resources" };
            foreach (var topPerforming in topPerformingCollections)
            {
                TopEmployee.Series.Add(topPerforming);
            }



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
            DataContext = this;
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

            /// TOP PERFORMING EMPLOYEES PER DEPARTMENT
            /// 
            DataContext = this;

            //Total absence

            TotalAbsence.Series.Clear();
            LiveCharts.SeriesCollection totalAbsence = new LiveCharts.SeriesCollection();
            var datetimeDTR = DateTime.Now;
            var listOfAvailableDate = db.DailyTimeRecords.Where(a => a.DateTimeStamps <= datetimeDTR).ToList();
            int d1 = 0, d2 = 0, d3 = 0, d4 = 0, d5 = 0, d6 = 0,d7 = 0, d8 = 0, d9 = 0, d10 = 0, d11 = 0, d12 = 0;
            foreach (var dtr in listOfAvailableDate)
            {
                if (dtr.TimeIn == null)
                {
                    if (dtr.DateTimeStamps.Value.Month == 1)
                        d1++;
                    if (dtr.DateTimeStamps.Value.Month == 2)
                        d2++;
                    if (dtr.DateTimeStamps.Value.Month == 3)
                        d3++;
                    if (dtr.DateTimeStamps.Value.Month == 4)
                        d4++;
                    if (dtr.DateTimeStamps.Value.Month == 5)
                        d5++;
                    if (dtr.DateTimeStamps.Value.Month == 6)
                        d6++;
                    if (dtr.DateTimeStamps.Value.Month == 7)
                        d7++;
                    if (dtr.DateTimeStamps.Value.Month == 8)
                        d8++;
                    if (dtr.DateTimeStamps.Value.Month == 9)
                        d9++;
                    if (dtr.DateTimeStamps.Value.Month == 10)
                        d10++;
                    if (dtr.DateTimeStamps.Value.Month == 11)
                        d11++;
                    if (dtr.DateTimeStamps.Value.Month == 12)
                        d12++;
                }
            }
            totalAbsence.Add(new LiveCharts.Wpf.ColumnSeries { DataLabels = true, Values = new LiveCharts.ChartValues<int> { d1,d2,d3,d4,d5,d6,d7,d8,d9,d10,d11,d12}, Title = "Months" });


            TotalAbsenceLabel = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            foreach (var absene in totalAbsence)
            {
                TotalAbsence.Series.Add(absene);
            }
            // Total Overtime

            TotalOvertime.Series.Clear();
            LiveCharts.SeriesCollection totalOvertime = new LiveCharts.SeriesCollection();
            int od1 = 0, od2 = 0, od3 = 0, od4 = 0, od5 = 0, od6 = 0, od7 = 0, od8 = 0, od9 = 0, od10 = 0, od11 = 0, od12 = 0;
            var overtime = DateTime.Now;
            var listOfOvertime = db.Overtimes.Where(a => a.Status == "Accepted").ToList();
            foreach (var ot in listOfOvertime)
            {
               
                    if (ot.DateOfOvertime.Value.Month == 1)
                        od1++;
                    if (ot.DateOfOvertime.Value.Month == 2)
                        od2++;
                    if (ot.DateOfOvertime.Value.Month == 3)
                        od3++;
                    if (ot.DateOfOvertime.Value.Month == 4)
                        od4++;
                    if (ot.DateOfOvertime.Value.Month == 5)
                        od5++;
                    if (ot.DateOfOvertime.Value.Month == 6)
                        od6++;
                    if (ot.DateOfOvertime.Value.Month == 7)
                        od7++;
                    if (ot.DateOfOvertime.Value.Month == 8)
                        od8++;
                    if (ot.DateOfOvertime.Value.Month == 9)
                        od9++;
                    if (ot.DateOfOvertime.Value.Month == 10)
                        od10++;
                    if (ot.DateOfOvertime.Value.Month == 11)
                        od11++;
                    if (ot.DateOfOvertime.Value.Month == 12)
                        od12++;
            }

            totalOvertime.Add(new LiveCharts.Wpf.ColumnSeries { DataLabels = true, Values = new LiveCharts.ChartValues<int> { od1, od2, od3, od4, od5, od6, od7, od8, od9, od10, od11, od12 }, Title = "Months" });

            foreach (var ot in totalOvertime)
            {
                TotalOvertime.Series.Add(ot);
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private void PrintDashboard(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;

                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(print, "Dashboard");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }
    }
}
