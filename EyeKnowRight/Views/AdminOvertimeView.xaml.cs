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

        private void ResetGrid(dynamic item = null)
        {
            string username = Application.Current.Properties["UserName"].ToString();
            var employeeModel = db.Employees.Where(a => a.UserName == username).FirstOrDefault();

            if (item != null)
            {
                OvertimeGrid.ItemsSource = item;
            }
            else if (employeeModel.SupervisedDepartment != null )
            {
                List<Overtime> newOvertime = new List<Overtime>();
                foreach (var user in db.Employees.ToList())
                {
                    foreach (var over in db.Overtimes.ToList())
                    {
                        if (user.Department == employeeModel.SupervisedDepartment && user.UserName == over.UserName)
                        {
                            newOvertime.Add(over);
                         }
                    }
                   
                }
             
                OvertimeGrid.ItemsSource = newOvertime;
                
            }
            else
            {
                var data = db.Overtimes.ToList();
                OvertimeGrid.ItemsSource = data;
            }


        }

        EyeKnowRightDB db = new EyeKnowRightDB();
        public AdminOvertimeView()
        {
            InitializeComponent();
            ResetGrid();
        }

        private void OvertimeClick(object sender, RoutedEventArgs e)
        {
            int overtimePk = (int)((Button)sender).Tag;
            var overtime = db.Overtimes.FirstOrDefault(a => a.OvertimePK == overtimePk);
            DataContext = overtime;
        
        }

        private void OvertimeAccept(object sender, RoutedEventArgs e)
        {
            int overtimePK = Int32.Parse(OvertimePK.Text);
            var overtime = db.Overtimes.FirstOrDefault(a => a.OvertimePK == overtimePK);
            overtime.Status = "Accepted";
            db.SaveChanges();
            ResetGrid();

            Notification notification = new Notification();
            notification.NotificationToWho = overtime.UserName;
            notification.Message = "Your overtime has been accepted. Congratulations";
            db.Notifications.Add(notification);
            db.SaveChanges();
        }

        private void OvertimeReject(object sender, RoutedEventArgs e)
        {
            int overtimePK = Int32.Parse(OvertimePK.Text);
            var overtime = db.Overtimes.FirstOrDefault(a => a.OvertimePK == overtimePK);
            overtime.Status = "Rejected";
            db.SaveChanges();
            ResetGrid();
        }

        private void SearchChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text != "")
            {
                Magnifier.Visibility = Visibility.Hidden;
                var text = SearchBox.Text;
                var data = db.Overtimes
                  .Where(a =>
                  a.Reason.StartsWith(text) || a.Reason.EndsWith(text) ||
                  a.Status.StartsWith(text) || a.Status.EndsWith(text) ||
                  a.UserName.StartsWith(text) || a.UserName.EndsWith(text) 
               ).ToList();
                OvertimeGrid.ItemsSource = data;
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
