using CAS.DTOs.Advisory;
using CAS.Implementation.Services;
using CAS.Interfaces.Services;
using CAS.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index(SearchAdvisoryRequestModel request)
        {
            request.Page = request.Page <= 0 ? 1 : request.Page;
            request.PageSize = request.PageSize <= 0 ? 10 : request.PageSize;

            ViewBag.Crops = (await _cropService.GetAllCropsAsync()).Data;
            ViewBag.Seasons = (await _seasonService.GetAllActiveSeasonsAsync()).Data;
            ViewBag.SoilTypes = (await _soilTypeService.GetAllSoilTypesForFarmerAsync()).Data;

            var result = await _advisoryAdvice.SearchAsync(request);

            return View(result.Data);
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Details(Guid id)
        {
            var farmerIdClaims = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Guid farmerId = default;

            while (!Guid.TryParse(farmerIdClaims, out farmerId))
                return BadRequest("Unable to parse farmer Id");

            var result = await _advisoryAdvice.GetFavouriteAdvisoryDetails(id, farmerId);

            if (!result.IsSuccess)
            {
                TempData["Error"] = result.Message;
                return RedirectToAction(nameof(Index));
            }

            return View(result.Data);
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

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ToggleFavourite(Guid advisoryId)
        {
            var farmerIdClaims = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Guid farmerId = default;

            while (!Guid.TryParse(farmerIdClaims, out farmerId))
                return BadRequest("Unable to parse farmer Id");

            Console.WriteLine($"Farmer ID: {farmerId}");
           

            var result = await _advisoryAdvice.ToggleFavouriteAsync(
                farmerId,
                advisoryId);

            return RedirectToAction(nameof(Details), new { id = advisoryId });
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Favourites()
        {

            var farmerIdClaims = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Guid farmerId = default;

            while (!Guid.TryParse(farmerIdClaims, out farmerId))
                return BadRequest("Unable to parse farmer Id");

            var result = await _advisoryAdvice.GetFavouriteAdvisoriesAsync(farmerId);

            return View(result.Data);
        }



    }
}
