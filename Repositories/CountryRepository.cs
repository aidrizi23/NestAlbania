using NestAlbania.Data;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Repositories
{
    public class CountryRepository : BaseRepository<Country>
    {
        private readonly ApplicationDbContext _context;
        public CountryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<PaginatedList<Country>> GetPaginatedCountries(int pageIndex = 1, int pageSize = 10)
        {
            var countries = _context.Countries.OrderByDescending(x => x.Id).AsQueryable();
            return await PaginatedList<Country>.CreateAsync(countries, pageIndex, pageSize);
        }
    }
}
