using AutoMapper;
using EducationPortal.API.DTO;
using EducationPortal.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        ResultDTO _resultDto = new ResultDTO();

        public CourseController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public List<CourseDTO> GetCourses()
        {
            var courses = _context.Courses.ToList();
            var courseDtos = _mapper.Map<List<CourseDTO>>(courses);
            return courseDtos;
        }

        [HttpGet("{id}")]
        public CourseDTO GetCoursesById(int id)
        {
            var courses = _context.Courses.Find(id);
            var courseDtos = _mapper.Map<CourseDTO>(courses);

            return courseDtos;
        }

        [HttpPost]
        public IActionResult AddCourse(CourseDTO courseDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

       

            var course = _mapper.Map<Course>(courseDto);
            _context.Courses.Add(course);
            _context.SaveChanges();

            return Ok("İşlem Başarılı");

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }






        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCourse(int id, CourseDTO courseDto)
        {
            if (id != courseDto.CourseId)
            {
                return BadRequest("Id Bulunamadi");
            }

            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _mapper.Map(courseDto, course);

            _context.Entry(course).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
