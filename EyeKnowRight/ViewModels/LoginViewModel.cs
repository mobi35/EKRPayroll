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
        EyeKnowRightDB db = new EyeKnowRightDB();
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




        public void Login()
        {

            var employee = db.Employees.Where(a => a.UserName == username && a.Password == password).ToList().Count();
            
            if(employee == 1)
            {
                windowManager.ShowWindow(shellViewModel); //If you want a modal dialog, then use ShowDialog that returns a bool?
                TryClose();
            }else
            {
                MessageBox.Show("Failed");
            }

        
          

        }




    }
}
