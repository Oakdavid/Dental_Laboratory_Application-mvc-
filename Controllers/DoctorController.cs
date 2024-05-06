using Dental_lab_Application_MVC_.Models.Dtos;
using Dental_lab_Application_MVC_.Models.Entites;
using Dental_lab_Application_MVC_.Models.Service.Interface;
using Microsoft.AspNetCore.Authorization;

//using Dental_lab_Application_MVC_.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace Dental_lab_Application_MVC_.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService service)
        {
            _doctorService = service;
        }
        [HttpGet]
        public IActionResult CreateDoctor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateDoctor(CreateDoctorRequestModel createDoctorRequestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var doctor = _doctorService.Create(createDoctorRequestModel);
            if(doctor != null)
            {
                TempData["Success"] = doctor.Message;

                return RedirectToAction("login", "User");
            }
            else
            {
                TempData["ErrorMessage"] = doctor.Message;
                return View(createDoctorRequestModel);  
            }
        }

        [Authorize(Roles = "HeadDoctor")]
        [HttpGet]
        public IActionResult ViewAllAvailableDoctor()
        {
            var viewAllAvailableDoctors = _doctorService.GetAllAvailableDoctors();
            return View(viewAllAvailableDoctors);
        }

        [Authorize(Roles = "HeadDoctor")]
        [HttpGet]
        public IActionResult ViewAllDoctors()
        {
            var allDoctors = _doctorService.GetAll();
              return View(allDoctors);
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet]
        public IActionResult UpdateDoctor(Guid id)
        {
            var ids = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = Guid.Parse(ids);
            var getUpdate = _doctorService.Get(userId);
            if (getUpdate == null)
            {
              //  return RedirectToAction("Index", "Home");
                return View("Error");
            }
            return View(getUpdate);
        }

        [Authorize(Roles = "Doctor")]
        [HttpPost]
        public IActionResult UpdateDoctor(UpdateDoctorRequestModel updateDoctorRequest)
        {
            var ids = User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userId = Guid.Parse(ids);
            var doctorUpdate = _doctorService.Get(userId);
            var existingDoctor = _doctorService.Update(updateDoctorRequest, doctorUpdate.UserId);
            if(existingDoctor != null)
            {
                TempData["Success"] = existingDoctor.Message;

                return RedirectToAction("DoctorDashBoard", "User");
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to update profile";
                return View(updateDoctorRequest);
            }
        }
    }
}

