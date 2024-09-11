
using System.Collections.Generic;
using System.Threading.Tasks;
using NestAlbania.Data;
using NestAlbania.FilterHelpers;
using NestAlbania.Repositories;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Services
{
    public class AgentService : IAgentService
    {
        private readonly AgentRepository _agentRepository;

        public AgentService(AgentRepository agentRepository)
        {
            _agentRepository = agentRepository;
        }

        public async Task<IEnumerable<Agent>> GetAllAgents()
        {
            return await _agentRepository.GetAll();
        }

        public async Task<Agent> GetAgentById(int id)
        {
            return await _agentRepository.GetById(id);
        }

        public async Task CreateAgent(Agent agent)
        {
            await _agentRepository.Create(agent);
        }

        public async Task EditAgent(Agent agent)
        {
            await _agentRepository.Edit(agent);
        }

        public async Task HardDeleteAgent(Agent agent)
        {
            await _agentRepository.Delete(agent);
        }

        public async Task<PaginatedList<Agent>> GetPaginatedAgentsAsync(int pageIndex = 1, int pageSize = 10)
        {
            return await _agentRepository.GetPaginatedAgent(pageIndex, pageSize);
        }
        

        public async Task<PaginatedList<Agent>> GetFilteredAgents(AgentObjectQuery query, int pageIndex = 1, int pageSize = 10)
        {
            return await _agentRepository.GetFilteredAgents(query, pageIndex, pageSize);
        }

        public async Task<Agent?> GetAgentByUserIdAsync(string userId)
        {
            return await _agentRepository.GetAgentByUserIdAsync(userId);
        }

        public async Task<Agent?> GetAgentWithPropertiesAsync(int id)
        {
            return await _agentRepository.GetAgentByIdAsyncWithProperties(id);
        }

        public async Task SoftDeleteAgentAsync(Agent agent)
        {
            await _agentRepository.SoftDeleteAgentAsync(agent);
        }

        public async Task UnDeleteAgentAsync(Agent agent)
        {
            await _agentRepository.UnDeleteAgentAsync(agent);
        }
        public async Task<Agent> GetTopSellingAgentAsync()
        {
            return await _agentRepository.GetTopSellingAgentAsync();
        }

    }
}

