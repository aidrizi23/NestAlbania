using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NestAlbania.Data;
using NestAlbania.Models;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Humanizer;
using NestAlbania.Services;

namespace NestAlbania.Controllers
{
    [Authorize]
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
        [AllowAnonymous] //  lejon userin te navigoje ne webpage edhe pse nuk eshte i loguar sepse useri sdo jete i loguar deri ketu
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme); // ben clear te gjitha cookies (funksionon edhe pa kete rresht kodi) per te bere nje clear login process
            ViewBag.Error = false;
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        //{
        //    ViewData["ReturnUrl"] = returnUrl;
        //    var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

        //    if (result.Succeeded)
        //    {
        //        _logger.LogInformation($"User with email {model.Email} logged in.");
        //        return RedirectToAction("Index", "Home");
        //    }
        //    else
        //    {
        //        ViewBag.Error = true;
        //        ModelState.AddModelError(string.Empty, "Invalid login attempt");
        //        return View(model);
        //    }
        //}

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // Successful login
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return View(model);
                }
            }

            // If model state is not valid, return to login view with errors
            return View(model);
        }
      

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation($"Logout");
            return RedirectToAction("Login", "Account");

        }


        [HttpGet]
        [AllowAnonymous]    
        public async Task<IActionResult> Register(string? returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            user.Id = Guid.NewGuid().ToString();
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "User");
            }
            var userRole = new ApplicationUserRole()
            {
                UserId = user.Id,
                RoleId = "e13fc5b7-cc45-4a6c-a8d2-02ab1298e678",
            };
            try
            {
                await _userRoleService.CreateAsync(userRole);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error creating user role.");

            }
            return View(model);

        }
        public async Task SignInWithGoogle()
        {
            //var redirectUrl = Url.Action("GoogleResponse", "Account");
            //var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
            //return Challenge(properties, GoogleDefaults.AuthenticationScheme);
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme, new AuthenticationProperties 
            {
                RedirectUri = Url.Action("GoogleResponse")
            });
        }
        [HttpGet("~/signin-google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            // Authenticate using the Google authentication scheme
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            // Ensure result and Principal are not null
            if (result?.Principal == null)
            {
                return BadRequest("Google authentication failed. No principal identities found.");
            }

            var emailClaim = result.Principal.FindFirst(ClaimTypes.Email);
            var email = emailClaim?.Value;

            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Google authentication failed. Email claim not found.");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                user = new ApplicationUser
                {
                    CustomUserName = email, // Custom username for the user
                    UserName = email,
                    Email = email,
                    Id = Guid.NewGuid().ToString()
                };
                var userRole = new ApplicationUserRole()
                {
                    UserId = user.Id,
                    RoleId = "e13fc5b7-cc45-4a6c-a8d2-02ab1298e678",
                };
                try
                {
                    await _userRoleService.CreateAsync(userRole);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Error creating user role.");

                }
                var createUserResult = await _userManager.CreateAsync(user);
                if (!createUserResult.Succeeded)
                {
                    return BadRequest("Failed to create new user.");
                }
            }

            // Sign in the user
            await _signInManager.SignInAsync(user, isPersistent: false);
            return RedirectToAction("Index", "Home");
        }


        [HttpGet("~/signout")]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}

