using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Doctor : ApplicationUser
    {
        public int Id { get; set; }
        public int Price { get; set; } 
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }        
    }
}
