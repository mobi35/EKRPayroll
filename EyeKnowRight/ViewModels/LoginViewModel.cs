using Caliburn.Micro;
using EyeKnowRight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
          
            else if(userModel != null && employee == null) { 
            if (userModel.NumberOfTries > 5 )
                {
                    passwordValidation = "Maximum tries";
                    NotifyOfPropertyChange("PasswordValidation");

                }else
                {
                    passwordValidation = "Wrong Password";
                    NotifyOfPropertyChange("PasswordValidation");
                }
            } else if (employee != null)
                {
                var user = dbz.Employees.Where(a => a.UserName == username && a.Password == password).FirstOrDefault().UserName;
                var date = DateTime.Now.Date;
                var dtrs = dbz.DailyTimeRecords.Where(a => a.DateTimeStamps == date && a.UserName == user).FirstOrDefault();
                    
                    dtrs.TimeIn = DateTime.Now;
                    userModel.NumberOfTries = 0;
                    if (dtrs.FirstTimeIn == null)
                    {
                        dtrs.FirstTimeIn = DateTime.Now;

                        if (dtrs.FirstTimeIn.Value.TimeOfDay.TotalMinutes > 480)
                        {
                            dtrs.Late = dtrs.FirstTimeIn.Value.TimeOfDay.TotalMinutes - 480;
                            int hour = (int)dtrs.Late / 60;
                            int minutes = (int)dtrs.Late % 60;
                            dtrs.LateString = $"{ hour }h:{ minutes }m";
                        }
                    }
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
