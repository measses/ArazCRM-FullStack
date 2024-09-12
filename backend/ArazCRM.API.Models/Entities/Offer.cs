using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArazCRM.API.Models.Entities
{
    public class Offer
    {
        public int OfferId { get; set; }  

      
        public int JobId { get; set; }
        public Job? Job { get; set; } 

      
        public int CustomerId { get; set; }
        public Customer? Customer { get; set; } 

        
        public DateTime OfferDate { get; set; }  
        public decimal OfferAmount { get; set; } 
        public bool Approved { get; set; }       
        public DateTime? ApprovalDate { get; set; } 

        public string? Notes { get; set; } 

       
        public DateTime DateCreated { get; set; } = DateTime.UtcNow; 
        public DateTime LastModified { get; set; } = DateTime.UtcNow; 
    }
}
