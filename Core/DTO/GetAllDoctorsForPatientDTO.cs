using Core.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class GetAllDoctorsForPatientDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Phone]
        public string Phone { get; set; }
        public string SpecializationName { get; set; }
        public Gender Gender { get; set; }  
        //public IFormFile Image { get; set; }
        public int Price { get; set; }
        public List<Appointment> Appointments { get; set; } = new List<Appointment>();
        public List<Time> Times { get; set; }
        
    }
}
