using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeKnowRight.Models
{
    public class Overtime
    {
        [Key]
        public int OvertimePK { get; set; }

        public DateTime? UntilWhatTime { get; set; }

        public string Reason { get; set; }

        public string UserName { get; set; }

        public DateTime? DateOfOvertime { get; set; }

        public string Status { get; set; }
    }
}
