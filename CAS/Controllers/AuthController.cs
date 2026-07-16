using CAS.DTOs.Auth;
using CAS.Interfaces.Repositories;
using CAS.Interfaces.Services;
using CAS.Models;
using CAS.Models.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;
using CAS.Contracts.Identity;

namespace CAS.Controllers
{
    public class AuthController(IUserService userService, IIdentityService identityService, IUserRepository userRepository) : Controller
    {
        private readonly IUserService _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        private readonly IIdentityService _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
        private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegisterFarmer()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterFarmer(RegisterFarmerRequestModel request)
        {
            var result = await _userService.RegisterFarmerAsync(request);

            if (result.IsSuccess)
            {
                ViewBag.Message = result.Message;
                return RedirectToAction("Login");
            }
            ViewBag.Message = result.Message;
            return View(request);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var normalizeEmail = _identityService.GetNormalizedEmail(request.Email);
            var user = await _userRepository.Get<User>(u => u.Email == normalizeEmail);

            if (user is null)
            {
                ViewBag.Message = "Invalid email or password";
                return View(request);
            }
            Console.WriteLine("DB PASSWORD HASH: " + user.PasswordHash);

            var verifyPassword = _identityService.VerifyPassword(user.PasswordHash, request.Password);

            Console.WriteLine("VERIFY PASSWORD: " + verifyPassword);

            if (!verifyPassword)
            {
                ViewBag.Message = "Invalid email or password";
                return View(request);
            }

            var role = await _identityService.GetRoleAsync(user);

            var loginResponseData = new LoginResponseData
            {
                FullName = user.FullName,
                UserId = user.Id,
                Role = role,
                Email = user.Email
            };

            var claims = new List<Claim>
             {
                    new Claim(ClaimTypes.Name, loginResponseData.FullName),
                   new Claim(ClaimTypes.Email, loginResponseData.Email),
                    new Claim(ClaimTypes.NameIdentifier, loginResponseData.UserId.ToString()),
                    new Claim(ClaimTypes.Role, loginResponseData.Role)


             };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authenticationProperties = new AuthenticationProperties();
            var principal = new ClaimsPrincipal(claimsIdentity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperties);
            if (role == "Farmer")
            {
                return RedirectToAction("FarmerDashboard", "User");
            }

            return RedirectToAction("AdminDashboard", "User");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }
    }
}
