using ArazCRM.API.Models.Entities;
using ArazCRM.API.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ArazCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : Controller
    {
        private readonly IJobService _jobService;
        public JobController(IJobService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobs()
        {
            var jobs = await _jobService.GetAllAsync();
            return Ok(jobs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Job>> GetJobById(int id)
        {
            var jobs = await _jobService.GetByIdAsync(id);
            if (jobs == null)
            {
                return NotFound();
            }
            return Ok(jobs);
        }

        [HttpPost]
        public async Task<ActionResult> AddJob([FromBody] Job job)
        {
            if (!ModelState.IsValid)
            {
                // Hata durumunda 400 BadRequest ve mesaj dönüyoruz
                return BadRequest(new { message = "Invalid data provided", errors = ModelState });
            }

            await _jobService.AddAsync(job);

            // Başarı durumunda 201 Created ve mesaj dönüyoruz
            return CreatedAtAction(nameof(GetJobById), new { id = job.JobId }, new { message = "Job added successfully", job });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(int id, [FromBody] Job job)
        {
            // Önce veritabanından mevcut kaydı alıyoruz
            var existingJob = await _jobService.GetByIdAsync(id);
            if (existingJob == null)
            {
                return NotFound(new { message = "Job not found" });
            }

            // Mevcut kaydın alanlarını güncellemek
            existingJob.JobType = job.JobType;
            existingJob.Description = job.Description;
            existingJob.StartDate = job.StartDate;
            existingJob.EndDate = job.EndDate;
            existingJob.Status = job.Status;
            existingJob.AssignedTo = job.AssignedTo;
            existingJob.Priority = job.Priority;
            existingJob.EstimatedCost = job.EstimatedCost;
            existingJob.ActualCost = job.ActualCost;
            existingJob.DateUpdated = DateTime.UtcNow; // Güncelleme zamanını güncelle

            // Değişiklikleri kaydet
            await _jobService.UpdateAsync(id, existingJob);

            return Ok(new { message = "Job updated successfully" });
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            try
            {
                var job = await _jobService.GetByIdAsync(id);
                if (job == null)
                {
                    return NotFound(new { message = "Job not found" });
                }

                await _jobService.DeleteAsync(id);
                // SaveChangesAsync çağrısına gerek yok, çünkü DeleteAsync metodu içinde yapılıyor

                return Ok(new { message = "Job deleted successfully" });
            }
            catch (Exception ex)
            {
                // Hata loglanabilir
                return StatusCode(500, new { message = "An error occurred while deleting the job", error = ex.Message });
            }
        }



        [HttpGet("status/{status}")]
        public async Task<IActionResult> GetJobsByStatus([FromRoute] JobStatus status)
        {
            var jobs = await _jobService.GetJobsByStatusAsync(status); 
            return Ok(jobs);
        }


    }
}
