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
    public class PatientRepo: IPatient
    {
        private readonly ApplicationDbContext context;
        public PatientRepo(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IEnumerable<Doctor> GetAllDoctors(string doctorName)
        {
            var doctors = context.Doctors
                .Include(x => x.Appointments)
                .ThenInclude(y => y.Times)
                .Where(e => e.FirstName.Contains(doctorName) ||
                            e.LastName.Contains(doctorName))
                .ToList();
            return doctors;
        }
        public bool CancelBooking(int id)
        {
            try
            {
                var appointment = context.Appointments.Find(id);

                if (appointment != null)
                {
                    appointment.Status = Status.Confirmed;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Cancellation failed: " + ex.Message);
            } 
          } 
        }
    }

