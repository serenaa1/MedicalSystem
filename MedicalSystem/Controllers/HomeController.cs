using MedicalSystem.Models;
using MedicalSystem.Models.Paciente;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace MedicalSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        DatabaseContext _dbContext;
        public HomeController(ILogger<HomeController> logger, DatabaseContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        [Authorize]
        public IActionResult Index()
        {
            var user = User.Identity.Name;
            //var users = _dbContext.Users.ToList();
            var data = _dbContext.Patients.Where(x => x.CreatedBy == user).ToList();
            if (data != null)
            {
                return View(data);
            }
            return View(data);
        }

        public IActionResult AddPatient(string name, string gender, DateTime date, string adress, string phone, string email, string history)
        {

            var user = User.Identity.Name;
            var idUserString = _dbContext.Users.Where(x => x.Name == user).FirstOrDefault();

            var data = _dbContext.Patients.Where(x => x.FullName == name).FirstOrDefault();

            if (data == null && user != null)
            {
                var newPatient = new Patient
                {
                    FullName = name,
                    Gender = gender,
                    DateOfBirth = date,
                    Address = adress,
                    PhoneNumber = phone,
                    Email = email,
                    MedicalHistory = history,
                    Invalidated = 20,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    CreatedBy = user, 
                    ModifiedBy = 2,
                    PatientId = 3
                };
                _dbContext.Patients.Add(newPatient);
            }

            _dbContext.SaveChanges();


            return Ok("Patient added successfully.");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Details()
        {
            return View();
        }
        public IActionResult Cards()
        {
            return View();
        }
    }
}