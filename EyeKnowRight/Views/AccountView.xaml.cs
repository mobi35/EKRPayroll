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
    public partial class AccountView : UserControl, INotifyPropertyChanged
    {
        private BackgroundWorker _backGroundWorker = new BackgroundWorker();

        private int workerState;

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
       
        private void AddUser(object sender, RoutedEventArgs e)
        {
         
           
            // END FIRST NAME

            if (CheckValidations() == 0) {
              
           

                if (Mode.Text == "Add") { 
            
            Employee addNew = new Employee();
            addNew.EmployeeID = "Buratsimagon";
            if (Gender_Male.IsChecked == true)
            {
                addNew.Gender = Gender_Male.Content.ToString();
            }else
            {
                addNew.Gender = Gender_Female.Content.ToString();
            }

      
            addNew.Picture = imageString;
         
            addNew.Salary = 0;
            addNew.FirstName = FirstName.Text;
            addNew.MiddleName = MiddleName.Text;
            addNew.LastName = LastName.Text;
            addNew.Email = Email.Text;
            addNew.Password = Password.Password;
            addNew.Address = Street.Text + ", " + City.Text;
            addNew.BirthDate = BirthDate.SelectedDate;
            addNew.Position = Position.Text;
                 
            addNew.Salary = Double.Parse(Salary.Text);
            addNew.JobTitle = JobTitle.Text;
            addNew.UserName = UserName.Text;
            addNew.DateRegistered = DateTime.Now;
            addNew.EmployeeID = "Tanjiro";
            addNew.Age = 69;
      
            db.Employees.Add(addNew);

            db.SaveChanges();

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
            if(step < 3) {  
            step++;
            }
            StepChangeVisibility(step);
            StepsCounter.Text = step.ToString();
        }

        private void StepChangeVisibility(int step)
        {
          
            LastStep.Visibility = Visibility.Collapsed;
            SecondStep.Visibility = Visibility.Collapsed;
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
                LastStep.Visibility = Visibility.Visible;
                this.step = 3;
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
                db.Employees.Remove(selectEmployee);
                db.SaveChanges();
            }
            SuccessfullyDeletedDialogBox.IsOpen = true;
            SuccessfullyDeletedDialogBoxText.Text = $"Successfully Deleted {EmployeeGrid.SelectedItems.Count} users";
            var data = db.Employees.ToList();
            EmployeeGrid.ItemsSource = data;
        }
        private void DeleteUser(object sender, RoutedEventArgs e)
        {
            DeleteText.Text = $"Are you sure you want to delete {EmployeeGrid.SelectedItems.Count} user/s?";
            AccountDeleteDialog.IsOpen = true;
        }

        private void OnSelect(object sender, SelectionChangedEventArgs e)
        {
            AccountDelete.IsEnabled = true;
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
    }
}
