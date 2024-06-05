namespace EducationPortal.API.DTO
{
    public class TeacherDTO
    {
        public int TeacherId { get; set; }
        public int CourseId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }
        public string Branch { get; set; }
        public  string Password { get; set; }
        public string Phone { get; set; }


    }
}
