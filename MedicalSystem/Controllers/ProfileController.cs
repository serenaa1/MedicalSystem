using Microsoft.AspNetCore.Mvc;

namespace MedicalSystem.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
