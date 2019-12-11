using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeKnowRight.Models
{
    public class Position
    {
        [Key]
        public int PositionPK { get; set; }
        
        public string PositionName { get; set; }

        public bool Status { get; set; }

    }
}
