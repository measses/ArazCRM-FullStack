using ArazCRM.API.Models.Entities;
using ArazCRM.API.Services.Abstract;
using ArazCRM.API.Services.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace ArazCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomeController : Controller
    {
       private readonly IIncomeService _incomeService;

        public IncomeController(IIncomeService incomeService)
        {
            _incomeService = incomeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Income>>> GetIncomes()
        {
            var incomes = await _incomeService.GetAllAsync();
            return Ok(incomes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Income>> GetIncomeById(int id)
        {
            var incomes = await _incomeService.GetByIdAsync(id);
            if (incomes == null)
            {
                return NotFound();

            }
            return Ok(incomes);
        }

        [HttpPost]
        public async Task<ActionResult> AddIncome([FromBody] Income income)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data provided", errors = ModelState });
            }
            await _incomeService.AddAsync(income);

            return CreatedAtAction(nameof(GetIncomeById), new { id = income.IncomeId }, new { message = "Income added successfully" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateIncome(int id, [FromBody] Income income)
        {
            // Önce veritabanından mevcut randevuyu buluyoruz
            var existingIncome = await _incomeService.GetByIdAsync(id);
            if (existingIncome == null)
            {
                return NotFound(new { message = "Income not found" });
            }

            // Mevcut kaydın alanlarını güncelle
            existingIncome.IncomeDate = income.IncomeDate;
            existingIncome.IncomeCategory = income.IncomeCategory;
            existingIncome.Amount = income.Amount;
            existingIncome.CustomerId = income.CustomerId;
            existingIncome.Description = income.Description;
            existingIncome.JobId = income.JobId;
            existingIncome.PaymentMethod = income.PaymentMethod;
            existingIncome.LastModified = DateTime.UtcNow;

            await _incomeService.UpdateAsync(id, existingIncome);
           

            return Ok(new { message = "Income updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIncome(int id)
        {
            try
            {
                var income = await _incomeService.GetByIdAsync(id);
                if (income == null)
                {
                    return NotFound(new { message = "Income not found" });

                }
                await _incomeService.DeleteAsync(id);
                return Ok(new { message = "Income deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the offer", error = ex.Message });
            }
        }

        [HttpGet("daterange")]
        public async Task<ActionResult<IEnumerable<Income>>> GetIncomesByDateRange(DateTime startDate, DateTime endDate)
        {
            var incomes = await _incomeService.GetIncomesByDateRangeAsync(startDate, endDate);
            if (!incomes.Any())
            {
                return NotFound(new { message = "No incomes found for the specified date range" });
            }
            return Ok(incomes);
        }

        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<IEnumerable<Income>>> GetIncomesByCustomerId(int customerId)
        {
            var incomes = await _incomeService.GetIncomesByCustomerIdAsync(customerId);
            if (!incomes.Any())
            {
                return NotFound(new { message = "No incomes found for this customer" });
            }
            return Ok(incomes);
        }

        [HttpGet("monthlyreport")]
        public async Task<ActionResult<IEnumerable<Income>>> GetMonthlyIncomeReport(int year, int month)
        {
            var incomes = await _incomeService.GetMonthlyIncomeReportAsync(year, month);
            if (!incomes.Any())
            {
                return NotFound(new { message = "No incomes found for the specified month" });
            }
            return Ok(incomes);
        }

        [HttpGet("yearlyreport")]
        public async Task<ActionResult<IEnumerable<Income>>> GetYearlyIncomeReport(int year)
        {
            var incomes = await _incomeService.GetYearlyIncomeReportAsync(year);
            if (!incomes.Any())
            {
                return NotFound(new { message = "No incomes found for the specified year" });
            }
            return Ok(incomes);
        }




    }
}
