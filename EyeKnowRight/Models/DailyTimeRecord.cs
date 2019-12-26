﻿


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

        public string AccumulatedString { get; set; }

        public double Late { get; set; }

        public string LateString { get; set; }

        public double OverTime { get; set; }

        public string OverTimeString { get; set; }

        public DateTime? DateTimeStamps { get; set; }
    
        public string Remarks { get; set; }

        public bool leaveEarned { get; set; }

    }
}
