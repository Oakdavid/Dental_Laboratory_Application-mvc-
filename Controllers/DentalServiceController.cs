using Dental_lab_Application_MVC_.Models.Dtos;
using Dental_lab_Application_MVC_.Models.Service.Implementation;
using Dental_lab_Application_MVC_.Models.Service.Interface;
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
            var service = _dentalService.Add(dentalServiceCreate);
            if (service != null)
            {
                // ViewBag.Message = "testing";
                ViewData["Message"] = "Dental service successfully Created";
                return View();
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Failed to create dental service");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult ViewAllDentalServices()
        {
            var getAllService = _dentalService.GetAll();
            return View(getAllService);
        }

        [HttpGet]
        public IActionResult UpdateDentalService(Guid id)
        {
            var getUpdate = _dentalService.Get(id);
            if(getUpdate == null)
            {
              return RedirectToAction("Index", "Home");
            }
            return View(getUpdate);
        }

        [HttpPost]
        public IActionResult UpdateDentalService(DentalServiceUpdateRequestModel updatedService)
        {
             var existingService = _dentalService.Update(updatedService);
            if(existingService != null)
            {
                ViewData["Message"] = "Successfully Created";
                return RedirectToAction("ViewAllDentalServices");
            }
            return View(existingService);
        }
    }
}

