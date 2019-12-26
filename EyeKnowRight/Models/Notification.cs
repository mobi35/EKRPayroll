using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeKnowRight.Models
{
    public class Notification
    {
        [Key]
        public int NotificationPK { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public DateTime? NotifiedOn { get; set; }

        public string NotificationToWho { get; set; }

    }
}
