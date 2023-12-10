using Core.DTO;
using Core.Interfaces;
using Core.Models;
using EFLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Vezeeta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        // private readonly IDashboard dashboard;
        public DashboardController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("NumOfDoctors")]
           public IActionResult GetNumDoctors()
                {
                   var NumOfDoctors = context.Doctors.Count();
                   return Ok(NumOfDoctors);
                }
  
        [HttpGet("GetNumPatients")]
           public IActionResult GetNumberOfPatients()
                {  
                   int numberOfPatients =context.ApplicationUsers.Count();
                   return Ok(new { NumberOfPatients = numberOfPatients }); 
                }
        [HttpGet("numOfRequests")]
           public IActionResult GetRequestCounts()
        {
            var countAppointments = context.Appointments.Count();
            return Ok(countAppointments);
        }
    }
}

          
    
       
     

       





