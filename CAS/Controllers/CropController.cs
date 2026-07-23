using CAS.Contracts.Enums;
using CAS.DTOs.Crop;
using CAS.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAS.Controllers
{
    public class CropController(ICropService cropService, ICloudinaryService cloudinaryService) : Controller
    {
        private readonly ICropService _cropService = cropService ?? throw new ArgumentNullException(nameof(cropService));
        private readonly ICloudinaryService _cloudinaryService = cloudinaryService ?? throw new ArgumentNullException(nameof(cloudinaryService));


        [Authorize(Roles = "Admin")]
        [AutoValidateAntiforgeryToken]
        [HttpGet]
        public async Task<IActionResult> AddCrop()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> AddCrop(CreateCropRequestModel request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var imageUploadResult = await _cloudinaryService.UploadImageAsync(request.Image);
            var imageUrl = imageUploadResult.Data.Url;

            var response = await _cropService.CreateCropAsync(imageUrl, request);

            if (!response.IsSuccess)
            {
                ViewBag.ErrorMessage = response.Message;
                return View(response.Message);

            }
            else
            {
                TempData["SuccessMessage"] = response.Message;
                return RedirectToAction("ListOfCrops");
            }

        }

        [Authorize(Roles = "Admin")]
        [AutoValidateAntiforgeryToken]
        [HttpGet("Crop/activate/{id}")]
        public async Task<IActionResult> ActivateCrop(Guid id)
        {

            var response = await _cropService.ActivateCropStatusAsync(id);

            if (!response.IsSuccess)
            {
                ViewBag.ErrorMessage = response.Message;
                return View(response.Message);

            }
            else
            {
                TempData["SuccessMessage"] = response.Message;
                return RedirectToAction("ListOfCrops");
            }

        }

        [Authorize(Roles = "Admin")]
        [AutoValidateAntiforgeryToken]
        [HttpGet("crop/deactivate/{id}")]
        public async Task<IActionResult> DeactivateCrop(Guid id)
        {

            var response = await _cropService.DeactivateCropStatusAsync(id);

            if (!response.IsSuccess)
            {
                ViewBag.ErrorMessage = response.Message;
                return View(response.Message);

            }
            else
            {
                TempData["SuccessMessage"] = response.Message;
                return RedirectToAction("ListOfCrops");
            }

        }

        [HttpGet]
        public async Task<IActionResult> ListOfCrops()
        {
            var response = await _cropService.GetAllCropsAsync();
            if(response.IsSuccess)
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

        [HttpGet]
        public async Task<IActionResult> ListOfCropsForAdmin()
        {
            var response = await _cropService.GetAllCropsForAdminAsync();
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

        [HttpGet("ViewCropDetails/{id}")]
        public async Task<IActionResult> ViewCropDetails(Guid id)
        {
           var response = await _cropService.GetCropByIdAsync(id);
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
    }
}
