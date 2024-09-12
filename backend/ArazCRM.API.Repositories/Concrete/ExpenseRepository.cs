using ArazCRM.API.Data;
using ArazCRM.API.Models.Entities;
using ArazCRM.API.Repositories.Abstract;
using Microsoft.EntityFrameworkCore; 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArazCRM.API.Repositories.Concrete
{
    public class ExpenseRepository : GenericRepository<Expense>, IExpenseRepository
    {
        private readonly AppDbContext _context;

        public ExpenseRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        // Belirli bir kategoriye göre giderleri getir
        public async Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(string category)
        {
            return await _context.Expenses
                .Where(expense => expense.ExpenseCategory == category)
                .ToListAsync();
        }

        // Belirli bir iş (jobId) için giderleri getir
        public async Task<IEnumerable<Expense>> GetExpensesByJobIdAsync(int jobId)
        {
            return await _context.Expenses
                .Where(expense => expense.JobId == jobId)
                .ToListAsync();
        }
    }
}
