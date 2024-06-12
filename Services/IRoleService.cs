﻿using NestAlbania.Data;

namespace NestAlbania.Services
{
    public interface IRoleService
    {
        Task<ApplicationRole> GetRoleByUserIdAsync(string UserId);
        Task<IEnumerable<ApplicationRole>> GetAllAsync();
        Task CreateAsync(ApplicationRole entity);
        Task UpdateAsync(ApplicationRole entity);
        Task DeleteAsync(string id);


    }
}