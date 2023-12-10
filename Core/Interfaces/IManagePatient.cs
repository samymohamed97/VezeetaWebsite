using Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IManagePatient
    {
        IEnumerable<PatientDTO> SearchPatients(string query);
        Task<DoctorDTO> GetDoctorByIdAsync(int id);
    }
}
