using ArazCRM.API.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArazCRM.API.Repositories.Abstract
{
    public interface IExpenseRepository : IGenericRepository<Expense>
    {
        // Belirli bir kategorideki giderleri getiren method
        Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(string category);

        // Belirli bir iş için tüm giderleri getiren method
        Task<IEnumerable<Expense>> GetExpensesByJobIdAsync(int jobId);
    }
}
