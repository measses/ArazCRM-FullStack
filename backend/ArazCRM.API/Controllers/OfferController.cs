using ArazCRM.API.Models.Entities;
using ArazCRM.API.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ArazCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OfferController : Controller
    {
        private readonly IOfferService _offerService;
        public OfferController(IOfferService offerService)
        {
            _offerService = offerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Offer>>> GetOffers()
        {
            var offers = await _offerService.GetAllAsync();
            return Ok(offers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Offer>> GetOfferById(int id)
        {
            var offers = await _offerService.GetByIdAsync(id);
            if (offers == null)
            {
                return NotFound();
            }
            return Ok(offers);
        }

        [HttpPost]
        public async Task<ActionResult> AddOffer([FromBody] Offer offer)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data provided", errors = ModelState });
            }
            await _offerService.AddAsync(offer);

            return CreatedAtAction(nameof(GetOfferById), new { id = offer.OfferId}, new {message = "Offer added successfully", offer});
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOffer(int id, [FromBody] Offer offer)
        {
            // Önce veritabanından mevcut teklifi buluyoruz
            var existingOffer = await _offerService.GetByIdAsync(id);
            if (existingOffer == null)
            {
                return NotFound(new { message = "Offer not found" });
            }

            // Mevcut kaydın alanlarını güncelle
            existingOffer.OfferDate = offer.OfferDate;
            existingOffer.OfferAmount = offer.OfferAmount;
            existingOffer.Notes = offer.Notes;

            if (offer.Approved && !existingOffer.Approved)
            {
                existingOffer.ApprovalDate = DateTime.UtcNow;
            }
            else if (!offer.Approved && existingOffer.Approved)
            {
                existingOffer.ApprovalDate = null;
            }
            existingOffer.Approved = offer.Approved;

            existingOffer.LastModified = DateTime.UtcNow;

            await _offerService.UpdateAsync(id, existingOffer);

            return Ok(new { message = "Offer updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOffer(int id)
        {
            try
            {
                var offer = await _offerService.GetByIdAsync(id);
                if(offer == null)
                {
                    return NotFound(new { message = "Offer not found" });
                }

                await _offerService.DeleteAsync(id);

                return Ok(new { message = "Offer deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the offer", error = ex.Message });
            }
        }

        [HttpGet("approved")]
        public async Task<ActionResult<IEnumerable<Offer>>> GetApprovedOffers()
        {
            var approvedOffers = await _offerService.GetApprovedOffersAsync();
            return Ok(approvedOffers);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Offer>>> GetOffersByCustomerId(int customerId)
        {
            var customerOffers = await _offerService.GetOffersByCustomerIdAsync(customerId);
            if (!customerOffers.Any())
            {
                return NotFound(new { message = "No offers found for this customer" });
            }
            return Ok(customerOffers);
        }

        [HttpGet("job/{jobId}/highest")]
        public async Task<ActionResult<Offer>> GetHighestOfferByJobId(int jobId)
        {
            var highestOffer = await _offerService.GetHighestOfferByJobIdAsync(jobId);
            if (highestOffer == null)
            {
                return NotFound(new { message = "No offers found for this job" });
            }
            return Ok(highestOffer);
        }




    }
}
