using Microsoft.EntityFrameworkCore;
using NestAlbania.Data;
using NestAlbania.Data.Enums;
using NestAlbania.FilterHelpers;
using NestAlbania.Repositories.Pagination;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NestAlbania.Repositories
{
    public class PropertyRepository : BaseRepository<Property>
    {
        private readonly ApplicationDbContext _context;

        public PropertyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Property>> GetAllPropertiesAsync()
        {
            return await _context.Properties.AsNoTracking().ToListAsync();
        }

        public async Task<PaginatedList<Property>> GetAllPaginatedPropertiesAsync(int pageIndex = 1, int pageSize = 10)
        {
            var source = _context.Properties
                .AsNoTracking()
                .Where(x => !x.isDeleted && !x.IsSold)
                .OrderByDescending(x => x.Id);
            return await PaginatedList<Property>.CreateAsync(source, pageIndex, pageSize);
        }
        
        public async Task<PaginatedList<Property>> GetPropertiesByNumberOfBedroomsAsync(int nrOfBedrooms, int pageIndex = 1, int pageSize = 10)
        {
            var paginatedProperties = _context.Properties
                .AsNoTracking()
                .Where(x => x.BedroomCount == nrOfBedrooms && !x.isDeleted)
                .OrderByDescending(x => x.Id);
            return await PaginatedList<Property>.CreateAsync(paginatedProperties, pageIndex, pageSize);
        }

        public async Task<PaginatedList<Property>> GetAllFilteredPropertiesAsync(PropertyObjectQuery query, int pageIndex, int pageSize, string sortOrder)
        {
            var properties = _context.Properties
                .AsNoTracking()
                .Where(x => !x.isDeleted && !x.IsSold);

            if (!string.IsNullOrEmpty(query.Name))
            {
                properties = properties.Where(p => p.Name.Contains(query.Name));
            }
            if (query.FullArea.HasValue)
            {
                var lowerBound = query.FullArea * 0.9;
                var upperBound = query.FullArea * 1.1;
                properties = properties.Where(p => p.FullArea >= lowerBound && p.FullArea <= upperBound);
            }
            if (query.InsideArea.HasValue)
            {
                properties = properties.Where(p => p.InsideArea >= query.InsideArea);
            }
            if (query.BedroomCount.HasValue)
            {
                properties = properties.Where(p => p.BedroomCount >= query.BedroomCount);
            }
            if (query.BathroomCount.HasValue)
            {
                properties = properties.Where(p => p.BathroomCount >= query.BathroomCount);
            }
            if (query.MinPrice.HasValue)
            {
                properties = properties.Where(p => p.Price >= query.MinPrice);
            }
            if (query.MaxPrice.HasValue)
            {
                properties = properties.Where(p => p.Price <= query.MaxPrice);
            }

            switch (sortOrder)
            {
                case "price-asc":
                    properties = properties.OrderBy(p => p.Price);
                    break;
                case "price-desc":
                    properties = properties.OrderByDescending(p => p.Price);
                    break;
                default:
                    properties = properties.OrderBy(p => p.Id); // Default sorting
                    break;
            }

            return await PaginatedList<Property>.CreateAsync(properties, pageIndex, pageSize);
        }

        public async Task<PaginatedList<Property>> GetAllPaginatedPropertiesByAgentIdAsync(int id, int pageIndex = 1, int pageSize = 10)
        {
            var properties = _context.Properties.AsNoTracking()
                // .Include(x => x.Agent)
                .Where(x => x.AgentId == id && x.isDeleted == false && x.IsSold == false).AsQueryable();
            
            return await PaginatedList<Property>.CreateAsync(properties, pageIndex, pageSize);
        }

        public async Task<List<Property>> GetFavoritePropertiesByUserIdAsync(string userId)
        {
            return await _context.Favorites
                .Where(f => f.UserId == userId)
                .Include(f => f.Property)
                .AsNoTrackingWithIdentityResolution()
                .Select(f => f.Property)
                .ToListAsync();
        }

        public async Task<List<Property>> GetFavoritePropertiesByAgentIdAsync(int agentId)
        {
            return await _context.Favorites
                .Where(f => f.Property.AgentId == agentId)
                .Include(f => f.Property)
                .AsNoTrackingWithIdentityResolution()
                .Select(f => f.Property)
                .ToListAsync();
        }

        public async Task SoftDeletePropertyAsync(Property property)
        {
            property.isDeleted = true;
            _context.Entry(property).Property(x => x.isDeleted).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task SellPropertyAsync(Property property)
        {
            property.IsSold = true;
            _context.Entry(property).Property(x => x.IsSold).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task UnDeletePropertyAsync(Property property)
        {
            property.isDeleted = false;
            _context.Entry(property).Property(x => x.isDeleted).IsModified = true;
            await _context.SaveChangesAsync();
        }

        public async Task<List<Property>> GetPropertiesByCategoryAsync(string category)
        {
            if (Enum.TryParse(typeof(Category), category, true, out var parsedCategory))
            {
                var enumCategory = (Category)parsedCategory;
                return await _context.Properties
                         .AsNoTracking()
                         .Where(p => p.Category == enumCategory)
                         .ToListAsync();
            }
            return new List<Property>(); 
        }
     
        public async Task<Dictionary<string, int>> GetSoldPropertiesByMonthAsync()
        {
            return await _context.Properties
                .AsNoTracking()
                .Where(p => p.IsSold)
                .GroupBy(p => p.PostedOn.ToString("yyyy-MM"))
                .Select(g => new { Month = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.Month, g => g.Count);
        }

        public async Task<Dictionary<string, int>> GetSoldPropertiesByDayAsync(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            return await _context.Properties
                .AsNoTracking()
                .Where(p => p.IsSold && p.PostedOn >= startDate && p.PostedOn <= endDate)
                .GroupBy(p => p.PostedOn.Date)
                .Select(g => new { Date = g.Key.ToString("yyyy-MM-dd"), Count = g.Count() })
                .ToDictionaryAsync(g => g.Date, g => g.Count);
        }
        
        public async Task<PaginatedList<Property>> GetAllPropertiesWithoutAgentAsync(int pageIndex = 1, int pageSize = 10)
        {
           var properties = _context.Properties.AsNoTracking()
               .Where(x => x.AgentId == null && !x.isDeleted && !x.IsSold);
           
           return await PaginatedList<Property>.CreateAsync(properties, pageIndex, pageSize);
        }
        
        public async Task<Property?> GetPropertyByIdWithAgentAsync(int id)
        {
            return await _context.Properties
                .AsNoTracking()
                .Include(x => x.Agent)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
