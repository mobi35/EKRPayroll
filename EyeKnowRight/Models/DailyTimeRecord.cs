


using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeKnowRight.Models
{
    public class DailyTimeRecord
    {
        [Key]
        public int DailyTimeRecordPK { get; set; }

        public string UserName { get; set; }

        public DateTime? TimeIn { get; set; }

        public DateTime? TimeOut { get; set; }

        public DateTime? FirstTimeIn { get; set; }

        public double Accumulated { get; set; }

        public double Late { get; set; }

        public double OverTime { get; set; }

        public DateTime? DateTimeStamps { get; set; }




    }
    }
