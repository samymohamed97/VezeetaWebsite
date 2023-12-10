using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IPatient
    {
        IEnumerable<Doctor> GetAllDoctors(string doctorName);
        bool CancelBooking(int id);
    }
}
