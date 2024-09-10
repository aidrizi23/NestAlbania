﻿using NestAlbania.Data;
using NestAlbania.FilterHelpers;
using NestAlbania.Repositories;
using NestAlbania.Repositories.Pagination;
using System.Threading.Tasks;

namespace NestAlbania.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly PropertyRepository _repository;

        public PropertyService(PropertyRepository repository)
        {
            _repository = repository;
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
        
        public async Task UnDeletePropertyAsync(Property property)
        {
            await _repository.UnDeletePropertyAsync(property);
        }
        
        
        public async Task<PaginatedList<Property>> GetAllPaginatedPropertiesWithoutAgentAsync(int pageIndex = 1, int pageSize = 10)
        {
            return await _repository.GetAllPropertiesWithoutAgentAsync(pageIndex, pageSize);
        }
        
        
        public async Task<Property?> GetPropertyByIdWithAgentAsync(int id)
        {
            return await _repository.GetPropertyByIdWithAgentAsync(id);
        }
        
    }
}
