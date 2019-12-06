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
    public partial class OvertimeView : UserControl
    {
      

        EyeKnowRightDB db = new EyeKnowRightDB();
        public OvertimeView()
        {
            InitializeComponent();
            var payroll = db.Payrolls.OrderByDescending(a => a.PayrollPK).Where(a => a.IsActive == false).ToList();
            
            PayrollGrid.ItemsSource = payroll;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int payrollPK = (int)((Button)sender).Tag;
            var deductions = db.Deductionss.Where(a => a.PayrollPK == payrollPK).ToList();
          //  DeductionGrid.ItemsSource = deductions;
        }
    }
}
