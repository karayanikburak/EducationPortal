using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.UI.Controllers
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
