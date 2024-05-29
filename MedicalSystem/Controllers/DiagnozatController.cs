using MedicalSystem.Models;
using MedicalSystem.Models.Diagnosis;
using MedicalSystem.Models.PatientDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalSystem.Controllers
{
    public class DiagnozatController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        DatabaseContext _dbContext;
        public DiagnozatController(ILogger<HomeController> logger, DatabaseContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        [Authorize]
        public IActionResult Index()
        {
            var user = User.Identity.Name;
            //var users = _dbContext.Users.ToList();
            var data = _dbContext.Diagnosis.ToList();
            if (data != null)
            {
                return View(data);
            }
            return View(data);
        }
        [Authorize]
        public IActionResult DiagnosesForDoctor()
        {
            var doctorName = User.Identity.Name;

            // Use LINQ to perform the query
            var diagnoses = _dbContext.Diagnosis
                .Where(d => d.DoctorName.Contains(doctorName))
                .Join(_dbContext.Patients,
                      d => d.PatientId,
                      p => p.Id,
                      (d, p) => new
                      {
                          Diagnosis = d,
                          Patient = p
                      })
                .ToList();

            // You can map the result to a ViewModel if needed
            var viewModel = diagnoses.Select(d => new DiagnosisForDoctors
            {
                Id = d.Diagnosis.Id,
                PatientId = d.Patient.Id,
                DiagnosisName = d.Diagnosis.DiagnosisName,
                Description = d.Diagnosis.Description,
                Symptoms = d.Diagnosis.Symptoms,
                Date = d.Diagnosis.Date,
                DoctorName = d.Diagnosis.DoctorName,
                Image = d.Diagnosis.Image,
                CreatedOn = d.Diagnosis.CreatedOn,
                ModifiedOn = d.Diagnosis.ModifiedOn,
                PatientName = d.Patient.FullName,
            }).ToList();

            return View(viewModel); // Or return as JSON, depending on your use case
        }

        public IActionResult DiagnozaDetails(int Id)
        {
            var diagnoses = _dbContext.Diagnosis.Where(x => x.Id == Id).ToList();

            string base64Image = null;
            var latestDiagnosis = diagnoses.FirstOrDefault();
            if (latestDiagnosis != null && latestDiagnosis.Image != null)
            {
                base64Image = Convert.ToBase64String(latestDiagnosis.Image);
            }

            var viewModel = new PatientDetailsVM()
            {
                Diagnoses = diagnoses,
                DiagnosisImageBase64 = base64Image
            };

            return View(viewModel);
        }

    }
}
