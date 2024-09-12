using ArazCRM.API.Models.Entities;
using ArazCRM.API.Repositories.Abstract;
using ArazCRM.API.Services.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArazCRM.API.Services.Concrete
{
    public class ExpenseService : GenericService<Expense>, IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository) : base(expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        // Kategoriye göre giderleri getir
        public async Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(string category)
        {
            return await _expenseRepository.GetExpensesByCategoryAsync(category);
        }

        // Job ID'ye göre giderleri getir
        public async Task<IEnumerable<Expense>> GetExpensesByJobIdAsync(int jobId)
        {
            return await _expenseRepository.GetExpensesByJobIdAsync(jobId);
        }
    }
}
