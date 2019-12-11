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

namespace EyeKnowRight
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class EmployeeOvertimeView : UserControl
    {


        EyeKnowRightDB db = new EyeKnowRightDB();
        public EmployeeOvertimeView()
        {
            InitializeComponent();
            var userName = Application.Current.Properties["UserName"].ToString();
            var employeeOvertime = db.Overtimes.Where(a => a.UserName == userName).ToList();
            OvertimeGrid.ItemsSource = employeeOvertime;

            OvertimeDate.DisplayDateStart = DateTime.Now;

        }

        private void RequestOvertime(object sender, RoutedEventArgs e)
        {
            DateTime? newDt = DateTime.Now;
            TimeSpan ts = new TimeSpan(17, 0, 0);
            newDt = newDt.Value.Date + ts;
            var userName = Application.Current.Properties["UserName"].ToString();
            var getOvertime = db.Overtimes.ToList();
            bool tenureOvertime = false;

            var leave = db.Leaves.Where(a => a.UserName == userName).ToList();
            bool leaveStack = false;
            foreach (var l in leave)
            {
                TimeSpan? dateRangeComparison = l.EndLeave - l.StartDate;

                for (int i = 0; i <= dateRangeComparison.Value.TotalDays; i++)
                {
                    if (l.StartDate.Value.AddDays(i).DayOfWeek != DayOfWeek.Sunday
                    && l.StartDate.Value.AddDays(i).DayOfWeek != DayOfWeek.Saturday
                         )
                    {
                            if (OvertimeDate.SelectedDate == l.StartDate.Value.AddDays(i))
                            {
                                leaveStack = true;
                            }
                        
                    }
                }

            }

            if (leaveStack)
            {
                MessageBox.Show("You have a leave on this date, you can't apply for overtime.");
            }
            else if (OvertimeDate.SelectedDate <= DateTime.Now)
            {
                MessageBox.Show("No Past Dates");
            } else if (   OvertimeUntil.SelectedTime <= newDt) {
                MessageBox.Show("Choose date after 5pm");
            } else
            {
              
                foreach (var ot in getOvertime)
                {
                    if (ot.UserName == userName )
                    {
                        if (ot.DateOfOvertime == OvertimeDate.SelectedDate)
                        {
                            tenureOvertime = true;
                            break;
                        }
                    }
                }

                if (!tenureOvertime) { 
                Overtime overtime = new Overtime();
                overtime.DateOfOvertime = OvertimeDate.SelectedDate;
                overtime.UntilWhatTime = OvertimeUntil.SelectedTime;
                overtime.Reason = ReasonForOvertime.Text;
                overtime.Status = "Pending";
                overtime.UserName = userName;
                db.Overtimes.Add(overtime);
                db.SaveChanges();


                var employeeOvertime = db.Overtimes.Where(a => a.UserName == userName).ToList();
                OvertimeGrid.ItemsSource = employeeOvertime;
                }else
                {
                    MessageBox.Show("No Overtime Tenure");
                }
            }
        }
    }
}
