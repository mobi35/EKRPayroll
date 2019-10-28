using EyeKnowRight.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeKnowRight.ViewModels
{
   public class AttendanceReportViewModel
    {

        public string FullName { get; set; }

        public DateTime? TimeIn { get; set; }

        public DateTime? TimeOut { get; set; }

        public DateTime? DateTimeStamps { get; set; }

        public string  Remarks { get; set; }


    }
}
