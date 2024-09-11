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
            return await _context.Properties.ToListAsync();
        }

        public async Task<PaginatedList<Property>> GetAllPaginatedPropertiesAsync(int pageIndex = 1, int pageSize = 10)
        {
            var source = _context.Properties.Where(x => x.isDeleted == false).OrderByDescending(x => x.Id).AsQueryable();
            return await PaginatedList<Property>.CreateAsync(source, pageIndex, pageSize);
        }

        public async Task<PaginatedList<Property>> GetAllPaginatedPropertiesByPrice(int price, int pageIndex = 1, int pageSize = 10)
        {
            var paginatedProperty = _context.Properties.OrderByDescending(x => x.Id).AsQueryable();
            paginatedProperty = paginatedProperty.Where(x => x.Price == price && x.isDeleted == false);
            return await PaginatedList<Property>.CreateAsync(paginatedProperty, pageIndex, pageSize);
        }

        public async Task<PaginatedList<Property>> GetPropertiesByNumberOfBedroomsAsync(int nrOfBedrooms, int pageIndex = 1, int pageSize = 10)
        {
            var paginatedProperties = _context.Properties.OrderByDescending(x => x.Id).AsQueryable();
            paginatedProperties = paginatedProperties.Where(x => x.BedroomCount == nrOfBedrooms && x.isDeleted == false);
            return await PaginatedList<Property>.CreateAsync(paginatedProperties, pageIndex, pageSize);
        }

        public async Task<PaginatedList<Property>> GetAllFilteredPropertiesAsync(PropertyObjectQuery query, int pageIndex, int pageSize, string sortOrder)
        {
            var properties = _context.Properties.Where(x => x.isDeleted == false && x.isSold == false).AsQueryable();

            if (!string.IsNullOrEmpty(query.Name))
            {
                properties = properties.Where(p => p.Name.Contains(query.Name));
            }
            if (query.FullArea.HasValue)
            {
                properties = properties.Where(p => p.FullArea >= query.FullArea * 0.9 && p.FullArea <= query.FullArea * 1.1);
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
            var properties = _context.Properties.Include(x => x.Agent).Where(x => x.AgentId == id && x.isDeleted == false && x.isSold == false);
            return await PaginatedList<Property>.CreateAsync(properties, pageIndex, pageSize);
        }

        public async Task<List<Property>> GetFavoritePropertiesByUserIdAsync(string userId)
        {
            return await _context.Properties
                .Where(p => p.IsFavorite && p.Agent.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Property>> GetFavoritePropertiesByAgentIdAsync(int agentId)
        {
            return await _context.Properties
                .Where(p => p.IsFavorite)
                .ToListAsync();
        }

        public async Task SoftDeletePropertyAsync(Property property)
        {
            property.isDeleted = true;
            await _context.SaveChangesAsync();
        }

        public async Task SellPropertyAsync(Property property)
        {
            property.isSold = true;
            await _context.SaveChangesAsync();
        }

        public async Task UnDeletePropertyAsync(Property property)
        {
            property.isDeleted = false;
            await _context.SaveChangesAsync();
        }
        public async Task<List<Property>> GetPropertiesByCategoryAsync(string category)
        {
            if (Enum.TryParse(typeof(Category), category, true, out var parsedCategory))
            {
                var enumCategory = (Category)parsedCategory;
                return await _context.Properties
                         .Where(p => p.Category == enumCategory)
                         .ToListAsync();
            }
            return new List<Property>(); 
        }
        public async Task<PaginatedList<Property>> GetPropertiesWithChangedPricesAsync(int pageIndex = 1, int pageSize = 10)
        {
            var query = _context.Properties
                .Where(p => p.PreviousPrice != p.Price)
                .OrderByDescending(p => p.PriceChangedDate)
                .AsQueryable();

            return await PaginatedList<Property>.CreateAsync(query, pageIndex, pageSize);
        }
        public async Task<Dictionary<string, int>> GetSoldPropertiesByMonthAsync()
        {
            var soldProperties = _context.Properties
                .Where(p => p.isSold == true) // Using isSold instead of SoldDate
                .ToList();

            var monthlySoldProperties = soldProperties
                .GroupBy(p => p.PostedOn.ToString("yyyy-MM")) // Assuming PostedOn can be used for grouping
                .Select(g => new
                {
                    Month = g.Key,
                    Count = g.Count()
                })
                .ToDictionary(g => g.Month, g => g.Count);

            return monthlySoldProperties;
        }
        public async Task<Dictionary<string, int>> GetSoldPropertiesByDayAsync(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var dailySoldProperties = await _context.Properties
                .Where(p => p.isSold == true && p.PostedOn >= startDate && p.PostedOn <= endDate)
                .GroupBy(p => p.PostedOn.Date)
                .Select(g => new
                {
                    Date = g.Key.ToString("yyyy-MM-dd"),
                    Count = g.Count()
                })
                .ToDictionaryAsync(g => g.Date, g => g.Count);

            return dailySoldProperties;
        }
    }
}
