using MedicalSystem.Models.Diagnosis;
using MedicalSystem.Models.Paciente;

namespace MedicalSystem.Models.PatientDetails
{
    public class PatientDetailsVM
    {
        public Patient Patient { get; set; }
        public List<DiagnosisVM> Diagnoses { get; set; } // A list to hold multiple diagnoses
        public string DiagnosisImageBase64 { get; set; }
    }
}
