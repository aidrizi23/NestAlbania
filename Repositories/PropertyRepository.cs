﻿using Microsoft.EntityFrameworkCore;
using NestAlbania.Data;
using NestAlbania.FilterHelpers;
using NestAlbania.Repositories.Pagination;
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

        public async Task<PaginatedList<Property>> GetAllPaginatedPropertiesAsync(int pageIndex = 1, int pageSize = 10)
        {
            var source = _context.Properties.OrderByDescending(x => x.Id).AsQueryable();
            return await PaginatedList<Property>.CreateAsync(source, pageIndex, pageSize);
        }

        public async Task<PaginatedList<Property>> GetAllPaginatedPropertiesByPrice(int price, int pageIndex = 1, int pageSize = 10)
        {
            var paginatedProperty = _context.Properties.OrderByDescending(x => x.Id).AsQueryable();
            paginatedProperty = paginatedProperty.Where(x => x.Price == price);
            return await PaginatedList<Property>.CreateAsync(paginatedProperty, pageIndex, pageSize);
        }

        public async Task<PaginatedList<Property>> GetPropertiesByNumberOfBedroomsAsync(int nrOfBedrooms, int pageIndex = 1, int pageSize = 10)
        {
            var paginatedProperties = _context.Properties.OrderByDescending(x => x.Id).AsQueryable();
            paginatedProperties = paginatedProperties.Where(x => x.BedroomCount == nrOfBedrooms);
            return await PaginatedList<Property>.CreateAsync(paginatedProperties, pageIndex, pageSize);
        }

        public async Task<PaginatedList<Property>> GetAllFilteredPropertiesAsync(PropertyObjectQuery query, int pageIndex, int pageSize, string sortOrder)
        {
            var properties = _context.Properties.AsQueryable();

            // Apply filters from query
            if (!string.IsNullOrEmpty(query.Name))
            {
                properties = properties.Where(p => p.Name.Contains(query.Name));
            }
            if (query.FullArea.HasValue)
            {
                properties = properties.Where(p => p.FullArea >= query.FullArea);
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

            // Apply sorting
            switch (sortOrder)
            {
                case "price-asc":
                    properties = properties.OrderBy(p => p.Price);
                    break;
                case "price-desc":
                    properties = properties.OrderByDescending(p => p.Price);
                    break;
                // Add more sorting options if needed
                default:
                    properties = properties.OrderBy(p => p.Id); // Default sorting
                    break;
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
