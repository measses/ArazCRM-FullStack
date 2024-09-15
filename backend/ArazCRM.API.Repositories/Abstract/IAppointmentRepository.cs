﻿using ArazCRM.API.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArazCRM.API.Repositories.Abstract
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<IEnumerable<Appointment>> GetAppointmentsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Appointment>> GetAppointmentsByCustomerIdAsync(int customerId);
        Task<IEnumerable<Appointment>> GetAppointmentsByLocationAsync(string location);
        Task<IEnumerable<Appointment>> GetAppointmentsByJobIdAsync(int jobId);

    }
}