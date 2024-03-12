using HospitalManagement.Services;
using HospitalManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HospitalManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly HospitalManagementServices HospitalManagementServices;
        public HomeController(HospitalManagementServices hospitalManagementServices)
        {
            HospitalManagementServices = hospitalManagementServices;
        }

        public IActionResult Index()
        {
            ViewData["GenderData"] = GetGenderDetails();
            ViewData["TreatmentDetailsData"] = GetTreatmentDetails();
            ViewData["MultipleMedicalConditionData"] = GetMultipleMedicalConditions();
            ViewData["UnsuccessfulTreatments"] = GetUnsuccessfulTreatments();
            ViewData["TreatmentSuccess"] = GetTreatmentSuccessRate();
            return View();
        }

        public List<PatientViewModel> GetGenderDetails()
        {
            List<PatientViewModel> result = HospitalManagementServices.GetGenderDetails();
            return result;
        }

        public List<PatientViewModel> GetTreatmentDetails()
        {
            var result = HospitalManagementServices.GetTreatmentDetails();
            return result;

        }

        public List<PatientViewModel> GetMultipleMedicalConditions()
        {
            var result = HospitalManagementServices.GetMultipleMedicalConditions();
            return result;
        }

        public List<PatientViewModel> GetUnsuccessfulTreatments()
        {
            var result = HospitalManagementServices.GetUnsuccessfulTreatments();
            return result;
        }

        public List<PatientViewModel> GetTreatmentSuccessRate()
        {
            var result = HospitalManagementServices.GetTreatmentSuccessRate();
            return result;
        }
    }
}
