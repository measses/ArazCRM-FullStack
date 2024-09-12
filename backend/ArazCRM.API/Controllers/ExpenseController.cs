using ArazCRM.API.Models.Entities;
using ArazCRM.API.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ArazCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : Controller
    {
        private readonly IExpenseService _expenseService;

        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpenses()
        {
            var expenses = await _expenseService.GetAllAsync();
            return Ok(expenses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Expense>> GetExpenseById(int id)
        {
            var expense = await _expenseService.GetByIdAsync(id);
            if (expense == null)
            {
                return NotFound(new { message = "Expense not found" });
            }
            return Ok(expense);
        }

        [HttpPost]
        public async Task<ActionResult> AddExpense([FromBody] Expense expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Model doğrulamasını kontrol et
            }
            await _expenseService.AddAsync(expense);
            return CreatedAtAction(nameof(GetExpenseById), new { id = expense.ExpenseId }, new { message = "Expense added successfully", expense });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateExpense(int id, [FromBody] Expense expense)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Model doğrulamasını kontrol et
            }

            // Kayıt var mı kontrol et
            var existingExpense = await _expenseService.GetByIdAsync(id);
            if (existingExpense == null)
            {
                return NotFound(new { message = "Expense not found" });
            }

            await _expenseService.UpdateAsync(id, expense);
            return Ok(new { message = "Expense updated successfully" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpense(int id)
        {
            var expense = await _expenseService.GetByIdAsync(id);
            if (expense == null)
            {
                return NotFound(new { message = "Expense not found" });
            }

            await _expenseService.DeleteAsync(id);
            return Ok(new { message = "Expense deleted successfully" });
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpensesByCategory(string category)
        {
            var expenses = await _expenseService.GetExpensesByCategoryAsync(category);
            return Ok(expenses);
        }

        [HttpGet("job/{jobId}")]
        public async Task<ActionResult<IEnumerable<Expense>>> GetExpensesByJobId(int jobId)
        {
            var expenses = await _expenseService.GetExpensesByJobIdAsync(jobId);
            return Ok(expenses);
        }
    }
}
