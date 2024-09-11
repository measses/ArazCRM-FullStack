using ArazCRM.API.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArazCRM.API.Services.Abstract
{
    public interface IInvoiceService : IGenericService<Invoice>
    {
        Task<IEnumerable<Invoice>> GetInvoicesByCustomerIdAsync(int customerId);
        Task<IEnumerable<Invoice>> GetInvoicesByJobIdAsync(int jobId);
    }
}
