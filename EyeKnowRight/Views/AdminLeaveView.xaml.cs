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
    public partial class AdminLeaveView : UserControl
    {
      

        EyeKnowRightDB db = new EyeKnowRightDB();
        public AdminLeaveView()
        {
            InitializeComponent();
          //  var user = Application.Current.Properties["UserName"].ToString();
            var leave = db.Leaves.Where(a => a.Status == "Pending").ToList();
            LeaveGrid.ItemsSource = leave;
        }

        private void LeaveClick(object sender, RoutedEventArgs e)
        {
            int leavePK = (int)((Button)sender).Tag;
            var leave = db.Leaves.FirstOrDefault(a => a.LeavePK == leavePK);
            DataContext = leave;
        }
    }
}
