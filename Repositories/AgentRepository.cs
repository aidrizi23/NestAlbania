using NestAlbania.Data;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Repositories
{
    public class AgentRepository : BaseRepository<Agent>
    {
        private readonly ApplicationDbContext _context;

    public AgentRepository(ApplicationDbContext context):base(context) {
            _context = context;
    }
        public async Task<PaginatedList<Agent>> GetPaginatedAgent(int pageIndex = 1, int pageSize = 10)
        {
            var agent = _context.Agents.AsQueryable();
            return await PaginatedList<Agent>.CreateAsync(agent, pageIndex, pageSize);
        }

    }
}
