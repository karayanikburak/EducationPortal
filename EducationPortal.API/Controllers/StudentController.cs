using AutoMapper;
using EducationPortal.API.DTO;
using EducationPortal.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        ResultDTO _resultDto = new ResultDTO();

        public StudentController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }

        [HttpPost]
        public IActionResult AddStudent(StudentDTO studentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var student = new Student()
            {
                FullName = studentDto.FullName,
                Email = studentDto.Email,
                Password = studentDto.Password,
                Phone = studentDto.Phone,
            };
            _context.Students.Add(student);
            _context.SaveChanges();

            return Ok();
        }


        [HttpDelete("{id}")]
        public ResultDTO DeleteStudent(int id)
        {
            var student = _context.Students.Where(x => x.StudentId == id).SingleOrDefault();
            if (student == null)
            {
                _resultDto.Status = false;
                _resultDto.Message = "Ogrenci Bulunamadı!";
                return _resultDto;
            }

            _context.Students.Remove(student);
            _context.SaveChanges();


            _resultDto.Status = true;
            _resultDto.Message = " Ogrenci Silindi";
            return _resultDto;
        }


        [HttpGet("{id}")]
        public StudentDTO GetStudentsById(int id)
        {
            var student = _context.Students.Find(id);
            var studentDto = _mapper.Map<StudentDTO>(student);

            return studentDto;
        }

        [HttpGet]
        public List<StudentDTO> GetStudents()
        {
            var students = _context.Students.ToList();
            var studentDtos = _mapper.Map<List<StudentDTO>>(students);
            return studentDtos;
        }

        [HttpPut]
        public ResultDTO UpdateStudent(StudentDTO dto)
        {
            var student = _context.Students.Where(s => s.StudentId == dto.StudentId).SingleOrDefault();
            if (student == null)
            {
                _resultDto.Status = false;
                _resultDto.Message = "Ogrenci Bulunamadı!";
                return _resultDto;
            }

            student.FullName = dto.FullName;
            student.Email = dto.Email;
            student.Phone = dto.Phone;
            _context.Students.Update(student);
            _context.SaveChanges();

            _resultDto.Status = true;
            _resultDto.Message = " Ogrenci Düzenlendi";
            return _resultDto;
        }




    }
}
