using ArazCRM.API.Models.Entities;
using ArazCRM.API.Repositories.Abstract;
using ArazCRM.API.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArazCRM.API.Services.Concrete
{
    public class OfferService : GenericService<Offer>, IOfferService
    {
        private readonly IOfferRepository _offerRepository;

        public OfferService(IOfferRepository offerRepository) : base(offerRepository)
        {
            _offerRepository = offerRepository;
        }

        // Onaylanmış teklifleri getir
        public async Task<IEnumerable<Offer>> GetApprovedOffersAsync()
        {
            var offers = await _offerRepository.GetAllAsync();
            return offers.Where(o => o.Approved); 
        }

        // Belirli bir iş için en yüksek teklifi getir
        public async Task<Offer> GetHighestOfferByJobIdAsync(int jobId)
        {
            
            var offers = await _offerRepository.GetAllAsync();
            return offers
                .Where(o => o.JobId == jobId)  
                .OrderByDescending(o => o.OfferAmount) 
                .FirstOrDefault(); 
        }

        public async Task<IEnumerable<Offer>> GetOffersByCustomerIdAsync(int customerId)
        {
            var offers = await _offerRepository.GetAllAsync();
            return offers.Where(o => o.CustomerId == customerId);
        }
    }
}
