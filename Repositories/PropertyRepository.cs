
using Microsoft.AspNetCore.Components.QuickGrid;
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;
using NestAlbania.Data;
using NestAlbania.FilterHelpers;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Repositories
{
    public class PropertyRepository : BaseRepository<Property>
    {
        private readonly ApplicationDbContext _context;
        public PropertyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Property>> GetAllPaginatedPropertiesAsync(int PageIndex = 1, int PageSize = 10)
        {
            var source = _context.Properties.OrderByDescending(x => x.Id).AsQueryable();
            return await PaginatedList<Property>.CreateAsync(source, PageIndex, PageSize);
        }

        public async Task <PaginatedList<Property>> GetAllPaginatedPropertiesByPrice(int price , int PageIndex = 1, int PageSize = 10)
        {
            var paginatedProperty = _context.Properties.OrderByDescending(x =>x.Id).AsQueryable();
            paginatedProperty = paginatedProperty.Where(x => x.Price == price);
            return await PaginatedList<Property>.CreateAsync(paginatedProperty, PageIndex, PageSize);

        }

        public async Task<PaginatedList<Property>> GetPropertiesByNumberOfBedroomsAsync(int nrOfBedrooms, int pageIndex = 1, int pageSize = 10)
        {
            var paginatedProperties = _context.Properties.OrderByDescending(x => x.Id).AsQueryable();
            paginatedProperties = paginatedProperties.Where(x => x.BedroomCount == nrOfBedrooms);
            return await PaginatedList<Property>.CreateAsync(paginatedProperties, pageIndex, pageSize);
        }



        public async Task<PaginatedList<Property>> GetAllFilteredPropertiesAsync(PropertyObjectQuery query, int pageIndex = 1, int pageSize = 10)
        {
            var properties = _context.Properties.AsQueryable(); // AsNoTracking is not necessary here

            // Apply filters
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                properties = properties.Where(x => x.Name.Contains(query.Name));
            }

            if (query.MinPrice.HasValue)
            {
                properties = properties.Where(x => x.Price >= query.MinPrice.Value);
            }

            if (query.MaxPrice.HasValue)
            {
                properties = properties.Where(x => x.Price <= query.MaxPrice.Value);
            }

            if (query.BedroomCount.HasValue)
            {
                properties = properties.Where(x => x.BedroomCount == query.BedroomCount);
            }

            if (query.BathroomCount.HasValue)
            {
                properties = properties.Where(x => x.BathroomCount == query.BathroomCount);
            }

            if (query.FullArea.HasValue)
            {
                properties = properties.Where(x => x.FullArea == query.FullArea);
            }

            if (query.InsideArea.HasValue)
            {
                properties = properties.Where(x => x.InsideArea == query.InsideArea);
            }

            if (!String.IsNullOrWhiteSpace(query.AgentName))
            {
                properties = properties.Where(x => x.Agent.Name.Contains(query.AgentName));
            }
            return await PaginatedList<Property>.CreateAsync(properties, pageIndex, pageSize);
        }

        public async Task<PaginatedList<Property>> GetAllPaginatedPropertiesByAgentIdAsync(int id, int pageIndex = 1, int pageSize = 10)
        
        {
            var properties = _context.Properties.Include(x => x.Agent).Where(x => x.AgentId == id);
            return await PaginatedList<Property>.CreateAsync(properties, pageIndex, pageSize);
        }
       
    }
}


