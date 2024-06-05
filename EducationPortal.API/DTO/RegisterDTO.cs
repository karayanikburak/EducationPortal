namespace EducationPortal.API.DTO
{
    public class RegisterDTO
    {
        public string? Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string? Password { get; set; }
    }
}
