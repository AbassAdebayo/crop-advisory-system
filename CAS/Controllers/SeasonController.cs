using CAS.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CAS.Controllers
{
    public class SeasonController : Controller
    {
        private readonly ISeasonService _seasonService;
        public SeasonController(ISeasonService seasonService)
        {
            _seasonService = seasonService ?? throw new ArgumentNullException(nameof(seasonService));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("Admin/ListOfSeasons")]
        public async Task<IActionResult> ListOfSeasons()
        {
            var response = await 
                _seasonService.GetAllSeasonsForAdminAsync();
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
        [HttpGet("Admin/ViewSeason/{id}")]
        public async Task<IActionResult> ViewSeason(Guid id)
        {
            var response = await _seasonService.GetActiveSeasonById(id);
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

        //[Authorize]
        //[HttpGet("ListOfActiveSeasons")]
        //public async Task<IActionResult> ListOfActiveSeasons()
        //{
        //    var response = await _seasonService.GetAllActiveSeasonsAsync();
        //    if (response.IsSuccess)
        //    {
        //        TempData["SuccessMessage"] = response.Message;
        //        return View(response);
        //    }
        //    else
        //    {
        //        ViewBag.ErrorMessage = response.Message;
        //        return View(response);
        //    }
        //}

        //[Authorize]
        //[HttpGet("ViewActiveSeason/{id}")]
        //public async Task<IActionResult> ViewActiveSeason(Guid id)
        //{
        //    var response = await _seasonService.GetActiveSeasonById(id);
        //    if (response.IsSuccess)
        //    {
        //        TempData["SuccessMessage"] = response.Message;
        //        return View(response);
        //    }
        //    else
        //    {
        //        ViewBag.ErrorMessage = response.Message;
        //        return View(response);
        //    }
        //}


    }
}
