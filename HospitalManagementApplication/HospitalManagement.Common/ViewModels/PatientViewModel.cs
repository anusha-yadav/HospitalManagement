namespace HospitalManagement.ViewModels
{
    public class PatientViewModel
    {
        public string? Gender { get; set; }
        public double Age { get; set; }
        public string? MedicalHistory { get; set; }

        public string? TreatmentType { get; set; }

        public string? FirstName { get; set; }
        public int PatientId { get; set; }
        public List<string>? MedicalConditions { get; set; }
        public string Address { get;  set; }
        public double SuccessRate { get; set; }
        public List<String>? Treatments { get; set; }
        public int Occurrences { get; set; }
    }
}
