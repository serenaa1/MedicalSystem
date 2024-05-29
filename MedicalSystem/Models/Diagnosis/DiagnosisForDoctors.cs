namespace MedicalSystem.Models.Diagnosis
{
    public class DiagnosisForDoctors
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
        public string PatientName { get; set; }
        // Navigation property
        public List<DiagnosisVM> DiagnosisList { get; set; }

        public DiagnosisVM Diagnosis { get; set; }
    }
}
