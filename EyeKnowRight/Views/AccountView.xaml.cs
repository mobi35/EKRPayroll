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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Header;

namespace EyeKnowRight
{


    public static class ValidatorExtensions
    {
        public static bool IsValidEmailAddress(this string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }
    }
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

        private void ResetGrid(dynamic item = null)
        {
            string username = Application.Current.Properties["UserName"].ToString();
            var employeeModel = db.Employees.Where(a => a.UserName == username).FirstOrDefault();

            if(item != null)
            {
                EmployeeGrid.ItemsSource = item;
            }
            else if (employeeModel.SupervisedDepartment != null)
            {
                var data = db.Employees.Where(a => a.Department == employeeModel.SupervisedDepartment && a.Position != "Admin" && a.UserName != employeeModel.UserName).ToList();
                EmployeeGrid.ItemsSource = data;
            }
            else
            {
                var data = db.Employees.ToList();
                EmployeeGrid.ItemsSource = data;
            }


        }

        public AccountView()
        {
          
            InitializeComponent();
           
            string username = Application.Current.Properties["UserName"].ToString();
            var employeeModel = db.Employees.Where(a => a.UserName == username).FirstOrDefault();

            ResetGrid();
           
            BirthDate.DisplayDateEnd = DateTime.Now.AddYears(-16);

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
            var employees = db.Employees.ToList();
            if (UserName.Text == "" && Mode.Text == "Add")
            {
                numberOfWrong++;
                StepChangeVisibility(2);
                UserName_ValidationMsg.Text = "This field is required";
                UserName_ValidationMsg.Visibility = Visibility.Visible;
            }
            else if (employees.Count > 0)
            {
                Employee last = db.Employees.ToList().Last();

                foreach (var emp in employees)
                {
                    if (UserName.Text == emp.UserName && Mode.Text == "Add")
                    {
                        numberOfWrong++;
                        StepChangeVisibility(2);
                        UserName_ValidationMsg.Text = "Username already existed";
                        UserName_ValidationMsg.Visibility = Visibility.Visible;
                        break;
                    }
                    else if (last == emp)
                    {
                        UserName_ValidationMsg.Visibility = Visibility.Collapsed;
                    }
                }

            }
            else
            {
                UserName_ValidationMsg.Visibility = Visibility.Collapsed;

            }

            if (Street.Text == "")
            {
                numberOfWrong++;
                StepChangeVisibility(1);
                Street_ValidationMsg.Text = "This field is required";
                Street_ValidationMsg.Visibility = Visibility.Visible;

            }
            else
            {
                Street_ValidationMsg.Visibility = Visibility.Collapsed;
            }


            if (City.Text == "")
            {
                numberOfWrong++;
                StepChangeVisibility(1);
                City_ValidationMsg.Text = "This field is required";
                City_ValidationMsg.Visibility = Visibility.Visible;
            }
            else
            {
                City_ValidationMsg.Visibility = Visibility.Collapsed;
            }

            if (BirthDate.SelectedDate.ToString() == "" && Mode.Text == "Add")
            {
                numberOfWrong++;
                StepChangeVisibility(1);
                BirthDate_ValidationMsg.Text = "This field is required";
                BirthDate_ValidationMsg.Visibility = Visibility.Visible;
            }
            else
            {
                BirthDate_ValidationMsg.Visibility = Visibility.Collapsed;
            }

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

            if (ImageName.Text == "" && Mode.Text == "Add")
            {
                numberOfWrong++;
                StepChangeVisibility(2);
                Image_Validation.Text = "Please upload your picture";
                Image_Validation.Visibility = Visibility.Visible;
            }
            else
            {
                Image_Validation.Visibility = Visibility.Collapsed;
            }


            if (Email.Text == "")
            {
                numberOfWrong++;
                StepChangeVisibility(2);
                Email_ValidationMsg.Text = "This field is required";
                Email_ValidationMsg.Visibility = Visibility.Visible;
            }else if (!ValidatorExtensions.IsValidEmailAddress(Email.Text))
            {
                numberOfWrong++;
                StepChangeVisibility(2);
                Email_ValidationMsg.Text = "Wrong Email Address";
                Email_ValidationMsg.Visibility = Visibility.Visible;
            }
            else
            {
                Email_ValidationMsg.Visibility = Visibility.Collapsed;
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
                numberOfWrong++;
                StepChangeVisibility(5);
                Salary_ValidationMsg.Text = "This field is required";
                Salary_ValidationMsg.Visibility = Visibility.Visible;
            }
            else if (Int32.Parse(Salary.Text) < 200 )
            {
                numberOfWrong++;
                StepChangeVisibility(5);
                Salary_ValidationMsg.Text = "Minimum of 200";
                Salary_ValidationMsg.Visibility = Visibility.Visible;
            }
            else if (Int32.Parse(Salary.Text) > 10000)
            {
                numberOfWrong++;
                StepChangeVisibility(5);
                Salary_ValidationMsg.Text = "Maximum of 10000";
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
                RepeatPassword_ValidationMsg.Visibility = Visibility.Collapsed;
             
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
                if (DateTime.Now.AddDays(i).Date.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.AddDays(i).Date.DayOfWeek != DayOfWeek.Sunday) { 
                DailyTimeRecord dailyTimeRecord = new DailyTimeRecord();
                dailyTimeRecord.DateTimeStamps = DateTime.Now.AddDays(i).Date;
                dailyTimeRecord.UserName = username;
                db.DailyTimeRecords.Add(dailyTimeRecord);
                db.SaveChanges();
                }
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

        private void ButtonPrint (object sender, RoutedEventArgs e)
        {
            int buttonId = (int)((Button)sender).Tag;
            //MessageBox.Show(xx.StartPayroll + " ");
            EmployeePrintView employeePrint = new EmployeePrintView(buttonId);
            employeePrint.Show();
        }
            private void AddUser(object sender, RoutedEventArgs e)
        {

           
            FirstName.IsEnabled = true;
            MiddleName.IsEnabled = true;
            LastName.IsEnabled = true;

            AddUserExecute();
        }

       private void GetAppraisal(object sender, RoutedEventArgs e)
        {
            string userName = (string)((Button)sender).Tag;
            var getEmployeeAppraisal = db.Evaluations.Where(a => a.UserName == userName).ToList();
            EmployeeAppraisal.ItemsSource = getEmployeeAppraisal;
        }

        private void PrintPayslip_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.IsEnabled = false;

                PrintDialog printDialog = new PrintDialog();
                if (printDialog.ShowDialog() == true)
                {
                    printDialog.PrintVisual(PrintPayslip,"Payslip");
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }
        
        private void GeneratePayslip(object sender, RoutedEventArgs e)
        {   
            int payrollPK = (int)((Button)sender).Tag;
            var payrollModel = db.Payrolls.FirstOrDefault(a => a.PayrollPK == payrollPK);
           
            int employeePK =Int32.Parse( userPayslip.Text);
            var employeeModel = db.Employees.FirstOrDefault(a => a.EmployeePK == employeePK);

            var deductionModel = db.Deductionss.FirstOrDefault(a => a.PayrollPK == payrollPK && a.UserName == employeeModel.UserName);

            Payslip paySlip = new Payslip();
            // USER DETAILS
            paySlip.EmployeeName = employeeModel.FirstName + " " +employeeModel.LastName;
            paySlip.EmployeeAddress = employeeModel.Address;
            paySlip.JobTitle = employeeModel.JobTitle;

            double totalAccumulated = 0;
            double totalEarned = 0;
            double totalLate = 0;
            double basicSalaryPerMinute = ((employeeModel.Salary / 9) / 60);
            double totalDeduction = 0;
            foreach (var dtr in db.DailyTimeRecords
                .Where(a => a.DateTimeStamps >= payrollModel.StartPayroll
            && a.DateTimeStamps <= payrollModel.EndPayroll && a.UserName == employeeModel.UserName))
            {
             
                totalLate += dtr.Late;
                totalAccumulated += dtr.Accumulated;
                totalEarned += dtr.Accumulated * basicSalaryPerMinute;


            }

            paySlip.Earnings_Accumulated = (int)totalAccumulated / 60 + " hr / " + (int)totalAccumulated % 60 + " min";

            paySlip.Earnings_TotalAccumulated = "P" + totalEarned.ToString("N");


            // DEDUCTIONS;;
            paySlip.Deductions_Late = (int)totalLate;
            paySlip.Deductions_Pagibig = deductionModel.PagibigDeduction;
            paySlip.Deductions_Philhealth = deductionModel.TinDeduction;
            paySlip.Deductions_SSS = deductionModel.SSSDeduction;
            paySlip.Deductions_TAX = 0;
           
            totalDeduction += totalLate;
            paySlip.Deductions_Late_String_Format = (int)totalLate / 60 + " hr / " + (int)totalLate % 60 + " m";
            totalDeduction += deductionModel.PagibigDeduction;
            totalDeduction += deductionModel.TinDeduction;
            totalDeduction += deductionModel.SSSDeduction;
            paySlip.Deductions_Total = "P"+ totalDeduction.ToString("N");

            paySlip.TotalSalary = totalEarned - totalDeduction;
            decimal NumberSalary =  Decimal.Round( (decimal)paySlip.TotalSalary,2);
            paySlip.TotalSalaryStringFormat = "P " + NumberSalary;
            DataContext = paySlip;
        }
        private void OpenPayslipList(object sender, RoutedEventArgs e)
        {
            int employeePK = (int)((Button)sender).Tag;
            userPayslip.Text = employeePK.ToString() ;
            ListPayslipGrid.ItemsSource = db.Payrolls.Where(a => a.IsActive == false).ToList();
        }
        private void GetEdit(object sender, RoutedEventArgs e)
        {

            if (sender != null)
            {
                FirstName.IsEnabled = false;
                MiddleName.IsEnabled = false;
                LastName.IsEnabled = false;

                PaternityLeave.IsEnabled = true;
                MaternityLeave.IsEnabled = true;
                PersonalLeave.IsEnabled = true;
                MedicalLeave.IsEnabled = true;
                SickLeave.IsEnabled = true;
                BereavementLeave.IsEnabled = true;

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
                UploadImageButton.Visibility = Visibility.Collapsed;
                BirthDate.Visibility = Visibility.Collapsed;
                StepChangeVisibility(1);
            }
        }


        private void TrainingClick(object sender, RoutedEventArgs e)
        {

            if (sender != null)
            {
                int employeeId = (int)((Button)sender).Tag;
                var employee = db.Employees.FirstOrDefault(a => a.EmployeePK == employeeId);
                employeeID.Text = employee.EmployeePK.ToString();
                TrainingType.ItemsSource = db.Trainings.Select(a => a.Name).ToList();
                TrainingList.ItemsSource = db.EmployeeTrainings.Where(a => a.UserName == employee.UserName).ToList();

                TrainingDate.DisplayDateStart = DateTime.Now;
            }

        }

        private void SubmitTraining(object sender, RoutedEventArgs e)
        {

            if (TrainingDate.SelectedDate != null) { 
            int employeePK = Int32.Parse(employeeID.Text);
            var employee = db.Employees.FirstOrDefault(a => a.EmployeePK == employeePK);

               

                EmployeeTraining empTrain = new EmployeeTraining();
            empTrain.FullName = employee.FirstName + " " + employee.LastName;
            empTrain.UserName = employee.UserName;
            empTrain.Training = TrainingType.Text;
            empTrain.DateOfTraining = TrainingDate.SelectedDate;
            empTrain.TimeOfTraining = TrainingTime.SelectedTime;
            empTrain.TrainingStatus = "Pending";
                db.EmployeeTrainings.Add(empTrain);

                Notification notification = new Notification();
                notification.NotificationToWho = empTrain.UserName;
                notification.Message = "You have a new training (" +  empTrain.Training +" ) on " + empTrain.DateOfTraining;
                db.Notifications.Add(notification);

                db.SaveChanges();



             TrainingList.ItemsSource = db.EmployeeTrainings.Where(a => a.UserName == employee.UserName).ToList();

            }
            else
            {
                MessageBox.Show("Select Date");
            }

        }
        
            private void AssignDepartmentClick(object sender, RoutedEventArgs e)
            {
                if (sender != null)
                {
                int employeeId = (int)((Button)sender).Tag;
                var employee = db.Employees.FirstOrDefault(a => a.EmployeePK == employeeId);

                DepartmentUserName.Text = employee.UserName;
            }

            }

       

      private void RemoveDepartment_Click(object sender, RoutedEventArgs e)
        {
            string userName = DepartmentUserName.Text;

            var employee = db.Employees.FirstOrDefault(a => a.UserName == userName);

            employee.SupervisedDepartment = null;
          
            db.SaveChanges();
            ResetGrid();

        }
        private void AddDepartment_Click(object sender, RoutedEventArgs e)
        {
            string userName = DepartmentUserName.Text;

          var employee = db.Employees.FirstOrDefault(a => a.UserName == userName);

            employee.SupervisedDepartment = DepartmentList.Text;
            db.SaveChanges();
            ResetGrid();

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
                ResetGrid();

               
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
                if(Position.SelectedItem == null)
                Position.SelectedIndex = 2;
          
                if (JobTitle.SelectedItem == null)
                    JobTitle.SelectedIndex = 1;
                if (Department.SelectedItem == null)
                    Department.SelectedIndex = 1;
                LastStep.Visibility = Visibility.Visible;
                this.step = 5;
            }

        }

        public void AddUserExecute()
        {
            if (CheckValidations() == 0)
            {
                if (Mode.Text == "Add")
                {
                   

                   
                    Employee addNew = new Employee();
                    addNew.EmployeeID = "New";
                    if (Gender_Male.IsChecked == true)
                    {
                        addNew.Gender = Gender_Male.Content.ToString();
                    }
                    else
                    {
                        addNew.Gender = Gender_Female.Content.ToString();
                    }
                    addNew.Picture = imageString;
                    addNew.Age = GiveBirthday(addNew.BirthDate);
                    addNew.Salary = 0;
                    addNew.FirstName = FirstName.Text;
                    addNew.MiddleName = MiddleName.Text;
                    addNew.LastName = LastName.Text;
                    addNew.Email = Email.Text;
                    addNew.Password = Password.Password;
                    addNew.Address = Street.Text + ", " + City.Text;
                    addNew.BirthDate = BirthDate.SelectedDate;
                    addNew.MaritalStatus = MaritalStatus.Text;
                    addNew.Position = Position.Text;
                    addNew.Status = "Active";
                    if (addNew.Gender == "Male")
                    {
                        addNew.PaternityLeave = 0;
                    }
                    else
                    {
                        addNew.MaternityLeave = 0;
                    }

                    addNew.SickLeave = 0;
                    addNew.BereavementLeave = 0;
                    addNew.MedicalLeave = 0;
                    addNew.PersonalLeave = 0;
                    addNew.Department = Department.Text;
                    addNew.IsActive = true;
                    addNew.SSSNumber = SSSNumber.Text;
                    addNew.PagibigNumber = PagibigNumber.Text;
                    addNew.TINNumber = TINNumber.Text;
                    addNew.LastAppraiseDate = null;
                    addNew.Salary = Double.Parse(Salary.Text);
                    addNew.JobTitle = JobTitle.Text;
                    addNew.UserName = UserName.Text;
                    addNew.DateRegistered = DateTime.Now;
                    addNew.EmployeeID = "Tanjiro";
                    addNew.Age = GiveBirthday(BirthDate.SelectedDate);
                    addNew.DaysContract = 30;
                    db.Employees.Add(addNew);

                    db.SaveChanges();

                    UpdateDailyTimeRecord(UserName.Text, addNew.DaysContract);
               
                    MaterialDesign.IsOpen = true;
                }
                else if (Mode.Text == "Edit")
                {
                
                    if (CheckValidations() == 0)
                    {

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
        
                        employee.BirthDate = BirthDate.SelectedDate;
                        employee.Position = Position.Text;
                        employee.PersonalLeave = float.Parse(PersonalLeave.Text);
                        
                        employee.SickLeave = float.Parse(SickLeave.Text);
                        if (employee.Gender == "Male")
                        {
                            employee.PaternityLeave = float.Parse(PaternityLeave.Text);
                        }
                        else
                        {
                            employee.MaternityLeave = float.Parse(MaternityLeave.Text);
                        }
                        employee.BereavementLeave = float.Parse(BereavementLeave.Text);
                        employee.MedicalLeave = float.Parse(MedicalLeave.Text);
                        employee.Department = Department.Text;
                        employee.SSSNumber = SSSNumber.Text;
                        employee.PagibigNumber = PagibigNumber.Text;
                        employee.TINNumber = TINNumber.Text;
                        employee.Salary = Double.Parse(Salary.Text);
                        employee.MaritalStatus = MaritalStatus.Text;
                        employee.JobTitle = JobTitle.Text;
                        employee.UserName = UserName.Text;
                        employee.DateRegistered = DateTime.Now;
                        employee.EmployeeID = "Tanjiro";
                        employee.Age = GiveBirthday(BirthDate.SelectedDate);

                        db.SaveChanges();
                        SuccessfullyEdited.IsOpen = true;
                    }
                }
                var data = db.Employees.ToList();
             



            }
 
             FirstName.Text = "" ;
            MiddleName.Text = "";
           LastName.Text = "";
             Email.Text = "";
          Password.Password = "";
            Street.Text = "";
                City.Text = "";
       
           Department.Text = "";
          
             SSSNumber.Text = "";
         PagibigNumber.Text = "";
             TINNumber.Text = "";
         
           Salary.Text = "";
     
            UserName.Text = "";
            ResetGrid();
        }
       

        void EnterClicked(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                AddUserExecute();
                e.Handled = true;
            }
        }

        private void AddNewUserClick(object sender, RoutedEventArgs e)
        {
            FirstName.IsEnabled = true;
            MiddleName.IsEnabled = true;
            LastName.IsEnabled = true;

            PaternityLeave.IsEnabled = false;
            MaternityLeave.IsEnabled = false;
            PersonalLeave.IsEnabled = false;
            MedicalLeave.IsEnabled = false;
            SickLeave.IsEnabled = false;
            BereavementLeave.IsEnabled = false;
            StepChangeVisibility(1);
            DataContext = null;
            Mode.Text = "Add";
            UploadImageButton.Visibility = Visibility.Visible;
            BirthDate.Visibility = Visibility.Visible;
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
                selectEmployee.Status = "Terminated";
                db.SaveChanges();
            }
            SuccessfullyDeletedDialogBox.IsOpen = true;
            SuccessfullyDeletedDialogBoxText.Text = $"Successfully Terminate {EmployeeGrid.SelectedItems.Count} users";
            ResetGrid();
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
                selectEmployee.NumberOfTries = 0;
                selectEmployee.Status = "Active";
                db.SaveChanges();
            }
            SuccessfullyDeletedDialogBox.IsOpen = true;
            SuccessfullyDeletedDialogBoxText.Text = $"Successfully Restored {EmployeeGrid.SelectedItems.Count} users";
            ResetGrid();
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
                ResetGrid(data);
            }
            else
            {
                Magnifier.Visibility = Visibility.Visible;
                ResetGrid();
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
                ImageName.Text = filename;
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
