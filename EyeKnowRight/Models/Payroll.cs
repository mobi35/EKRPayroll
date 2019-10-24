using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeKnowRight.Models
{
    public class Payroll
    {
        [Key]
        public int PayrollPK { get; set; }

        public DateTime? StartPayroll { get; set; }

        public DateTime? EndPayroll { get; set; }

        public int NumberOfWorkingDays { get; set; }

        public bool IsActive { get; set; }


    }
}
