using NestAlbania.Data;
using NestAlbania.FilterHelpers;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Services
{
    public interface IPropertyService
    {
        Task<PaginatedList<Property>> GetAllPaginatedPropertiesAsync(int PageIndex = 1, int pageSize = 10);
        Task<Property> GetPropertyByIdAsync(int id);
        Task CreatePropertyAsync(Property property);
        Task DeletePropertyAsync(Property property);
        Task EditPropertyAsync(Property property);
        Task<PaginatedList<Property>> GetAllPaginatedPropertiesByPrice(int Price, int PageIndex = 1, int pageSize = 10);

        Task<PaginatedList<Property>> GetPropertiesByNumberOfBedroomsAsync(int nrOfBedrooms, int pageIndex = 1, int pageSize = 10);
        Task<PaginatedList<Property>> GetAllFilteredPropertiesAsync(PropertyObjectQuery query, int pageIndex = 1, int pageSize = 10);

    }
}