using ArazCRM.API.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArazCRM.API.Services.Abstract
{
    public interface IExpenseService : IGenericService<Expense>
    {
        // Belirli bir kategorideki giderleri getiren method
        Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(string category);

        // Belirli bir iş için tüm giderleri getiren method
        Task<IEnumerable<Expense>> GetExpensesByJobIdAsync(int jobId);
    }
}
