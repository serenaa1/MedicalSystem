namespace MedicalSystem.Models.Paciente
{
    public class Patient
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string MedicalHistory { get; set; }
        public int Invalidated { get; set; }
        public string CreatedBy { get; set; }
        public int ModifiedBy { get; set; }
        public List<Patient> PatientsList { get; set; }
        public int PatientId { get; set; }

    }
}
