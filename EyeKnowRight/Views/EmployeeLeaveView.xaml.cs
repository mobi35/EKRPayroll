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
    public partial class EmployeeLeaveView : UserControl
    {
      

        EyeKnowRightDB db = new EyeKnowRightDB();
        public EmployeeLeaveView()
        {
            InitializeComponent();
            var user = Application.Current.Properties["UserName"].ToString();
            var name = db.Employees.FirstOrDefault(a => a.UserName == user);
            if(name.Gender == "Male")
            {
                TypeOfLeave.Items.Add("Paternity Leave");
            }
            else
            {
                TypeOfLeave.Items.Add("Maternity Leave");
            }
            var leave = db.Leaves.Where(a => a.UserName == user).ToList();

            LeaveGrid.ItemsSource = leave;
        }

        private void SendRequestLeave(object sender, RoutedEventArgs e)
        {
            if(StartLeaveDate.SelectedDate < DateTime.Now || EndLeaveDate.SelectedDate < DateTime.Now)
            {

                MessageBox.Show("No past dates");
            }else if (StartLeaveDate.SelectedDate == null || EndLeaveDate.SelectedDate == null)
            {
                MessageBox.Show("Please fill up the date");
            }
            else if (StartLeaveDate.SelectedDate > EndLeaveDate.SelectedDate)
            {
                MessageBox.Show("This is not a valid date");
            }
            else if (ReasonForLeave.Text == "")
            {
                MessageBox.Show("Please add a reason for leaving");
            }else if(TypeOfLeave.SelectedItem == null)
            {
                MessageBox.Show("Please add the type of leave");
            }else
            {
                TimeSpan? dateRangeComparison = EndLeaveDate.SelectedDate - StartLeaveDate.SelectedDate;
                var user = Application.Current.Properties["UserName"].ToString();
                var getUser = db.Employees.FirstOrDefault(a => a.UserName == user);

               // bool leaveOk = false;
                if(TypeOfLeave.Text == "Paternity Leave" && getUser.PaternityLeave < dateRangeComparison.Value.TotalDays)
                {
                    MessageBox.Show("Not enough paternity leave");
                }
                else if (TypeOfLeave.Text == "Sick Leave" && getUser.SickLeave < dateRangeComparison.Value.TotalDays)
                {
                    MessageBox.Show("Not enough sick leave");
                }else { 
                Leave leave = new Leave();
                leave.ReasonForLeaving = ReasonForLeave.Text;
                leave.UserName = user;
                leave.TypeOfLeave = TypeOfLeave.Text;
                leave.StartDate = StartLeaveDate.SelectedDate;
                leave.EndLeave = EndLeaveDate.SelectedDate;
                    leave.Status = "Pending";
                db.Leaves.Add(leave);
                db.SaveChanges();

                    var leaveList = db.Leaves.Where(a => a.UserName == user).ToList();

                    LeaveGrid.ItemsSource = leaveList;
                    MessageBox.Show("Leave Added");
                }
            }
            //MessageBox.Show("No past dates");
            //int payrollPK = (int)((Button)sender).Tag;
            //var deductions = db.Deductionss.Where(a => a.PayrollPK == payrollPK).ToList();
            //  DeductionGrid.ItemsSource = deductions;
        }
    }
}
