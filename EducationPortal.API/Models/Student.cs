using System.Collections.Generic;
using System.Diagnostics;

namespace EducationPortal.API.Models
{
    public class Student
    {
        public int StudentId { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string? Password { get; set; }

        public ICollection<Grade> Grades { get; set; }


    }
}
