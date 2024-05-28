using MedicalSystem.Migrations;
using MedicalSystem.Models;
using MedicalSystem.Models.Diagnosis;
using MedicalSystem.Models.PatientDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

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
            var patient = _dbContext.Patients.Where(x => x.Id == Id).FirstOrDefault();
            var diagnoses = _dbContext.Diagnosis.Where(x => x.PatientId == Id).ToList(); 

            string base64Image = null;
            var latestDiagnosis = diagnoses.FirstOrDefault(); 
            if (latestDiagnosis != null && latestDiagnosis.Image != null)
            {
                base64Image = Convert.ToBase64String(latestDiagnosis.Image);
            }

            var viewModel = new PatientDetailsVM()
            {
                Diagnoses = diagnoses, 
                Patient = patient,
                DiagnosisImageBase64 = base64Image
            };

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult AddDiagnosis(string patientId, string diagnosisName, string description, string symptoms, DateTime date, string doctorName, IFormFile img)
        {
            var user = User.Identity.Name;
            var idUser = _dbContext.Users.Where(x => x.Name == user).FirstOrDefault();
            var idPatient = _dbContext.Patients.Where(x => x.FullName == patientId).Select(x => x.Id).FirstOrDefault();
            var data = _dbContext.Diagnosis.Where(x => x.PatientId == idPatient).FirstOrDefault();

            byte[] imageBytes = null;
            if (img != null && img.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    img.CopyTo(memoryStream);
                    imageBytes = memoryStream.ToArray();
                }
            }

            if (data == null && idUser != null)
            {
                var newDiagnosis = new DiagnosisVM
                {
                    DoctorName = idUser.Name,
                    PatientId = Convert.ToInt32(idPatient),
                    DiagnosisName = diagnosisName,
                    Description = description,
                    Symptoms = symptoms,
                    Date = date,
                    Image = imageBytes,
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now
                };

                _dbContext.Diagnosis.Add(newDiagnosis);
            }

            _dbContext.SaveChanges();
            return Ok("Diagnoza u shtua me sukses");
        }

    }
}
