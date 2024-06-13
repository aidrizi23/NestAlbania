using NestAlbania.Data;
using NestAlbania.Repositories;
using NestAlbania.Repositories.Pagination;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System.Threading.Tasks;

namespace NestAlbania.Services
{
    public class JobApplyService : IJobApplyService
    {
        private readonly JobApplyRepository _jobApplyRepo;

        public async Task<IEnumerable<JobApply>> GetAllJobApplicationsAsync()
        {
            return await _jobApplyRepo.GetAll();
        }

        public async Task<JobApply> GetJobApplicatonByIdAsync(int id)
        {
            try
            {
                return await _jobApplyRepo.GetById(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw; 
            }
        }

        public async Task CreateJobApplicationAsync(JobApply application)
        {
            try
            {
                await _jobApplyRepo.Create(application);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public async Task EditJobApplicationAsync(JobApply application)
        {
            try
            {
                await _jobApplyRepo.Edit(application);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public async Task DeleteJobApplicationAsync(JobApply application)
        {
            try
            {
                await _jobApplyRepo.Delete(application);
            }
            catch 
            {
                throw;            
            }

        }

        public async Task<PaginatedList<JobApply>> GetAllPaginatedJobApplicationsAsync(int pageIndex=1, int pageSize = 10)
        {
            try
            {
                return await _jobApplyRepo.GetPaginatedJobApplications(pageIndex, pageSize);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

    }
}
