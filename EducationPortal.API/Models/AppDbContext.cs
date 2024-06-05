using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.API.Models
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public DbSet<Course> Courses { get; set; }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Student>Students { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Grade> Grades { get; set; }


        public AppDbContext(DbContextOptions options) : base(options)
        {

        }



    }
}
