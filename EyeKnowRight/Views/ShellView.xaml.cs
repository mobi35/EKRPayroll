using EyeKnowRight.Models;
using System;
using System.Collections.Generic;
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

namespace EyeKnowRight.Views
{

    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
            try
            {
                UserNameText.Text = Application.Current.Properties["UserName"].ToString();
            }
            catch (Exception e)
            {

            }
            ButtonOpen.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            FuckingGrid.Margin = new Thickness(270, 15, 0, 0);
        }
        private void ClosePanel(object sender, RoutedEventArgs e)
        {
            FuckingGrid.Margin = new Thickness(50, 15, 0, 0);
        }
        private void OpenPanel(object sender, RoutedEventArgs e)
        {
            FuckingGrid.Margin = new Thickness(270, 15, 0, 0);
        }
        private void AccountSelect(object sender, RoutedEventArgs e)
        {
            FuckingGrid.Children.Add(new AccountView());

        }
        private void PayrollSelect(object sender, RoutedEventArgs e)
        {
            FuckingGrid.Children.Add(new PayrollView());

        }

        private void EmployeeOvertime(object sender, RoutedEventArgs e)
        {
            FuckingGrid.Children.Add(new OvertimeView());

        }

        private void DashboardSelect(object sender, RoutedEventArgs e)
        {
            FuckingGrid.Children.Add(new DashboardView());

        }

        
        private void EmployeeLeave(object sender, RoutedEventArgs e)
        {
            FuckingGrid.Children.Add(new EmployeeLeaveView());

        }

        private void AdminLeaveSelect(object sender, RoutedEventArgs e)
        {
            FuckingGrid.Children.Add(new AdminLeaveView());

        }

        EyeKnowRightDB db = new EyeKnowRightDB();
        private void LogoutAccount(object sender, RoutedEventArgs e)
        {
            var user = Application.Current.Properties["UserName"].ToString();
            var date = DateTime.Now.Date;
            var dtr = db.DailyTimeRecords.Where(a => a.DateTimeStamps == date && a.UserName == user).FirstOrDefault();
            dtr.TimeOut = DateTime.Now;
            db.SaveChanges();
            double timeInDate = 0;
            if (dtr.TimeIn != null)
            {
                if (dtr.TimeIn.Value.TimeOfDay.TotalMinutes < 480)
                {
                    timeInDate = 480;
                }
                else
                {
                    timeInDate = dtr.TimeIn.Value.TimeOfDay.TotalMinutes;
                }

              
              
                if (dtr.TimeOut.Value.TimeOfDay.TotalMinutes <= 1020)
                {

                    double accumulatedTime = dtr.TimeOut.Value.TimeOfDay.TotalMinutes - timeInDate;
                    dtr.Accumulated += accumulatedTime;
                }
                else
                {
                    dtr.Accumulated += 1020 - timeInDate;
                }

                int hour = (int)dtr.Accumulated  / 60;
                int minutes = (int)dtr.Accumulated % 60;
                dtr.AccumulatedString = $"{ hour }h:{ minutes }m";

                db.SaveChanges();


            }
            else
            {

            }

            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();

        }



    }
}
