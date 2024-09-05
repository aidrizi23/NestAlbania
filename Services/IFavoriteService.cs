using NestAlbania.Data;

namespace NestAlbania.Services
{
    public interface IFavoriteService
    {
        Task<IEnumerable<UserFavorite>> GetUserFavoritesAsync(string userId);
        Task<bool> AddFavoriteAsync(string userId, int propertyId);
        Task RemoveFavoriteAsync(int favoriteId);

    }
}
