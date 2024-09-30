using NestAlbania.Data;
using NestAlbania.FilterHelpers;
using NestAlbania.Repositories.Pagination;
using System.Collections.Generic;
using System.Threading.Tasks;
using NestAlbania.Models; // Ensure correct namespace for Property model

public interface IPropertyService
{
    Task<PaginatedList<Property>> GetAllPaginatedPropertiesAsync(int pageIndex = 1, int pageSize = 10);
    Task<Property> GetPropertyByIdAsync(int id);
    Task CreatePropertyAsync(Property property);
    Task HardDeletePropertyAsync(Property property);
    Task EditPropertyAsync(Property property);
    Task<IEnumerable<Property>> GetAllPropertiesAsync(); // Kept this one with IEnumerable as per service implementation
    //Task<PaginatedList<Property>> GetAllPaginatedPropertiesByPrice(int price, int pageIndex = 1, int pageSize = 10);
    //Task<PaginatedList<Property>> GetPropertiesByNumberOfBedroomsAsync(int nrOfBedrooms, int pageIndex = 1, int pageSize = 10);
    Task<PaginatedList<Property>> GetAllFilteredPropertiesAsync(PropertyObjectQuery query, int pageIndex = 1, int pageSize = 10, string sortOrder = "default");

    Task<PaginatedList<Property>> GetAllPaginatedPropertiesByAgentIdAsync(int id, int pageIndex = 1, int pageSize = 10);
    Task SoftDeletePropertyAsync(Property property);
    Task SellPropertyAsync(Property property);
    Task UnDeletePropertyAsync(Property property);

    Task<List<Property>> GetPropertiesByCategoryAsync(string category);
    Task<Dictionary<string, int>> GetSoldPropertiesByMonthAsync();
    Task<Dictionary<string, int>> GetSoldPropertiesByDayAsync(int year, int month);
    Task<PaginatedList<Property>> GetAllPaginatedPropertiesWithoutAgentAsync(int pageIndex = 1, int pageSize = 10);
    Task<Property?> GetPropertyByIdWithAgentAsync(int id);


}
