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
      

        EyeKnowRightDB db = new EyeKnowRightDB();
        public AdminLeaveView()
        {
            InitializeComponent();
          //  var user = Application.Current.Properties["UserName"].ToString();
            var leave = db.Leaves.Where(a => a.Status == "Pending").ToList();
            LeaveGrid.ItemsSource = leave;
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
            if (leave.TypeOfLeave == "Sick Leave")
            {
                user.SickLeave -= (int)difference.Value.TotalDays;
            } else if (leave.TypeOfLeave == "Medical Leave")
            {

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
    }
}
