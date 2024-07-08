﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NestAlbania.Data;
using NestAlbania.FilterHelpers;
using NestAlbania.Repositories.Pagination;

namespace NestAlbania.Repositories
{
    public class AgentRepository : BaseRepository<Agent>
    {
        private readonly ApplicationDbContext _context;

        public AgentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<PaginatedList<Agent>> GetPaginatedAgent(int pageIndex = 1, int pageSize = 10)
        {
            var agentsQuery = _context.Agents
                .Include(a => a.Properties) // Include related properties if needed
                .OrderByDescending(x => x.Id)
                .AsQueryable();

            return await PaginatedList<Agent>.CreateAsync(agentsQuery, pageIndex, pageSize);
        }

        public async Task<Agent> GetAgentByPropertyIdAsync(int id)
        {
            return await _context.Agents
                .Include(a => a.Properties)
                .FirstOrDefaultAsync(x => x.Properties.Any(p => p.Id == id));
        }

        public async Task<PaginatedList<Agent>> GetFilteredAgents(AgentObjectQuery query, int pageIndex = 1, int pageSize = 10)
        {
            var agentsQuery = _context.Agents.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                agentsQuery = agentsQuery.Where(x => x.Name.ToLower().Contains(query.Name.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(query.Surname))
            {
                agentsQuery = agentsQuery.Where(x => x.Surname == query.Surname);
            }
            if (!string.IsNullOrWhiteSpace(query.YearsOfExeperience))
            {
                agentsQuery = agentsQuery.Where(x => x.YearsOfExeperience == query.YearsOfExeperience);
            }
            if (!string.IsNullOrWhiteSpace(query.Email))
            {
                agentsQuery = agentsQuery.Where(x => x.Email == query.Email);
            }

            return await PaginatedList<Agent>.CreateAsync(agentsQuery, pageIndex, pageSize);
        }

        public async Task<Agent> GetAgentByUserIdAsync(string userId)
        {
            return await _context.Agents
                .Include(a => a.Properties)
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<Agent> GetAgentByIdAsyncWProperties(int id)
        {
            return await _context.Agents
                .Include(a => a.Properties)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
