using Microsoft.EntityFrameworkCore;
using NestAlbania.Data;
using NestAlbania.FilterHelpers;
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
        public async Task<Agent> GetPropertiesByAgentId(int propertyId)
        {
           return await _context.Agents.Include(a => a.Properties).FirstOrDefaultAsync(a => a.Properties.Any(p => p.Id == propertyId));

        }


        public async Task<PaginatedList<Agent>> GetFilteredAgents(AgentObjectQuery query,int pageIndex = 1, int pageSize = 10)
        {
            var agents = _context.Agents.AsQueryable();

            if (!String.IsNullOrWhiteSpace(query.Name))
            {
                agents = agents.Where(x => x.Name.ToLower().Contains(query.Name.ToLower()));
            }
            if (!String.IsNullOrWhiteSpace(query.Surname))
            {
                agents = agents.Where(x => x.Surname == query.Surname);
            }
            if (!String.IsNullOrWhiteSpace(query.YearsOfExeperience))
            {
                agents = agents.Where(x => x.YearsOfExeperience == query.YearsOfExeperience);
            }
            if (!String.IsNullOrWhiteSpace(query.Email))
            {
                agents = agents.Where(x => x.Email == query.Email);
            }

            return await PaginatedList<Agent>.CreateAsync(agents, pageIndex, pageSize);
        }

    }
}
