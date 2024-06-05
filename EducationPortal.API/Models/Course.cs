using System.Diagnostics;

namespace EducationPortal.API.Models
{
    public class Course
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; }

        public ICollection<Grade> Grades { get; set; }

    }
}
