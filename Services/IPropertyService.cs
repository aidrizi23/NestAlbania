using NestAlbania.Data;
using NestAlbania.FilterHelpers;
using NestAlbania.Repositories.Pagination;

public interface IPropertyService
{
    Task<PaginatedList<Property>> GetAllPaginatedPropertiesAsync(int pageIndex = 1, int pageSize = 10);
    Task<Property> GetPropertyByIdAsync(int id);
    Task CreatePropertyAsync(Property property);
    Task HardDeletePropertyAsync(Property property);
    Task EditPropertyAsync(Property property);

    Task<PaginatedList<Property>> GetAllFilteredPropertiesAsync(PropertyObjectQuery query, int pageIndex = 1, int pageSize = 10, string sortOrder = "default");

    Task<PaginatedList<Property>> GetAllPaginatedPropertiesByAgentIdAsync(int id, int pageIndex = 1, int pageSize = 10);
    
    Task SoftDeletePropertyAsync(Property property);
    Task SellPropertyAsync(Property property);
    Task UnDeletePropertyAsync(Property property);
    Task<PaginatedList<Property>> GetAllPaginatedPropertiesWithoutAgentAsync(int pageIndex = 1, int pageSize = 10);
    Task<Property?> GetPropertyByIdWithAgentAsync(int id);
    Task<List<Property>> GetFavoritePropertiesByUserIdAsync(string userId);
    Task<List<Property>> GetFavoritePropertiesByAgentIdAsync(int agentId);


}
