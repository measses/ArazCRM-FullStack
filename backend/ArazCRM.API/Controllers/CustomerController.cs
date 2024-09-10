using ArazCRM.API.Models.Entities;
using ArazCRM.API.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ArazCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await _customerService.GetAllAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        [HttpPost]

        public async Task<ActionResult> AddCustomer([FromBody]Customer customer)
        {
            await _customerService.AddAsync(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.CustomerId }, customer);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer customer)
        {
            // İlgili id'yi customer objesine atayalım
            customer.CustomerId = id;

            // Güncelleme işlemini yapıyoruz
            await _customerService.UpdateAsync(customer);

            // Başarılı güncelleme sonrası bir mesaj döndürüyoruz
            return Ok(new { message = "Customer updated successfully" });
        }






        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _customerService.GetByIdAsync(id);

            if (customer == null)
            {
                return NotFound(new { message = "Customer not found" });
            }

            await _customerService.DeleteAsync(id);

            // Silme işlemi başarılı olduğunda bir mesaj döndürelim
            return Ok(new { message = "Customer deleted successfully" });
        }



        // Özel metod için yeni endpoint:
        [HttpGet("byType/{type}")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomersByType(string type)
        {
            var customers = await _customerService.GetCustomersByTypeAsync(type);
            if (!customers.Any())
            {
                return NotFound();
            }
            return Ok(customers);
        }



    }
}
