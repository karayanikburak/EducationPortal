using AutoMapper;
using EducationPortal.API.DTO;
using EducationPortal.API.Models;

namespace EducationPortal.API.Mapping
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Teacher, TeacherDTO>().ReverseMap();
            CreateMap<Course, CourseDTO>().ReverseMap();
            CreateMap<Student, StudentDTO>().ReverseMap();
            CreateMap<Announcement, AnnouncementDTO>().ReverseMap();
            CreateMap<Grade, GradeDTO>().ReverseMap();


            CreateMap<AppUser, UserDTO>().ReverseMap();

        }
    }
}
