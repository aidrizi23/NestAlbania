using NestAlbania.Data;
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

    }
}