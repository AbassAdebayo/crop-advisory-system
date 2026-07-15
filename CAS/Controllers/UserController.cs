using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CAS.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AdminDashboard()
        {
            var name = User?.FindFirst(ClaimTypes.Name)?.Value;
            var startName = name?.Substring(0, 1).ToUpper();
            ViewBag.NameAvatar = startName;

            return View();
        }

        public IActionResult FarmerDashboard()
        {
            var name = User?.FindFirst(ClaimTypes.Name)?.Value;
            var startName = name?.Substring(0, 1).ToUpper();
            ViewBag.NameAvatar = startName;
            return View();
        }
    }
}
