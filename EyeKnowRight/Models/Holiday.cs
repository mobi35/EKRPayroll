using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeKnowRight.Models
{
    public class Holiday
    {
        [Key]
        public int HolidayPK { get; set; }

        public DateTime? Month { get; set; }

        public string HolidayName { get; set; }

        public float SalaryInrease { get; set; }
    }
}
