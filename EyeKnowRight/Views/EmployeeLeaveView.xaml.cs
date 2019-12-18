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
                Paternity.Visibility = Visibility.Visible;
            }
            else
            {
                TypeOfLeave.Items.Add("Maternity Leave");
                Maternity.Visibility = Visibility.Visible;
            }
            var leave = db.Leaves.Where(a => a.UserName == user).OrderByDescending(a => a.LeavePK).ToList();
            var userList = db.Employees.Where(a => a.UserName == user).ToList();
            DataContext = userList;
            LeaveGrid.ItemsSource = leave;

            StartLeaveDate.DisplayDateStart = name.DateRegistered.Value.Date.AddDays(1);
           
        }

        private void SendRequestLeave(object sender, RoutedEventArgs e)
        {
            TimeSpan? dateRangeComparison = EndLeaveDate.SelectedDate - StartLeaveDate.SelectedDate;
            int numberOfWorkingDays = 0;


            var userName = Application.Current.Properties["UserName"].ToString();
            var employeeLeave = db.Leaves.Where(a => a.UserName == userName).ToList();

            var overtime = db.Overtimes.Where(a => a.UserName == userName).ToList();
            bool leaveStack = false;
            bool overStack = false;



            for (int i = 0; i <= dateRangeComparison.Value.TotalDays; i++)
            {
                if (StartLeaveDate.SelectedDate.Value.Date.AddDays(i).DayOfWeek != DayOfWeek.Sunday
                && StartLeaveDate.SelectedDate.Value.Date.AddDays(i).DayOfWeek != DayOfWeek.Saturday
                     )
                {
                    foreach (var empl in employeeLeave)
                    {
                        if (empl.StartDate.Value.AddDays(i) == StartLeaveDate.SelectedDate.Value.AddDays(i))
                        {
                            leaveStack = true;
                        }
                    }

                    foreach (var over in overtime)
                    {
                        if (over.DateOfOvertime.Value.AddDays(i) == StartLeaveDate.SelectedDate.Value.AddDays(i))
                        {
                            overStack = true;
                        }
                    }


                    numberOfWorkingDays++;
                }
                var leaveList = db.Leaves.Where(a => a.UserName == userName).OrderByDescending(a => a.LeavePK).ToList();

                LeaveGrid.ItemsSource = leaveList;
            }



            if (TypeOfLeave.Text != "Sick Leave" && StartLeaveDate.SelectedDate < DateTime.Now )
            {
                MessageBox.Show("No past dates");
            } else if (StartLeaveDate.SelectedDate == null || EndLeaveDate.SelectedDate == null)
            {
                MessageBox.Show("Please fill up the date");
            }
            else if (StartLeaveDate.SelectedDate > EndLeaveDate.SelectedDate && TypeOfLeave.Text != "Sick Leave" )
            {
                MessageBox.Show("This is not a valid date");
            }
            else if (TypeOfLeave.Text == "Sick Leave" && StartLeaveDate.SelectedDate >= DateTime.Now.AddDays(-15))
            {
                MessageBox.Show("This date is too late. ");
            }
            else if (ReasonForLeave.Text == "")
            {
                MessageBox.Show("Please add a reason for leaving");
            } else if (TypeOfLeave.SelectedItem == null)
            {
                MessageBox.Show("Please add the type of leave");
            } else if (numberOfWorkingDays == 0)
            {
                MessageBox.Show("Please select atleast 1 working days");
            }
            else {

                var user = Application.Current.Properties["UserName"].ToString();
                var getUser = db.Employees.FirstOrDefault(a => a.UserName == user);

                // bool leaveOk = false;
                if (TypeOfLeave.Text == "Paternity Leave" && getUser.PaternityLeave < numberOfWorkingDays)
                {
                    MessageBox.Show("Not enough paternity leave");
                }
                else if (TypeOfLeave.Text == "Bereavement Leave" && getUser.BereavementLeave < numberOfWorkingDays)
                {
                    MessageBox.Show("Not enough Bereavement leave");
                }
                else if (TypeOfLeave.Text == "Maternity Leave" && getUser.MaternityLeave < numberOfWorkingDays)
                {
                    MessageBox.Show("Not enough Maternity leave");
                }
                else if (TypeOfLeave.Text == "Medical Leave" && getUser.MedicalLeave < numberOfWorkingDays)
                {
                    MessageBox.Show("Not enough Medical leave");
                }
                else if (TypeOfLeave.Text == "Personal Leave" && getUser.PersonalLeave < numberOfWorkingDays)
                {
                    MessageBox.Show("Not enough Personal leave");
                }
                else if (TypeOfLeave.Text == "Sick Leave" && getUser.SickLeave < numberOfWorkingDays)
                {
                    MessageBox.Show("Not enough sick leave");
                } else {

                    if (overStack)
                    {
                        MessageBox.Show("You have overtime in this date.");
                    }
                    else if (!leaveStack) {
                        Leave leave = new Leave();
                        leave.ReasonForLeaving = ReasonForLeave.Text;
                        leave.UserName = user;
                        leave.TypeOfLeave = TypeOfLeave.Text;
                        leave.StartDate = StartLeaveDate.SelectedDate.Value.Date;
                        leave.EndLeave = EndLeaveDate.SelectedDate.Value.Date;
                        leave.Status = "Pending";
                        db.Leaves.Add(leave);
                        db.SaveChanges();


                        Notification notification = new Notification();
                        notification.NotificationToWho = db.Employees.FirstOrDefault(a => a.UserName == user).Department;
                        notification.UserName = user;
                        notification.Message = user+  " has requested for a leave on " + StartLeaveDate.SelectedDate.Value.ToString("D") + " until " + EndLeaveDate.SelectedDate.Value.ToString("D");
                        db.Notifications.Add(notification);
                        db.SaveChanges();


                        var leaveList = db.Leaves.Where(a => a.UserName == user).OrderByDescending(a => a.LeavePK).ToList();

                        LeaveGrid.ItemsSource = leaveList;
                        MessageBox.Show("Leave Added");
                    } else
                    {
                        MessageBox.Show("No leave tenure");
                    }
                }
            }
            //MessageBox.Show("No past dates");
            //int payrollPK = (int)((Button)sender).Tag;
            //var deductions = db.Deductionss.Where(a => a.PayrollPK == payrollPK).ToList();
            //  DeductionGrid.ItemsSource = deductions;
        }
    }
}
