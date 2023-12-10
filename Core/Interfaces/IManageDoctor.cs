using Core.DTO;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IManageDoctor
    {
        IEnumerable<Doctor> SearchDoctors(string doctorName);
        Doctor GetById(int id);
        Task<bool> CreateDoctorAsync(DoctorDTO newDoctor);
        Task<bool> UpdateDoctorAsync(int id, DoctorDTO updatedDoctor);
        Task<bool> DeleteDoctorAsync(int id);
    }
}
