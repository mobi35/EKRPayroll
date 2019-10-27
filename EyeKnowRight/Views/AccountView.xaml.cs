using EyeKnowRight.Models;
using EyeKnowRight.Views;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    public partial class AccountView : UserControl, INotifyPropertyChanged
    {
        private BackgroundWorker _backGroundWorker = new BackgroundWorker();

        private int workerState;

        private string _gender;

        public string Gender {
            get { return _gender; }
            set
            {
                _gender = value;
            }
        }

        List<DailyTimeRecord> dailyTimeRecord  =new List<DailyTimeRecord>();

        public event PropertyChangedEventHandler PropertyChanged;
        public int WorkerState
        {
            get { return workerState; }
            set
            {
                workerState = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("WorkerState"));
            }
        }

        EyeKnowRightDB db = new EyeKnowRightDB();
        public AccountView()
        {
          
            InitializeComponent();
          
            var data = db.Employees.ToList();
            EmployeeGrid.ItemsSource = data;
        }
        int numberOfWrong = 0;
     
        private int CheckValidations()
        {
            numberOfWrong = 0;
            if (Password.Password != RepeatPassword.Password)
            {
                numberOfWrong++;
                RepeatPassword_ValidationMsg.Text = "Password doesn't match";
                RepeatPassword_ValidationMsg.Visibility = Visibility.Visible;
                StepChangeVisibility(2);
            }
            else
            {
                RepeatPassword_ValidationMsg.Visibility = Visibility.Collapsed;
               
            }


            // START FIRST NAME

            if (FirstName.Text == "")
            {
                numberOfWrong++;
                StepChangeVisibility(1);
                FirstName_ValidationMsg.Text = "This field is required";
                FirstName_ValidationMsg.Visibility = Visibility.Visible;
            }
            else
            {

                FirstName_ValidationMsg.Visibility = Visibility.Collapsed;
                
            }

            if (MiddleName.Text == "")
            {
                numberOfWrong++;
                StepChangeVisibility(1);

                MiddleName_ValidationMsg.Text = "This field is required";
                MiddleName_ValidationMsg.Visibility = Visibility.Visible;
            }
            else
            {
                MiddleName_ValidationMsg.Visibility = Visibility.Collapsed;
               
            }

            if (LastName.Text == "")
            {
                numberOfWrong++;
                StepChangeVisibility(1);

                LastName_ValidationMsg.Text = "This field is required";
                LastName_ValidationMsg.Visibility = Visibility.Visible;
            }
            else
            {

                LastName_ValidationMsg.Visibility = Visibility.Collapsed;
              
            }

            if (Salary.Text == "")
            {
                Salary.Text = "0";
                numberOfWrong++;
                StepChangeVisibility(3);
                Salary_ValidationMsg.Text = "This field is required";
                Salary_ValidationMsg.Visibility = Visibility.Visible;
            }
            else
            {

                Salary_ValidationMsg.Visibility = Visibility.Collapsed;
                
            }

            if (Password.Password == "")
            {
                numberOfWrong++;
                StepChangeVisibility(2);

                Password_ValidationMsg.Text = "This field is required";
                Password_ValidationMsg.Visibility = Visibility.Visible;
            }
            else
            {
                Password_ValidationMsg.Visibility = Visibility.Collapsed;
                
            }

            if (RepeatPassword.Password == "")
            {
                numberOfWrong++;
                StepChangeVisibility(2);
                RepeatPassword_ValidationMsg.Text = "This field is required";
                RepeatPassword_ValidationMsg.Visibility = Visibility.Visible;
            }
            else
            {
                Salary_ValidationMsg.Visibility = Visibility.Collapsed;
             
            }


            if (FirstName.Text != "")
            {
                char[] numbers = "1234567890".ToCharArray();
                foreach (var num in numbers)
                {

                    if (FirstName.ToString().Contains(num.ToString()))
                    {
                        numberOfWrong++;
                        StepChangeVisibility(1);
                        FirstName_ValidationMsg.Text = "This is not a valid name";
                        FirstName_ValidationMsg.Visibility = Visibility.Visible;
                        break;
                    }
                    else
                    {
                        FirstName_ValidationMsg.Visibility = Visibility.Collapsed;
                       
                    }
                }
            }

            return numberOfWrong;
        }
        public void UpdateDailyTimeRecord(string username, int months )
        {
            var computedDateMonths = DateTime.Now.AddMonths(months);
            TimeSpan? totalDaysOfMonths = computedDateMonths - DateTime.Now;
            var dateNow = DateTime.Now;
           
            for (int i = 0; i <= totalDaysOfMonths.Value.TotalDays; i++)
            {
                DailyTimeRecord dailyTimeRecord = new DailyTimeRecord();
                dailyTimeRecord.DateTimeStamps = DateTime.Now.AddDays(i).Date;
                dailyTimeRecord.UserName = username;
                db.DailyTimeRecords.Add(dailyTimeRecord);
                db.SaveChanges();
            }
        }
        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var xx = (Employee)EmployeeGrid.SelectedItem;
            //MessageBox.Show(xx.StartPayroll + " ");
            EmployeePrintView employeePrint = new EmployeePrintView(xx.EmployeePK);
            employeePrint.Show();
        }
        private void AppraisalDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var xx = (Evaluation)EmployeeAppraisal.SelectedItem;
            //MessageBox.Show(xx.StartPayroll + " ");
            EvaluationPrintView evaluation = new EvaluationPrintView(xx.EvaluationPK);
            evaluation.Show();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void AddUser(object sender, RoutedEventArgs e)
        {
            if (CheckValidations() == 0) {
                if (Mode.Text == "Add") { 
            
            Employee addNew = new Employee();
            addNew.EmployeeID = "New";
            if (Gender_Male.IsChecked == true)
            {
                addNew.Gender = Gender_Male.Content.ToString();
            }else
            {
                addNew.Gender = Gender_Female.Content.ToString();
            }
            addNew.Picture = imageString;
            addNew.Age =  GiveBirthday(addNew.BirthDate);
            addNew.Salary = 0;
            addNew.FirstName = FirstName.Text;
            addNew.MiddleName = MiddleName.Text;
            addNew.LastName = LastName.Text;
            addNew.Email = Email.Text;
            addNew.Password = Password.Password;
            addNew.Address = Street.Text + ", " + City.Text;
            addNew.BirthDate = BirthDate.SelectedDate;
            addNew.Position = Position.Text;
            addNew.PersonalLeave = Int32.Parse(PersonalLeave.Text);
            addNew.MaternityLeave = Int32.Parse(MaternityLeave.Text);
            addNew.PaternityLeave = Int32.Parse(PaternityLeave.Text);
            addNew.SickLeave = Int32.Parse(SickLeave.Text);
            addNew.BereavementLeave = Int32.Parse(BereavementLeave.Text);
            addNew.MedicalLeave = Int32.Parse(MedicalLeave.Text);
            addNew.Department = Department.Text;
            addNew.IsActive = true;
            addNew.SSSNumber = SSSNumber.Text;
            addNew.PagibigNumber = PagibigNumber.Text;
            addNew.TINNumber = TINNumber.Text;
            addNew.LastAppraiseDate = null;
            addNew.Salary = Double.Parse(Salary.Text);
            addNew.JobTitle = JobTitle.SelectedValue.ToString();
            addNew.UserName = UserName.Text;
            addNew.DateRegistered = DateTime.Now;
            addNew.EmployeeID = "Tanjiro";
            addNew.Age = 69;
             addNew.DaysContract = Int32.Parse(MonthsOfStay.Text);
            db.Employees.Add(addNew);

            db.SaveChanges();

             UpdateDailyTimeRecord(UserName.Text, addNew.DaysContract);

             MaterialDesign.IsOpen = true;
                }else if (Mode.Text == "Edit")
                {
                    if(CheckValidations() == 0) { 
                    int employeePk = Int32.Parse(EmployeePK.Text);
                    var employee = db.Employees.FirstOrDefault(a => a.EmployeePK == employeePk);
                    if (Gender_Male.IsChecked == true)
                    {
                        employee.Gender = Gender_Male.Content.ToString();
                    }
                    else
                    {
                        employee.Gender = Gender_Female.Content.ToString();
                    }
                    employee.Salary = 0;
                    employee.FirstName = FirstName.Text;
                    employee.MiddleName = MiddleName.Text;
                    employee.LastName = LastName.Text;
                    employee.Email = Email.Text;
                    employee.Password = Password.Password;
                    employee.Address = Street.Text + ", " + City.Text;
                    employee.BirthDate = BirthDate.SelectedDate;
                    employee.Position = Position.Text;
                    employee.PersonalLeave = Int32.Parse(PersonalLeave.Text);
                    employee.MaternityLeave = Int32.Parse(MaternityLeave.Text);
                    employee.PaternityLeave = Int32.Parse(PaternityLeave.Text);
                    employee.SickLeave = Int32.Parse(SickLeave.Text);
                    employee.BereavementLeave = Int32.Parse(BereavementLeave.Text);
                    employee.MedicalLeave = Int32.Parse(MedicalLeave.Text);
                    employee.Department = Department.Text;
                    employee.SSSNumber = SSSNumber.Text;
                    employee.PagibigNumber = PagibigNumber.Text;
                    employee.TINNumber = TINNumber.Text;
                    employee.Salary = Double.Parse(Salary.Text);
                    employee.JobTitle = JobTitle.Text;
                    employee.UserName = UserName.Text;
                    employee.DateRegistered = DateTime.Now;
                    employee.EmployeeID = "Tanjiro";
                    employee.Age = 69;
                  
                    db.SaveChanges();
                        SuccessfullyEdited.IsOpen = true;
                    }
                }
                var data = db.Employees.ToList();
                EmployeeGrid.ItemsSource = data;
            }
            
        }

       private void GetAppraisal(object sender, RoutedEventArgs e)
        {
            string userName = (string)((Button)sender).Tag;
            var getEmployeeAppraisal = db.Evaluations.Where(a => a.UserName == userName).ToList();
            EmployeeAppraisal.ItemsSource = getEmployeeAppraisal;

        }
       private void GetEdit(object sender, RoutedEventArgs e)
        {

            if (sender != null)
            {
                int employeeId = (int)((Button)sender).Tag;
                var employee = db.Employees.FirstOrDefault(a => a.EmployeePK == employeeId);
                DataContext = employee;
                try { 
                Street.Text = employee.Address.ToString().Split(',')[0];
                City.Text = employee.Address.ToString().Split(',')[1].Trim();
                Password.Password = employee.Password;
                RepeatPassword.Password = employee.Password;
                }
                catch(Exception x)
                {

                }
                EmployeePK.Text = employeeId.ToString();
                Mode.Text = "Edit"; 
            }
        }

        private void EditUser(object sender, RoutedEventArgs e)
        {
            StepChangeVisibility(1);
            if (sender != null)
            {
                int employeeId = Int32.Parse(EmployeePK.Text);

                var employee = db.Employees.FirstOrDefault(a => a.EmployeePK == employeeId);
                employee.FirstName = FirstName.Text;
                employee.MiddleName = MiddleName.Text;
                employee.LastName = LastName.Text;
                employee.Email = Email.Text;
                employee.Salary = Double.Parse(Salary.Text);
                employee.JobTitle = JobTitle.Text;
                employee.Address = Street.Text + ", " + City.Text;
                employee.BirthDate = BirthDate.SelectedDate;
                employee.Position = Position.Text;
                employee.UserName = UserName.Text;
                employee.BirthDate = DateTime.Now;
                employee.DateRegistered = DateTime.Now;
                if (Gender_Male.IsChecked == true)
                {
                    employee.Gender = Gender_Male.Content.ToString();
                }
                else
                {
                    employee.Gender = Gender_Female.Content.ToString();
                }
                db.SaveChanges();


                var data = db.Employees.ToList();
                EmployeeGrid.ItemsSource = data;

            }
        }
        int step = 1;
        private void PreviousStep_Click(object sender, RoutedEventArgs e)
        {
            if(step > 1) { 
            step--;
            }
            StepChangeVisibility(step);
            StepsCounter.Text = step.ToString();
        }

        private void NextStep_Click(object sender, RoutedEventArgs e)
        {
            if(ThirdStep != null)
            {
                if(Gender == "Male")
                {
                    MaternityPanel.Visibility = Visibility.Collapsed;
                    PaternityPanel.Visibility = Visibility.Visible;
                }else
                {
                    MaternityPanel.Visibility = Visibility.Visible;
                    PaternityPanel.Visibility = Visibility.Collapsed;
                }

            }
            if(step < 5) {  
            step++;
            }
            StepChangeVisibility(step);
            StepsCounter.Text = step.ToString();
        }
        public int AttendancePK = 0;

        private void GetAttendance(object sender, RoutedEventArgs e)
        {
            int attendancePK = (int)((Button)sender).Tag;
         
             var employee = db.Employees.FirstOrDefault(a => a.EmployeePK == attendancePK);
            AttendancePK = employee.EmployeePK;
            EmployeeAttendance.ItemsSource = db.DailyTimeRecords.Where(a => a.UserName == employee.UserName).ToList();


        }

        private void StepChangeVisibility(int step)
        {
          
            LastStep.Visibility = Visibility.Collapsed;
            SecondStep.Visibility = Visibility.Collapsed;
            ThirdStep.Visibility = Visibility.Collapsed;
            Fourth.Visibility = Visibility.Collapsed;
            FirstStep.Visibility = Visibility.Collapsed;
            StepsCounter.Text = step.ToString();

            if (step == 1) { 
                FirstStep.Visibility = Visibility.Visible;
                this.step = 1;
            }
            else if(step == 2) {  
                SecondStep.Visibility = Visibility.Visible;
                this.step = 2;
            }
            else if(step == 3) { 
                ThirdStep.Visibility = Visibility.Visible;
                this.step = 3;
            }else if (step== 4) 
            {
                Fourth.Visibility = Visibility.Visible;
                this.step = 4;
            }
            else if (step == 5)
            {
                LastStep.Visibility = Visibility.Visible;
                this.step = 5;
            }

        }

        private void AddNewUserClick(object sender, RoutedEventArgs e)
        {
            StepChangeVisibility(1);
            Mode.Text = "Add";
            DataContext = null;
        }

        private void DoneClose(object sender, RoutedEventArgs e)
        {
            AddUserDialog.IsOpen = false;
        }

        private void AccountDeleteYes(object sender, RoutedEventArgs e)
        {
            List<int> userToDelete = new List<int>();
            for(int i = EmployeeGrid.SelectedItems.Count - 1; i>= 0; i--)
            {
                userToDelete.Add(((Employee)EmployeeGrid.SelectedItems[i]).EmployeePK);
            }
            foreach (int id in userToDelete)
            {
               var selectEmployee =  db.Employees.First(a => a.EmployeePK == id);
                selectEmployee.IsActive = false;
                db.SaveChanges();
            }
            SuccessfullyDeletedDialogBox.IsOpen = true;
            SuccessfullyDeletedDialogBoxText.Text = $"Successfully Terminate {EmployeeGrid.SelectedItems.Count} users";
            var data = db.Employees.ToList();
            EmployeeGrid.ItemsSource = data;
        }
        private void AccountRestoreYes(object sender, RoutedEventArgs e)
        {
            List<int> userToRestore = new List<int>();
            for (int i = EmployeeGrid.SelectedItems.Count - 1; i >= 0; i--)
            {
                userToRestore.Add(((Employee)EmployeeGrid.SelectedItems[i]).EmployeePK);
            }
            foreach (int id in userToRestore)
            {
                var selectEmployee = db.Employees.First(a => a.EmployeePK == id);
                selectEmployee.IsActive = true;
                db.SaveChanges();
            }
            SuccessfullyDeletedDialogBox.IsOpen = true;
            SuccessfullyDeletedDialogBoxText.Text = $"Successfully Restored {EmployeeGrid.SelectedItems.Count} users";
            var data = db.Employees.ToList();
            EmployeeGrid.ItemsSource = data;
        }
        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            DeleteText.Text = $"Are you sure you want to delete {EmployeeGrid.SelectedItems.Count} user/s?";
            AccountDeleteDialog.IsOpen = true;
        }

        private void RestoreUser(object sender, RoutedEventArgs e)
        {
            RestoreText.Text = $"Are you sure you want to restore {EmployeeGrid.SelectedItems.Count} user/s?";
            AccountRestoreDialog.IsOpen = true;
        }

        private void OnSelect(object sender, SelectionChangedEventArgs e)
        {
            AccountDelete.IsEnabled = true;
            AccountRestore.IsEnabled = true;
        }

        private void SearchChanged(object sender, TextChangedEventArgs e)
        {
            if(SearchBox.Text != "")
            {
                Magnifier.Visibility = Visibility.Hidden;
                var text = SearchBox.Text;
                var data = db.Employees
                  .Where(a =>
                  a.FirstName.StartsWith(text) || a.FirstName.EndsWith(text) ||
                  a.MiddleName.StartsWith(text) || a.MiddleName.EndsWith(text) ||
                  a.LastName.StartsWith(text) || a.LastName.EndsWith(text) ||
                  a.UserName.StartsWith(text) || a.UserName.EndsWith(text) ||
                  a.Address.StartsWith(text) || a.Address.EndsWith(text)
               ).ToList();
                EmployeeGrid.ItemsSource = data;
            }
            else
            {
              
                Magnifier.Visibility = Visibility.Visible;
                var data = db.Employees.ToList();
                EmployeeGrid.ItemsSource = data;

            }
        }

        private void GetAccountDetailsData(object sender, RoutedEventArgs e)
        {
            if (sender != null)
            {

                int employeeId = (int)((Button)sender).Tag;

                var employee = db.Employees.FirstOrDefault(a => a.EmployeePK == employeeId);
                DataContext = employee;
                try
                {
                    Street.Text = employee.Address.ToString().Split(',')[0];
                    City.Text = employee.Address.ToString().Split(',')[1].Trim();
                    Password.Password = employee.Password;
                    RepeatPassword.Password = employee.Password;
                }
                catch (Exception x)
                {

                }

                AcountDetails.IsOpen = true;

                var dtr = db.DailyTimeRecords.Where(a => a.UserName == employee.UserName).ToList();

                EmployeeDTR.ItemsSource = dtr;
                EmployeeDTR.Items.Refresh();
            }
        }




        byte[] imageString;
        private void OpenFileDialog(object sender, RoutedEventArgs e)
        {
        
        }

        private void Button_Click(object sender, RoutedEventArgs e)
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
        

        private void PrintAttendance(object sender, RoutedEventArgs e)
        {
            DailyAttendanceReport dailyAttendance = new DailyAttendanceReport(dailyTimeRecord);
            dailyAttendance.Show();

        }
            private void StartDateUpdate(object sender, SelectionChangedEventArgs e)
        {


           if(AttendanceStartDate.SelectedDate != null & AttendanceEndDate.SelectedDate != null)
            {
                if(AttendanceStartDate.SelectedDate > AttendanceEndDate.SelectedDate)
                { 
                    MessageBox.Show("Please enter a valid date");
                }
                else
                {
                    var employee = db.Employees.FirstOrDefault(a => a.EmployeePK == AttendancePK);
                    var startDate = AttendanceStartDate.SelectedDate.Value.Date;
                    var endDate = AttendanceEndDate.SelectedDate.Value.Date;
                    var dailyRecords = db.DailyTimeRecords.Where(a => a.UserName == employee.UserName && a.DateTimeStamps >= startDate && a.DateTimeStamps <= endDate).ToList();
                    EmployeeAttendance.ItemsSource = dailyRecords;
                    dailyTimeRecord = dailyRecords;
                }
            }
        }

        private void EndDateUpdate(object sender, SelectionChangedEventArgs e)
        {
            if (AttendanceStartDate.SelectedDate != null & AttendanceEndDate.SelectedDate != null)
            {
                if (AttendanceStartDate.SelectedDate > AttendanceEndDate.SelectedDate)
                {
                    MessageBox.Show("Please enter a valid date");
                }
                else
                {
                    var employee = db.Employees.FirstOrDefault(a => a.EmployeePK == AttendancePK);
                    var startDate = AttendanceStartDate.SelectedDate.Value.Date;
                    var endDate = AttendanceEndDate.SelectedDate.Value.Date;
                     var dailyRecords = db.DailyTimeRecords.Where(a => a.UserName == employee.UserName && a.DateTimeStamps >= startDate && a.DateTimeStamps <= endDate).ToList();
                    EmployeeAttendance.ItemsSource = dailyRecords;
                    dailyTimeRecord = dailyRecords;
                }
            }
        }
        public static int? GiveBirthday(DateTime? dateOfBirth)
        {
            var birthDate = 0;
            try
            {
                if (dateOfBirth.Value.Month >= DateTime.Now.Month)
                {
                    birthDate = DateTime.Now.Year - dateOfBirth.Value.Year;
                    birthDate -= 1;
                }
                else
                {
                    birthDate = DateTime.Now.Year - dateOfBirth.Value.Year;
                }
            }
            catch (Exception e)
            {
            }
            return birthDate;
        }

        private void MaleCheck(object sender, RoutedEventArgs e)
        {
            if(Gender_Male.Content != null)
            _gender = Gender_Male.Content.ToString();
        }
        private void FemaleCheck(object sender, RoutedEventArgs e)
        {
            if (Gender_Female.Content != null)
                _gender = Gender_Female.Content.ToString();
        }
    }
}
