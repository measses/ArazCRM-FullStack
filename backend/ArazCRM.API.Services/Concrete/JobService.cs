using ArazCRM.API.Models.Entities;
using ArazCRM.API.Repositories.Abstract;
using ArazCRM.API.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArazCRM.API.Services.Concrete
{
    public class JobService : GenericService<Job>, IJobService
    {
        private readonly IGenericRepository<Job> _repository;
        public JobService(IGenericRepository<Job> repository) : base(repository)
        {
            _repository = repository;

        }

        public async Task<IEnumerable<Job>> GetJobsByStatusAsync(JobStatus status) 
        {
            var jobs = await _repository.GetAllAsync();
            return jobs.Where(j => j.Status == status);
        }

    }
}
