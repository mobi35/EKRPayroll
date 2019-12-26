using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeKnowRight.Models
{
    public class EmployeeTraining
    {
        [Key]
        public int EmployeeTrainingPK { get; set; }
        
        public string UserName { get; set; }

        public string FullName { get; set; }

        public string Training { get; set; }

        public DateTime? DateOfTraining { get; set; }

        public DateTime? TimeOfTraining { get; set; }

        public string TrainingStatus { get; set; }
    }
}
