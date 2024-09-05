using NestAlbania.Data;
using NestAlbania.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NestAlbania.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly FavoriteRepository _favoriteRepository;

        public FavoriteService(FavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public async Task<IEnumerable<UserFavorite>> GetUserFavoritesAsync(string userId)
        {
            return await _favoriteRepository.GetFavoritesByUserIdAsync(userId);
        }

        public async Task<bool> AddFavoriteAsync(string userId, int propertyId)
        {
            if (await _favoriteRepository.IsFavoriteExistsAsync(userId, propertyId))
                return false;

            var favorite = new UserFavorite
            {
                UserId = userId,
                PropertyId = propertyId
            };

            await _favoriteRepository.AddFavoriteAsync(favorite);
            return true;
        }

        public async Task RemoveFavoriteAsync(int favoriteId)
        {
            await _favoriteRepository.RemoveFavoriteAsync(favoriteId);
        }
    }
}
