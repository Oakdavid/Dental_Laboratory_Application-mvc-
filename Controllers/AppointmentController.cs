using Dental_lab_Application_MVC_.Models.Dtos;
using Dental_lab_Application_MVC_.Models.Entites;
using Dental_lab_Application_MVC_.Models.Service.Implementation;
using Dental_lab_Application_MVC_.Models.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Numerics;
using System.Security.Claims;

namespace Dental_lab_Application_MVC_.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IDoctorService _doctorService;

        public AppointmentController(IAppointmentService appointmentService, IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
        }

        [Authorize(Roles = "Patient")]
        [HttpGet]
        public IActionResult CreateAppointment()
        {

            var ids = Guid.Parse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var createAppointment = _appointmentService.Get(ids);
            if (createAppointment == null)
            {
                return View("Error");
            }
            return View(createAppointment);
        }

        [Authorize(Roles = "Patient")]
        [HttpPost]
        public IActionResult CreateAppointment(AppointmentRequestModel appointmentRequestModel)
        {

            appointmentRequestModel.UserId = Guid.Parse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var appointment = _appointmentService.Create(appointmentRequestModel);
            if (appointment != null)
            {
                TempData["Success"] = appointment.Message;

                return RedirectToAction("PatientDashBoard", "User");
            }
            else
            {
                TempData["ErrorMessage"] = appointment.Message;
                return View(appointment);
            }
        }

        [Authorize(Roles = "HeadDoctor") ]
        [HttpGet]
        public IActionResult AssignDoctorToAppointment()
        {
            return View();
        }

        [Authorize(Roles = "HeadDoctor")]
        [HttpPost]
        public IActionResult AssignDoctorToAppointment(Guid patientId, Guid doctorId)
        {
            var allAvailableDoctors = _doctorService.GetAllAvailableDoctors();
            if(allAvailableDoctors.Any())
            {
                
                ViewBag.AvailableDoctors = new SelectList(allAvailableDoctors, "Id", "Name");
            }
            var assignDoctor = _appointmentService.AssignDoctorToAppointment(patientId, doctorId);
            if (assignDoctor != null)
            {
                return RedirectToAction("HeadDoctorDashBoard", "User");
            }
            else
            {
                return View();
            }
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet]
        public IActionResult CloseBookedAppointment()
        {
            var doctorIds = Guid.Parse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var closeAppointment = _appointmentService.GetDoctorAssignedAppointments(doctorIds);
            if (closeAppointment == null)
            {
                return View();
            }
            return View(closeAppointment);
        }

        [Authorize(Roles = "Doctor")]
        [HttpPost]
        public IActionResult CloseAssignedAppointment( Guid doctorId)
        {
            var doctorIds = Guid.Parse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var closeAppointment = _appointmentService.CloseAssignedAppointment( doctorIds);
            if (closeAppointment)
            {
                return RedirectToAction("DoctorDashBoard", "User");
            }
            else
            {

                return View();
            }
        }


        [Authorize(Roles = "Doctor, HeadDoctor")]
        [HttpGet]
        public IActionResult GetAllInitializedAppointment()
        {
            var allInitializedAppointment = _appointmentService.GetAllInitializedAppointment();
            var getAllAvailableDoctors = _doctorService.GetAllAvailableDoctors();
            
            if(allInitializedAppointment == null)
            {
                return View("No Appointments Found");
            }

            if(getAllAvailableDoctors == null)
            {
                return View("No Doctors Found");
            }

            if (getAllAvailableDoctors.Any())
            {
                ViewBag.AvailableDoctors = new SelectList(getAllAvailableDoctors, "Id", "Name");
            }

            return View(allInitializedAppointment);
        }

        [Authorize(Roles = "Patient")]
        [HttpGet]
        public IActionResult PatientViewBookedAppointment()
        {
            var patientId = Guid.Parse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var patientBookAppointment = _appointmentService.PatientViewBookedAppointment(patientId);
            if(patientBookAppointment == null)
            {
                return View();
            }
            return View(patientBookAppointment);
        }

        [Authorize(Roles ="Patient")]
        [HttpPost]
        public IActionResult PatientViewBookedAppointment(Guid patientId)
        {
            patientId = Guid.Parse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var patientBookedAppointment = _appointmentService.PatientViewBookedAppointment(patientId);
            if(patientBookedAppointment != null)
            {
                TempData["Message"] = patientBookedAppointment.Message;
                return RedirectToAction("PatientDashBoard", "User");
            }
            else
            {
                TempData["ErrorMessage"] = patientBookedAppointment.Message;
                return View(patientBookedAppointment);
            }
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet]
        public IActionResult AllAssignedAppointmentsToADoctor()
        {
            var doctorIds = Guid.Parse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var doctorAssignedAppointment = _appointmentService.GetDoctorAssignedAppointments(doctorIds);
            if(doctorAssignedAppointment == null)
            {
                return View();
            }
            return View(doctorAssignedAppointment);
        }

        [Authorize(Roles ="Doctor, HeadDoctor")]  //trying out
        [HttpPost]
        public IActionResult AllAssignedAppointmentsToADoctor(Guid doctorId)
        {
            var doctorIds = Guid.Parse(User?.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var doctorAssignedAppointment = _appointmentService.GetDoctorAssignedAppointments(doctorIds);
            if(doctorAssignedAppointment.Any())
            {
               
                return View(doctorAssignedAppointment);
            }
            else
            {
                ViewBag.Message = "No assigned appointments found for this doctor";
                return View(doctorAssignedAppointment);
            }
        }

        [HttpGet]
        public IActionResult AllClosedAppointment()
        {
            var allClosedAppointment = _appointmentService.GetAllClosedAppointment();
            if(allClosedAppointment.Any())
            {
                return View(allClosedAppointment);
            }
            else
            {
                return View("No Closed Appointment found");
            }
        }
    }
}
