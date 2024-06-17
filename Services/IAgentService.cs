using System.Collections.Generic;
using System.Threading.Tasks;
using NestAlbania.Data;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Services
{
    public interface IAgentService
    {
        Task<IEnumerable<Agent>> GetAllAgents();
        Task<Agent> GetAgentById(int id);
        Task CreateAgent(Agent agent);
        Task EditAgent(Agent agent);
        Task DeleteAgent(int id);
        Task<PaginatedList<Agent>> GetPaginatedAgent(int pageIndex = 1, int pageSize = 10);
    }
}
