using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArazCRM.API.Models.Entities
{
    public class Appointment
    {
        public int AppointmentId { get; set; }  


        public int CustomerId { get; set; }
        public Customer Customer { get; set; } 


        public int? JobId { get; set; } 
        public Job? Job { get; set; }   


        public DateTime AppointmentDate { get; set; } 
        public string Location { get; set; }          
        public string Purpose { get; set; }           
        public string Status { get; set; }            

        public string? Notes { get; set; }            

    
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime LastModified { get; set; } = DateTime.UtcNow; 
    }
}
