using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.UI.Controllers
{
    public class TeacherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
