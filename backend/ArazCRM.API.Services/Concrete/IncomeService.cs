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
    public class IncomeService : GenericService<Income>, IIncomeService
    {
        private readonly IIncomeRepository _incomeRepository;
        public IncomeService(IIncomeRepository incomeRepository) : base(incomeRepository)
        {
            _incomeRepository = incomeRepository;
        }
 
        public async Task<IEnumerable<Income>> GetIncomesByCustomerIdAsync(int customerId)
        {
            return await _incomeRepository.GetIncomesByCustomerIdAsync(customerId);
        }

        public async Task<IEnumerable<Income>> GetIncomesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
           return await _incomeRepository.GetIncomesByDateRangeAsync(startDate, endDate);
        }

        public async Task<IEnumerable<Income>> GetMonthlyIncomeReportAsync(int year, int month)
        {
            return await _incomeRepository.GetMonthlyIncomeReportAsync((int)year, (int)month);
        }

        public async Task<IEnumerable<Income>> GetYearlyIncomeReportAsync(int year)
        {
            return await _incomeRepository.GetYearlyIncomeReportAsync(year);
        }
    }
}
