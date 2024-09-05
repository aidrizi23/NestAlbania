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
            return View(favorites);
        }

        [HttpPost]
        // [Route("add")]
        public async Task<IActionResult> AddFavorite(int propertyId)
        {
            var user = await _userManager.GetUserAsync(User);
            var success = await _favoriteService.AddFavoriteAsync(user.Id, propertyId);

            if (!success)
            {
             
                return BadRequest("Property is already a favorite.");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RemoveFavorite(int favoriteId)
        {
            await _favoriteService.RemoveFavoriteAsync(favoriteId);
            return RedirectToAction(nameof(Index));
        }

       
    }

}
