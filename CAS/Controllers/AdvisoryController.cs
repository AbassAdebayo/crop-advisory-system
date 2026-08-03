using CAS.DTOs.Advisory;
using CAS.Implementation.Services;
using CAS.Interfaces.Services;
using CAS.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAS.Controllers
{
    public class AdvisoryController : Controller
    {
        private readonly ICropService _cropService;
        private readonly ISoilTypeService _soilTypeService;
        private readonly ISeasonService _seasonService;
        private readonly IAdvisoryService _advisoryAdvice;
        public AdvisoryController(ISoilTypeService soilTypeService, ISeasonService seasonService, 
            ICropService cropService, IAdvisoryService advisoryAdvice)
        {
            _soilTypeService = soilTypeService;
            _seasonService = seasonService;
            _cropService = cropService;
            _advisoryAdvice = advisoryAdvice;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> AddAdvisories()
        {
            var crops = await _cropService.GetAllCropsAsync();
            ViewBag.Crops = crops.Data;

            var seasons = await _seasonService.GetAllActiveSeasonsAsync();
            ViewBag.Seasons = seasons.Data;


            var soilTypes = await _soilTypeService.GetAllSoilTypesForFarmerAsync();
            ViewBag.SoilTypes = soilTypes.Data;

            return View(new CreateBulkAdvisoriesRequest());
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddAdvisories(CreateBulkAdvisoriesRequest request)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Crops = await _cropService.GetAllCropsAsync();
                ViewBag.Seasons = await _seasonService.GetAllActiveSeasonsAsync();
                ViewBag.SoilTypes = await _soilTypeService.GetAllSoilTypesForFarmerAsync();

                return View(request);
            }

            var response = await _advisoryAdvice.CreateAdvisoryAsync(request.Advisories);


            if (!response.IsSuccess)
            {
                ViewBag.ErrorMessage = response.Message;
                return View(response.Message);

            }
            else
            {
                TempData["SuccessMessage"] = response.Message;
                return RedirectToAction("Index");
            }
        }


    }
}
