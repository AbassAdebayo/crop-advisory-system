using CAS.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CAS.Controllers
{
    public class CropController(ICropService cropService) : Controller
    {
        private readonly ICropService _cropService = cropService ?? throw new ArgumentNullException(nameof(cropService));

        [HttpGet]
        public async Task<IActionResult> AllCrops()
        {
            var response = await _cropService.GetAllCropsAsync();
            if(response.IsSuccess)
            {
                return View(response.Data);
            }
            else
            {
               ViewBag.Message = response.Message;
                return View("Error", response.Message);
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
