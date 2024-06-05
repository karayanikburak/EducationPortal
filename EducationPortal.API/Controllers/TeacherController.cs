using AutoMapper;
using EducationPortal.API.DTO;
using EducationPortal.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EducationPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController:ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        ResultDTO _resultDto = new ResultDTO();

        public TeacherController(AppDbContext context)
        {
            _context = context;

        }
        [HttpGet("{id}")]
        public IActionResult GetTeacherById(int id)
        {
            var teacher = _context.Teachers.Find(id);
            if (teacher == null) 
            { 
                return NotFound(); 
            }


            return Ok(teacher);
        }

        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            var teacher = await _context.Teachers.ToListAsync();
            return Ok(teacher);
        }

        [HttpPost]
        public async Task<ActionResult<TeacherDTO>> PostTeacher(TeacherDTO teacherDTO)
        {
            var teacher = new Teacher
            {
                FullName = teacherDTO.FullName,
                Email = teacherDTO.Email,
                Branch = teacherDTO.Branch,
                Phone = teacherDTO.Phone,
                Password = teacherDTO.Password,
                CourseId = teacherDTO.CourseId,
            };

            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            teacherDTO.TeacherId = teacher.TeacherId;

            return Ok();
        }

        [HttpDelete("{id}")]
        public ResultDTO DeleteTeacher(int id)
        {
            var teacher = _context.Teachers.Where(x => x.TeacherId == id).SingleOrDefault();
            if (teacher == null)
            {
                _resultDto.Status = false;
                _resultDto.Message = "Ogretmen Bulunamadı!";
                return _resultDto;
            }

            _context.Teachers.Remove(teacher);
            _context.SaveChanges();


            _resultDto.Status = true;
            _resultDto.Message = " Ogretmen Silindi";
            return _resultDto;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, TeacherDTO teacherDTO)
        {
            if (id != teacherDTO.TeacherId)
            {
                return BadRequest("ID Bulunamadi");
            }

            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            teacher.FullName = teacherDTO.FullName;
            teacher.Email = teacherDTO.Email;
            teacher.Branch = teacherDTO.Branch;
            teacher.Phone = teacherDTO.Phone;


            await _context.SaveChangesAsync();

            return Ok();
        }




    }
}
