using ArazCRM.API.Models.Entities;
using ArazCRM.API.Repositories.Abstract;
using ArazCRM.API.Services.Abstract;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArazCRM.API.Services.Concrete
{
    public class InvoiceService : GenericService<Invoice>, IInvoiceService
    {
        private readonly IGenericRepository<Invoice> _repository;

        public InvoiceService(IGenericRepository<Invoice> repository) : base(repository)
        {
            _repository = repository;
        }

        // Müşteriye göre fatura listeleme
        public async Task<IEnumerable<Invoice>> GetInvoicesByCustomerIdAsync(int customerId)
        {
            var invoices = await _repository.GetAllAsync(); // Veritabanından tüm faturaları getiriyoruz
            return invoices.Where(i => i.CustomerId == customerId); // Filtreleme işlemi
        }

        // İşe göre fatura listeleme
        public async Task<IEnumerable<Invoice>> GetInvoicesByJobIdAsync(int jobId)
        {
            var invoices = await _repository.GetAllAsync(); // Veritabanından tüm faturaları getiriyoruz
            return invoices.Where(invoice => invoice.JobId == jobId); // Filtreleme işlemi
        }
    }
}
