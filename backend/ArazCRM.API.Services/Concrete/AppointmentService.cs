using ArazCRM.API.Models.Entities;
using ArazCRM.API.Repositories.Abstract;
using ArazCRM.API.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArazCRM.API.Services.Concrete
{
    public class AppointmentService : GenericService<Appointment>, IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository) : base(appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByCustomerIdAsync(int customerId)
        {
            return await _appointmentRepository.GetAppointmentsByCustomerIdAsync(customerId);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _appointmentRepository.GetAppointmentsByDateRangeAsync(startDate, endDate);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByJobIdAsync(int jobId)
        {
            return await _appointmentRepository.GetAppointmentsByJobIdAsync(jobId);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByLocationAsync(string location)
        {
            return await _appointmentRepository.GetAppointmentsByLocationAsync(location);
        }
    }
}
