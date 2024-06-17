﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NestAlbania.Data;
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

        public async Task DeleteAgent(int id)
        {
            var agent = await _agentRepository.GetById(id);
            if (agent != null)
            {
                await _agentRepository.Delete(agent);
            }
        }
        public async Task<PaginatedList<Agent>> GetPaginatedAgent(int pageIndex = 1, int pageSize = 10)
        {
            return await _agentRepository.GetPaginatedAgent(pageIndex, pageSize);
        }
    }
}
