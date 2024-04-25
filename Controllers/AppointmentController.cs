using Dental_lab_Application_MVC_.Models.Dtos;
using Dental_lab_Application_MVC_.Models.Entites;
using Dental_lab_Application_MVC_.Models.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Dental_lab_Application_MVC_.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        //[Authorize(Roles = "Patient")]
        [HttpGet]
        public IActionResult CreateAppointment()
        {
            return View();
        }

        //[Authorize(Roles = "Patient")]
        [HttpPost]
        public IActionResult CreateAppointment(AppointmentRequestModel appointmentRequestModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var appointments = _appointmentService.Add(appointmentRequestModel);
            if(appointments != null)
            {
                TempData["Success"] = appointments.Message;
                return RedirectToAction("PatientDashBoard", "User");
            }
            else
            {
                TempData["ErrorMessage"] = appointments.Message;
                return View(appointmentRequestModel);
            }
        }

    }
}
