using Microsoft.AspNetCore.Mvc;

namespace EducationPortal.UI.Controllers
{
    public class AnnouncementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
