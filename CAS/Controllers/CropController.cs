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
                ViewBag.Message = response.Message;
                return View("Error", response.Message);

            }
            else
            {
                ViewBag.Message = response.Message;
                return RedirectToAction("ListOfCrops");
            }

        }

        [HttpGet]
        public async Task<IActionResult> ListOfCrops()
        {
            var response = await _cropService.GetAllCropsAsync();
            if(response.IsSuccess)
            {
                return View(response.Data);
            }
            else
            {
               ViewBag.Message = response.Message;
                return View();
            }
        }

        [HttpGet("ViewCropDetails/{id}")]
        public async Task<IActionResult> ViewCropDetails(Guid id)
        {
           var response = await _cropService.GetCropByIdAsync(id);
            if (response.IsSuccess)
            {
                return View(response.Data);
            }
            else
            {
                ViewBag.Message = response.Message;
                return View("Error", response.Message);
            }
        }
    }
}
