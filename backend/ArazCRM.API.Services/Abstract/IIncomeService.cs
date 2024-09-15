using ArazCRM.API.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArazCRM.API.Services.Abstract
{
    public interface IIncomeService : IGenericService<Income>
    {
        Task<IEnumerable<Income>> GetIncomesByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<Income>> GetIncomesByCustomerIdAsync(int customerId);
        Task<IEnumerable<Income>> GetMonthlyIncomeReportAsync(int year, int month);
        Task<IEnumerable<Income>> GetYearlyIncomeReportAsync(int year);
    }
}
