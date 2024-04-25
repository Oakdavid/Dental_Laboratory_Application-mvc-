using Dental_lab_Application_MVC_.Models.Dtos;
using Dental_lab_Application_MVC_.Models.Service.Implementation;
using Dental_lab_Application_MVC_.Models.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Dental_lab_Application_MVC_.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;

        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        [HttpGet]
        public IActionResult CreatePatient()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePatient(PatientCreateRequestModel requestModel)
        {
            var patient = _patientService.Add(requestModel);
            if (patient != null)
            {
                TempData["Success"] = patient.Message;

                return RedirectToAction("login", "User");
            }
            else
            {
                TempData["ErrorMessage"] = patient.Message;
                return View(requestModel);
            }
        }

        [Authorize(Roles = "HeadDoctor")]
        [HttpGet]
        public IActionResult ViewAllPatient()
        {
            var viewAllPatient = _patientService.GetAll();
            return(View(viewAllPatient));
        }
        //[HttpGet]
        //public IActionResult ViewAllAssignedPatient(Guid docId)
        //{
        //    // var allAssignedPatientsToDoctor = _patientService.GetAllPatientAssigned(docId);
        //    var ids = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    var userId = Guid.Parse(ids);
        //    var allAssignedPatientsToDoctor = _patientService.GetAllPatientAssigned(userId);
        //    if(allAssignedPatientsToDoctor != null)
        //    {
                
        //        return RedirectToAction("DoctorDashBoard", "User");
        //    }
        //    else
        //    {
        //        TempData["ErrorMessage"] = "Failed to assign patient";
        //    }
        //    return null;
        //}

        [Authorize(Roles = "Patient")]
        [HttpGet]
        public IActionResult UpdatePatientProfile(Guid id)
        {
            var ids = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = Guid.Parse(ids);
            var getUpdate = _patientService.Get(userId);
            if (getUpdate == null)
            {
                //return RedirectToAction("UpdatePatientProfile");
                return View("Error");

            }
             return View(getUpdate);
        }

        [Authorize(Roles = "Patient")]
        [HttpPost]
        public IActionResult UpdatePatientProfile(UpdatePatientRequestModel updatePatientRequest)
        {
            var ids = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = Guid.Parse(ids);
            var patientUpdate = _patientService.Get(userId);
            var existingPatient = _patientService.Update(updatePatientRequest, patientUpdate.UserId);
            if (existingPatient != null)
            {
                TempData["Success"] = existingPatient.Message;

                return RedirectToAction("PatientDashBoard", "User");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update Profile";
                return View(updatePatientRequest);
            }
        }
    }
}
