using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeKnowRight.ViewModels
{
    public class AccountViewModel : Screen
    {
        private string age =" ACCOUNT";

        public string Bagwis
        {
            get { return age; }
            set { age = value; }
        }
        private double accumulated = 0;
        public double Accumulated
        {
            get { return accumulated / 20; }
            set { accumulated = value; }
        }

    }
}
