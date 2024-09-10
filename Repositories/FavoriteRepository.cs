using Microsoft.EntityFrameworkCore;
using NestAlbania.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestAlbania.Repositories
{
    public class FavoriteRepository
    {
        private readonly ApplicationDbContext _context;

        public FavoriteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserFavorite>> GetFavoritesByUserIdAsync(string userId)
        {
            return await _context.Favorites
                .Include(f => f.Property)
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }

        public async Task<UserFavorite> AddFavoriteAsync(UserFavorite favorite)
        {
            _context.Favorites.Add(favorite);
            await _context.SaveChangesAsync();
            return favorite;
        }

        public async Task RemoveFavoriteAsync(int favoriteId)
        {
            var favorite = await _context.Favorites.FindAsync(favoriteId);
            if (favorite != null)
            {
                _context.Favorites.Remove(favorite);

                await _context.SaveChangesAsync();
            }
        }
        public async Task<List<Property>> GetFavoritePropertiesByUserIdAsync(string userId)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Property)
                .Select(f => f.Property)
                .ToListAsync();
        }

        // Retrieves favorite properties based on agent ID
        public async Task<List<Property>> GetFavoritePropertiesByAgentIdAsync(int agentId)
        {
            return await _context.Favorites
                .Where(f => f.Property.AgentId == agentId)
                .Include(f => f.Property)
                .Select(f => f.Property)
                .ToListAsync();
        }

        public async Task<bool> IsFavoriteExistsAsync(string userId, int propertyId)
        {
            return await _context.Favorites.AnyAsync(f => f.UserId == userId && f.PropertyId == propertyId);
        }
    }
}
