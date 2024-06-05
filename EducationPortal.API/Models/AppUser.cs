using Microsoft.AspNetCore.Identity;

namespace EducationPortal.API.Models
{
    public class AppUser:IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public string PicUrl { get; set; }
    }
}
