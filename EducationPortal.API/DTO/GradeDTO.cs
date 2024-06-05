namespace EducationPortal.API.DTO
{
    public class GradeDTO
    {
        public int GradeId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int? Exam1 { get; set; }
        public int? Exam2 { get; set; }
        public int? Exam3 { get; set; }
        public double? Average { get; set; }
        public bool? Status { get; set; }

    }
}
