using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeKnowRight.Models
{
    public class Deductions
    {
        [Key]
        public int DeductionPK { get; set; }

        public int PayrollPK { get; set; }

        public double BasicSalary { get; set; }

        public double LateDeduction { get; set; }

        public double OverTimeAddition { get; set; }

        public double AllAccumulatedTimeAddition { get; set; }

        public double SSSDeduction { get; set; }

        public double TinDeduction { get; set; }

        public double PagibigDeduction { get; set; }

        public double TotalSalary { get; set; }

        public string UserName { get; set; }

        public int? WorkingDays { get; set; }

        public int? DaysPresent { get; set; }

    }
}
