using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.UI.Controllers
{
    public class StudentController : Controller

    {
        private readonly IConfiguration _configuration;

        public StudentController(IConfiguration configuration = null)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            string ApiBaseUrl = _configuration["ApiBaseUrl"]!;
            ViewBag.ApiBaseUrl = ApiBaseUrl;
            return View();
        }
    }
}
