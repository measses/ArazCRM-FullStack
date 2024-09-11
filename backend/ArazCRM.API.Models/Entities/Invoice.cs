using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArazCRM.API.Models.Entities
{
    public class Invoice
    {
        public int InvoiceId { get; set; }  

        public int CustomerId { get; set; } 
        public Customer? Customer { get; set; } 

        public int JobId { get; set; } 
        public Job? Job { get; set; } 


        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public bool Paid { get; set; } 
        public DateTime? PaymentDate { get; set; } 
        public string PaymentMethod { get; set; }
        public string? Notes { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime LastModified { get; set; } = DateTime.UtcNow;
    }
}
