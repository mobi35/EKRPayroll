using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeKnowRight.Models
{
    public class Department
    {
        [Key]
        public int DepartmentPK { get; set; }
        
        public string DepartmentName { get; set; }

        public string SupervisorName { get; set; }


    }
}
