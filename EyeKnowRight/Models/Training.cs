using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeKnowRight.Models
{
    public class Training
    {
        [Key]
        public int TrainingPK { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? DateMade { get; set; }
    }
}
