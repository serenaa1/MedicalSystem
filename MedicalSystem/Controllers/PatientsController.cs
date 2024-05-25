using MedicalSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalSystem.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        DatabaseContext _dbContext;
        public PatientsController(ILogger<HomeController> logger, DatabaseContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        [Authorize]
        public IActionResult Index(int Id)
        {
            var data = _dbContext.Patients.Where(x => x.Id == Id).FirstOrDefault();
            return View(data);
        }
    }
}
