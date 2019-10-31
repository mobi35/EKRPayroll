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
    public partial class AdminOvertimeView : UserControl
    {
      

        EyeKnowRightDB db = new EyeKnowRightDB();
        public AdminOvertimeView()
        {
            InitializeComponent();
            var leave = db.Leaves.Where(a => a.Status == "Pending").ToList();
            OvertimeGrid.ItemsSource = leave;

            
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
            TimeSpan? difference = leave.EndLeave - leave.StartDate;
            for (int i = 0; i <= difference.Value.TotalDays;i++)
            {
                var leaveDate = leave.StartDate.Value.AddDays(i).Date;
                var getDTR = db.DailyTimeRecords.FirstOrDefault(a => a.UserName == user.UserName && a.DateTimeStamps == leaveDate);
                getDTR.Remarks = "Leave";
                getDTR.Accumulated = 540;
                db.SaveChanges();
            }
            if (leave.TypeOfLeave == "Sick Leave")
            {
                user.SickLeave -= (int)difference.Value.TotalDays;
            } else if (leave.TypeOfLeave == "Medical Leave")
            {
                user.MedicalLeave -= (int)difference.Value.TotalDays;
            }
            else if (leave.TypeOfLeave == "Bereavement Leave")
            {
                user.BereavementLeave -= (int)difference.Value.TotalDays;
            }
            else if (leave.TypeOfLeave == "Personal Leave")
            {
                user.PersonalLeave -= (int)difference.Value.TotalDays;
            }
            else if (leave.TypeOfLeave == "Paternity Leave")
            {
                user.PaternityLeave -= (int)difference.Value.TotalDays;
            }
            else if (leave.TypeOfLeave == "Maternity Leave")
            {
                user.MaternityLeave -= (int)difference.Value.TotalDays;
            }
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
                OvertimeGrid.ItemsSource = data;
            }
            else
            {
                Magnifier.Visibility = Visibility.Visible;
                var data = db.Leaves.ToList();
                OvertimeGrid.ItemsSource = data;
            }
        }

    }
}
