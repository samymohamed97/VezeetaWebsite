using Core.DTO;
using EFLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Vezeeta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PatientController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public PatientController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("GetAllAppointments")]
        public IActionResult GetAllDoctors(string doctorName)
        {
            var doctors = context.Doctors
                .Include(x => x.Appointments)
                .ThenInclude(y => y.Times)
                .Where(e => e.FirstName.Contains(doctorName) ||
                e.LastName.Contains(doctorName)
                );
            var GelAllDoctors = doctors.Select(b => new GetAllDoctorsForPatientDTO
            {
                FirstName = b.FirstName,
                LastName = b.LastName,
                Phone = b.Phone,
                SpecializationName = b.Specialization.SpName,
                Gender = b.Gender,
                Price = b.Price,
            }).ToList();
                
            return Ok (doctors);
        }

        [HttpPost("BookingAppointment")]
            public IActionResult MakeBooking(int timeId, string discountCodeCoupon = null)
            {
                try
                {
                    return Ok("Booking successful!");
                }
                catch (Exception ex)
                {        
                    return BadRequest("Booking failed: " + ex.Message);
                }
            }

        [HttpDelete("{id}")]
        public IActionResult CancelBooking(int id)
        {
            var cancellationResult = context.Appointments.Find(id);

            if (cancellationResult == null)
            {
                return Ok("Appointment cancelled successfully");
            }
            else
            {
                return NotFound("Appointment already cancelled");
            }
        }
    }
 }







