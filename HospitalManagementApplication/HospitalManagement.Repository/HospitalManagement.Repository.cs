using HospitalManagement.Data.Entity;
using HospitalManagement.ViewModels;

namespace HospitalManagement.Repository
{
    public class HospitalManagementRepository
    {
        private readonly HospitalManagementContext Context;

        public HospitalManagementRepository(HospitalManagementContext context)
        {
            Context = context;
        }

        public List<PatientViewModel> GetGenderDetails()
        {
            List<PatientViewModel> result = Context.Patients
                .Where(patient => patient.MedicalHistories.Any(p => p.MedicalCondition == "Hypertension"))
                .GroupBy(patient => patient.Gender)
                .Select(group => new PatientViewModel
                {
                    Gender = group.Key,
                    Age = group.Average(patient => patient.Age),
                    MedicalHistory = "Hypertension"
                })
                .ToList();
            return result;
        }

        public List<PatientViewModel> GetTreatmentDetails()
        {
            var result = Context.MedicalHistories
                                .Where(medicalHistory => medicalHistory.MedicalCondition == "Hypertension")
                                .GroupBy(medicalHistory => new
                                {
                                    TreatmentType = medicalHistory.Treatment,
                                    FirstName = medicalHistory.Patient.FirstName,
                                    Age = medicalHistory.Patient.Age
                                })
                                .Select(group => new PatientViewModel
                                {
                                    TreatmentType = group.Key.TreatmentType,
                                    FirstName = group.Key.FirstName,
                                    Age = group.Key.Age
                                })
                                .ToList();

            return result;
        }

        public List<PatientViewModel> GetMultipleMedicalConditions()
        {
            var result = Context.MedicalHistories
                            .GroupBy(medicalHistory => new
                            {
                                PatientId = medicalHistory.PatientId,
                                FirstName = medicalHistory.Patient.FirstName,
                                Age = medicalHistory.Patient.Age
                            })
                            .Where(group => group.Select(h => h.MedicalCondition).Distinct().Count() > 1)
                            .Select(group => new PatientViewModel
                            {
                                FirstName = group.Key.FirstName,
                                Age = group.Key.Age,
                                MedicalConditions = group.Select(h => h.MedicalCondition).Distinct().ToList()
                            })
                            .ToList();
            return result;
        }

        public List<PatientViewModel> GetUnsuccessfulTreatments()
        {
            var result = Context.Patients
                                     .Where(patient =>
                                         patient.TreatmentRecords.Any(
                                             record => record.Outcome == "Failed"
                                         )
                                     )
                                     .Select(patient => new PatientViewModel
                                     {
                                         FirstName = patient.FirstName,
                                         Age = patient.Age,
                                         Address = patient.Address,
                                         MedicalConditions = patient.MedicalHistories.Select(x => x.MedicalCondition).ToList(),

                                     })
                                     .ToList();
            return result;
        }

        public List<PatientViewModel> GetTreatmentSuccessRate()
        {
            var result = Context.TreatmentRecords
                                .Where(r => r.Patient.MedicalHistories.Any(x => x.MedicalCondition == "Hypertension"))
                                .GroupBy(r => "Hypertension")
                                .Select(group => new PatientViewModel
                                {
                                    MedicalHistory = group.Key,
                                    SuccessRate = CalculateSuccessRate(group.ToList())
                                })
                                .ToList();
            return result;
        }

        private static double CalculateSuccessRate(List<TreatmentRecord> records)
        {
            if (records.Count == 0)
                return 0;

            int successfulOutcomes = records.Count(record => record.Outcome == "Successful");
            return (double)successfulOutcomes / records.Count * 100;
        }

        public List<PatientViewModel> GetCombinationTreatments()
        {
            var result = Context.Patients
                                                        .Where(patient => patient.TreatmentRecords
                                                            .Where(record => record.PatientId == patient.PatientId)
                                                            .Select(record => record.TreatmentType)
                                                            .Distinct()
                                                            .Count() > 1)
                                                        .Select(patient => new PatientViewModel
                                                        {
                                                            PatientId = patient.PatientId,
                                                            FirstName = patient.FirstName,
                                                            Age = patient.Age,
                                                            Treatments = patient.TreatmentRecords
                                                                .Where(record => record.PatientId == patient.PatientId)
                                                                .Select(record => record.TreatmentType)
                                                                .Distinct()
                                                                .ToList(),
                                                            Occurrences = patient.TreatmentRecords
                                                                .Count(record => record.PatientId == patient.PatientId)
                                                        })
                                                        .ToList();
            return result;
        }
    }
}