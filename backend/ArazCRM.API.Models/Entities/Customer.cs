using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArazCRM.API.Models.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }  
        public string Name { get; set; }

        public string? Phone { get; set; }   
        public string? Email { get; set; }   
        public string? Address { get; set; } 
        public string? City { get; set; }    

        public string CustomerType { get; set; }
        public string? Notes { get; set; } 

        public DateTime DateCreated { get; set; } = DateTime.UtcNow; 
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;

        public ICollection<Job> Jobs { get; set; } = new List<Job>();
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public ICollection<Offer> Offers { get; set; } = new List<Offer>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Income> Incomes { get; set; } = new List<Income>();





    }
}
