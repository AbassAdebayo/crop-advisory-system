using CAS.Implementation.Services;
using CAS.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAS.Controllers
{
    public class SoilTypeController(ISoilTypeService soilTypeService) : Controller
    {
        private readonly ISoilTypeService _soilTypeService = soilTypeService ?? throw new ArgumentNullException(nameof(soilTypeService));


        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/ListOfSoilTypesForAdmin")]
        public async Task<IActionResult> ListOfSoilTypesForAdmin()
        {
            var response = await _soilTypeService.GetAllSoilTypesForAdminAsync();
            if (response.IsSuccess)
            {
                TempData["SuccessMessage"] = response.Message;
                return View(response);
            }
            else
            {
                ViewBag.ErrorMessage = response.Message;
                return View(response);
            }
        }

        [Authorize]
        [HttpGet("Farmer/ListOfCropTypesForFarmer")]
        public async Task<IActionResult> ListOfSoilTypesForFarmer()
        {
            var response = await _soilTypeService.GetAllSoilTypesForFarmerAsync();
            if (response.IsSuccess)
            {
                TempData["SuccessMessage"] = response.Message;
                return View(response);
            }
            else
            {
                ViewBag.ErrorMessage = response.Message;
                return View(response);
            }
        }

        [Authorize]
        [HttpGet("Farmer/ViewSoilTypeDetails/{id}")]
        public async Task<IActionResult> ViewSoilTypeDetailsForFarmer(Guid id)
        {
            var response = await _soilTypeService.GetSoilTypeDetailsForFarmerAsync(id);
            if (response.IsSuccess)
            {
                TempData["SuccessMessage"] = response.Message;
                return View(response);
            }
            else
            {
                ViewBag.ErrorMessage = response.Message;
                return View(response);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/ViewSoilTypeDetails/{id}")]
        public async Task<IActionResult> ViewSoilTypeDetailsForAdmin(Guid id)
        {
            var response = await _soilTypeService.GetSoilTypeDetailsForAdminAsync(id);
            if (response.IsSuccess)
            {
                TempData["SuccessMessage"] = response.Message;
                return View(response);
            }
            else
            {
                ViewBag.ErrorMessage = response.Message;
                return View(response);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("Soiltype/activate/{id}")]
        public async Task<IActionResult> ActivateSoilType(Guid id)
        {

            var response = await _soilTypeService.ActivateSoilTypeStatusAsync(id);

            if (!response.IsSuccess)
            {
                ViewBag.ErrorMessage = response.Message;
                return View(response.Message);

            }
            else
            {
                TempData["SuccessMessage"] = response.Message;
                return RedirectToAction("ListOfSoilTypesForAdmin");
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpGet("soiltype/deactivate/{id}")]
        public async Task<IActionResult> DeactivateSoilType(Guid id)
        {

            var response = await _soilTypeService.DeactivateSoilTypeStatusAsync(id);

            if (!response.IsSuccess)
            {
                ViewBag.ErrorMessage = response.Message;
                return View(response.Message);

            }
            else
            {
                TempData["SuccessMessage"] = response.Message;
                return RedirectToAction("ListOfSoilTypesForAdmin");
            }

        }

    }
}
