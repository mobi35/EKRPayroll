
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EyeKnowRight.Models
{
    public class Employee
    {
        [Key]
        public int EmployeePK { get; set; }
        [MaxLength(10)]
        public string EmployeeID { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }
        public string  LastName { get; set; }

        [MaxLength(40)]
        public string UserName { get; set; }
        [MaxLength(40)]
        public string Password { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        [MaxLength(50)]
        public string Gender { get; set; }
     
        public DateTime? DateRegistered { get; set; }
       
        public int? Age { get; set; }
        [MaxLength(100)]
        public string Address { get; set; }
        [MaxLength(50)]
        public string Position { get; set; }
        public string JobTitle { get; set; }
        public double Salary { get; set; }
        public byte[] Picture { get; set; }
        public DateTime? BirthDate { get; set; }
        public int? MyProperty { get; set; }
        public int MaternityLeave { get; set; }
        public int PaternityLeave { get; set; }
        public int SickLeave { get; set; }
        public int BereavementLeave { get; set; }
        public int MedicalLeave { get; set; }
        public int PersonalLeave { get; set; }
        public int NumberOfTries { get; set; }
        public int DaysContract { get; set; }
        public string SSSNumber { get; set; }
        public string TINNumber { get; set; }
        public string PagibigNumber { get; set; }
        public DateTime? LastAppraiseDate { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public int RemainingLeave { get; set; }
    }
}
