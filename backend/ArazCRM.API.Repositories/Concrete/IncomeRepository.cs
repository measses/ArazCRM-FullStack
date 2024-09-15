using ArazCRM.API.Data;
using ArazCRM.API.Models.Entities;
using ArazCRM.API.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArazCRM.API.Repositories.Concrete
{
    public class IncomeRepository : GenericRepository<Income>, IIncomeRepository
    {
        private readonly AppDbContext _context;

        public IncomeRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Income>> GetIncomesByCustomerIdAsync(int customerId)
        {
            return await _context.Incomes
                .Where(i => i.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Income>> GetIncomesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Incomes
                .Where(i => i.IncomeDate >= startDate && i.IncomeDate <= endDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Income>> GetMonthlyIncomeReportAsync(int year, int month)
        {
            return await _context.Incomes
                .Where(i => i.IncomeDate.Year == year && i.IncomeDate.Month == month)
                .ToListAsync();
        }


        public async Task<IEnumerable<Income>> GetYearlyIncomeReportAsync(int year)
        {
            return await _context.Incomes
                .Where(i => i.IncomeDate.Year == year)
                .ToListAsync();
        }

    }
}
