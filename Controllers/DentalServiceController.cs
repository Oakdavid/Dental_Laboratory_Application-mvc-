using Dental_lab_Application_MVC_.Models.Dtos;
using Dental_lab_Application_MVC_.Models.Entites;
using Dental_lab_Application_MVC_.Models.Service.Implementation;
using Dental_lab_Application_MVC_.Models.Service.Interface;
using Microsoft.AspNetCore.Authorization;

//using Dental_lab_Application_MVC_.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Dental_lab_Application_MVC_.Controllers
{
    public class DentalServiceController : Controller
    {
        private readonly IDentalServiceService _dentalService;

        public DentalServiceController(IDentalServiceService dentalService)
        {
            _dentalService = dentalService;
        }
        [HttpGet]
        public IActionResult CreateDentalService()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateDentalService(DentalServiceCreateRequestModel dentalServiceCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var service = _dentalService.Create(dentalServiceCreate);
            if (service != null)
            {
                
                TempData["Message"] = service.Message;
                return RedirectToAction("HeadDoctorDashBoard", "User");

            }
            else
            {
                TempData["ErrorMessage"] = service.Message;
                return View(dentalServiceCreate);
            }
        }

        [HttpGet]
        public IActionResult ViewAllDentalServices()
        {
            var getAllService = _dentalService.GetAll();
            return View(getAllService);
        }

        [Authorize(Roles = "HeadDoctor")]
        [HttpGet]
        public IActionResult UpdateDentalService(Guid id)
        {
            var getUpdate = _dentalService.Get(id);
            if(getUpdate == null)
            {
                return View("Error");
            }
            return View(getUpdate);
        }

        [Authorize(Roles = "HeadDoctor")]
        [HttpPost]
        public IActionResult UpdateDentalService(DentalServiceUpdateRequestModel updatedService)
        {
             var existingService = _dentalService.Update(updatedService);
            if(existingService != null)
            {
                TempData["Message"] = existingService.Message;
                return RedirectToAction("HeadDoctorDashBoard", "User");
            }
            return View(existingService);
        }
    }
}

