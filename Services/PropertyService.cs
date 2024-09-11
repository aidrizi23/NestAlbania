using Microsoft.EntityFrameworkCore;
using NestAlbania.Data;
using NestAlbania.FilterHelpers;
using NestAlbania.Repositories;
using NestAlbania.Repositories.Pagination;
using NestAlbania.Models; // Ensure you have the correct Models namespace
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NestAlbania.Data.Enums;

namespace NestAlbania.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly PropertyRepository _repository;

        public PropertyService(PropertyRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<Property>> GetPropertiesWithChangedPricesAsync()
        {
            return await _repository.GetPropertiesWithChangedPricesAsync();
        }


        // Correct version of GetAllPropertiesAsync
        public async Task<IEnumerable<Property>> GetAllPropertiesAsync()
        {
            // Get all properties from the database using the repository
            return await _repository.GetAllPropertiesAsync();
        }



        public async Task<PaginatedList<Property>> GetAllPaginatedPropertiesAsync(int pageIndex = 1, int pageSize = 10)
        {
            return await _repository.GetAllPaginatedPropertiesAsync(pageIndex, pageSize);
        }

        public async Task<Property> GetPropertyByIdAsync(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task CreatePropertyAsync(Property property)
        {
            await _repository.Create(property);
        }

        public async Task HardDeletePropertyAsync(Property property)
        {
            await _repository.Delete(property);
        }

        public async Task EditPropertyAsync(Property property)
        {
            await _repository.Edit(property);
        }

        public async Task<PaginatedList<Property>> GetPropertiesByNumberOfBedroomsAsync(int nrOfBedrooms, int pageIndex = 1, int pageSize = 10)
        {
            return await _repository.GetPropertiesByNumberOfBedroomsAsync(nrOfBedrooms, pageIndex, pageSize);
        }

        public async Task<PaginatedList<Property>> GetAllPaginatedPropertiesByPrice(int price, int pageIndex = 1, int pageSize = 10)
        {
            return await _repository.GetAllPaginatedPropertiesByPrice(price, pageIndex, pageSize);
        }

        public async Task<PaginatedList<Property>> GetAllFilteredPropertiesAsync(PropertyObjectQuery query, int pageIndex, int pageSize, string sortOrder)
        {
            return await _repository.GetAllFilteredPropertiesAsync(query, pageIndex, pageSize, sortOrder);
        }

        public async Task<PaginatedList<Property>> GetAllPaginatedPropertiesByAgentIdAsync(int id, int pageIndex = 1, int pageSize = 10)
        {
            return await _repository.GetAllPaginatedPropertiesByAgentIdAsync(id, pageIndex, pageSize);
        }

        public async Task<List<Property>> GetFavoritePropertiesByUserIdAsync(string userId)
        {
            return await _repository.GetFavoritePropertiesByUserIdAsync(userId);
        }

        public async Task<List<Property>> GetFavoritePropertiesByAgentIdAsync(int agentId)
        {
            return await _repository.GetFavoritePropertiesByAgentIdAsync(agentId);
        }

        public async Task SoftDeletePropertyAsync(Property property)
        {
            await _repository.SoftDeletePropertyAsync(property);
        }

        public async Task SellPropertyAsync(Property property)
        {
            await _repository.SellPropertyAsync(property);
        }
        public async Task<List<Property>> GetPropertiesByCategoryAsync(string category)
        {
            return await _repository.GetPropertiesByCategoryAsync(category);
        }
        public async Task<Dictionary<string, int>> GetSoldPropertiesByMonthAsync()
        {
            return await _repository.GetSoldPropertiesByMonthAsync(); // Ensure the return type matches
        }
        public async Task<Dictionary<string, int>> GetSoldPropertiesByDayAsync(int year, int month)
        {
            return await _repository.GetSoldPropertiesByDayAsync(year, month);
        }
        public async Task UnDeletePropertyAsync(Property property)
        {
            await _repository.UnDeletePropertyAsync(property);
        }
    }
}
