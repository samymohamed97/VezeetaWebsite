using Core.DTO;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLayer.Respositories
{
    public class ManagePatientRepo : IManagePatient
    {
        public readonly ApplicationDbContext context;
        public ManagePatientRepo(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<PatientDTO> SearchPatients(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return Enumerable.Empty<PatientDTO>();
            }

            var searchResults = context.ApplicationUsers
                .Where(e =>
                    e.FirstName.Contains(query) ||
                    e.LastName.Contains(query)
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

            return searchResults;
        }
        public async Task<DoctorDTO> GetDoctorByIdAsync(int id)
        {
            var entity = await context.Doctors
                .Include(a => a.Specialization)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (entity == null)
            {
                return null;
            }
            var details = new DoctorDTO
            {
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                Email = entity.Email,
                Phone = entity.Phone,
                Gender = entity.Gender,
                DateOfBirth = entity.DateOfBirth,
                Age = entity.Age,
                SpName = entity.Specialization?.SpName,
            };

            return details;
        }
    }
}
