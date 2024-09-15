using ArazCRM.API.Models.Entities;
using ArazCRM.API.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ArazCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointments()
        {
            var appointments = await _appointmentService.GetAllAsync();
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetAppointmentById(int id)
        {
            var appointments = await _appointmentService.GetByIdAsync(id);
            if (appointments == null)
            {
                return NotFound();

            }
            return Ok(appointments);
        }

        [HttpPost]
        public async Task<ActionResult> AddAppointment([FromBody] Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data provided", errors = ModelState });
            }
            await _appointmentService.AddAsync(appointment);

            return CreatedAtAction(nameof(GetAppointmentById), new { id = appointment.AppointmentId }, new { message = "Appointment added successfully" });

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAppointment(int id, [FromBody] Appointment appointment)
        {
            // Önce veritabanından mevcut randevuyu buluyoruz
            var existingAppointment = await _appointmentService.GetByIdAsync(id);
            if (existingAppointment == null)
            {
                return NotFound(new { message = "Appointment not found" });
            }

            // Mevcut kaydın alanlarını güncelle
            existingAppointment.AppointmentDate = appointment.AppointmentDate;
            existingAppointment.Location = appointment.Location;
            existingAppointment.Purpose = appointment.Purpose;
            existingAppointment.Status = appointment.Status;
            existingAppointment.Notes = appointment.Notes;

            // Müşteri ve iş kimlikleri güncelleniyorsa, onları da güncelle
            existingAppointment.CustomerId = appointment.CustomerId;
            existingAppointment.JobId = appointment.JobId;

            // Son güncelleme zamanını güncelle
            existingAppointment.LastModified = DateTime.UtcNow;

            // Değişiklikleri kaydet
            await _appointmentService.UpdateAsync(id, existingAppointment);

            return Ok(new { message = "Appointment updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(int id)
        {
            try
            {
                var appointment = await _appointmentService.GetByIdAsync(id);
                if (appointment == null)
                {
                    return NotFound(new { message = "Appointment not found" });

                }
                await _appointmentService.DeleteAsync(id);
                return Ok(new { message = "Appointment deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the offer", error = ex.Message });
            }
        }
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentByCustomer(int customerId)
        {
            var appointments = await _appointmentService.GetAppointmentsByCustomerIdAsync(customerId);
            if (!appointments.Any())
            {
                return NotFound(new { message = "No appointments found for this customer" });
            }
            return Ok(appointments);
        }

        [HttpGet("date")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            var appointments = await _appointmentService.GetAppointmentsByDateRangeAsync(startDate, endDate);

            if (!appointments.Any())
            {
                return NotFound(new { message = "No appointments found in the given date range" });
            }

            return Ok(appointments);
        }

        [HttpGet("job/{jobId}")]

        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentByJob(int jobId)
        {
            var appointments = await _appointmentService.GetAppointmentsByJobIdAsync(jobId);
            if (!appointments.Any())
            {
                return NotFound(new { message = "No appointments found for this job" });
            }
            return Ok(appointments);
        }

        [HttpGet("location/")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentsByLocation([FromQuery] string location)
        {
            var appointments = await _appointmentService.GetAppointmentsByLocationAsync(location);
            if (!appointments.Any())
            {
                return NotFound(new { message = "No appointments found for this location" });
            }
            return Ok(appointments);
        }


    }
}