using HospitalManagement.Repository;
using HospitalManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagement.Services
{
    public class HospitalManagementServices
    {
        private readonly HospitalManagementRepository HospitalManagementRepository;

        public HospitalManagementServices(HospitalManagementRepository hospitalManagementRepository)
        {
            HospitalManagementRepository = hospitalManagementRepository;
        }

        public List<PatientViewModel> GetGenderDetails()
        {
            return HospitalManagementRepository.GetGenderDetails();
        }

        public List<PatientViewModel> GetTreatmentDetails()
        {
            return HospitalManagementRepository.GetTreatmentDetails();
        }

        public List<PatientViewModel> GetMultipleMedicalConditions()
        {
            return HospitalManagementRepository.GetMultipleMedicalConditions();
        }

        public List<PatientViewModel> GetUnsuccessfulTreatments()
        {
            return HospitalManagementRepository.GetUnsuccessfulTreatments();
        }

        public List<PatientViewModel> GetTreatmentSuccessRate()
        {
            return HospitalManagementRepository.GetTreatmentSuccessRate();
        }
    }
}
