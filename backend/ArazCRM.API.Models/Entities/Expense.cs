using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArazCRM.API.Models.Entities
{
    public class Expense
    {
        public int ExpenseId { get; set; }  


        public int JobId { get; set; }
        public Job Job { get; set; }

        public string ExpenseCategory { get; set; } 
        public decimal Amount { get; set; } 
        public DateTime ExpenseDate { get; set; } 

        public string Vendor { get; set; } 
        public string PaymentMethod { get; set; } 

        public string? Notes { get; set; } 

        public DateTime DateCreated { get; set; } = DateTime.UtcNow; 
        public DateTime LastModified { get; set; } = DateTime.UtcNow; 
    }
}
