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
    public class ManagementDoctorController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public ManagementDoctorController(ApplicationDbContext context)
        {
            this.context = context;   
        }
        [HttpGet]
        public IActionResult Search(string DoctorName)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(DoctorName))
                {
                    return BadRequest("Search query is required.");
                }

                var searchResults = context.Doctors.Include(s => s.Specialization)
                    .Where(e =>
                        e.FirstName.Contains(DoctorName) ||
                        e.LastName.Contains(DoctorName))

                    .Select(a => new
                    {
                        a.Specialization.SpName,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        Email = a.Email,
                        Phone = a.Phone,
                        Age = a.Age,
                        DateOfBirth = a.DateOfBirth,
                        Gender = a.Gender,
                        Image = a.Image

                    }).ToList();

                return Ok(searchResults);
            }
            catch (Exception ex)
            {
                return BadRequest("Not Found");
            }
        }
      
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            try
            {
                var entity = context.Doctors.Include(a => a.Specialization).FirstOrDefault(e => e.Id == id);

                if (entity == null)
                {
                    return BadRequest("Iam not found");
                }
                //using var datastrem = new MemoryStream();
                //await newe.Image.CopyToAsync(datastrem);

                var details = new DoctorDTO
                {
                    //Image = datastrem.ToArray(),
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Email = entity.Email,
                    Phone = entity.Phone,
                    SpName = entity.Specialization.SpName,
                    Gender = entity.Gender,
                    DateOfBirth = entity.DateOfBirth,
                    Age = entity.Age,

                };

                return Ok(new { details });
            }
            catch (Exception ex)
            {
                return BadRequest("Not Found");
            }
        }
        
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] DoctorDTO newDoctor)
        {
            var Additem = context.Doctors.Include(s => s.Specialization);
            try
            {
                using var datastrem = new MemoryStream();
                await newDoctor.Image.CopyToAsync(datastrem);

                var DoctorToAdd = new Doctor
                {

                    FirstName = newDoctor.FirstName,
                    LastName = newDoctor.LastName,
                    Email = newDoctor.Email,
                    Phone = newDoctor.Phone,
                    Gender = newDoctor.Gender,
                    DateOfBirth = newDoctor.DateOfBirth,
                    Age = newDoctor.Age,
                    Image = datastrem.ToArray()
                };

                context.Doctors.Add(DoctorToAdd);
                context.SaveChanges();

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(false);
            }

        }
        
        [HttpPut("{Id}")]
        public IActionResult Edit(int Id, [FromForm] DoctorDTO editDoctor)
        {
            try
            {
                var entityToUpdate = context.ApplicationUsers.Find(Id);

                if (entityToUpdate == null)
                {
                    return NotFound(false);
                }
                // entityToUpdate.Image = editEntity.Image;
                entityToUpdate.FirstName = editDoctor.FirstName;
                entityToUpdate.LastName = editDoctor.LastName;
                entityToUpdate.Email = editDoctor.Email;
                entityToUpdate.Phone = editDoctor.Phone;
              //  entityToUpdate.Id = editDoctor.SpecializationId;
                entityToUpdate.Gender = editDoctor.Gender;
                entityToUpdate.DateOfBirth = editDoctor.DateOfBirth;
                entityToUpdate.Age = editDoctor.Age;

                context.SaveChanges();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(false);
            }
        }
       
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var entityToDelete = context.ApplicationUsers.Find(id);

                if (entityToDelete == null)
                {
                    return NotFound();
                }
                context.ApplicationUsers.Remove(entityToDelete);
                context.SaveChanges();

                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest(false);
            }
        }
    }
}
