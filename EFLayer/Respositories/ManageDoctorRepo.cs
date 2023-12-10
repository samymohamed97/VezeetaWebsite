using Core.DTO;
using Core.Interfaces;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLayer.Respositories
{
    public class ManageDoctorRepo : IManageDoctor
    {
        private readonly ApplicationDbContext context;
        public ManageDoctorRepo(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Doctor> SearchDoctors(string doctorName)
        {
            if (string.IsNullOrWhiteSpace(doctorName))
            {
                return Enumerable.Empty<Doctor>();
            }

            var searchResults = context.Doctors
                .Include(s => s.Specialization)
                .Where(e =>
                    e.FirstName.Contains(doctorName) ||
                    e.LastName.Contains(doctorName))
                .ToList();

            return searchResults;
        }
        public Doctor GetById(int id)
        {
            var doctor = context.Doctors
                .Include(a => a.Specialization)
                .FirstOrDefault(e => e.Id == id);

            return doctor;
        }
        public async Task<bool> CreateDoctorAsync(DoctorDTO newDoctor)
        {
            using var dataStream = new MemoryStream();
            await newDoctor.Image.CopyToAsync(dataStream);

            var entityToAdd = new Doctor
            {
                FirstName = newDoctor.FirstName,
                LastName = newDoctor.LastName,
                Email = newDoctor.Email,
                Phone = newDoctor.Phone,
                Gender = newDoctor.Gender,
                DateOfBirth = newDoctor.DateOfBirth,
                Age = newDoctor.Age,
                Image = dataStream.ToArray()
            };

            context.Doctors.Add(entityToAdd);
            await context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> UpdateDoctorAsync(int id, DoctorDTO updatedDoctor)
        { 
                var entityToUpdate = await context.Doctors.FindAsync(id);

                if (entityToUpdate == null)
                {
                    return false;
                }
                entityToUpdate.FirstName = updatedDoctor.FirstName;
                entityToUpdate.LastName = updatedDoctor.LastName;
                entityToUpdate.Email = updatedDoctor.Email;
                entityToUpdate.Phone = updatedDoctor.Phone;
                entityToUpdate.Gender = updatedDoctor.Gender;
                entityToUpdate.DateOfBirth = updatedDoctor.DateOfBirth;
                entityToUpdate.Age = updatedDoctor.Age;
                await context.SaveChangesAsync();

                return true;
        }
        public async Task<bool> DeleteDoctorAsync(int id)
        {    
                var entityToDelete = await context.Doctors.FindAsync(id);
            if (entityToDelete == null)
            {
                return false;
            }
                context.Doctors.Remove(entityToDelete);
                await context.SaveChangesAsync();
            return true;  
        }
    }
}

