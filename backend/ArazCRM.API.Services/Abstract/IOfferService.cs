using ArazCRM.API.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArazCRM.API.Services.Abstract
{
    public interface IOfferService : IGenericService<Offer>
    {
        Task<IEnumerable<Offer>> GetApprovedOffersAsync();
        Task<IEnumerable<Offer>> GetOffersByCustomerIdAsync(int customerId);
        Task<Offer> GetHighestOfferByJobIdAsync(int jobId);
    }
}
