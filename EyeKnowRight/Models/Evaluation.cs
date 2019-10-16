using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeKnowRight.Models
{
    public class Evaluation
    {
        [Key]
        public int EvaluationPK { get; set; }
        public string UserName { get; set; }
        public DateTime? DateAppraise { get; set; }
        public int Answer1 { get; set; }
        public int Answer2 { get; set; }
        public int Answer3 { get; set; }
        public int Answer4 { get; set; }
        public int Answer5 { get; set; }
        public int Answer6 { get; set; }
        public int Answer7 { get; set; }
        public int Answer8 { get; set; }
        public int Answer9 { get; set; }
        public int Answer10 { get; set; }
        public int Answer11{ get; set; }
        public int Answer12 { get; set; }
        public int Answer13 { get; set; }
        public int Answer14 { get; set; }
        public int Answer15 { get; set; }
        public int Answer16 { get; set; }
        public int Answer17 { get; set; }
        public int Answer18 { get; set; }
        public int Answer19 { get; set; }
        public int Answer20 { get; set; }
        public int TotalScore { get; set; }
        public string Comment { get; set; }
        public string Remarks { get; set; }

    }
}
