
using Microsoft.Data.SqlClient.DataClassification;
using Microsoft.EntityFrameworkCore;
using NestAlbania.Data;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Repositories
{
    public class PropertyRepository : BaseRepository<Property>
    {
        private readonly ApplicationDbContext _context;
        public PropertyRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Property>> GetAllPaginatedPropertiesAsync(int PageIndex = 1, int PageSize = 10)
        {
            var source = _context.Properties.OrderByDescending(x => x.Id).AsQueryable();
            return await PaginatedList<Property>.CreateAsync(source, PageIndex, PageSize);
        }
    }
}
