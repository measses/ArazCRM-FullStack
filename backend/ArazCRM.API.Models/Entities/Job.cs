using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArazCRM.API.Models.Entities
{
    public class Job
    {
        public int JobId { get; set; }  // Primary Key

        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        public JobStatus Status { get; set; }
        public string JobType { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string AssignedTo { get; set; }
        public string Priority { get; set; }
        public decimal EstimatedCost { get; set; }
        public decimal? ActualCost { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateUpdated { get; set; } = DateTime.UtcNow;
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
        public ICollection<Offer> Offers { get; set; } = new List<Offer>();
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();




    }
}
