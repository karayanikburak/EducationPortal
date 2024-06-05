namespace EducationPortal.API.Models
{
    public class Teacher
    {
        public int TeacherId { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public string FullName { get; set; }
        public string Branch { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }


        public ICollection<Announcement> Announcements { get; set; }

    }
}
