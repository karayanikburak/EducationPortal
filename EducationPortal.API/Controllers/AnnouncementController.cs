using AutoMapper;
using EducationPortal.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EducationPortal.API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using EducationPortal.API.Models;



namespace EducationPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        ResultDTO _resultDto = new ResultDTO();

        public AnnouncementController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public List<AnnouncementDTO> GetAnnouncements()
        {
            var announcements = _context.Announcements.ToList();
            var announcementsDto = _mapper.Map<List<AnnouncementDTO>>(announcements);
            return announcementsDto;
        }

        [HttpGet("{id}")]
        public ActionResult<AnnouncementDTO> GetAnnouncementsById(int id)
        {
            var announcement = _context.Announcements.Find(id);

            if (announcement == null)
            {
                return NotFound("Id bulunamadı");
            }

            var announcementDto = _mapper.Map<AnnouncementDTO>(announcement);
            return announcementDto;
        }


        [HttpPut]
        public ResultDTO UpdateAnnouncement(AnnouncementDTO announcementDto)
        {
            var announce = _context.Announcements.Where(s => s.AnnouncementId == announcementDto.AnnouncementId).SingleOrDefault();
            if (announce == null)
            {
                _resultDto.Status = false;
                _resultDto.Message = "Duyuru Bulunamadı!";
                return _resultDto;
            }

            announce.Title = announcementDto.Title;
            announce.Content = announcementDto.Content;
           
            _context.Announcements.Update(announce);
            _context.SaveChanges();

            _resultDto.Status = true;
            _resultDto.Message = " Duyuru Düzenlendi";
            return _resultDto;
        }
        [HttpPost]
        public IActionResult AddAnnouncement(AnnouncementDTO announcementDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var announcement = new Announcement()
            {
                Title = announcementDTO.Title,
                Content = announcementDTO.Content,

            };
            _context.Announcements.Add(announcement);
            _context.SaveChanges();

            return Ok();
        }




        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            var announcement = await _context.Announcements.FindAsync(id);

            if (announcement == null)
            {
                return NotFound();
            }

            _context.Announcements.Remove(announcement);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
