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
    public partial class PayrollView : UserControl
    {
      

        EyeKnowRightDB db = new EyeKnowRightDB();
        public PayrollView()
        {
            InitializeComponent();
        
            var payroll = db.Payrolls.Where(a => a.IsActive == false).ToList();
            PayrollGrid.ItemsSource = payroll;


            var holidays = db.Holidays.ToList();
            HolidayGrid.ItemsSource = holidays;
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            var xx = (Payroll)PayrollGrid.SelectedItem;
            //MessageBox.Show(xx.StartPayroll + " ");
            PayrollPrintView payrollPrintView = new PayrollPrintView(xx.PayrollPK);
            payrollPrintView.Show();

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

        private void AddHoliday_Click(object sender, RoutedEventArgs e)
        {
            Holiday holiday = new Holiday();
            holiday.HolidayName = HolidayName.Text;
            holiday.Month = HolidayDate.SelectedDate;
            holiday.SalaryInrease = float.Parse(SalaryIncrease.Text);

            //VALIDATION

            var holidayList = db.Holidays.ToList();
            bool isValid = true;
            foreach(var holi in holidayList)
            {
                if (holi.HolidayName == HolidayName.Text)
                {
                    isValid = false;
                    break;
                }
               else if (holi.HolidayName == HolidayName.Text)
                {
                    isValid = false;
                    break;
                }
               else if (holi.Month == HolidayDate.SelectedDate)
                {
                    isValid = false;
                    break;
                }
            }

            if (isValid)
            {
                db.Holidays.Add(holiday);
                db.SaveChanges();

                var holidays = db.Holidays.ToList();
                HolidayGrid.ItemsSource = holidays;
            }
            else
            {
                MessageBox.Show("Something's wrong dear");
            }

        }
    }
}
