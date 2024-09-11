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
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {

        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        // Müşteri tipine göre filtreleme yapabilecek özel metot tanımladım.
        public async Task<IEnumerable<Customer>> GetCustomersByTypeAsync(string customerType)
        {
            return await _context.Customers
                .Where(c => c.CustomerType == customerType)
                .ToListAsync();
        }
    }
}
