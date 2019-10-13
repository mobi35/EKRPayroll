using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeKnowRight.Models
{
    public class Leave
    {
        [Key]
        public int LeavePK { get; set; }

        public string UserName { get; set; }
        public string TypeOfLeave { get; set; }

        public string ReasonForLeaving { get; set; }
        public DateTime? StartDate { get; set; }

        public DateTime? EndLeave { get; set; }

        public string Status { get; set; }

       

    }
}
