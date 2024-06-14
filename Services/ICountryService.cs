using NestAlbania.Data;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Services
{
    public interface ICountryService
    {
        Task CreateCountry(Country country);
        Task EditCountry(Country country);
        Task DeleteCountry(Country country);
        Task<Country> GetCountryById(int id);
        Task<PaginatedList<Country>> GetPaginatedCountries(int pageIndex = 1, int pageSize = 10);
        Task<IEnumerable<Country>> GetAllCountries();
    }
}
