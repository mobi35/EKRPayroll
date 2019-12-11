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
    public partial class DepartmentView : UserControl
    {
      

        EyeKnowRightDB db = new EyeKnowRightDB();
        public DepartmentView()
        {
            InitializeComponent();
            DepartmentGrid.ItemsSource = db.Departments.ToList();

            PositionGrid.ItemsSource = db.Positions.ToList();
        }

        private void DepartmentAddClick(object sender, RoutedEventArgs e)
        {
            string dep = DepName.Text;
            if (db.Departments.FirstOrDefault(a => a.DepartmentName == dep) != null)
            {
                Department_ValidationMsg.Text = "This department already exist";
                Department_ValidationMsg.Visibility = Visibility.Visible;
            }else { 
            Department depModel = new Department();
            depModel.DepartmentName = DepName.Text;
                db.Departments.Add(depModel);
                db.SaveChanges();
                Department_ValidationMsg.Visibility = Visibility.Collapsed;
                DepartmentGrid.ItemsSource = db.Departments.ToList();
                DepName.Text = "";
            }
        }

        private void PosotionAddClick(object sender, RoutedEventArgs e)
        {
            string dep = PositionName.Text;
            if (db.Positions.FirstOrDefault(a => a.PositionName == dep) != null)
            {
                PositionName_ValidationMsg.Text = "This position already exist";
                PositionName_ValidationMsg.Visibility = Visibility.Visible;
            }
            else
            {
                Position posModel = new Position();
                posModel.PositionName = PositionName.Text;
                db.Positions.Add(posModel);
                db.SaveChanges();
                PositionName_ValidationMsg.Visibility = Visibility.Collapsed;
                PositionGrid.ItemsSource = db.Positions.ToList();
                PositionName.Text = "";
            }
        }

     

        private void DisablePosition(object sender, RoutedEventArgs e)
        {
            int positionPK = (int)((Button)sender).Tag;
            var data = db.Positions.FirstOrDefault(a => a.PositionPK == positionPK);
            data.Status = false; db.SaveChanges(); DepartmentGrid.ItemsSource = db.Departments.ToList();

            PositionGrid.ItemsSource = db.Positions.ToList();
        }

   

        private void EnablePosition(object sender, RoutedEventArgs e)
        {
            int positionPK = (int)((Button)sender).Tag; 
            var data = db.Positions.FirstOrDefault(a => a.PositionPK == positionPK);
            data.Status = true; db.SaveChanges(); DepartmentGrid.ItemsSource = db.Departments.ToList();

            PositionGrid.ItemsSource = db.Positions.ToList();
            MessageBox.Show("bulbol");
        }

        private void DisableDepartment(object sender, RoutedEventArgs e)
        {
            int departmentPK = (int)((Button)sender).Tag;
            var data = db.Departments.FirstOrDefault(a => a.DepartmentPK == departmentPK);
            data.Status = false;
            db.SaveChanges(); DepartmentGrid.ItemsSource = db.Departments.ToList();

            PositionGrid.ItemsSource = db.Positions.ToList();
        }

        private void EnableDepartment(object sender, RoutedEventArgs e)
        {
            int departmentPK = (int)((Button)sender).Tag;
            var data = db.Departments.FirstOrDefault(a => a.DepartmentPK == departmentPK);
            data.Status = true;
            db.SaveChanges(); DepartmentGrid.ItemsSource = db.Departments.ToList();

            PositionGrid.ItemsSource = db.Positions.ToList();

        }







    }
    }
