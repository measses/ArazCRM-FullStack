using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArazCRM.API.Models.Entities
{
    public class Income
    {
        public int IncomeId { get; set; } 

        public int CustomerId { get; set; } 
        public Customer? Customer { get; set; }

        public int? JobId { get; set; } 
        public Job? Job { get; set; }

        public DateTime IncomeDate { get; set; } 
        public decimal Amount { get; set; } 
        public string IncomeCategory { get; set; } 
        public string? Description { get; set; } 
        public string? PaymentMethod { get; set; } 

        public DateTime DateCreated { get; set; } = DateTime.UtcNow; 
        public DateTime LastModified { get; set; } = DateTime.UtcNow; 
    }
}
