 using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NestAlbania.Data;
using NestAlbania.Models;
using NestAlbania.Services;

namespace NestAlbania.Controllers
{

    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IUserRoleService _userRoleService;
        public AccountController(UserManager<ApplicationUser> userManager, 
                                SignInManager<ApplicationUser> signInManager, 
                                ILogger<AccountController> logger,
                                IUserRoleService userRoleService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _userRoleService = userRoleService;
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        
        [Route("login")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    // Successful login
                    return RedirectToAction( "Index", "Property");
                }
                else if (result.IsLockedOut)
                {
                    return RedirectToAction("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }
            
            return View(model);
        }
      

        [HttpGet]
        [AllowAnonymous]
        [Route("logout")]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation($"Logout");
            return RedirectToAction("Login", "Account");

        }
        
        public IActionResult Lockout()
        {
            return View();
        }
    }
}

