using ArazCRM.API.Models.Entities;
using ArazCRM.API.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ArazCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : Controller
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            var invoices = await _invoiceService.GetAllAsync();
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoiceById(int id)
        {
            var invoice = await _invoiceService.GetByIdAsync(id);
            if (invoice == null)
            {
                return NotFound(new { message = "Invoice not found" });

            }
            return Ok(invoice);
        }


        [HttpPost]
        public async Task<ActionResult> AddInvoice([FromBody] Invoice invoice)
        {
            await _invoiceService.AddAsync(invoice);
            return CreatedAtAction(nameof(GetInvoiceById), new { id = invoice.InvoiceId }, new { message = "Invoice added successfully", invoice });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, [FromBody] Invoice invoice)
        {
            await _invoiceService.UpdateAsync(id, invoice);
            return Ok(new { message = "Invoice updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            await _invoiceService.DeleteAsync(id);
            return Ok(new { message = "Invoice deleted successfully" });
        }
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoicesByCustomerId(int customerId)
        {
            var invoices = await _invoiceService.GetInvoicesByCustomerIdAsync(customerId);
            return Ok(invoices);
        }

        [HttpGet("job/{jobId}")]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoicesByJobId(int jobId)
        {
            var invoices = await _invoiceService.GetInvoicesByJobIdAsync(jobId);
            return Ok(invoices);
        }

    }
}
