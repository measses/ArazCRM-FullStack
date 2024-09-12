using ArazCRM.API.Models.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArazCRM.API.Repositories.Abstract
{
    public interface IOfferRepository : IGenericRepository<Offer>
    {
        Task<IEnumerable<Offer>> GetApprovedOffersAsync();
        Task<IEnumerable<Offer>> GetOffersByCustomerIdAsync(int customerId);
        Task<Offer> GetHighestOfferByJobIdAsync(int jobId);
    }
}
