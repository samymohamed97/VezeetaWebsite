using Core.DTO;
using Core.Models;
using EFLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Vezeeta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        public readonly ApplicationDbContext context;
        public DoctorController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public IActionResult GetAllBookingsForDoctor(string PatientName)
        {
            try
            {
                var query = context.ApplicationUsers
                    .Where(b => b.FirstName == PatientName)
                    .Include(b => b.Appointments)
                    .ThenInclude(c => c.Times)
                    .Where(e =>
                    e.FirstName.Contains(PatientName) ||
                    e.LastName.Contains(PatientName));

                var GetAppointments = query.Select(b => new AllAppointmentsForDoctorDTO
                {
                    FirstName = b.FirstName,
                    LastName = b.LastName,
                    Email = b.Email,
                    Phone = b.Phone,
                    Age = b.Age,
                    Gender = b.Gender,
                    appointments = b.Appointments
                }).ToList();
                return Ok(GetAppointments);
            }
            catch (Exception ex)
            {
                return BadRequest("Error fetching bookings: " + ex.Message);
            }
        }
        [HttpPost]
        public IActionResult AppAppointments(DoctorAppointmentDTO doc)
        {
            //var AddDay = new Appointment
            //{
            //    Days = doctorAppointmentDTO.days
            //};
            //var AddTime = new Time
            //{
            //    Times = doctorAppointmentDTO.time
            //};

            var AddPrice = new Doctor
            {
                Price = doc.price,
            };

            context.Doctors.Add(AddPrice);
            //context.Appointments.Add(AddDay);
            //context.Times.Add(AddTime);
            context.SaveChanges();
            return Ok();
        }


    }
}

    





