using EyeKnowRight.Models;
using MaterialDesignThemes.Wpf;
using Microsoft.Reporting.WinForms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
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
using System.Windows.Forms.Integration;
using EyeKnowRight.Views;

namespace EyeKnowRight
{
    /// <summary>
    /// Interaction logic for Account.xaml
    /// </summary>
    public partial class TrainingView : UserControl
    {
      

        EyeKnowRightDB db = new EyeKnowRightDB();
        public TrainingView()
        {
            InitializeComponent();
        
          //  var payroll = db.Payrolls.Where(a => a.IsActive == false).ToList();
         //   PayrollGrid.ItemsSource = payroll;

            var training = db.Trainings.ToList();
            TrainingGrid.ItemsSource = training;

            EmployeeTraining.ItemsSource = db.EmployeeTrainings.ToList();
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
          //  var xx = (Payroll)PayrollGrid.SelectedItem;
            //MessageBox.Show(xx.StartPayroll + " ");
         //   PayrollPrintView payrollPrintView = new PayrollPrintView(xx.PayrollPK);
        //    payrollPrintView.Show();

        }

        
        private void TrainingDelete(object sender, RoutedEventArgs e)
        {
           
            int trainingPK = (int)((Button)sender).Tag;
            var training = db.Trainings.FirstOrDefault(a => a.TrainingPK == trainingPK);
            db.Trainings.Remove(training);
            db.SaveChanges();
            var trainingList = db.Trainings.ToList();
            TrainingGrid.ItemsSource = trainingList;
        }
            private void PrintPayroll(object sender, RoutedEventArgs e)
        {
            int payrollPK = (int)((Button)sender).Tag;
            PayrollPrintView payrollPrintView = new PayrollPrintView(payrollPK);
            payrollPrintView.Show();

        }
            private void Button_Click(object sender, RoutedEventArgs e)
        {
            int payrollPK = (int)((Button)sender).Tag;
            var deductions = db.Deductionss.Where(a => a.PayrollPK == payrollPK).ToList();
            DeductionGrid.ItemsSource = deductions;
        }

        private void AddTraining_Click(object sender, RoutedEventArgs e)
        {
            Training training = new Training();
            training.Name = TrainingName.Text;
            training.Description = TrainingDescription.Text;
            
            training.DateMade = DateTime.Now;
            db.Trainings.Add(training);
            db.SaveChanges();

            var trainingList = db.Trainings.ToList();
            TrainingGrid.ItemsSource = trainingList;

        }


        private void TrainingAccept(object sender, RoutedEventArgs e)
        {
            int trainingPK = (int)((Button)sender).Tag;
            var training = db.EmployeeTrainings.FirstOrDefault(a => a.EmployeeTrainingPK == trainingPK);
            training.TrainingStatus = "Completed";

            Notification notification = new Notification();
            notification.NotificationToWho = training.UserName;
            notification.Message = "You have completed the " + training.Training + " training. Congratulations";
            db.Notifications.Add(notification);


            db.SaveChanges();
            var trainingList = db.EmployeeTrainings.ToList();
            EmployeeTraining.ItemsSource = trainingList;
        }

        private void TrainingDecline(object sender, RoutedEventArgs e)
        {
            int trainingPK = (int)((Button)sender).Tag;
            var training = db.EmployeeTrainings.FirstOrDefault(a => a.EmployeeTrainingPK == trainingPK);
            training.TrainingStatus = "Failed";
            db.SaveChanges();
            var trainingList = db.EmployeeTrainings.ToList();
            EmployeeTraining.ItemsSource = trainingList;

            Notification notification = new Notification();
            notification.NotificationToWho = training.UserName;
            notification.Message = "You have failed the " + training.Training + " training. Congratulations";
            db.Notifications.Add(notification);

        }


    }
}
