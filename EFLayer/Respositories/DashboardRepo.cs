using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFLayer.Respositories
{
    public class DashboardRepo : IDashboard
    {
        private readonly ApplicationDbContext context;
        public DashboardRepo(ApplicationDbContext context)
        {
            this.context = context;
        }

        public int GetNumberOfDoctors()
        {
            return context.Doctors.Count();
        }

        public int GetNumberOfPatients()
        {
            return context.ApplicationUsers.Count();
        }

        public int GetRequestCounts()
        {
            return context.Appointments.Count();
        }
    }

   
}
