using ArazCRM.API.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArazCRM.API.Services.Abstract
{
    public interface ICustomerService : IGenericService<Customer>
    {
        Task<IEnumerable<Customer>> GetCustomersByTypeAsync(string customerType);
     
     
    }
}
