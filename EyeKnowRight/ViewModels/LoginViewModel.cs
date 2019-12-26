using Caliburn.Micro;
using EyeKnowRight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EyeKnowRight.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string age ="";

        private readonly ShellViewModel shellViewModel;
        private readonly IWindowManager windowManager;

        public LoginViewModel(ShellViewModel shellViewModel, IWindowManager windowManager)
        {
            this.windowManager = windowManager;
            this.shellViewModel = shellViewModel;
        }
        EyeKnowRightDB dbz = new EyeKnowRightDB();
        public string Bagwis
        {
            get { return age; }
            set { age = value; }
        }

        private string username;
        private string password;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
        private string passwordValidation ;
        public string PasswordValidation
        {
            get { return passwordValidation; }
            set { passwordValidation = value;
             
            }
        }


        public void ExecuteFilterView(ActionExecutionContext context)
        {
            var keyArgs = context.EventArgs as KeyEventArgs;

            if (keyArgs != null && keyArgs.Key == Key.Enter)
            {
                Login();
            }
        }

        public void Login()
        {

          
            var employee = dbz.Employees.Where(a => a.UserName == username && a.Password == password).FirstOrDefault();

            var userModel = dbz.Employees.Where(a => a.UserName == username).FirstOrDefault();

            var checkTriesEmployee = dbz.Employees.FirstOrDefault(a => a.UserName == username);
            if (checkTriesEmployee != null)
            {
                checkTriesEmployee.NumberOfTries += 1;
                dbz.SaveChanges();
            }

            if (userModel == null)
            {
                passwordValidation = "Wrong Information";
                NotifyOfPropertyChange("PasswordValidation");
            }
            else if (userModel.IsActive == false)
            {
                passwordValidation = "This user is already terminated";
                NotifyOfPropertyChange("PasswordValidation");
            }
            else if (userModel.DateRegistered.Value.AddMonths(userModel.DaysContract) <= DateTime.Now)
            {
                passwordValidation = "This user is already expired.";
                NotifyOfPropertyChange("PasswordValidation");
            }

            else if (userModel != null && employee == null)
            {
                if (userModel.NumberOfTries > 5 && userModel.Position != "Admin")
                {
                    passwordValidation = "Maximum of 5 tries reached (Account Locked)";
                    NotifyOfPropertyChange("PasswordValidation");

                }
                else
                {
                    passwordValidation = "Wrong Password";
                    NotifyOfPropertyChange("PasswordValidation");
                }
            }
            else if (userModel.NumberOfTries > 5 && userModel.Position != "Admin")
            {

                passwordValidation = "Maximum of 5 tries reached (Account Locked)";
                NotifyOfPropertyChange("PasswordValidation");

            }
            else if (employee != null)
                {

                employee.NumberOfTries = 0;
                dbz.SaveChanges();
                     Application.Current.Properties["UserName"] = Username;
                    windowManager.ShowWindow(shellViewModel);
                TryClose();
            }
                else
            {
                   
                passwordValidation = "Wrong information";
                NotifyOfPropertyChange("PasswordValidation");
            }


           
         
        }




    }
}
