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
    public class CustomerService : GenericService<Customer>, ICustomerService
    {
        private readonly IGenericRepository<Customer> _repository;
        public CustomerService(IGenericRepository<Customer> repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Customer>> GetCustomersByType(string customerType)
        {
            var customers = await _repository.GetAllAsync();
            return customers.Where(c => c.CustomerType == customerType);
        }

        public async Task<IEnumerable<Customer>> GetCustomersByTypeAsync(string customerType)
        {
            var customers = await _repository.GetAllAsync();
            return customers.Where(c => c.CustomerType == customerType);
        }
    }
}
