using MedicalSystem.Models.Diagnosis;
using MedicalSystem.Models.Paciente;

namespace MedicalSystem.Models.Diagnosis
{
    public class DiagnosisVM
    {
        public int Id { get; set; }  // Primary key
        public int PatientId { get; set; }
        public string DiagnosisName { get; set; }
        public string Description { get; set; }
        public string Symptoms { get; set; }
        public DateTime Date { get; set; }
        public string DoctorName { get; set; }
        public byte[] Image { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }

        // Navigation property
        public List<DiagnosisVM> DiagnosisList { get; set; }

        public DiagnosisVM Diagnosis { get; set; }
    }
}
