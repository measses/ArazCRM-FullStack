using ArazCRM.API.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArazCRM.API.Repositories.Abstract
{
    public interface IJobRepository : IGenericRepository<Job>
    {
        Task<IEnumerable<Job>> GetJobsByStatusAsync(JobStatus status);
    }
}
