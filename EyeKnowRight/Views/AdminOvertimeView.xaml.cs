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
    public partial class AdminOvertimeView : UserControl
    {

        EyeKnowRightDB db = new EyeKnowRightDB();
        public AdminOvertimeView()
        {
            InitializeComponent();
            var overtime = db.Overtimes.ToList();
            OvertimeGrid.ItemsSource = overtime;
        }

        private void OvertimeClick(object sender, RoutedEventArgs e)
        {
            int overtimePk = (int)((Button)sender).Tag;
            var overtime = db.Overtimes.FirstOrDefault(a => a.OvertimePK == overtimePk);
            DataContext = overtime;
        
        }

        private void OvertimeAccept(object sender, RoutedEventArgs e)
        {
            int overtimePK = Int32.Parse(OvertimePK.Text);
            var overtime = db.Overtimes.FirstOrDefault(a => a.OvertimePK == overtimePK);
            overtime.Status = "Accepted";
            db.SaveChanges();
            var overtimeList = db.Overtimes.ToList();
            OvertimeGrid.ItemsSource = overtimeList;
        }

        private void OvertimeReject(object sender, RoutedEventArgs e)
        {
            int overtimePK = Int32.Parse(OvertimePK.Text);
            var overtime = db.Overtimes.FirstOrDefault(a => a.OvertimePK == overtimePK);
            overtime.Status = "Rejected";
            db.SaveChanges();
            var overtimeList = db.Overtimes.ToList();
            OvertimeGrid.ItemsSource = overtimeList;
        }

        private void SearchChanged(object sender, TextChangedEventArgs e)
        {
            if (SearchBox.Text != "")
            {
                Magnifier.Visibility = Visibility.Hidden;
                var text = SearchBox.Text;
                var data = db.Overtimes
                  .Where(a =>
                  a.Reason.StartsWith(text) || a.Reason.EndsWith(text) ||
                  a.Status.StartsWith(text) || a.Status.EndsWith(text) ||
                  a.UserName.StartsWith(text) || a.UserName.EndsWith(text) 
               ).ToList();
                OvertimeGrid.ItemsSource = data;
            }
            else
            {
                Magnifier.Visibility = Visibility.Visible;
                var data = db.Overtimes.ToList();
                OvertimeGrid.ItemsSource = data;
            }
        }

    }
}
