namespace EducationPortal.API.Models
{
    public class Grade
    {
        public int GradeId { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public int? Exam1 { get; set; }
        public int? Exam2 { get; set; }
        public int? Exam3 { get; set; }
        public double? Average { get; set; }
        public bool? Status { get; set; }

    }
}
