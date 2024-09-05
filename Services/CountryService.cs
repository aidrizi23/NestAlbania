using NestAlbania.Data;
using NestAlbania.Repositories;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Services
{
    public class CountryService : ICountryService
    {
        private readonly CountryRepository _countryRepository;
        public CountryService(CountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        public async Task CreateCountry(Country country)
        {
            await _countryRepository.Create(country);
        }

        public async Task EditCountry(Country country)
        {
            await _countryRepository.Edit(country);
        }
        public async Task DeleteCountry(Country country)
        {
            await _countryRepository.Delete(country);
        }
        public async Task<Country> GetCountryById(int id)
        {
            return await _countryRepository.GetById(id);
        }
        public async Task<PaginatedList<Country>> GetPaginatedCountries(int pageIndex = 1, int pageSize = 10)
        {
            return await _countryRepository.GetPaginatedCountries(pageIndex, pageSize);
        }
        public async Task<IEnumerable<Country>> GetAllCountries()
        {
            return await _countryRepository.GetAll();
        }
    }
}
