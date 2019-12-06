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
    public partial class AdminLeaveView : UserControl
    {


        private void ResetGrid(dynamic item = null)
        {
            string username = Application.Current.Properties["UserName"].ToString();
            var employeeModel = db.Employees.Where(a => a.UserName == username).FirstOrDefault();

            if (item != null)
            {
                LeaveGrid.ItemsSource = item;
            }
            else if (employeeModel.SupervisedDepartment != null)
            {
                List<Leave> newLeave = new List<Leave>();
                foreach (var user in db.Employees.ToList())
                {
                    foreach (var leave in db.Leaves.ToList())
                    {
                        if (user.Department == employeeModel.SupervisedDepartment && user.UserName == leave.UserName)
                        {
                            newLeave.Add(leave);
                        }
                    }
                }

                LeaveGrid.ItemsSource = newLeave.OrderByDescending(a => a.LeavePK).ToList();

            }
            else
            {
                var data = db.Leaves.OrderByDescending(a => a.LeavePK).ToList();
                LeaveGrid.ItemsSource = data;
            }


        }

        EyeKnowRightDB db = new EyeKnowRightDB();
        public AdminLeaveView()
        {
            InitializeComponent();
            //  var user = Application.Current.Properties["UserName"].ToString();
            ResetGrid();

            
        }

        private void LeaveClick(object sender, RoutedEventArgs e)
        {
            int leavePK = (int)((Button)sender).Tag;
            var leave = db.Leaves.FirstOrDefault(a => a.LeavePK == leavePK);
            DataContext = leave;
        }

        private void LeaveAccept(object sender, RoutedEventArgs e)
        {
            int leavePK = Int32.Parse(LeavePK.Text);
            var leave = db.Leaves.FirstOrDefault(a => a.LeavePK == leavePK);
            leave.Status = "Accepted";
            var user = db.Employees.FirstOrDefault(a => a.UserName == leave.UserName);
            TimeSpan? dateRangeComparison = leave.EndLeave - leave.StartDate;
            int numberOfWorkingDays = 0;
            for (int i = 0; i <= dateRangeComparison.Value.TotalDays; i++)
            {
              
                if (leave.StartDate.Value.AddDays(i).DayOfWeek != DayOfWeek.Sunday
                && leave.StartDate.Value.AddDays(i).DayOfWeek != DayOfWeek.Saturday)
                {
                    var leaveDate = leave.StartDate.Value.AddDays(i).Date;
                    var getDTR = db.DailyTimeRecords.FirstOrDefault(a => a.UserName == user.UserName && a.DateTimeStamps == leaveDate);
                    getDTR.Remarks = leave.TypeOfLeave;
                    if(leave.TypeOfLeave == "Sick Leave")
                    {
                        user.SickLeaveCredit += 1;
                    }
                    getDTR.Accumulated = 540;
                    db.SaveChanges();
                    numberOfWorkingDays++;
                   
                }
            }
          
            if (leave.TypeOfLeave == "Sick Leave")
            {
                user.SickLeave -= numberOfWorkingDays;
            } else if (leave.TypeOfLeave == "Medical Leave")
            {
                user.MedicalLeave -= numberOfWorkingDays;
            }
            else if (leave.TypeOfLeave == "Bereavement Leave")
            {
                user.BereavementLeave -= numberOfWorkingDays;
            }
            else if (leave.TypeOfLeave == "Personal Leave")
            {
                user.PersonalLeave -= numberOfWorkingDays;
            }
            else if (leave.TypeOfLeave == "Paternity Leave")
            {
                user.PaternityLeave -= numberOfWorkingDays;
            }
            else if (leave.TypeOfLeave == "Maternity Leave")
            {
                user.MaternityLeave -= numberOfWorkingDays;
            }
            db.SaveChanges();


            Notification notification = new Notification();
            notification.NotificationToWho = user.UserName;
            notification.Message = "Your leave has been accepted. Congratulations";
            db.Notifications.Add(notification);
            db.SaveChanges();


        }

        private void LeaveReject(object sender, RoutedEventArgs e)
        {
            int leavePK = Int32.Parse(LeavePK.Text);
            var leave = db.Leaves.FirstOrDefault(a => a.LeavePK == leavePK);
            leave.Status = "Rejected";
            db.SaveChanges();
        }

        private void SearchChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text != "")
            {
                Magnifier.Visibility = Visibility.Hidden;
                var text = SearchBox.Text;
                var data = db.Leaves
                  .Where(a =>
                  a.ReasonForLeaving.StartsWith(text) || a.ReasonForLeaving.EndsWith(text) ||
                  a.Status.StartsWith(text) || a.Status.EndsWith(text) ||
                  a.UserName.StartsWith(text) || a.UserName.EndsWith(text) 
               ).ToList();
                ResetGrid(data);
            }
            else
            {
                Magnifier.Visibility = Visibility.Visible;
                ResetGrid();
            }
        }

    }
}
