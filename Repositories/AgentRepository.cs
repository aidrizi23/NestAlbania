using Microsoft.EntityFrameworkCore;
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

        public async Task<Agent> GetAgentByPropertyIdAsync(int id)
        {
            return await _context.Agents.Include(p => p.Properties).FirstOrDefaultAsync(x => x.Properties == x.Properties.FirstOrDefault(x => x.AgentId == id));
        }

    }
}
