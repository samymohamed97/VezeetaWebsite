using Core.DTO;
using Core.Models;
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
    public class ManagementPatientController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public ManagementPatientController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet("Get All info about Patient")]
        public IActionResult SearchPatient(string PatientName)
        {
            if (string.IsNullOrWhiteSpace(PatientName))
            {
                return BadRequest("Search query is required.");
            }
            var searchResults = context.ApplicationUsers
                .Where(e =>
                    e.FirstName.Contains(PatientName) ||
                    e.LastName.Contains(PatientName)           
                )
                .Select(e => new PatientDTO
                {
                   // Image = e.Image,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    Phone = e.Phone,
                    Age = e.Age,
                    DateOfBirth = e.DateOfBirth,
                    Gender = e.Gender
                })
                .ToList();

            return Ok(searchResults);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var entity = context.Doctors.Include(a => a.Specialization).FirstOrDefault(e => e.Id == id);

            if (entity == null)
            {
                return NotFound();
            }
            //using var datastrem = new MemoryStream();
            //await newe.Image.CopyToAsync(datastrem);
            var details = new DoctorDTO
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Phone = entity.Phone,
                Gender = entity.Gender,
                DateOfBirth = entity.DateOfBirth,
                Age = entity.Age,
                SpName = entity.Specialization.SpName,
                //Image = datastrem.ToArray(),
            };
            return Ok(new { details });
        }
    }
    }
   
