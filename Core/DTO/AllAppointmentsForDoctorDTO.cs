using Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class AllAppointmentsForDoctorDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        public int Age { get; set; }
        public Gender Gender { get; set; }
        public ICollection <Appointment> appointments { get; set; }
        public ICollection<Time> time { get; set; }
    }
}
