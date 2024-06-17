using NestAlbania.Data;
using NestAlbania.Repositories;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly PropertyRepository _repository;
        public PropertyService(PropertyRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaginatedList<Property>> GetAllPaginatedPropertiesAsync(int PageIndex = 1, int pageSize = 10)
        {
            return await _repository.GetAllPaginatedPropertiesAsync(PageIndex, pageSize);
        }

        public async Task<Property> GetPropertyByIdAsync(int id)
        {
            return await _repository.GetById(id);
        } 

        public async Task CreatePropertyAsync(Property property)
        {
            await _repository.Create(property);
        }

        public async Task DeletePropertyAsync(Property property)
        {
            await _repository.Delete(property);
        }
        public async Task EditPropertyAsync(Property property)
        {
            await _repository.Edit(property);   
        }

        public async Task<PaginatedList<Property>> GetPropertiesByNumberOfBedroomsAsync(int nrOfBedrooms, int pageIndex = 1, int pageSize = 10)
        {
            return await _repository.GetPropertiesByNumberOfBedroomsAsync(nrOfBedrooms);    
        }


    }
}
