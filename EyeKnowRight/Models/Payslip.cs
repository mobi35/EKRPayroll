using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeKnowRight.Models
{
    public class Payslip
    {
        public string EmployeeName { get; set; }
        public string  EmployeeAddress { get; set; }
        public string JobTitle { get; set; }
        public string Earnings_Accumulated { get; set; } 
        public string Earnings_TotalAccumulated { get; set; }
        public double Deductions_Late { get; set; }

        public string Deductions_Late_String_Format { get; set; }
        public double Deductions_SSS { get; set; }
        public double Deductions_Philhealth { get; set; }
        public double Deductions_Pagibig { get; set; }
        public double Deductions_TAX { get; set; }
        public string Deductions_Total { get; set; }
        public double TotalSalary { get; set; }

        public string TotalSalaryStringFormat { get; set; }

    }
}
