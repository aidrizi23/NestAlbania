using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NestAlbania.Data;
using NestAlbania.Services;

namespace NestAlbania.Controllers
{
    
    [Route("favorite")]
    public class FavoriteController : Controller
    {
        private readonly IFavoriteService _favoriteService;
        private readonly UserManager<ApplicationUser> _userManager;

        public FavoriteController(IFavoriteService favoriteService, UserManager<ApplicationUser> userManager)
        {
            _favoriteService = favoriteService;
            _userManager = userManager;
        }
        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var favorites = await _favoriteService.GetUserFavoritesAsync(user.Id);

            ViewData["ActivePage"] = "favoriteIndex";  
            return View(favorites);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite(int propertyId)
        {
            var user = await _userManager.GetUserAsync(User);
            var success = await _favoriteService.AddFavoriteAsync(user.Id, propertyId); 

            return RedirectToAction("Index", "Property");
        }

        [HttpPost]
        [Route("removefavorite")]
        public async Task<IActionResult> RemoveFavorite(int favoriteId)
        {
            if (favoriteId <= 0)
            {
                return BadRequest("Invalid favorite ID.");
            }

            await _favoriteService.RemoveFavoriteAsync(favoriteId);

            ViewData["ActivePage"] = "favoriteIndex";
            return RedirectToAction("Favorites","Property");
            return RedirectToAction("Index", "Favorite");
        }


    }

}
