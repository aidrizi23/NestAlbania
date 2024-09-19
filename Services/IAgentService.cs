using System.Collections.Generic;
using System.Threading.Tasks;
using NestAlbania.Data;
using NestAlbania.FilterHelpers;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Services
{
    public interface IAgentService
    {
        Task<IEnumerable<Agent>> GetAllAgents();
        Task<Agent> GetAgentById(int id);
        Task CreateAgent(Agent agent);
        Task EditAgent(Agent agent);
        Task HardDeleteAgent(Agent agent);
        Task<PaginatedList<Agent>> GetPaginatedAgent(int pageIndex = 1, int pageSize = 10);

        Task<Agent> GetAgentByPropertyIdAsync(int id);

        Task<PaginatedList<Agent>> GetFilteredAgents(AgentObjectQuery query, int pageIndex = 1, int pageSize = 10);
        Task<Agent> GetAgentByUserIdAsync(string userId);
        Task<Agent> GetAgentWPropertiesAsync(int id);

        Task SoftDeleteAgentAsync(Agent agent);
        Task UnDeleteAgentAsync(Agent agent);
        Task<Agent> GetTopSellingAgentAsync();
    }
}
