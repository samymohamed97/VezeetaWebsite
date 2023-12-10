using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTO
{
    public class DoctorAppointmentDTO
    {
        public int price { get; set; }
        public Days days { get; set; }
        public DateTime time { get; set; }
        }
    }

