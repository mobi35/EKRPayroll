using EyeKnowRight.Models;
using EyeKnowRight.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace EyeKnowRight.Views
{

    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
            try
            {
                var user  = Application.Current.Properties["UserName"].ToString();
                UserNameText.Text = user;

               var employee = db.Employees.FirstOrDefault(a => a.UserName == user);
                if(employee.Position == "Admin")
                {
                    AdminPanel.Visibility = Visibility.Visible;
                    HRPanel.Visibility = Visibility.Collapsed;
                    UserPanel.Visibility = Visibility.Collapsed;
                }
                else if(employee.Position == "Employee")
                {
                    UserPanel.Visibility = Visibility.Visible;
                    AdminPanel.Visibility = Visibility.Collapsed;
                    HRPanel.Visibility = Visibility.Collapsed;

                }
                else
                {
                    HRPanel.Visibility = Visibility.Visible;
                    AdminPanel.Visibility = Visibility.Collapsed;
                    UserPanel.Visibility = Visibility.Collapsed;
                }
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

        private void AppraisalSelect(object sender, RoutedEventArgs e)
        {
            FuckingGrid.Children.Add(new AppraisalView());

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
        public void DisableOtherOptions()
        {
            ChangeAccountPassword.Visibility = Visibility.Collapsed;
            UpdateAccountInformation.Visibility = Visibility.Collapsed;
            ChangeAccountPicture.Visibility = Visibility.Collapsed;
        }
        private void Click_ChangePassword(object sender, RoutedEventArgs e)
        {
            DisableOtherOptions();
            ChangeAccountPassword.Visibility = Visibility.Visible;
        }

   
        private void Click_UpdateInformation(object sender, RoutedEventArgs e)
        {
            DisableOtherOptions();
            UpdateAccountInformation.Visibility = Visibility.Visible;

            var userName = Application.Current.Properties["UserName"].ToString();
            var employee = db.Employees.FirstOrDefault(a => a.UserName == userName);

            DataContext = employee;
        }
       
        private void Click_ChangePicture(object sender, RoutedEventArgs e)
        {
            DisableOtherOptions();
            ChangeAccountPicture.Visibility = Visibility.Visible;

            var user = Application.Current.Properties["UserName"].ToString();
            var employee = db.Employees.FirstOrDefault(a => a.UserName == user);
            DataContext = employee;
        }


        private void Update_Password(object sender, RoutedEventArgs e)
        {
            var userName = Application.Current.Properties["UserName"].ToString();

            var employee = db.Employees.FirstOrDefault(a => a.UserName == userName);


            if (CurrentPassword.Password == "" || NewPassword.Password == "" 
                || NewPasswordConfirm.Password == "" )
            {
                CompleteFieldValidation.Visibility = Visibility.Visible;
                CompleteFieldValidation_Text.Text = "Please fill all the fields";
                
            }else
            {
                CompleteFieldValidation.Visibility = Visibility.Collapsed;
            }
            
            if(employee.Password != CurrentPassword.Password)
            {
                CurrentPassword_Validation_Text.Text = "Wrong Old Password";
                CurrentPassword_Validation.Visibility = Visibility.Visible;
            }else
            {
                CurrentPassword_Validation.Visibility = Visibility.Collapsed;
            }

            if (employee.Password != CurrentPassword.Password)
            {
              // Wrong Old Password
            }
            else if (NewPassword.Password != NewPasswordConfirm.Password)
            {
                CurrentPassword_Validation.Visibility = Visibility.Collapsed;
                OldPassword_Validation.Visibility = Visibility.Visible;
                OldPassword_Validation.Text = "New Password Doesn't Match";
            }else
            {
                OldPassword_Validation.Visibility = Visibility.Visible;
                OldPassword_Validation.Foreground = Brushes.Green;
                OldPassword_Validation.Text = "Successfully Changed";
                employee.Password = NewPassword.Password;
                db.SaveChanges();
                NewPasswordConfirm.Password = "";
                NewPassword.Password = "";
                CurrentPassword.Password = "";
            }


        }

        byte[] imageString;
        private void Image_Upload(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".jpg";
            ofd.Filter = "Image files (*.jpg)|*.jpg|All Files (*.*)|*.*";

            if (ofd.ShowDialog() == true)
            {
                string filename = ofd.FileName;
                // ImageName.Text = filename;

                BitmapImage bi3 = new BitmapImage();
                bi3.BeginInit();
                bi3.UriSource = new Uri(filename, UriKind.Relative);
                bi3.EndInit();

                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bi3));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    imageString = ms.ToArray();
                }

            }
        }

        private void GetAccountDetailsData(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {
                var user = Application.Current.Properties["UserName"].ToString();
              

                var employee = db.Employees.FirstOrDefault(a => a.UserName == user);
                DataContext = employee;
              

                AcountDetails.IsOpen = true;

                var dtr = db.DailyTimeRecords.Where(a => a.UserName == employee.UserName).ToList();

                EmployeeDTR.ItemsSource = dtr;
                EmployeeDTR.Items.Refresh();
            }
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

        

       private void Update_ProfilePicture(object sender, RoutedEventArgs e)
        {

            if(imageString == null)
            {
                MessageBox.Show("Please select a picture first");
            }else { 

            var user = Application.Current.Properties["UserName"].ToString();
            var employee = db.Employees.FirstOrDefault(a => a.UserName == user);

            employee.Picture = imageString;
            db.SaveChanges();

            DataContext = employee;

                MessageBox.Show("Profile picture changed. The picture will show after you login again");
            }

        }
        private void UpdateInformationClick(object sender, RoutedEventArgs e)
        {
            if (FirstName.Text == "" || MiddleName.Text == "" || LastName.Text == "" || Email.Text == "" || Address.Text == "")
            {
                UpdateInformationValidation.Visibility = Visibility.Visible;
                UpdateInformationValidation.Text = "Please fill all the fields";
            }else
            {


                UpdateInformationValidation.Visibility = Visibility.Visible;
                UpdateInformationValidation.Foreground = Brushes.Green;
                UpdateInformationValidation.Text = "Successfully Updated";
                var user = Application.Current.Properties["UserName"].ToString();
                var employee = db.Employees.FirstOrDefault(a => a.UserName == user);

                employee.FirstName = FirstName.Text;
                employee.MiddleName = MiddleName.Text;
                employee.LastName = LastName.Text;
                employee.Email = Email.Text;
                employee.Address = LastName.Text;


                db.SaveChanges();
            }


       
        }

        private void ReportClick(object sender, RoutedEventArgs e)
        {
            Reports(TypeOfReports.Text, StartDate.SelectedDate, EndDate.SelectedDate, Int32.Parse(NumberOfEntries.Text));
            MessageBox.Show("PRINTED");
        }

        public void Reports(string typeOfReports, DateTime? startDate, DateTime? endDate, int numberOfEntries)
        {

            if (typeOfReports == "Attendance Reports")
            {

                List<AttendanceReportViewModel> reportsVM = new List<AttendanceReportViewModel>();

                var employee = db.Employees.ToList();

                var dailyTimeRecord = db.DailyTimeRecords.Where(a => a.DateTimeStamps >= startDate && a.DateTimeStamps <= endDate).ToList();

                foreach (var emp in employee)
                {
                    foreach (var dtr in dailyTimeRecord)
                    {
                        if (emp.UserName == dtr.UserName)
                        {
                            reportsVM.Add(new AttendanceReportViewModel
                            {
                                FullName = emp.FirstName + " " + emp.MiddleName + " " + emp.LastName,
                                TimeIn = dtr.TimeIn,
                                TimeOut = dtr.TimeOut,
                                DateTimeStamps = dtr.DateTimeStamps,
                                Remarks = dtr.Remarks
                            });

                        }
                    }
                }
                MainReportsView reportsView = new MainReportsView(reportsVM.Take(numberOfEntries).ToList());
                reportsView.Show();

            }else if (typeOfReports == "Assesment Reports")
            {
                var assesmentList = db.Evaluations.Where(a => a.DateAppraise >= startDate && a.DateAppraise <= endDate).ToList();
                MainAssesmentReportsView assesmentReports = new MainAssesmentReportsView(assesmentList.Take(numberOfEntries).ToList());
                assesmentReports.Show();
            }else if (typeOfReports == "Employee Masterlist")
            {
                var employeeList = db.Employees.Where(a => a.DateRegistered >= startDate && a.DateRegistered <= endDate);
                MainEmployeeMasterlistView mainEmployee = new MainEmployeeMasterlistView(employeeList.Take(numberOfEntries).ToList());
                mainEmployee.Show();
            }else if (typeOfReports == "Top Performing Employees")
            {
                var employees = db.Employees.ToList();
                List<TopPerformingEmployees> topPerformingEmp = new List<TopPerformingEmployees>();
                foreach(var emp in employees)
                {
                    double empLate = 0;
                    int totalAttendance = 0;
                    foreach (var dtr in db.DailyTimeRecords.Where(a => a.DateTimeStamps >= startDate && a.DateTimeStamps <= endDate).ToList())
                    {
                        if(emp.UserName == dtr.UserName)
                        {
                            empLate += dtr.Late;
                            if (dtr.TimeIn != null && dtr.TimeOut != null)
                                totalAttendance++;
                        }
                    }
                    topPerformingEmp.Add(new TopPerformingEmployees
                    {
                        UserName = emp.UserName,
                        TotalAttendance = totalAttendance,
                        TotalLate = empLate
                    });
                }

                MainTopPerformingView mainTop = new MainTopPerformingView(topPerformingEmp.OrderBy(a => a.TotalLate).Take(numberOfEntries).ToList());
                mainTop.Show();

            }


               


        }
    }
}
