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
    public class OfferRepository : GenericRepository<Offer>, IOfferRepository
    {
        private readonly AppDbContext _context;
        public OfferRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }
        //Onaylanmış Teklifleri Getir
        public async Task<IEnumerable<Offer>> GetApprovedOffersAsync()
        {
            return await _context.Offers
                                 .Where(o => o.Approved) 
                                 .ToListAsync(); 
        }

        //Belirli Bir Tarih Aralığındaki Teklifleri Getir
        public async Task<Offer> GetHighestOfferByJobIdAsync(int jobId)
        {
            return await _context.Offers
                                 .Where(o => o.JobId == jobId) 
                                 .OrderByDescending(o => o.OfferAmount) 
                                 .FirstOrDefaultAsync(); 
        }

        //Müşteriye Göre Teklifleri Getir
        public async Task<IEnumerable<Offer>> GetOffersByCustomerIdAsync(int customerId)
        {

            return await _context.Offers
                                 .Where(o => o.CustomerId == customerId) 
                                 .ToListAsync(); 
        }



    }
}
