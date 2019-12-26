using EyeKnowRight.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
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
    /// <summary>
    /// Interaction logic for AppraisalView.xaml
    /// </summary>
    /// 

    public partial class AppraisalView : UserControl
    {

        private void ResetGrid(dynamic item = null)
        {
            string username = Application.Current.Properties["UserName"].ToString();
            var employeeModel = db.Employees.Where(a => a.UserName == username).FirstOrDefault();

            if (item != null)
            {
                AppraisalGrid.ItemsSource = item;
            }
            else if (employeeModel.SupervisedDepartment != null)
            {
                var data = db.Employees.Where(a => a.Department == employeeModel.SupervisedDepartment && a.LastAppraiseDate == null).ToList();
                AppraisalGrid.ItemsSource = data;
            }
            else
            {
                var data = db.Employees.Where(a => a.LastAppraiseDate == null).ToList();
                AppraisalGrid.ItemsSource = data;
            }


        }


        EyeKnowRightDB db = new EyeKnowRightDB();
        public AppraisalView()
        {
            InitializeComponent();
            //DateTime? date = new DateTime(1900, 01, 01);
            ResetGrid();

        }

        int step = 1;
        private void PreviousStep_Click(object sender, RoutedEventArgs e)
        {
            if (step > 1)
            {
                step--;
            }
            StepChangeVisibility(step);
            StepsCounter.Text = step.ToString();
        }



        private void NextStep_Click(object sender, RoutedEventArgs e)
        {
            if (step < 2)
            {
                step++;
            }
            StepChangeVisibility(step);
            StepsCounter.Text = step.ToString();
        }


        private void StepChangeVisibility(int step)
        {

            AppraiseStep1.Visibility = Visibility.Collapsed;
            AppraiseStep2.Visibility = Visibility.Collapsed;
         
            StepsCounter.Text = step.ToString();

            if (step == 1)
            {
                AppraiseStep1.Visibility = Visibility.Visible;
                this.step = 1;
            }
            else if (step == 2)
            {
                AppraiseStep2.Visibility = Visibility.Visible;
                this.step = 2;
            }
      

        }

        private void AppraiseUserClick(object sender, RoutedEventArgs e)
        {
            string userName = (string)((Button)sender).Tag;
            UserTextBox.Text = userName;

        }

        private void AppraiseUser(object sender, RoutedEventArgs e)
        {
            Evaluation evaluation = new Evaluation();

            evaluation.UserName = UserTextBox.Text;
            evaluation.Answer1 = (int)Answer1.Value;
            evaluation.Answer2 = (int)Answer2.Value;
            evaluation.Answer3 = (int)Answer3.Value;
            evaluation.Answer4 = (int)Answer4.Value;
            evaluation.Answer5 = (int)Answer5.Value;
            evaluation.Answer6 = (int)Answer6.Value;
            evaluation.Answer7 = (int)Answer7.Value;
            evaluation.Answer8 = (int)Answer8.Value;
            evaluation.Answer9 = (int)Answer9.Value;
            evaluation.Answer10 = (int)Answer10.Value;
            evaluation.TotalScore = ( (int)Answer1.Value + (int)Answer2.Value + (int)Answer3.Value + (int)Answer4.Value + (int)Answer5.Value +
                 (int)Answer6.Value + (int)Answer7.Value + (int)Answer8.Value + (int)Answer9.Value + (int)Answer10.Value ) / 10;
            evaluation.Comment = Comments.Text;
            evaluation.Remarks = Remarks.Text;
            evaluation.DateAppraise = DateTime.Now;
            db.Evaluations.Add(evaluation);
          
             var user = UserTextBox.Text;
             var getUser = db.Employees.FirstOrDefault(a => a.UserName == user);
            getUser.LastAppraiseDate = DateTime.Now;
            db.SaveChanges();
            ResetGrid();
        }
    }
}
