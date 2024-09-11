using ArazCRM.API.Models.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArazCRM.API.Services.Abstract
{
    public interface IJobService : IGenericService<Job>
    {
        Task<IEnumerable<Job>> GetJobsByStatusAsync(JobStatus status);
    }
}
